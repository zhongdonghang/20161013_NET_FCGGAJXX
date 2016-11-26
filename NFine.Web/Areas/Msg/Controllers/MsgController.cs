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

        //public ActionResult Index()
        //{
        //    return View();
        //}

        private UserApp userApp = new UserApp();

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
            //string txtPhone =  Request["keyValue"];

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
    }
}
