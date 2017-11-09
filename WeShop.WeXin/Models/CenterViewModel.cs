using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WeShop.EFModel;

namespace WeShop.WeXin.Models
{
    public class CenterViewModel
    {
        public IEnumerable<User> Users { get; set; }
    }
}