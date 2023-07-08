using Common.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface  ICommonDataAccess
    {
        public IList<SideBarMenus> GetMenu(string role);
    }
}
