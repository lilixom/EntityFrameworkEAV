using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAP.Persistence;

namespace TAP.FileService.Domain.Entity.Mapping.Parts
{
    internal class FileMetadataEAVMapping : IModelCreating
    {
        public dynamic Building(string schema)
        {
            var item = new TAP.Persistence.Plugin4Ef.OracleEntityConfiguration<FileMetadataEAVProperty>(schema);
            item.ToTable("FILEMETADATAPROPERTY", schema);
            item.HasKey(t => new { t.FileId, t.Name });
            item.Property(t => t.FileId).HasColumnName("FILEID");
            item.Property(t => t.Name).HasColumnName("NAME");
            item.Property(t => t.Value).HasColumnName("VALUE");

            return item;
        }
    }
}