using System;

namespace Brotli
{
    public class BrotliDecodeException : BrotliException
    {
        public int Code { get; set; }
        public String ErrorText { get; set; }


        public BrotliDecodeException(int code, String errorText) : base() {
            Code = code;
            ErrorText = errorText;
        }

        public BrotliDecodeException(String message, int code, String errorText) : base(message) {
            Code = code;
            ErrorText = errorText;
        }
        public BrotliDecodeException(String message, Exception innerException, int code, String errorText) : base(message, innerException) {
            Code = code;
            ErrorText = errorText;
        }
    }
}
