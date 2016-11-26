using Newtonsoft.Json;
using NFine.Application;
using NFine.Application.SystemManage;
using NFine.Application.SystemSecurity;
using NFine.Code;
using NFine.Data.Extensions;
using NFine.Domain._02_ViewModel;
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



        /// <summary>
        /// App登录
        /// </summary>
        /// <returns></returns>
        public string AppLogin(string username, string password)
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
