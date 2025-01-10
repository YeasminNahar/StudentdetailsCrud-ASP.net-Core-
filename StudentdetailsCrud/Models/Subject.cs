using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StudentdetailsCrud.Models
{
    public class Subject
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SubjectId { get; set; }
        //[Required, Display(Name = "Subject Name")]
        public string? SubjectName { get; set; }
        public virtual IList<Addmission> Addmissions { get; set; }
    }
}
