using Common.Model.Common;
using DataAccess.Interfaces;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Functional
{
    public class CommonService:ICommonService
    {
        private readonly Lazy<ICommonDataAccess> _commonDataAccess;
        public CommonService(Lazy<ICommonDataAccess> commonDataAccess)
        {
             this._commonDataAccess = commonDataAccess;
        }

        public IList<SideBarMenus> GetMenu(string role)
        {
            var result = _commonDataAccess.Value.GetMenu(role);
            return result;
        }
    }
}
