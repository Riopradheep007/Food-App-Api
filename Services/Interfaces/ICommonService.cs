using Common.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public  interface ICommonService
    {
        public IList<SideBarMenus> GetMenu(string role);
    }
}
