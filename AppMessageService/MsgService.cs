﻿//using NFine.Application.Message;
//using NFine.Code;
//using NFine.Domain._03_Entity.Message;
//using NSTool.XGPush;
//using NSTool.XGPush.Base;
//using NSTool.XGPush.Core;
//using System;
//using System.Collections.Generic;
//using System.Configuration;
//using System.Linq;
//using System.Text;

//namespace AppMessageService
//{
//    public class MsgService
//    {
//        private long ACCESS_ID = 0;
//        private string ACCESS_KEY = string.Empty;
//        private string SECRET_KEY = string.Empty;
//        private string BAG_NAME = string.Empty;



//        public MsgService()
//        {
//            ACCESS_ID = long.Parse(ConfigurationManager.AppSettings["ACCESS_ID"]);
//            ACCESS_KEY = ConfigurationManager.AppSettings["ACCESS_KEY"];
//            SECRET_KEY = ConfigurationManager.AppSettings["SECRET_KEY"];
//            BAG_NAME = ConfigurationManager.AppSettings["BAG_NAME"];
//        }

//        /// <summary>
//        /// 发送给所有设备
//        /// </summary>
//        public bool SendSimpleAllDevice(string title,string Content)
//        {
//            bool isTrue = false;
//            QQXGProvider qqxg = new QQXGProvider();
//            XGPushAllDeviceParam xgp = new XGPushAllDeviceParam();
//            xgp.Timestamp = null; //1299865775;
//            xgp.Valid_time = 600; //600;
//            xgp.Sign = null;
//            //实际key请使用实际的，下面仅演示作用
//            //============测试key1==============
//            xgp.Access_id = ACCESS_ID;
//            xgp.Secret_Key = SECRET_KEY;

//            xgp.Message_type = 1;
//            xgp.Expire_time = 3600;
//            xgp.Message = new NotifyMessage()
//            {
//                Custom_content = new SerializableDictionary<string, string>(){
//                  {"type","123"},{"type1","456"}
//                },
//                Clearable = 1,
//                Title = title,
//                Content = Content, //中文测试
//                Vibrate = 1,
//                Ring = 1,
//                //Action = new NotifyMessageAction()
//                //{
//                //    Action_type = 2,
//                //    Browser = new NotifyMessageAction_Browser()
//                //    {
//                //        Url = "http://baidu.com",
//                //        Confirm = 0
//                //    },
//                //    Intent = "http://baidu.com",
//                //    Activity = "XGPushDemo"
//                //}
//            };
//            XGResult<XGPushResult> a = qqxg.PushAllDevices(xgp);
//            if (!string.IsNullOrEmpty(a.Result.Push_id)) isTrue = true;

//            B_AppMessageApp objB_AppMessageApp = new B_AppMessageApp();
//            //objB_AppMessageApp.SubmitForm()
//            B_AppMessageEntity objB_AppMessageEntity = new B_AppMessageEntity();
//            objB_AppMessageEntity.F_Msg_Type = "即时推送全部消息";
//            objB_AppMessageEntity.F_SendTime = DateTime.Now;
//            objB_AppMessageEntity.F_Sender = OperatorProvider.Provider.GetCurrent().UserName;
//            objB_AppMessageEntity.F_Accepter = "all";
//            objB_AppMessageEntity.F_Title = title;
//            objB_AppMessageEntity.F_Content = Content;
//            objB_AppMessageEntity.F_State = isTrue ? "成功" : "失败";
//            objB_AppMessageEntity.F_Result = a.Err_msg + a.Result + a.Ret_code;
//            objB_AppMessageApp.SubmitForm(objB_AppMessageEntity, string.Empty);
//            return isTrue;
//        }
//    }
//}
