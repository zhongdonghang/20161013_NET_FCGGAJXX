
//using GTPushTools;
using GTPushTools;
using NFine.Application.Message;
using NFine.Application.SystemManage;
using NFine.Code;
using NFine.Domain._03_Entity.Message;
using NFine.Domain.Entity.SystemManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppMessageService
{
    /// <summary>
    /// 个推业务服务
    /// </summary>
  public  class GeTuiMsgService
    {
        public GeTuiMsgService()
        {

        }

        private GTPushProvider objGTPushProvider = new GTPushProvider();

        public bool pushMessageToAllApp(string content)
        {
            bool isTrue = false;
            string result = objGTPushProvider.pushMessageToApp(content);
            if (result.Contains("ok")) isTrue = true;
 
            //插入消息总表
            B_AppMessageApp objB_AppMessageApp = new B_AppMessageApp();
            B_AppMessageEntity objB_AppMessageEntity = new B_AppMessageEntity();
            objB_AppMessageEntity.F_Msg_Type = "即时推送全部消息";
            objB_AppMessageEntity.F_SendTime = DateTime.Now;
            objB_AppMessageEntity.F_Sender = OperatorProvider.Provider.GetCurrent().UserName;
            objB_AppMessageEntity.F_Accepter = "all";
            objB_AppMessageEntity.F_Content = content;
            objB_AppMessageEntity.F_State = isTrue ? "成功" : "失败";
            objB_AppMessageEntity.F_Result = result;
            objB_AppMessageApp.SubmitForm(objB_AppMessageEntity, string.Empty);

            //插入每个人的消息记录
            B_AppUserMessageLogApp objB_AppUserMessageLogApp = new B_AppUserMessageLogApp();
            List<B_AppUserMessageLogEntity> ens = new List<B_AppUserMessageLogEntity>();

            //获取当前所有用户
            UserApp objUserApp = new UserApp();
            List<UserEntity> allUsers = objUserApp.GetList();
            foreach (UserEntity item in allUsers)
            {
                B_AppUserMessageLogEntity log = new B_AppUserMessageLogEntity();
                log.F_Msg_Type = "即时推送全部消息";
                log.F_SendTime = objB_AppMessageEntity.F_SendTime;
                log.F_Sender = OperatorProvider.Provider.GetCurrent().UserName;
                log.F_UserName = item.F_Account;
                log.F_Phone = item.F_MobilePhone;
                log.F_Cid = item.F_NickName;
                log.F_Title = "title";
                log.F_Content = content;
                log.F_State = objB_AppMessageEntity.F_State;

                log.F_ReadState = "N";
                log.F_ReaderTime = DateTime.Now;
                log.F_ReaderLocation = "未知";
                log.F_ReaderLocationX = "0.0";
                log.F_ReaderLocationY = "0.0";
                log.F_Result = result;
                ens.Add(log);
            }
            objB_AppUserMessageLogApp.SubmitForm(ens);
            return isTrue;
        }
    }
}
