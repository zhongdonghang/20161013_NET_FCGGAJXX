using NFine.Domain.Entity.SystemManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Domain._02_ViewModel
{
    /// <summary>
    /// 注册/登录返回的对象
    /// </summary>
    public class AppRegsiterResult
    {
        public UserEntity CurrentUser { get; set; }
        public OrganizeEntity CurrentOrganizeEntity { get; set; }
    }
}
