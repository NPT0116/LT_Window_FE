using CommunityToolkit.Mvvm.ComponentModel;
using PhoneSelling.Data.Common.Validators;
using PhoneSelling.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneSelling.Data.Repositories.InventoryTransactionRepository.Dtos
{
    public partial class CreateInboundTransactionDto : BaseEntity
    {
        [ObservableProperty]
        [NotDefault(ErrorMessage ="Sản phẩm không được để trống")]
        private Guid variantId;
        
        [ObservableProperty]
        [Range(1, int.MaxValue, ErrorMessage ="Số lượng nhập kho phải lớn hơn 0")]
        private int quantity;

        [ObservableProperty] private string variantIdError = string.Empty;
        [ObservableProperty] private string quantityError = string.Empty;
        
        partial void OnVariantIdChanged(Guid newId)
        {
            ValidateProperty(newId, nameof(VariantId));
            VariantIdError = GetFirstError(nameof(VariantId));
        }

        partial void OnQuantityChanged(int newQuantity)
        {
            Debug.WriteLine("On quantity triggerd");
            ValidateProperty(newQuantity, nameof(Quantity));
            QuantityError = GetFirstError(nameof(Quantity));
        }

        public bool Validate()
        {
            ValidateAllProperties();

            VariantIdError = GetFirstError(nameof(VariantId));
            QuantityError = GetFirstError(nameof(Quantity));

            return HasErrors;

        }
    }
}
