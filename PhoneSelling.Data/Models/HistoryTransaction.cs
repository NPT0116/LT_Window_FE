using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace PhoneSelling.Data.Models
{
    public partial class HistoryTransaction : BaseEntity
    {
        [ObservableProperty] Guid transactionID;
        [ObservableProperty] Guid variantID;
        [ObservableProperty] int transactionType;
        [ObservableProperty] int quantity;
        [ObservableProperty] DateTime transactionDate;

        [ObservableProperty] Guid? invoiceDetailID;
        public string TransactionTypeDisplay => TransactionType == 0 ? "NHẬP" : TransactionType == 1 ? "XUẤT" : "KHÔNG RÕ";


    }
}
