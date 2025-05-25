using ChIllya.Utils;
using ChIllya.ViewModels;
using ChIllya.Views.Popups;
using System.Diagnostics;

namespace ChIllya.Views.Contents;

public partial class DownloadView : BaseView
{
	private readonly DownloadViewModel? _viewModel;

	public DownloadView()
	{
        InitializeComponent();
        SetBindingSearchResult();

		_viewModel = App.Instance!.ServiceProvider?.GetService<DownloadViewModel>()!;

		if (_viewModel == null)
		{
			WarningPopup.DisplayError("Cannot get Download View Model");
		}

		BindingContext = _viewModel;
    }

    public DownloadView(DownloadViewModel viewModel)
	{
		InitializeComponent();
		SetBindingSearchResult();

		_viewModel = viewModel;
		BindingContext = _viewModel;
	}

	public void SetBindingSearchResult()
	{
		var binding = new Binding()
		{
			Source = input,
			Path = "Text"
		};

		input.SetBinding(Entry.ReturnCommandParameterProperty, binding);
	}
}