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

        /// <summary>
        /// 获取消息发送列表
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public List<B_AppUserMessageLogEntity> GetList(Pagination pagination, string keyword)
        {
            var expression = ExtLinq.True<B_AppUserMessageLogEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.Or(t => t.F_RealName.Contains(keyword)); //根据接收人名字查询
               // expression = expression.Or(t => t.F_Phone.Contains(keyword)); //根据接收人手机号码查询
              //  expression = expression.Or(t => t.F_Content.Contains(keyword)); //根据消息内容查询
            }
            return service.FindList(expression, pagination);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="pageIndex">从1开始</param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public List<B_AppUserMessageLogEntity> GetList(string userName,string pageIndex,string PageSize) 
        {
            var expression = ExtLinq.True<B_AppUserMessageLogEntity>();
            string sql = "SELECT TOP "+ PageSize + " * "+
                " FROM "+
                " ( " +
                  "  SELECT ROW_NUMBER() OVER(ORDER BY F_SendTime desc) AS RowNumber, * " +
                 "   FROM[B_AppUserMessageLog] where F_UserName = 'zxf' " +
                " ) A " + 
                 "   WHERE RowNumber > "+ PageSize + " * ("+ pageIndex + " - 1)";
            return service.FindList(sql);
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
