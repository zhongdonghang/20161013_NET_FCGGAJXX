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
    public class B_AppMessageApp
    {
        private IB_AppMessageRepository service = new B_AppMessageRepository();

        public List<B_AppMessageEntity> GetList(Pagination pagination, string keyword)
        {
            var expression = ExtLinq.True<B_AppMessageEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.F_Title.Contains(keyword));
                expression = expression.Or(t => t.F_Sender.Contains(keyword));
               // expression = expression.Or(t => t.F_MobilePhone.Contains(keyword));
            }
           // expression = expression.And(t => t.F_Account != "admin");
            return service.FindList(expression, pagination);
        }

        public List<B_AppMessageEntity> GetList()
        {
            return service.IQueryable().ToList();
        }

        public B_AppMessageEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }

        public void DeleteForm(string keyValue)
        {
            //if (service.IQueryable().Count(t => t.F_ParentId.Equals(keyValue)) > 0)
            //{
            //    throw new Exception("删除失败！操作的对象包含了下级数据。");
            //}
            //else
            //{
                service.Delete(t => t.F_Id == keyValue);
          //  }
        }

        public void SubmitForm(B_AppMessageEntity appMessageEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                appMessageEntity.Modify(keyValue);
                service.Update(appMessageEntity);
            }
            else
            {
                appMessageEntity.CreateWithGuId();
                service.Insert(appMessageEntity);
            }
        }
    }
}
