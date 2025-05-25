using System.Windows.Input;

namespace ChIllya.UIComponents;

public partial class TabBar : ContentView
{
    public static readonly BindableProperty OnNavigateProperty =
        BindableProperty.Create(nameof(OnNavigate), typeof(ICommand), typeof(TabBar));

    public ICommand OnNavigate
    {
        get => (ICommand)GetValue(OnNavigateProperty);
        set => SetValue(OnNavigateProperty, value);
    }

    public TabBar()
	{
		InitializeComponent();
	}

    private void OnTabBarItemClick(object sender, ContentView content)
    {
        if (sender is not TabBarItem tabBarItem) return;

        ResetForeground();
        if (App.Instance!.Resources["IconSelected"] is Color color) tabBarItem.Foreground = color;

        OnNavigate.Execute(content);
    }

    private void ResetForeground()
    {
        var child = tabBarContainer.Children;

        foreach (var item in child)
        {
            if (item is not TabBarItem tabBarItem) continue;
            if (App.Instance!.Resources["Icon"] is Color color) tabBarItem.Foreground = color;
        }
    }
}