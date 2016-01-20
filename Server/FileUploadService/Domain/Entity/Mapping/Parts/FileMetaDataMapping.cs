using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAP.Persistence;

namespace TAP.FileService.Domain.Entity.Mapping.Parts
{
    internal class FileMetaDataMapping : IModelCreating
    {
        public dynamic Building(string schema)
        {
            var item = new TAP.Persistence.Plugin4Ef.OracleEntityConfiguration<FileMetadata>(schema);
            item.ToTable("FILEMETADATA", schema);

            item.HasKey(t => t.Id);
            item.Property(t => t.Id).HasColumnName("ID");
            item.Property(t => t.IsDel).HasColumnName("ISDEL");
            item.Property(t => t.CatalogUri).HasColumnName("CATALOGURI");
            item.Property(t => t.MediaId).HasColumnName("MEDIAID");
            item.Property(t => t.CreatedTime).HasColumnName("CREATEDTIME");
            item.Property(t => t.Extension).HasColumnName("EXTENSION");
            item.Property(t => t.LastModifiedTime).HasColumnName("LASTMODIFIEDTIME");
            item.Property(t => t.LastModifier).HasColumnName("LASTMODIFIER");
            item.Property(t => t.Owner).HasColumnName("OWNER");
            item.Property(t => t.ResourceName).HasColumnName("RESOURCENAME");
            item.Property(t => t.ResourceSize).HasColumnName("RESOURCESIZE");
            item.Property(t => t.Revision).HasColumnName("REVISION");

            //item.HasMany(o => o.Tags).WithOptional().HasForeignKey(f => f.FileId);
            item.HasMany(h => h.Versions).WithRequired().HasForeignKey(f => f.FileId);
            item.Property(t => t.IsDel).HasColumnType("odp_internal_use_type");
            item.HasMany(t => t.Propertys).WithRequired().HasForeignKey(f => f.FileId);
            //item.Ignore(t => t.Propertys);
            return item;
        }
    }
}