using AppMessageService;
using NFine.Application.SystemManage;
using NFine.Domain.Entity.SystemManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NFine.Web.Areas.Msg.Controllers
{
    public class MsgController : ControllerBase
    {
        //
        // GET: /Msg/Msg/
        private UserApp userApp = new UserApp();

        private MsgService objMsgService = new MsgService();

        public ViewResult ToList()
        {
            ViewResult vr = new ViewResult();
            vr.ViewName = "ToList";
            return vr;
        }

        public ViewResult ToAdd()
        {
            ViewResult vr = new ViewResult();
            vr.ViewName = "ToAdd";
            return vr;
        }


        public ActionResult GoToSendPage()
        {
            List<UserEntity> list = userApp.GetList(Request["keyValue"]);
            string text = "";
            foreach (var item in list)
            {
                text += item.F_RealName + "|" + item.F_MobilePhone + ","+"\n";
            }
            text = text.Trim(',');
            ViewData["phone"] = text;
            return View("SendMsg");
        }

        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm()
        {
            bool isTrue = objMsgService.SendSimpleAllDevice(Request["title"], Request["content"]);
            if (isTrue)
            {
                return Success("操作成功。");
            }
            return Error("发送失败，请稍后再试");
        }

    }
}
