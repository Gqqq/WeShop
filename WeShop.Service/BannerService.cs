using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeShop.EFModel;
using WeShop.IRepositoty;
using WeShop.IService;

namespace WeShop.Service
{
   public class BannerService : BaseService<Banner>,IBannerService
    {
       public BannerService(IBannerRepositoty bannerRepositoty) : base(bannerRepositoty)
       {

       }
    }
}
