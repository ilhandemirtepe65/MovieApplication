using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Common
{
    public abstract class EntityBase
    {
       
        public string? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ? LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}
