using _NnBnH.MainNnBnH.Actions;
using Avalonia;
using Avalonia.Controls;

namespace _NnBnH.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
            InitializeComponent();

#if DEBUG
        this.AttachDevTools();
#endif

    }

    private void Button_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        ApplicationActions.ApplicationExit();
    }

    private void Binding_1(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
    }
}