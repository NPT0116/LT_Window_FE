﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PhoneSelling.Data.Common.Contracts.Requests;
using PhoneSelling.Data.Common.Internal.Responses;
using PhoneSelling.Data.Models;
using PhoneSelling.ViewModel.Base;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

public partial class PaginationQueryViewModel<T, TQuery> : ObservableObject where TQuery : PaginationQuery, new()
{
    private readonly Func<TQuery, Task<PaginationResult<T>>> _fetchDataFunc;

    [ObservableProperty] private ObservableCollection<T> items = new();
    [ObservableProperty] private int totalItems;
    [ObservableProperty] private int totalPages;
    [ObservableProperty] private bool isLoading;
    [ObservableProperty] private TQuery query = new();
    private string lastSearchKey = string.Empty;
    public PaginationQueryViewModel(Func<TQuery, Task<PaginationResult<T>>> fetchDataFunc)
    {
        _fetchDataFunc = fetchDataFunc;
        LoadDataCommand = new AsyncRelayCommand<bool?>(LoadDataAsync);
        NextPageCommand = new RelayCommand(NextPage, () => Query.PageNumber < TotalPages);
        PreviousPageCommand = new RelayCommand(PreviousPage, () => Query.PageNumber > 1);
        ToggleSortCommand = new RelayCommand(ToggleSort);
    }

    public IAsyncRelayCommand LoadDataCommand { get; }
    public IRelayCommand NextPageCommand { get; }
    public IRelayCommand PreviousPageCommand { get; }
    public IRelayCommand ToggleSortCommand { get; }

    private async Task LoadDataAsync(bool? shouldResetPagination)
    {
        if (_fetchDataFunc == null) return;
        //if(lastSearchKey != string.Empty && Query.SearchKey == lastSearchKey) return;
        string json = JsonSerializer.Serialize(Query, new JsonSerializerOptions
        {
            WriteIndented = true, // Pretty-print JSON output
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase, // Ensure JSON uses camelCase
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull // Ignore null values
        });

        Debug.WriteLine(json);
        IsLoading = true;
        try
        {
            if(shouldResetPagination == null || shouldResetPagination == true)
            {
                Query.ResetPagination();
            }
            var paginationResult = await _fetchDataFunc(Query);
            lastSearchKey = Query.SearchKey;
            TotalItems = paginationResult.TotalRecords;
            TotalPages = paginationResult.TotalPages;
            Items.Clear();
            foreach (var item in paginationResult.Data)
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
        Query.PageNumber++;
        LoadDataCommand.Execute(false);
        NextPageCommand.NotifyCanExecuteChanged();
        PreviousPageCommand.NotifyCanExecuteChanged(); // Refresh the previous button
    }

    private void PreviousPage()
    {
        if (Query.PageNumber > 1)
        {
            Query.PageNumber--;
            LoadDataCommand.Execute(false);
            NextPageCommand.NotifyCanExecuteChanged();
            PreviousPageCommand.NotifyCanExecuteChanged(); // Refresh the previous button
        }
    }

    private void ToggleSort()
    {
        Query.SortDirection = Query.SortDirection == SortDirection.Ascending ? SortDirection.Descending : SortDirection.Ascending;
    }
}
