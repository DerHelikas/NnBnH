using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Platform;

namespace _NnBnH.Views.UIcontrols;

public partial class MessageBoxWindow : Window
{
    public enum Icons
    {
        MessageBoxCSexceptionIcon,
        MessageBoxAPIexceptionIcon,
        Nothing
    }

#if DEBUG
    public MessageBoxWindow()
    {
        InitializeComponent();
    }
#endif

    public MessageBoxWindow(string caption, string body, Icons Icon = Icons.Nothing)
    {
        InitializeComponent();

        this.Message.Text = body;
        this.Caption.Text = caption;
        this.Title = caption;

        if (Icon == Icons.Nothing)
        {
            this.MessageIcon.IsVisible = false;
        }
        else
            this.MessageIcon.Source = (Avalonia.Media.IImage?)AssetLoader.Open(new System.Uri($"avares://_NnBnH/Assets/Icons/{Icon}.png"));


    }
}