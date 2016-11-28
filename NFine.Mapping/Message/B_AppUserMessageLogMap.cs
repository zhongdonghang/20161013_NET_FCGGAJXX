using NFine.Domain._03_Entity.Message;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Mapping.Message
{
    public class B_AppUserMessageLogMap : EntityTypeConfiguration<B_AppUserMessageLogEntity>
    {
        public B_AppUserMessageLogMap()
        {
            this.ToTable("B_AppUserMessageLog");
            this.HasKey(t => t.F_Id);
        }
    }
}
