﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using PhoneSelling.ViewModel.Pages.Inventory.Transaction.InboundTransaction;
using PhoneSelling.Data.Models;
using PhoneSelling.Data.Repositories.InventoryTransactionRepository.Dtos;
using CommunityToolkit.Mvvm.Messaging;
using Navigation.Helpers;
using System.Diagnostics;
using System.Text.Json;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Navigation.Views.Inventory.Transaction
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CreateInboundTransactionPage : Page
    {
        public CreateInboundTransactionViewModel ViewModel { get; set; }
        public CreateInboundTransactionPage()
        {
            this.InitializeComponent();
            ViewModel = new();
            this.DataContext = ViewModel;
            WeakReferenceMessenger.Default.Register<Message>(this, (r, message) =>
            {
                DialogHelper.ShowDialogAsync(message.status ? "THÀNH CÔNG" : "LỖI", message.message, "Đóng", this.XamlRoot);
            });
        }
        private async void ItemSearch_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            Debug.WriteLine("Text Changed");
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                if (string.IsNullOrEmpty(sender.Text))
                {
                    var row = (CreateInboundTransactionDto)sender.DataContext;
                    row.VariantId = Guid.Empty;
                }
                var variants = await App.SearchVariants(sender.Text);
                if (variants != null)
                {
                    sender.ItemsSource = variants.OrderBy(v => v.Item.ItemName).ToList();
                }
            }
        }

        private void ItemSearch_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            var row = (CreateInboundTransactionDto)sender.DataContext;
            Debug.WriteLine("Item Chosen !");

            if (args.SelectedItem is Variant variant)
            {
                // Update the row's properties
                row.VariantId = variant.VariantID;
                sender.Text = $"{variant.Item.ItemName} - {variant.Color.Name} - {variant.Storage}";

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Remove Button Click !");
            if (sender is Button button && button.DataContext is CreateInboundTransactionDto item)
            {
                ViewModel.RemoveVariant(item);
            }
        }
    }
}
