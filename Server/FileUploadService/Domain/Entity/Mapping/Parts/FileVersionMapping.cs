using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAP.Persistence;

namespace TAP.FileService.Domain.Entity.Mapping.Parts
{
    internal class FileVersionMapping : IModelCreating
    {
        public dynamic Building(string schema)
        {
            var item = new TAP.Persistence.Plugin4Ef.OracleEntityConfiguration<FileVersion>(schema);
            item.ToTable("FILEVERSION", schema);

            item.HasKey(t => t.Id);
            item.Property(t => t.Id).HasColumnName("ID");
            item.Property(t => t.FileId).HasColumnName("FILEID");
            item.Property(t => t.FileInfor).HasColumnName("FILEINFOR");

            item.Property(t => t.Remark).HasColumnName("REMARK");
            item.Property(t => t.ModifiedTime).HasColumnName("MODIFIEDTIME");
            item.Property(T => T.Modifier).HasColumnName("MODIFIER");
            item.Property(t => t.Revision).HasColumnName("REVISION");
            item.Ignore(t => t.FileMetadata);
            return item;
        }
    }
}