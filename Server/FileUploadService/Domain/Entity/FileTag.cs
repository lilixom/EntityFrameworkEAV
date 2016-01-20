using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAP.FileService.Infrastructure;

namespace TAP.FileService.Domain.Entity
{
    public class FileTag : IEntity<string>
    {
        public string Id { get; set; }

        public string FileId { get; set; }

        public string Name { get; set; }
    }
}