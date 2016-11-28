using NFine.Code;
using NFine.Domain._03_Entity.Message;
using NFine.Domain._04_IRepository.Message;
using NFine.Repository.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Application.Message
{
    public class B_AppUserMessageLogApp
    {
        private IB_AppUserMessageLogRepository service = new B_AppUserMessageLogRepository();

       

        public List<B_AppUserMessageLogEntity> GetList(string userName)
        {
            var expression = ExtLinq.True<B_AppUserMessageLogEntity>();

            return service.FindList("select * from [B_AppUserMessageLog] where  F_UserName='"+ userName + "' order by F_SendTime desc");
        }

        public List<B_AppUserMessageLogEntity> GetList(Pagination pagination, string keyword,string userName)
        {
            var expression = ExtLinq.True<B_AppUserMessageLogEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.F_Content.Contains(keyword));
            }
            if (!string.IsNullOrEmpty(userName))
            {
                expression = expression.And(t => t.F_UserName == userName);
            }
            return service.FindList(expression, pagination);
        }

        public List<B_AppUserMessageLogEntity> GetList()
        {
            return service.IQueryable().ToList();
        }

        public B_AppUserMessageLogEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }


        /// <summary>
        /// 批量插入
        /// </summary>
        /// <param name="ens"></param>
        public void SubmitForm(List<B_AppUserMessageLogEntity> ens )
        {
            foreach (var item in ens)
            {
                item.CreateWithGuId();
            }
            service.Insert(ens);
        }


        public void SubmitForm(B_AppUserMessageLogEntity log, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                log.Modify(keyValue);
                service.Update(log);
            }
            else
            {
                log.CreateWithGuId();
                service.Insert(log);
            }
        }
    }
}
