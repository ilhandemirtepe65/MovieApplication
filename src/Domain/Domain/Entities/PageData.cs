using Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class PageData : EntityBase
    {
       
        [Key]
        public int Id { get; set; }
        public int Page { get; set; }
        public List<Movie> Results { get; set; }
        public int Total_pages { get; set; }
        public int Total_results { get; set; }
    }
}
