using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PhoneSelling.Data.Models;
using PhoneSelling.ViewModel.Base;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

public partial class PaginationQueryViewModel<T> : ObservableObject
{
    private readonly Func<Task<IEnumerable<T>>> _fetchDataFunc;

    [ObservableProperty] private ObservableCollection<T> items = new();
    [ObservableProperty] private int totalItems;
    [ObservableProperty] private int pageNumber = 1;
    [ObservableProperty] private int pageSize = 5;
    [ObservableProperty] private string searchKey = string.Empty;
    [ObservableProperty] private bool isLoading;
    [ObservableProperty] private bool isAscending = true;
    public PaginationQueryViewModel(Func<Task<IEnumerable<T>>> fetchDataFunc)
    {
        _fetchDataFunc = fetchDataFunc;
        LoadDataCommand = new AsyncRelayCommand(LoadDataAsync);
        NextPageCommand = new RelayCommand(NextPage, () => PageNumber <= Math.Floor((float)TotalItems / PageSize));
        PreviousPageCommand = new RelayCommand(PreviousPage, () => PageNumber > 1);
        ToggleSortCommand = new RelayCommand(ToggleSort);
    }

    public IAsyncRelayCommand LoadDataCommand { get; }
    public IRelayCommand NextPageCommand { get; }
    public IRelayCommand PreviousPageCommand { get; }
    public IRelayCommand ToggleSortCommand { get; }

    private async Task LoadDataAsync()
    {
        if (_fetchDataFunc == null) return;

        IsLoading = true;
        try
        {
            var data = await _fetchDataFunc();
            Items.Clear();
            TotalItems = data.Count();
            var paginatedData = data.Skip((PageNumber - 1) * PageSize).Take(PageSize).ToList();
            foreach (var item in paginatedData)
            {
                Items.Add(item);
            }
        }
        finally
        {
            IsLoading = false;
            NextPageCommand.NotifyCanExecuteChanged();
            PreviousPageCommand.NotifyCanExecuteChanged();
        }
    }

    private void NextPage()
    {
        PageNumber++;
        LoadDataCommand.Execute(null);
        NextPageCommand.NotifyCanExecuteChanged();
        PreviousPageCommand.NotifyCanExecuteChanged(); // Refresh the previous button
    }

    private void PreviousPage()
    {
        if (PageNumber > 1)
        {
            PageNumber--;
            LoadDataCommand.Execute(null);
            NextPageCommand.NotifyCanExecuteChanged();
            PreviousPageCommand.NotifyCanExecuteChanged(); // Refresh the previous button
        }
    }

    private void ToggleSort()
    {
        IsAscending = !IsAscending;
        Items = new ObservableCollection<T>(IsAscending
            ? Items.OrderBy(x => x.ToString()).ToList()
            : Items.OrderByDescending(x => x.ToString()).ToList());
    }
}
