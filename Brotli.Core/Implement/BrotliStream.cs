using System;
using System.IO;
using System.IO.Compression;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace Brotli
{
    public class BrotliStream : Stream
    {
        const int BufferSize = 64 * 1024;
        protected Stream _stream = null;
        protected MemoryStream _intermediateStream = new MemoryStream();
        protected CompressionMode _mode = CompressionMode.Compress;
        protected IntPtr _state = IntPtr.Zero;
        protected IntPtr _ptrInputBuffer = IntPtr.Zero;
        protected IntPtr _ptrOutputBuffer = IntPtr.Zero;

        protected IntPtr _ptrNextInput = IntPtr.Zero;
        protected IntPtr _ptrNextOutput = IntPtr.Zero;
        protected UInt32 _availableIn = 0;
        protected UInt32 _availableOut = BufferSize;

        protected Byte[] _managedBuffer;
        protected Boolean _endOfStream = false;
        protected int _readOffset = 0;
        protected BrotliDecoderResult _lastDecodeResult = BrotliDecoderResult.NeedsMoreInput;
        protected Boolean _leaveOpen = false;

        public BrotliStream(Stream baseStream, CompressionMode mode,bool leaveOpen)
        {
            if (baseStream == null) throw new ArgumentNullException("baseStream");
            _mode = mode;
            _stream = baseStream;
            _leaveOpen = leaveOpen;
            if (_mode == CompressionMode.Compress)
            {
                _state = Brolib.BrotliEncoderCreateInstance();
                if (_state == IntPtr.Zero)
                {
                    throw new BrotliException("Unable to create brotli encoder instance");
                }
                Brolib.BrotliEncoderSetParameter(_state, BrotliEncoderParameter.Quality, 5);
                Brolib.BrotliEncoderSetParameter(_state, BrotliEncoderParameter.LGWin, 22);
            }
            else
            {
                _state = Brolib.BrotliDecoderCreateInstance();
                if (_state == IntPtr.Zero)
                {
                    throw new BrotliException("Unable to create brotli decoder instance");
                }
                //follow the brotli default standard
                var succ = Brolib.BrotliDecoderSetParameter(_state, BrotliDecoderParameter.LargeWindow, 1U);
                if (!succ)
                {
                    throw new BrotliException("failed to set decoder parameter to large window");
                }
            }
            _ptrInputBuffer = Marshal.AllocHGlobal(BufferSize);
            _ptrOutputBuffer = Marshal.AllocHGlobal(BufferSize);
            _ptrNextInput = _ptrInputBuffer;            
            _ptrNextOutput = _ptrOutputBuffer;

            _managedBuffer = new Byte[BufferSize];
        }
        public BrotliStream(Stream baseStream, CompressionMode mode):this(baseStream,mode,false)
        {

        }

        /// <summary>
        /// Set the compress quality(0~11)
        /// </summary>
        /// <param name="quality">compress quality</param>
        public void SetQuality(uint quality)
        {
            if (quality < 0 || quality > 11)
            {
                throw new ArgumentException("quality", "the range of quality is 0~11");
            }
            Brolib.BrotliEncoderSetParameter(_state, BrotliEncoderParameter.Quality, quality);
        }

        /// <summary>
        /// Set the compress LGWin(10~24)
        /// </summary>
        /// <param name="window">the window size</param>
        public void SetWindow(uint window)
        {
            if (window < 10 || window > 24)
            {
                throw new ArgumentException("window", "the range of window is 10~24");
            }
            Brolib.BrotliEncoderSetParameter(_state, BrotliEncoderParameter.LGWin, window);
        }

        public override bool CanRead
        {
            get
            {
                if (_stream == null)
                {
                    return false;
                }

                return (_mode == System.IO.Compression.CompressionMode.Decompress && _stream.CanRead);
            }
        }

        public override bool CanSeek
        {
            get
            {
                return false;
            }
        }

        public override bool CanWrite
        {
            get
            {
                if (_stream == null)
                {
                    return false;
                }

                return (_mode == System.IO.Compression.CompressionMode.Compress && _stream.CanWrite);
            }
        }

        public override long Length
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override long Position
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public override async Task FlushAsync(CancellationToken cancellationToken)
        {
            if (_stream == null)
            {
                throw new ObjectDisposedException(null, "Underlying stream is disposed");
            }
            if (_mode == CompressionMode.Compress)
            {
                await FlushBrotliStreamAsync(false).ConfigureAwait(false);
            }

        }
        public override void Flush()
        {
            AsyncHelper.RunSync(() => FlushAsync());
        }

        protected virtual async Task FlushBrotliStreamAsync(Boolean finished)
        {
            //test if the resource has been freed
            if (_state == IntPtr.Zero) return;
            if (Brolib.BrotliEncoderIsFinished(_state)) return;
            BrotliEncoderOperation op = finished ? BrotliEncoderOperation.Finish : BrotliEncoderOperation.Flush;
            UInt32 totalOut = 0;
            while (true)
            {
                var compressOK = Brolib.BrotliEncoderCompressStream(_state, op, ref _availableIn, ref _ptrNextInput, ref _availableOut, ref _ptrNextOutput, out totalOut);
                if (!compressOK) throw new BrotliException("Unable to finish encode stream");
                var extraData = _availableOut != BufferSize;
                if (extraData)
                {
                    var bytesWrote = (int)(BufferSize - _availableOut);
                    Marshal.Copy(_ptrOutputBuffer, _managedBuffer, 0, bytesWrote);                    
                    await _stream.WriteAsync(_managedBuffer, 0, bytesWrote).ConfigureAwait(false);
                    _availableOut = BufferSize;
                    _ptrNextOutput = _ptrOutputBuffer;
                }
                if (Brolib.BrotliEncoderIsFinished(_state)) break;
                if (!extraData) break;
            }

        }

        protected virtual void FlushBrotliStream(Boolean finished)
        {

            AsyncHelper.RunSync(() => FlushBrotliStreamAsync(finished));
        }

        protected override void Dispose(bool disposing)
        {
            if (_mode == CompressionMode.Compress)
            {
                FlushBrotliStream(true);
            }
            base.Dispose(disposing);
            if (!_leaveOpen)  _stream.Dispose();
            _intermediateStream.Dispose();
            if (_ptrInputBuffer!=IntPtr.Zero) Marshal.FreeHGlobal(_ptrInputBuffer);
            if (_ptrOutputBuffer != IntPtr.Zero) Marshal.FreeHGlobal(_ptrOutputBuffer);
            _managedBuffer = null;
            _ptrInputBuffer = IntPtr.Zero;
            _ptrOutputBuffer = IntPtr.Zero;
            if (_state != IntPtr.Zero)
            {
                if (_mode == CompressionMode.Compress)
                {
                    Brolib.BrotliEncoderDestroyInstance(_state);
                }
                else
                {
                    Brolib.BrotliDecoderDestroyInstance(_state);
                }
                _state = IntPtr.Zero;
            }
        }


        public void TruncateBeginning(MemoryStream ms, int numberOfBytesToRemove)
        {
#if NETSTANDARD2_0
            ArraySegment<byte> buf;
            if(ms.TryGetBuffer(out buf))
            {
                Buffer.BlockCopy(buf.Array, numberOfBytesToRemove, buf.Array, 0, (int)ms.Length - numberOfBytesToRemove);
                ms.SetLength(ms.Length - numberOfBytesToRemove);
            }
            else
            {
                throw new UnauthorizedAccessException();
            }
#else
            byte[] buf = ms.GetBuffer();
            Buffer.BlockCopy(buf, numberOfBytesToRemove, buf, 0, (int)ms.Length - numberOfBytesToRemove);
            ms.SetLength(ms.Length - numberOfBytesToRemove);
#endif
        }

        public override async Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
        {
            if (_mode != CompressionMode.Decompress) throw new BrotliException("Can't read on this stream");


            int bytesRead = (int)(_intermediateStream.Length - _readOffset);
            uint totalCount = 0;
            Boolean endOfStream = false;
            Boolean errorDetected = false;
            while (bytesRead < count)
            {
                while (true)
                {
                    if (_lastDecodeResult == BrotliDecoderResult.NeedsMoreInput)
                    {
                        _availableIn = (UInt32) await _stream.ReadAsync(_managedBuffer, 0, (int)BufferSize).ConfigureAwait(false);
                        _ptrNextInput = _ptrInputBuffer;
                        if (_availableIn <= 0)
                        {
                            endOfStream = true;
                            break;
                        }
                        Marshal.Copy(_managedBuffer, 0, _ptrInputBuffer, (int)_availableIn);
                    }
                    else if (_lastDecodeResult == BrotliDecoderResult.NeedsMoreOutput)
                    {
                        Marshal.Copy(_ptrOutputBuffer, _managedBuffer, 0, BufferSize);
                        await _intermediateStream.WriteAsync(_managedBuffer, 0, BufferSize).ConfigureAwait(false);
                        bytesRead += BufferSize;
                        _availableOut = BufferSize;
                        _ptrNextOutput = _ptrOutputBuffer;
                    }
                    else
                    {
                        //Error or OK
                        endOfStream = true;
                        break;
                    }
                    _lastDecodeResult = Brolib.BrotliDecoderDecompressStream(_state, ref _availableIn, ref _ptrNextInput,
                        ref _availableOut, ref _ptrNextOutput, out totalCount);
                    if (bytesRead >= count) break;
                }
                if (endOfStream && !Brolib.BrotliDecoderIsFinished(_state))
                {
                    errorDetected = true;
                }

                if (_lastDecodeResult == BrotliDecoderResult.Error || errorDetected)
                {
                    var error = Brolib.BrotliDecoderGetErrorCode(_state);
                    var text = Brolib.BrotliDecoderErrorString(error);
                    throw new BrotliDecodeException(String.Format("Unable to decode stream,possibly corrupt data.Code={0}({1})", error, text), error, text);
                }

                if (endOfStream && !Brolib.BrotliDecoderIsFinished(_state) && _lastDecodeResult == BrotliDecoderResult.NeedsMoreInput)
                {
                    throw new BrotliException("Unable to decode stream,unexpected EOF");
                }

                if (endOfStream && _ptrNextOutput != _ptrOutputBuffer)
                {
                    int remainBytes = (int)(_ptrNextOutput.ToInt64() - _ptrOutputBuffer.ToInt64());
                    bytesRead += remainBytes;
                    Marshal.Copy(_ptrOutputBuffer, _managedBuffer, 0, remainBytes);
                    await _intermediateStream.WriteAsync(_managedBuffer, 0, remainBytes).ConfigureAwait(false);
                    _ptrNextOutput = _ptrOutputBuffer;
                }
                if (endOfStream) break;
            }

            if (_intermediateStream.Length - _readOffset >= count || endOfStream)
            {                
                _intermediateStream.Seek(_readOffset, SeekOrigin.Begin);
                var bytesToRead = (int)(_intermediateStream.Length - _readOffset);
                if (bytesToRead > count) bytesToRead = count;
                await _intermediateStream.ReadAsync(buffer, offset, bytesToRead).ConfigureAwait(false);
                TruncateBeginning(_intermediateStream, _readOffset + bytesToRead);
                _readOffset = 0;
                return bytesToRead;
            }

            return 0;
        }
        public override int Read(byte[] buffer, int offset, int count)
        {
            async Task<int> task()
            {
                return await ReadAsync(buffer,offset,count).ConfigureAwait(false);

            }
            return AsyncHelper.RunSync(task);
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotImplementedException();
        }

        public override void SetLength(long value)
        {
            throw new NotImplementedException();
        }

        static int totalWrote = 0;

        public override async Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
        {
            if (_mode != CompressionMode.Compress) throw new BrotliException("Can't write on this stream");
            totalWrote += count;
            //Console.WriteLine(String.Format("Write {0} bytes,total={1} bytes.", count, totalWrote));

            UInt32 totalOut = 0;
            int bytesRemain = count;
            int currentOffset = offset;

            Boolean compressOK = true;
            while (bytesRemain > 0)
            {
                int copyLen = bytesRemain > BufferSize ? BufferSize : bytesRemain;
                Marshal.Copy(buffer, currentOffset, _ptrInputBuffer, copyLen);
                bytesRemain -= copyLen;
                currentOffset += copyLen;
                _availableIn = (UInt32)copyLen;
                _ptrNextInput = _ptrInputBuffer;
                while (_availableIn > 0)
                {
                    compressOK = Brolib.BrotliEncoderCompressStream(_state, BrotliEncoderOperation.Process, ref _availableIn, ref _ptrNextInput, ref _availableOut,
                        ref _ptrNextOutput, out totalOut);
                    if (!compressOK) throw new BrotliException("Unable to compress stream");
                    if (_availableOut != BufferSize)
                    {
                        var bytesWrote = (int)(BufferSize - _availableOut);
                        //Byte[] localBuffer = new Byte[bytesWrote];
                        Marshal.Copy(_ptrOutputBuffer, _managedBuffer, 0, bytesWrote);
                        await _stream.WriteAsync(_managedBuffer, 0, bytesWrote).ConfigureAwait(false);
                        _availableOut = BufferSize;
                        _ptrNextOutput = _ptrOutputBuffer;
                    }
                }
                if (Brolib.BrotliEncoderIsFinished(_state)) break;
            }
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            async Task task()
            {
                await WriteAsync(buffer,offset,count).ConfigureAwait(false);

            }
            AsyncHelper.RunSync(task);
        }
    }

#if NET35
    /// <summary>
    /// Improve compability issue on FX35
    /// </summary>
    public static class StreamCopyExtension
    {
        public static void CopyTo(this Stream source,Stream destination, int bufferSize=4*1024)
        {
            if (source==null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            if (destination==null)
            {
                throw new ArgumentNullException(nameof(destination));
            }
            if (!source.CanRead)
            {
                throw new InvalidOperationException("source stream is not readable");
            }
            if (!destination.CanWrite)
            {
                throw new InvalidOperationException("destination stream is not writeable");
            }
            if (bufferSize<=0)
            {
                throw new InvalidOperationException("buffer size should be greate than zero");
            }


            byte[] buffer = new byte[bufferSize];
            int read;
            while ((read = source.Read(buffer, 0, buffer.Length)) > 0)
                destination.Write(buffer, 0, read);
        }
    }
#endif
}
