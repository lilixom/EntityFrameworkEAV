using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAP.FileService.Infrastructure;

namespace TAP.FileService.Domain.Entity
{
    public class FileVersion : IEntity<string>
    {
        public string FileId { get; set; }

        public string Id { get; set; }

        public String FileInfor { get; set; }

        public FileMetadata FileMetadata { get; set; }

        public long Revision { get; set; }

        public string Remark { get; set; }

        public DateTime ModifiedTime { get; set; }

        public string Modifier { get; set; }
    }
}