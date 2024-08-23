using Store.Core.Entities.Base;
using Store.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Core.Entities
{
    public class AppFile : BaseEntity
    {
        public string FileName { get; set; }
        public string Extension { get; set; }
        public StorageType Storage {  get; set; }
        [NotMapped]
        public override DateTime? UpdatedAt { get => base.UpdatedAt; set => base.UpdatedAt = value; }
    }
}
