using PhoneSelling.Data.Models;
using PhoneSelling.Data.Repositories.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneSelling.Data.Repositories.PhoneRepository
{
    public interface IPhoneRepository : IGenericRepository<Phone>
    {
    }
}
