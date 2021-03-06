using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.Web;
using BP.Sys;
using BP.WF.Port;
using System.Net.Mail;

namespace BP.WF
{
    /// <summary>
    /// 消息类型
    /// </summary>
    public class SMSMsgType
    {
        /// <summary>
        /// 自定义消息
        /// </summary>
        public const string Self = "Self";
        /// <summary>
        /// 抄送消息
        /// </summary>
        public const string CC = "CC";
        /// <summary>
        /// 待办消息
        /// </summary>
        public const string ToDo = "ToDo";
        /// <summary>
        /// 其他
        /// </summary>
        public const string Etc = "Etc";
    }
	/// <summary>
	/// 消息状态
	/// </summary>
    public enum MsgSta
    {
        /// <summary>
        /// 未开始
        /// </summary>
        UnRun,
        /// <summary>
        /// 成功
        /// </summary>
        RunOK,
        /// <summary>
        /// 失败
        /// </summary>
        RunError,
        /// <summary>
        /// 禁止发送
        /// </summary>
        Disable
    }
	/// <summary>
	/// 消息属性
	/// </summary>
	public class SMSAttr:EntityMyPKAttr
	{
        /// <summary>
        /// 消息标记（有此标记的不在发送）
        /// </summary>
        public const string MsgFlag = "MsgFlag";
		/// <summary>
		/// 状态 0 未发送， 1 发送成功，2发送失败.
		/// </summary>
        public const string EmailSta = "EmailSta";
        /// <summary>
        /// 邮件
        /// </summary>
        public const string Email = "Email";
        /// <summary>
        /// 邮件标题
        /// </summary>
        public const string EmailTitle = "EmailTitle";
        /// <summary>
        /// 邮件内容
        /// </summary>
        public const string EmailDoc = "EmailDoc";
        /// <summary>
        /// 发送人
        /// </summary>
        public const string Sender = "Sender";
        /// <summary>
        /// 发送给
        /// </summary>
        public const string SendTo = "SendTo";
        /// <summary>
        /// 插入日期
        /// </summary>
        public const string RDT = "RDT";
        /// <summary>
        /// 发送日期
        /// </summary>
        public const string SendDT = "SendDT";
        /// <summary>
        /// 是否读取
        /// </summary>
        public const string IsRead = "IsRead";
        /// <summary>
        /// 状态 0 未发送， 1 发送成功，2发送失败.
        /// </summary>
        public const string MobileSta = "MobileSta";
        /// <summary>
        /// 手机
        /// </summary>
        public const string Mobile = "Mobile";
        /// <summary>
        /// 手机信息
        /// </summary>
        public const string MobileInfo = "MobileInfo";
        /// <summary>
        /// 是否提示过了
        /// </summary>
        public const string IsAlert = "IsAlert";
        /// <summary>
        /// 消息类型
        /// </summary>
        public const string MsgType = "MsgType";
	}
	/// <summary>
	/// 消息
	/// </summary> 
    public class SMS : EntityMyPK
    {
        #region 新方法 2013 
        /// <summary>
        /// 发送手机消息
        /// </summary>
        /// <param name="Mobile">手机号</param>
        /// <param name="doc">手机内容</param>
        public static void SendSMS_del(string Mobile, string doc)
        {
            // 如果不启用消息机制.
            if (BP.WF.Glo.IsEnableSysMessage == false)
                return;

            SMS sms = new SMS();
            sms.MyPK = DBAccess.GenerGUID();
            sms.HisEmailSta = MsgSta.UnRun;
            sms.Email = Mobile;
            sms.Title = doc;
            sms.Sender = BP.Web.WebUser.No;
            sms.RDT = BP.DA.DataType.CurrentDataTime;
            try
            {
                sms.Insert();
            }
            catch
            {
                sms.CheckPhysicsTable();
                sms.Insert();
            }
        }
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="userNo">接受人</param>
        /// <param name="msgTitle">标题</param>
        /// <param name="msgDoc">内容</param>
        /// <param name="msgFlag">标记</param>
        /// <param name="msgType">类型</param>
        public static void SendMsg(string userNo, string msgTitle, string msgDoc, string msgFlag, string msgType)
        {
           
            SMS sms = new SMS();
            sms.MyPK = DBAccess.GenerGUID();
            sms.HisEmailSta = MsgSta.UnRun;

            sms.Sender=WebUser.No;
            sms.SendTo = userNo;

            sms.Title = msgTitle;
            sms.DocOfEmail = msgDoc;

            sms.Sender = BP.Web.WebUser.No;
            sms.RDT = BP.DA.DataType.CurrentDataTime;
            
            sms.MsgFlag = msgFlag; // 消息标志.
            sms.MsgType = msgType; // 消息类型.
            sms.Insert();
        }
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="mobileNum">手机号吗</param>
        /// <param name="mobileInfo">短信信息</param>
        /// <param name="email">邮件</param>
        /// <param name="title">标题</param>
        /// <param name="infoBody">邮件内容</param>
        /// <param name="msgFlag">消息标记，可以为空。</param>
        /// <param name="guestNo">用户编号</param>
        public static void SendMsg(string mobileNum, string mobileInfo, string email, string title, string
            infoBody, string msgFlag,string msgType,string guestNo)
        {

            SMS sms = new SMS();
            sms.Sender = WebUser.No;
            sms.RDT = BP.DA.DataType.CurrentDataTimess;
            sms.SendTo = guestNo;

            // 邮件信息
            sms.HisEmailSta = MsgSta.UnRun;
            sms.Title = title;
            sms.DocOfEmail = infoBody;

            //手机信息.
            sms.Mobile = mobileNum;
            sms.HisMobileSta = MsgSta.UnRun;
            sms.MobileInfo = mobileInfo;
            sms.MsgFlag = msgFlag; // 消息标志.

            if (string.IsNullOrEmpty(msgFlag))
            {
                sms.MyPK = DBAccess.GenerGUID();
                sms.Insert();
            }
            else
            {
                // 如果已经有该PK,就不让插入了.
                try
                {
                    sms.MyPK = msgFlag;
                    sms.Insert();
                }
                catch
                {
                }
            }
        }
        #endregion 新方法

        #region 手机短信属性
        /// <summary>
        /// 手机号码
        /// </summary>
        public string Mobile
        {
            get
            {
                return this.GetValStringByKey(SMSAttr.Mobile);
            }
            set
            {
                SetValByKey(SMSAttr.Mobile, value);
            }
        }
        /// <summary>
        /// 手机状态
        /// </summary>
        public MsgSta HisMobileSta
        {
            get
            {
                return (MsgSta)this.GetValIntByKey(SMSAttr.MobileSta);
            } 
            set
            {
                SetValByKey(SMSAttr.MobileSta, (int)value);
            }
        }
        /// <summary>
        /// 手机信息
        /// </summary>
        public string MobileInfo
        {
            get
            {
                return this.GetValStringByKey(SMSAttr.MobileInfo);
            }
            set
            {
                SetValByKey(SMSAttr.MobileInfo, value);
            }
        }
        #endregion

        #region  邮件属性
        /// <summary>
        /// 邮件状态
        /// </summary>
        public MsgSta HisEmailSta
        {
            get
            {
                return (MsgSta)this.GetValIntByKey(SMSAttr.EmailSta);
            }
            set
            {
                this.SetValByKey(SMSAttr.EmailSta, (int)value);
            }
        }
        /// <summary>
        /// Email
        /// </summary>
        public string Email
        {
            get
            {
                return this.GetValStringByKey(SMSAttr.Email);
            }
            set
            {
                SetValByKey(SMSAttr.Email, value);
            }
        }
        /// <summary>
        /// 发送给
        /// </summary>
        public string SendToEmpNo
        {
            get
            {
                return this.GetValStringByKey(SMSAttr.SendTo);
            }
            set
            {
                SetValByKey(SMSAttr.SendTo, value);
            }
        }
        /// <summary>
        /// 发送给 
        /// </summary>
        public string SendTo
        {
            get
            {
                return this.GetValStringByKey(SMSAttr.SendTo);
            }
            set
            {
                SetValByKey(SMSAttr.SendTo, value);
            }
        }
        /// <summary>
        /// 消息标记(可以用它来避免发送重复)
        /// </summary>
        public string MsgFlag
        {
            get
            {
                return this.GetValStringByKey(SMSAttr.MsgFlag);
            }
            set
            {
                SetValByKey(SMSAttr.MsgFlag, value);
            }
        }
        /// <summary>
        /// 类型
        /// </summary>
        public string MsgType
        {
            get
            {
                return this.GetValStringByKey(SMSAttr.MsgType);
            }
            set
            {
                SetValByKey(SMSAttr.MsgType, value);
            }
        }
        /// <summary>
        /// 发送人
        /// </summary>
        public string Sender
        {
            get
            {
                return this.GetValStringByKey(SMSAttr.Sender);
            }
            set
            {
                SetValByKey(SMSAttr.Sender, value);
            }
        }
        /// <summary>
        /// 记录日期
        /// </summary>
        public string RDT
        {
            get
            {
                return this.GetValStringByKey(SMSAttr.RDT);
            }
            set
            {
                SetValByKey(SMSAttr.RDT, value);
            }
        }
        /// <summary>
        /// 发送日期
        /// </summary>
        public string SendDT
        {
            get
            {
                return this.GetValStringByKey(SMSAttr.SendDT);
            }
            set
            {
                SetValByKey(SMSAttr.SendDT, value);
            }
        }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title
        {
            get
            {
                return this.GetValStringByKey(SMSAttr.EmailTitle);
            }
            set
            {
                SetValByKey(SMSAttr.EmailTitle, value);
            }
        }
        /// <summary>
        /// 邮件内容
        /// </summary>
        public string DocOfEmail
        {
            get
            {
                string doc = this.GetValStringByKey(SMSAttr.EmailDoc);
                if (string.IsNullOrEmpty(doc))
                    return this.Title;
                return doc.Replace('~', '\'');
            }
            set
            {
                SetValByKey(SMSAttr.EmailDoc, value);
            }
        }
        public string Doc
        {
            get
            {
                return this.DocOfEmail;
            }
            set
            {
                SetValByKey(SMSAttr.EmailDoc, value);
            }
        }
        #endregion

        #region 构造函数
        /// <summary>
        /// UI界面上的访问控制
        /// </summary>
        public override UAC HisUAC
        {
            get
            {
                UAC uac = new UAC();
                uac.OpenAll();
                return uac;
            }
        }
        /// <summary>
        /// 消息
        /// </summary>
        public SMS()
        {
        }
        /// <summary>
        /// Map
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;

                Map map = new Map("Sys_SMS");
                map.EnDesc = "消息";

                map.AddMyPK();

                map.AddTBString(SMSAttr.Sender, null, "发送人(可以为空)", false, true, 0, 200, 20);
                map.AddTBString(SMSAttr.SendTo, null, "发送给(可以为空)", false, true, 0, 200, 20);
                map.AddTBDateTime(SMSAttr.RDT, "写入时间", true, false);

                map.AddTBString(SMSAttr.Mobile, null, "手机号(可以为空)", false, true, 0, 30, 20);
                map.AddTBInt(SMSAttr.MobileSta, (int)MsgSta.UnRun, "消息状态", true, true);
                map.AddTBString(SMSAttr.MobileInfo, null, "短信信息", false, true, 0, 1000, 20);

                map.AddTBString(SMSAttr.Email, null, "Email(可以为空)", false, true, 0, 200, 20);
                map.AddTBInt(SMSAttr.EmailSta, (int)MsgSta.UnRun, "EmaiSta消息状态", true, true);
                map.AddTBString(SMSAttr.EmailTitle, null, "标题", false, true, 0, 3000, 20);
                map.AddTBStringDoc(SMSAttr.EmailDoc, null, "内容", false, true);
                map.AddTBDateTime(SMSAttr.SendDT,null, "发送时间", false, false);

                map.AddTBInt(SMSAttr.IsRead, 0, "是否读取?", true, true);
                map.AddTBInt(SMSAttr.IsAlert, 0, "是否提示?", true, true);

                map.AddTBString(SMSAttr.MsgFlag, null, "消息标记(用于防止发送重复)", false, true, 0, 200, 20);
                map.AddTBString(SMSAttr.MsgType, null, "消息类型(CC抄送,ToDo待办)", false, true, 0, 200, 20);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="mail"></param>
        /// <param name="mailTitle"></param>
        /// <param name="mailDoc"></param>
        /// <returns></returns>
        public static bool SendEmailNow(string mail, string mailTitle, string mailDoc)
        {
            System.Net.Mail.MailMessage myEmail = new System.Net.Mail.MailMessage();
            myEmail.From = new System.Net.Mail.MailAddress("ccflow.cn@gmail.com", "ccflow", System.Text.Encoding.UTF8);

            myEmail.To.Add(mail);
            myEmail.Subject = mailTitle;
            myEmail.SubjectEncoding = System.Text.Encoding.UTF8;//邮件标题编码

            myEmail.Body = mailDoc;
            myEmail.BodyEncoding = System.Text.Encoding.UTF8;//邮件内容编码
            myEmail.IsBodyHtml = true;//是否是HTML邮件

            myEmail.Priority = MailPriority.High;//邮件优先级

            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential(SystemConfig.GetValByKey("SendEmailAddress", "ccflow.cn@gmail.com"),
                SystemConfig.GetValByKey("SendEmailPass", "ccflow123"));
            //上述写你的邮箱和密码
            client.Port = SystemConfig.GetValByKeyInt("SendEmailPort", 587); //使用的端口
            client.Host = SystemConfig.GetValByKey("SendEmailHost", "smtp.gmail.com");

            // 经过ssl加密. 
            if (SystemConfig.GetValByKeyInt("SendEmailEnableSsl", 1) == 1)
                client.EnableSsl = true;  //经过ssl加密.
            else
                client.EnableSsl = false; //经过ssl加密.


            try
            {
                object userState = myEmail;
                client.SendAsync(myEmail, userState);
                return true;
            }
            catch
            {
                return false;
            }
        }
         
    }
	/// <summary>
	/// 消息s
	/// </summary> 
    public class SMSs : Entities
    {
        public override Entity GetNewEntity
        {
            get
            {
                return new SMS();
            }
        }
        public SMSs()
        {
        }
    }
}
 