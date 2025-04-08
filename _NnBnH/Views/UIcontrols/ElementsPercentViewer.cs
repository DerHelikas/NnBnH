using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Media;
using System.Xml.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _NnBnH.Views.UIcontrols;

public class ElementsPercentViewer : Control
{
    public ElementsPercentViewer()
    {

    }
    public static readonly StyledProperty<IBrush[]> ElementsColorsProperty =
      AvaloniaProperty.Register<ElementsPercentViewer, IBrush[]>(nameof(ElementsColors),
          defaultValue: []);

    public static readonly StyledProperty<int[]> ElementsPointsProprty =
        AvaloniaProperty.Register<ElementsPercentViewer, int[]>(nameof(ElementsPointsProprty), defaultValue: []);

    public IBrush[] ElementsColors
    {
        get => GetValue(ElementsColorsProperty);
        set => SetValue(ElementsColorsProperty, value);
    }

    public int[] ElementsPoints
    {
        get => GetValue(ElementsPointsProprty);
        set => SetValue(ElementsPointsProprty, value);
    }


    public override void Render(DrawingContext context)
    {

        int SmallerIndex = int.Min(ElementsColors.Length, ElementsPoints.Length);


        List<int> Percents = new List<int>();
        int TotalPoints = ElementsPoints.Sum();
        for (int i = 0; i < SmallerIndex; i++)
        {

            Percents.Add((int)Math.Round(((ElementsPoints[i] / (double)TotalPoints) * 100)));
        }

        for (int i = 0, xx = 0; i < SmallerIndex; i++)
        {
            int w = (int)(Math.Round((Percents[i] / 100f) * Bounds.Width));

            context.FillRectangle(ElementsColors[i], new Rect(xx, 0, w, Bounds.Height));
            xx += w;
        }

        base.Render(context);
    }
}