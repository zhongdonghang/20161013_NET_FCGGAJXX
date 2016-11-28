using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.igetui.api.openservice;
using com.igetui.api.openservice.igetui;
using com.igetui.api.openservice.igetui.template;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace GTPushTools
{
    public class GTPushProvider
    {
        private string AppID = string.Empty;
        private string AppSecret = string.Empty;
        private string AppKey = string.Empty;
        private string MasterSecret = string.Empty;
        private string HOST = string.Empty;

        public GTPushProvider()
        {
            AppID = ConfigurationManager.AppSettings["GTAppID"];
            AppSecret = ConfigurationManager.AppSettings["GTAppSecret"];
            AppKey = ConfigurationManager.AppSettings["GTAppKey"];
            MasterSecret = ConfigurationManager.AppSettings["GTMasterSecret"];
            HOST = ConfigurationManager.AppSettings["GTHOST"];
        }

        public string pushMessageToApp(string content)
        {
            IGtPush push = new IGtPush(HOST, AppKey, MasterSecret);
            // 定义"AppMessage"类型消息对象，设置消息内容模板、发送的目标App列表、是否支持离线发送、以及离线消息有效期(单位毫秒)
            AppMessage message = new AppMessage();

            TransmissionTemplate template = TransmissionTemplateDemo(content);
            //  template.TransmissionContent = "0000000000000000000";
            message.IsOffline = true;                         // 用户当前不在线时，是否离线存储,可选
            message.OfflineExpireTime = 1000 * 3600 * 12;     // 离线有效时间，单位为毫秒，可选
            message.Data = template;
            //判断是否客户端是否wifi环境下推送，2:4G/3G/2G,1为在WIFI环境下，0为无限制环境
            //message.PushNetWorkType = 0; 
            //message.Speed = 1000;

            List<String> appIdList = new List<String>();
            appIdList.Add(AppID);

            List<String> phoneTypeList = new List<String>();   //通知接收者的手机操作系统类型
            phoneTypeList.Add("ANDROID");

            List<String> provinceList = new List<String>();    //通知接收者所在省份

            List<String> tagList = new List<string>();

            message.AppIdList = appIdList;


            String pushResult = push.pushMessageToApp(message);
            return pushResult;
        }

        public string PushMessageToList(string content,List<string> cids)
        {
            // 推送主类（方式1，不可与方式2共存）
            IGtPush push = new IGtPush(HOST, AppKey, MasterSecret);
            // 推送主类（方式2，不可与方式1共存）此方式可通过获取服务端地址列表判断最快域名后进行消息推送，每10分钟检查一次最快域名
            //IGtPush push = new IGtPush("",APPKEY,MASTERSECRET);
            ListMessage message = new ListMessage();

            TransmissionTemplate template = TransmissionTemplateDemo(content);
            // 用户当前不在线时，是否离线存储,可选
            message.IsOffline = true;
            // 离线有效时间，单位为毫秒，可选
            message.OfflineExpireTime = 1000 * 3600 * 12;
            message.Data = template;
            //message.PushNetWorkType = 0;        //判断是否客户端是否wifi环境下推送，1为在WIFI环境下，0为不限制网络环境。
            //设置接收者
            List<com.igetui.api.openservice.igetui.Target> targetList = new List<com.igetui.api.openservice.igetui.Target>();

            foreach (var item in cids)
            {
                com.igetui.api.openservice.igetui.Target target1 = new com.igetui.api.openservice.igetui.Target();
                target1.appId = AppID;
                target1.clientId = item;
                targetList.Add(target1);
            }

            String contentId = push.getContentId(message);
            String pushResult = push.pushMessageToList(contentId, targetList);
            return pushResult;
        }


        /*
        * 
        * 所有推送接口均支持四个消息模板，依次为透传模板，通知透传模板，通知链接模板，通知弹框下载模板
        * 注：IOS离线推送需通过APN进行转发，需填写pushInfo字段，目前仅不支持通知弹框下载功能
        *
        */
        //透传模板动作内容
        public TransmissionTemplate TransmissionTemplateDemo(string content)
        {
            TransmissionTemplate template = new TransmissionTemplate();
            template.AppId = AppID;
            template.AppKey = AppKey;
            template.TransmissionType = "1";            //应用启动类型，1：强制应用启动 2：等待应用启动
            template.TransmissionContent = content;  //透传内容

            //设置客户端展示时间
            String begin = "2016-11-25 14:28:10";
            String end = "2016-12-30 14:38:20";
            template.setDuration(begin, end);

            return template;
        }

    }
}
