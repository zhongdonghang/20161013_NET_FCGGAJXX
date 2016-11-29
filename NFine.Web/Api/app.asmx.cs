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
    [WebService(Namespace = "http://xx.duncan.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class app : System.Web.Services.WebService
    {
        [WebMethod(Description = "更新指定消息已读状态,msgId记录编号，readerLocation地理位置描述，readerLocationX地理位置x，readerLocationY地理位置Y")]
        public void UpdateMsgToReaded(string msgId, string readerLocation, string readerLocationX, string readerLocationY)
        {
            HttpContext.Current.Response.Write(new ApiServiceApp().UpdateMsgToReaded(msgId,readerLocation,readerLocationX,readerLocationY));
        }

        [WebMethod(Description = "根据登录名查询历史消息记录，loginName为登录账户名,pageIndex页脚索引从1开始，分页参数记得一定传整数")]
        public void GetMsgByAccount(string loginName,string pageIndex,string pageSize)
        {
            HttpContext.Current.Response.Write(new ApiServiceApp().GetMsgLogByAccount(loginName, pageIndex, pageSize));
        }

        [WebMethod(Description = "app登录方法")]
        public void AppLogin(string loginName,string loginPass,string cid)
        {
            HttpContext.Current.Response.Write(new ApiServiceApp().AppLogin(loginName, loginPass, cid));
        }

        [WebMethod(Description = "app注册方法")]
        public void AppReg(string cid, string F_Account, string F_RealName, string F_MobilePhone)
        {
            HttpContext.Current.Response.Write(new ApiServiceApp().AppReg(cid, F_Account, F_RealName, F_MobilePhone));
        }
    }
}
