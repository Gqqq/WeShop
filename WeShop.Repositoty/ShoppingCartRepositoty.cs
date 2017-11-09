using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeShop.EFModel;
using WeShop.IRepositoty;

namespace WeShop.Repositoty
{
    public class ShoppingCartRepositoty:BaseRepositoty<ShoppingCart>,IShoppingCartRepositoty
    {
    }
}
