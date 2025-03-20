using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneSelling.Data.Repositories.ItemGroupRepository.ApiService.Common
{
    public class ItemGroupDto
    {
        public Guid ItemGroupID { get; set; }

        public string ItemGroupName { get; set; } = string.Empty;
    }

}
