using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StudentdetailsCrud.Models
{
    public class Student
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StudentId { get; set; }
        //[Required, Display(Name = "Student Name")]
        public string? StudentName { get; set; }
        //[Required]
        public int Age { get; set; }
        //[Required]
        public DateTime DateOfBirth { get; set; }
        //[Required]
        public bool Addmited { get; set; }
        //[Required]
        public string? image { get; set; }
        public virtual IList<Addmission>? Addmissions { get; set; }
    }
}
