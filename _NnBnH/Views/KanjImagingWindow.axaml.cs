using System;
using System.Collections.Generic;
using _NnBnH.Models;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace _NnBnH.Views;

public partial class KanjImagingWindow : Window
{
    public KanjImagingWindow()
    {
        InitializeComponent();
    }
    public KanjImagingWindow(List<Kanji> KanjiTable)
    {
        InitializeComponent();

        DataContext = new _NnBnH.ViewModels.KanjImagingWindowViewModel(KanjiTable);
    }
}