using PhoneSelling.Data.Models;
using PhoneSelling.Data.Repositories.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneSelling.Data.Repositories.PhoneRepository
{
    public class MockPhoneRepository : IPhoneRepository
    {
        private readonly List<Phone> Phones = new List<Phone>()
        {
            new Phone("iPhone11", 100000),
            new Phone("iPhone11 Pro Max", 200000),
            new Phone("iPhone12", 300000),
            new Phone("iPhone12 Pro Max", 230000),
            new Phone("iPhone13", 200000),
            new Phone("iPhone13 Pro Max", 250000),
            new Phone("iPhone14", 100000)
        };
        public Task<IEnumerable<Phone>> GetAll()
        {
            var phones = new List<Phone>()
            {
                new Phone("iPhone11", 100000),
                new Phone("iPhone11 Pro Max", 200000),
                new Phone("iPhone12", 300000),
                new Phone("iPhone12 Pro Max", 230000),
                new Phone("iPhone13", 200000),
                new Phone("iPhone13 Pro Max", 250000),
                new Phone("iPhone14", 100000)
            };

            return Task.FromResult<IEnumerable<Phone>>(phones);
        }

        public Task<Phone?> GetById(Guid id)
        {
            var phone = Phones.FirstOrDefault(p => p.Id == id);

            return Task.FromResult(phone);
        }
    }
}
