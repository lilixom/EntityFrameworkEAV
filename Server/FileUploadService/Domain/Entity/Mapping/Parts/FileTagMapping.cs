using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAP.Persistence;

namespace TAP.FileService.Domain.Entity.Mapping.Parts
{
    internal class FileTagMapping : IModelCreating
    {
        public dynamic Building(string schema)
        {
            var item = new TAP.Persistence.Plugin4Ef.OracleEntityConfiguration<FileTag>(schema);
            item.ToTable("FILETAG", schema);

            item.HasKey(t => t.Id);
            item.Property(t => t.Id).HasColumnName("ID");
            item.Property(t => t.Name).HasColumnName("NAME");

            return item;
        }
    }
}