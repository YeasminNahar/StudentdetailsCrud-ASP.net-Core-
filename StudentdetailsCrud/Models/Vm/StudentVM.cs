using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StudentdetailsCrud.Models.Vm
{
    public class StudentVM
    {

        public StudentVM()
        {
            this.Addmissions = new List<Addmission>();
        }
        public int StudentId { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        public string? StudentName { get; set; }
        [Required(ErrorMessage = "Age is required.")]
        public int Age { get; set; }
        [Required(ErrorMessage = "Date of Birth is required.")]
        public DateTime DateOfBirth { get; set; }

        [NotMapped]
        public IFormFile? imageFile { get; set; }
        public string? image { get; set; }

        public bool Addmited { get; set; }
        public virtual List<Addmission>? Addmissions { get; set; }
    }
}

