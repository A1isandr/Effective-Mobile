using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.ReactiveUI;
using Frontend.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;

namespace Frontend.Views;

public partial class MainView : ReactiveUserControl<MainViewModel>
{
    public MainView()
    {
        InitializeComponent();

        this.WhenActivated(disposables =>
        {
            this
                .BindCommand(ViewModel,
                    viewModel => viewModel.Refresh,
                    view => view.RefreshButton)
                .DisposeWith(disposables);

            this
                .BindCommand(ViewModel,
                    view => view.SaveToFile,
                    viewModel => viewModel.SaveButton)
                .DisposeWith(disposables);
        });
    }
}
