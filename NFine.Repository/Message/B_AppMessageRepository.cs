using NFine.Data;
using NFine.Domain._03_Entity.Message;
using NFine.Domain._04_IRepository.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Repository.Message
{
    public class B_AppMessageRepository : RepositoryBase<B_AppMessageEntity>, IB_AppMessageRepository
    {
    }
}
