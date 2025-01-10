using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StudentdetailsCrud.Models
{
    public class Addmission
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AddmId { get; set; }
        //[Required, Display(Name = "Student Name")]
        [ForeignKey("Student")]
        public int StudentId { get; set; }
        [ForeignKey("Subject")]
        public int SubjectId { get; set; }
        public virtual Student Student { get; set; }
        public virtual Subject Subject { get; set; }
    }
}
