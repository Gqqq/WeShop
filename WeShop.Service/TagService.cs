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
    public class TagService:BaseService<Tag>,ITagService
    {
        public TagService(ITagRepositoty baseRepositoty) : base(baseRepositoty)
        {
        }
    }
}
