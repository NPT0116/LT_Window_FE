using PhoneSelling.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneSelling.Data.Repositories.ColorRepository
{
    public interface IColorRepository
    {
        Task<Color> GetByIdAsync(Guid id);
        Task<Color> UpdateAsync(Guid id, Color colorDto);
        Task<string> GetImageByVariantIdAsync(Guid variantId);
    }

}
