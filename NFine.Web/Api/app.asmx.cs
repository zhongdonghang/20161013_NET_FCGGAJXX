using NFine.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace NFine.Web.Api
{
    /// <summary>
    /// app 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class app : System.Web.Services.WebService
    {

        [WebMethod(Description = "app登录方法")]
        public void AppLogin(string loginName,string loginPass)
        {
            HttpContext.Current.Response.Write(new ApiServiceApp().AppLogin(loginName, loginPass));
        }
    }
}
