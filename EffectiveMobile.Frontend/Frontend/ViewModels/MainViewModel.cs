using Avalonia.Controls.Notifications;
using DynamicData;
using Frontend.Models;
using Frontend.Services.Interfaces;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using SukiUI.Toasts;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace Frontend.ViewModels;

public class MainViewModel : ViewModelBase
{
    #region Dependencies

    private readonly IOrdersService _ordersService;
    private readonly ISaveToFileService _saveToFileService;
    private readonly ISukiToastManager _toastManager;

    #endregion

    #region Properties

    public ObservableCollection<Order> Orders { get; set; } = [];

    [ObservableAsProperty]
    public bool IsRefreshing { get; set; }

    [ObservableAsProperty]
    public bool IsSaving { get; set; }

    [ObservableAsProperty]
    public bool IsFiltering { get; set; }

    [ObservableAsProperty]
    public bool IsBusy { get; set; }

    [Reactive]
    public int? DistrictId { get; set; }

    [Reactive]
    public TimeSpan? Time { get; set; }

    #endregion

    #region Commands

    public ReactiveCommand<Unit, Unit> Refresh { get; }

    public ReactiveCommand<Unit, Unit> SaveToFile { get; }

    public ReactiveCommand<int, Unit> Filter { get; }

    #endregion

    #region Constructors

    public MainViewModel(
        IOrdersService ordersService,
        ISaveToFileService saveToFileService,
        ISukiToastManager toastManager)
    {
        _ordersService = ordersService;
        _saveToFileService = saveToFileService;
        _toastManager = toastManager;

        Refresh = ReactiveCommand.CreateFromTask(RefreshOrdersAsync);
        SaveToFile = ReactiveCommand.CreateFromTask(SaveOrdersAsync);
        Filter = ReactiveCommand.CreateFromTask<int>((id, _ ) => FilterOrders(id));

        Refresh
            .ThrownExceptions
            .Subscribe(ShowException);

        Refresh
            .IsExecuting
            .ToPropertyEx(this, x => x.IsRefreshing);

        SaveToFile
            .ThrownExceptions
            .Subscribe(ShowException);

        SaveToFile
            .IsExecuting
            .ToPropertyEx(this, x => x.IsSaving);

        Filter
            .IsExecuting
            .ToPropertyEx(this, x => x.IsFiltering);

        this
            .WhenAnyValue(x => x.DistrictId)
            .WhereNotNull()
            .Throttle(TimeSpan.FromMilliseconds(100))
            .ObserveOn(RxApp.MainThreadScheduler)
            .InvokeCommand(Filter);

        this.WhenAnyValue(x => x.DistrictId)
            .Where(districtId => districtId is null)
            .Select(_ => Unit.Default)
            .InvokeCommand(Refresh);

        this
            .WhenAnyValue(x => x.IsRefreshing, x => x.IsFiltering)
            .Select(conditions => conditions is { Item1: true, Item2: true })
            .ToPropertyEx(this, x => x.IsBusy);
    }

    #endregion

    #region Methods

    private async Task RefreshOrdersAsync()
    {
        var orders = await _ordersService.GetOrdersAsync();

        if (orders is null) return;

        Orders.Clear();
        Orders.AddRange(orders);
    }

    private async Task SaveOrdersAsync()
    {
        await _saveToFileService.SaveAsync(Orders);
    }

    private async Task FilterOrders(int districtId)
    {
        var orders = await _ordersService.GetOrdersAsync();

        if (orders is null) return;

        Orders.Clear();
        Orders.AddRange(orders
            .Where(order => order.DistrictId == districtId && order.DueTime.TimeOfDay >= Time)
            .ToList());
    }

    private void ShowException(Exception exception)
    {
        _toastManager
            .CreateToast()
            .OfType(NotificationType.Error)
            .WithTitle(exception.GetType().Name)
            .WithContent(exception.Message)
            .Dismiss().ByClicking()
            .Queue();
    }

    #endregion
}