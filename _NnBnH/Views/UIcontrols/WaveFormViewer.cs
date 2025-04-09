using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _NnBnH.Views.UIcontrols
{
    internal class WaveFormViewer : Control
    {

        public static readonly StyledProperty<IBrush> Background =
            AvaloniaProperty.Register<WaveFormViewer, IBrush>(nameof(BackGroundColor));

        public static readonly StyledProperty<IBrush> PeakC =
            AvaloniaProperty.Register<WaveFormViewer, IBrush>(nameof(PeakC));

        public IBrush BackGroundColor { get => GetValue(Background); set => SetValue(Background, value); }
        public IBrush PeakColor { get => GetValue(PeakC); set => SetValue(PeakC, value); }

        private static List<byte> waveformtest = new List<byte>();

        public WaveFormViewer()
        {
            waveformtest = new List<byte>();
            for (int i = 0; i < 100; i++)
            {
                waveformtest.Add((byte)new Random().Next(0, byte.MaxValue));
            }

        }

        

        public override void Render(DrawingContext context)
        {
            context.FillRectangle(
                BackGroundColor,
                new Rect(0, 0, this.Bounds.Width, this.Bounds.Height));

            double maxPeakW = this.Bounds.Width / waveformtest.Count;

            for (double i = 0, W = 0; i < waveformtest.Count; i++)
            {
                double H = (this.Bounds.Height / 2) / waveformtest[(int)i];
                W = i * maxPeakW;

                context.FillRectangle(
                        PeakColor,
                        new Rect(W, (this.Bounds.Height / 2) - H, maxPeakW,  H)
                    );

                context.FillRectangle(
                        PeakColor,
                        new Rect(W, this.Bounds.Height / 2, maxPeakW, H)
                    );
            }

            base.Render(context);
        }
    }
}
