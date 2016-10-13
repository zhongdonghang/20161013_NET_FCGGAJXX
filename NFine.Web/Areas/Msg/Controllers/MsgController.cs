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
    }
}
