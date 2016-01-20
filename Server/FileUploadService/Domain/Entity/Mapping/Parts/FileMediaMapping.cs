using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAP.Persistence;

namespace TAP.FileService.Domain.Entity.Mapping.Parts
{
    internal class FileMediaMapping : IModelCreating
    {
        public dynamic Building(string schema)
        {
            var item = new TAP.Persistence.Plugin4Ef.OracleEntityConfiguration<FileMedia>(schema);
            item.ToTable("FILEMEDIA", schema);
            item.HasKey(t => t.Id);
            item.Property(t => t.Id).HasColumnName("ID");
            item.Property(t => t.RelativePath).HasColumnName("RELATIVEPATH");
            item.Property(t => t.FullPath).HasColumnName("FULLPATH");
            item.Property(t => t.CheckSum).HasColumnName("CHECKSUM");
            item.Ignore(t => t.FileStream);
            return item;
        }
    }
}