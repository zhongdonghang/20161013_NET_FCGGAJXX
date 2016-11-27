using NFine.Domain._03_Entity.Message;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Mapping.Message
{
    public class B_AppMessageMap: EntityTypeConfiguration<B_AppMessageEntity>
    {
        public B_AppMessageMap()
        {
            this.ToTable("B_AppMessage");
            this.HasKey(t => t.F_Id);
        }
    }
}
