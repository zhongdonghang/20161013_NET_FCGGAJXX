using Newtonsoft.Json;
using NFine.Application;
using NFine.Application.Message;
using NFine.Application.SystemManage;
using NFine.Application.SystemSecurity;
using NFine.Code;
using NFine.Data.Extensions;
using NFine.Domain._02_ViewModel;
using NFine.Domain._03_Entity.Message;
using NFine.Domain.Entity.SystemManage;
using NFine.Domain.Entity.SystemSecurity;
using NFine.Domain.IRepository.SystemManage;
using NFine.Repository.SystemManage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Application
{
    /// <summary>
    /// app终端接口服务类
    /// </summary>
    public class ApiServiceApp
    {
        #region 组织架构底层服务类
        private IOrganizeRepository orgDllService = new OrganizeRepository();
        private IUserRepository userDllService = new UserRepository();
        private UserLogOnApp userLogOnApp = new UserLogOnApp();

        #endregion

        private B_AppUserMessageLogApp objB_AppUserMessageLogApp = new B_AppUserMessageLogApp();

        public string UpdateMsgToReaded(string msgId,string readerLocation,string readerLocationX,string readerLocationY)
        {
            ReturnSimpleResult1 ret = new ReturnSimpleResult1();
            try
            {
                B_AppUserMessageLogEntity oldObj = objB_AppUserMessageLogApp.GetForm(msgId);
                oldObj.F_ReadState = "Y";
                oldObj.F_ReaderTime = DateTime.Now;
                oldObj.F_ReaderLocation = readerLocation;
                oldObj.F_ReaderLocationX = readerLocationX;
                oldObj.F_ReaderLocationY = readerLocationY;

                objB_AppUserMessageLogApp.SubmitForm(oldObj, msgId);
                ret.Msg = "操作成功";
                ret.ResultCode = "0";
            }
            catch (Exception ex)
            {
                ret.Msg = ex.ToString();
                ret.ResultCode = "-1";
            }

            return Newtonsoft.Json.JsonConvert.SerializeObject(ret);
        }


        /// <summary>
        /// 根据登录账户查询历史消息列表
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public string GetMsgLogByAccount(string username, string index, string pagesize)
        {
            ReturnPageResult<B_AppUserMessageLogEntity> ret = new ReturnPageResult<B_AppUserMessageLogEntity>();

            try
            {
                List<B_AppUserMessageLogEntity> list =  objB_AppUserMessageLogApp.GetList(username, index,pagesize);
                ret.ResultCode = "0";
                ret.Msg = "查询成功";
                PageObject<B_AppUserMessageLogEntity> page = new PageObject<B_AppUserMessageLogEntity>();
                page.PageCount = list.Count%int.Parse(pagesize)==0 ? list.Count / int.Parse(pagesize): (list.Count / int.Parse(pagesize)) + 1;
                page.PageIndex =int.Parse( index);
                page.RecordCount = list.Count;
                page.PageSize = int.Parse(pagesize);
                page.Data = list;

                ret.Page = page;
            }
            catch (Exception ex)
            {
                ret.Msg = ex.ToString();
                ret.ResultCode = "-1";
            }

            return Newtonsoft.Json.JsonConvert.SerializeObject(ret);
        }



        /// <summary>
        /// App登录
        /// </summary>
        /// <returns></returns>
        public string AppLogin(string username, string password,string cid)
        {
            ReturnSimpleResult2<AppRegsiterResult> ret = new ReturnSimpleResult2<AppRegsiterResult>();
            try
            {
                LogEntity logEntity = new LogEntity();
                logEntity.F_ModuleName = "App登录";
                logEntity.F_Type = DbLogType.Login.ToString();
                UserEntity userEntity = new UserApp().CheckLogin(username, password);
                if (userEntity != null)
                {
                    AppRegsiterResult vm = new AppRegsiterResult();
                    vm.CurrentUser = userEntity;
                    OrganizeEntity orgEntity = orgDllService.FindEntity(t => t.F_Id == userEntity.F_OrganizeId);
                    vm.CurrentOrganizeEntity = orgEntity;
                    ret.Msg = "登录成功";
                    ret.ResultCode = "200";
                    ret.t = vm;

                    //写入CID  用User表里面F_NickName存放cid
                    //  userDllService.Update()
                    userEntity.F_NickName = cid;
                    userDllService.Update(userEntity);

                    #region 写登录日志
                    logEntity.F_Account = userEntity.F_Account;
                    logEntity.F_NickName = userEntity.F_RealName;
                    logEntity.F_Result = true;
                    logEntity.F_Description = "登录成功";
                    new LogApp().WriteDbLog(logEntity);
                    #endregion
                }
                else
                {
                    ret.Msg = "登录失败";
                    ret.ResultCode = "-1";
                    ret.t = null;

                    #region 写登录日志
                    logEntity.F_Account = userEntity.F_Account;
                    logEntity.F_Result = false;
                    logEntity.F_Description = "登录失败";
                    new LogApp().WriteDbLog(logEntity);
                    #endregion  
                }
            }
            catch (Exception ex)
            {
                ret.Msg = ex.ToString();
                ret.ResultCode = "-1";
                ret.t = null;
            }
            return Newtonsoft.Json.JsonConvert.SerializeObject(ret);
        }



    }
}
