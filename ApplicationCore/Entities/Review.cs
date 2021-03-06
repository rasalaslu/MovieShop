using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    [Table("Review")]
    public class Review
    {
        public int MovieId { get; set; }
        public int UserId { get; set; }
        [Column(TypeName = "decimal(3, 2)")]
        public decimal Rating { get; set; }
        [MaxLength(4096)]
        public string ReviewText { get; set; }

        public Movie Movie { get; set; }
        public User User { get; set; }
    }
}
