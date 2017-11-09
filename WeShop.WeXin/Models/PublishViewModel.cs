using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WeShop.EFModel;

namespace WeShop.WeXin.Models
{
    public class PublishViewModel
    {
        public IEnumerable<Sort> Sort1 { get; set; }
        public IEnumerable<Sort> Sort2 { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<Stock> Stocks { get; set; }
        public IEnumerable<Tag> Tags { get; set; }
    }
}