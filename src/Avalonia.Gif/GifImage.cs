using Avalonia.Animation;
using Avalonia.Controls;
using Avalonia.Logging;
using Avalonia.Media;
using Avalonia.Rendering.Composition;
using Avalonia.VisualTree;
using System;
using System.IO;
using System.Numerics;

namespace Avalonia.Gif
{
    public class GifImage : Control
    {
        public static readonly StyledProperty<string> SourceUriRawProperty =
            AvaloniaProperty.Register<GifImage, string>("SourceUriRaw");

        public static readonly StyledProperty<Uri> SourceUriProperty =
            AvaloniaProperty.Register<GifImage, Uri>("SourceUri");

        public static readonly StyledProperty<Stream> SourceStreamProperty =
            AvaloniaProperty.Register<GifImage, Stream>("SourceStream");

        public static readonly StyledProperty<IterationCount> IterationCountProperty =
            AvaloniaProperty.Register<GifImage, IterationCount>("IterationCount", IterationCount.Infinite);

        private GifInstance? _gifInstance;

        public static readonly StyledProperty<StretchDirection> StretchDirectionProperty =
            AvaloniaProperty.Register<GifImage, StretchDirection>("StretchDirection");

        public static readonly StyledProperty<Stretch> StretchProperty =
            AvaloniaProperty.Register<GifImage, Stretch>("Stretch");

        private CompositionCustomVisual? _customVisual;

        private object? _initialSource = null;

        protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
        {
            switch (change.Property.Name)
            {
                case nameof(SourceUriRaw):
                case nameof(SourceUri):
                case nameof(SourceStream):
                    SourceChanged(change);
                    break;
                case nameof(Stretch):
                case nameof(StretchDirection):
                    InvalidateArrange();
                    InvalidateMeasure();
                    Update();
                    break;
                case nameof(IterationCount):
                    IterationCountChanged(change);
                    break;
                case nameof(Bounds):
                    Update();
                    break;
            }

            base.OnPropertyChanged(change);
        }

        public string SourceUriRaw
        {
            get => GetValue(SourceUriRawProperty);
            set => SetValue(SourceUriRawProperty, value);
        }

        public Uri SourceUri
        {
            get => GetValue(SourceUriProperty);
            set => SetValue(SourceUriProperty, value);
        }

        public Stream SourceStream
        {
            get => GetValue(SourceStreamProperty);
            set => SetValue(SourceStreamProperty, value);
        }

        public IterationCount IterationCount
        {
            get => GetValue(IterationCountProperty);
            set => SetValue(IterationCountProperty, value);
        }

        public StretchDirection StretchDirection
        {
            get => GetValue(StretchDirectionProperty);
            set => SetValue(StretchDirectionProperty, value);
        }

        public Stretch Stretch
        {
            get => GetValue(StretchProperty);
            set => SetValue(StretchProperty, value);
        }

        private static void IterationCountChanged(AvaloniaPropertyChangedEventArgs e)
        {
            var image = e.Sender as GifImage;
            if (image is null || e.NewValue is not IterationCount iterationCount)
                return;

            image.IterationCount = iterationCount;
        }

        protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
        {
            var compositor = ElementComposition.GetElementVisual(this)?.Compositor;
            if (compositor == null || _customVisual?.Compositor == compositor)
                return;
            _customVisual = compositor.CreateCustomVisual(new CustomVisualHandler());
            ElementComposition.SetElementChildVisual(this, _customVisual);
            _customVisual.SendHandlerMessage(CustomVisualHandler.StartMessage);

            if (_initialSource is not null)
            {
                UpdateGifInstance(_initialSource);
                _initialSource = null;
            }

            Update();
            base.OnAttachedToVisualTree(e);
        }


        private void Update()
        {
            if (_customVisual is null || _gifInstance is null)
                return;

            var dpi = this.GetVisualRoot()?.RenderScaling ?? 1.0;
            var sourceSize = _gifInstance.GifPixelSize.ToSize(dpi);
            var viewPort = new Rect(Bounds.Size);

            var scale = Stretch.CalculateScaling(Bounds.Size, sourceSize, StretchDirection);
            var scaledSize = sourceSize * scale;
            var destRect = viewPort
                .CenterRect(new Rect(scaledSize))
                .Intersect(viewPort);

            if (Stretch == Stretch.None)
            {
                _customVisual.Size = new Vector2((float)sourceSize.Width, (float)sourceSize.Height);
            }
            else
            {
                _customVisual.Size = new Vector2((float)destRect.Size.Width, (float)destRect.Size.Height);
            }

            _customVisual.Offset = new Vector3((float)destRect.Position.X, (float)destRect.Position.Y, 0);
        }

        private class CustomVisualHandler : CompositionCustomVisualHandler
        {
            private TimeSpan _animationElapsed;
            private TimeSpan? _lastServerTime;
            private GifInstance? _currentInstance;
            private bool _running;

            public static readonly object StopMessage = new(), StartMessage = new();

            public override void OnMessage(object message)
            {
                if (message == StartMessage)
                {
                    _running = true;
                    _lastServerTime = null;
                    RegisterForNextAnimationFrameUpdate();
                }
                else if (message == StopMessage)
                {
                    _running = false;
                }
                else if (message is GifInstance instance)
                {
                    _currentInstance?.Dispose();
                    _currentInstance = instance;
                }
            }

            public override void OnAnimationFrameUpdate()
            {
                if (!_running) return;
                Invalidate();
                RegisterForNextAnimationFrameUpdate();
            }

            public override void OnRender(ImmediateDrawingContext drawingContext)
            {
                if (_running)
                {
                    if (_lastServerTime.HasValue) _animationElapsed += (CompositionNow - _lastServerTime.Value);
                    _lastServerTime = CompositionNow;
                }

                try
                {
                    if (_currentInstance is null || _currentInstance.IsDisposed) return;

                    var bitmap = _currentInstance.ProcessFrameTime(_animationElapsed);
                    if (bitmap is not null)
                    {
                        drawingContext.DrawBitmap(bitmap, new Rect(_currentInstance.GifPixelSize.ToSize(1)),
                            GetRenderBounds());
                    }
                }
                catch (Exception e)
                {
                    Logger.Sink?.Log(LogEventLevel.Error, "GifImage Renderer ", this, e.ToString());

                }
            }
        }

        /// <summary>
        /// Measures the control.
        /// </summary>
        /// <param name="availableSize">The available size.</param>
        /// <returns>The desired size of the control.</returns>
        protected override Size MeasureOverride(Size availableSize)
        {
            var result = new Size();
            var scaling = this.GetVisualRoot()?.RenderScaling ?? 1.0;
            if (_gifInstance != null)
            {
                result = Stretch.CalculateSize(availableSize, _gifInstance.GifPixelSize.ToSize(scaling),
                    StretchDirection);
            }

            return result;
        }

        /// <inheritdoc/>
        protected override Size ArrangeOverride(Size finalSize)
        {
            if (_gifInstance is null) return new Size();
            var scaling = this.GetVisualRoot()?.RenderScaling ?? 1.0;
            var sourceSize = _gifInstance.GifPixelSize.ToSize(scaling);
            var result = Stretch.CalculateSize(finalSize, sourceSize);
            return result;
        }


        private void SourceChanged(AvaloniaPropertyChangedEventArgs e)
        {
            if (e.NewValue is null || (e.NewValue is string value && !Uri.IsWellFormedUriString(value, UriKind.Absolute)))
            {
                return;
            }

            if (_customVisual is null)
            {
                _initialSource = e.NewValue;
                return;
            }

            UpdateGifInstance(e.NewValue);

            InvalidateArrange();
            InvalidateMeasure();
            Update();
        }

        private void UpdateGifInstance(object source)
        {
            _gifInstance?.Dispose();
            _gifInstance = new GifInstance(source);
            _gifInstance.IterationCount = IterationCount;
            _customVisual?.SendHandlerMessage(_gifInstance);
        }
    }
}
