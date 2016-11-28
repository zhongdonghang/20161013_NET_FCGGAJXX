using AppMessageService;
using NFine.Application.Message;
using NFine.Application.SystemManage;
using NFine.Code;
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
        private B_AppMessageApp appMessageApp = new B_AppMessageApp();
        //  private MsgService objMsgService = new MsgService();
        private GeTuiMsgService objGeTuiMsgService = new GeTuiMsgService();
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(Code.Pagination pagination, string keyword)
        {
            var data = new
            {
                rows = appMessageApp.GetList(pagination, keyword),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }

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


        public ActionResult GoToSendAllPage()
        {
            ViewData["sendType"] = "All";
            return View("SendMsg");
        }

        public ActionResult GoToSendPage()
        {
            List<UserEntity> list = userApp.GetList(Request["keyValue"]);
            string text = "";
            foreach (var item in list)
            {
                text += item.F_RealName + "(" + item.F_Account + "),";
            }
            text = text.Trim(',');
            ViewData["sendType"] = "NotAll";
            ViewData["phone"] = text;
            return View("SendMsg");
        }

        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitForm()
        {
            bool isTrue = objGeTuiMsgService.pushMessageToAllApp(Request["content"]);//.SendSimpleAllDevice(Request["title"], Request["content"]);
            if (isTrue)
            {
                return Success("操作成功。");
            }
            return Error("发送失败，请稍后再试");
        }

        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public ActionResult SendMsg2UserList()
        {
            //p001(p001),曾训峰(zxf)
            string txtAccepter = Request["txtAccepter"].ToString().Trim();
            string[] strAr = txtAccepter.Split(',');
            List<string> listAccount = new List<string>();
            foreach (var item in strAr)
            {
                int start = item.IndexOf('(')+1;
                string account = item.Substring(start);
                account = account.TrimEnd(')');
                listAccount.Add(account);
            }
            List<UserEntity> listUser = userApp.GetList(listAccount);
            List<string> cids = new List<string>();
            foreach (var item in listUser)
            {
                cids.Add(item.F_NickName);
            }
            string content =  Request["content"].ToString().Trim();
            bool isTrue = objGeTuiMsgService.PushMessageToList(Request["content"], cids, txtAccepter);//.SendSimpleAllDevice(Request["title"], Request["content"]);
            if (isTrue)
            {
                return Success("操作成功。");
            }
            return Error("发送失败，请稍后再试");
        }

    }
}
