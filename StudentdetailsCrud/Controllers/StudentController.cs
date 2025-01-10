using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentdetailsCrud.Models;
using StudentdetailsCrud.Models.Vm;

namespace StudentdetailsCrud.Controllers
{
    public class StudentController : Controller
    {
        private readonly StudentDbContext db;
        private readonly IWebHostEnvironment en;

        public StudentController(StudentDbContext db, IWebHostEnvironment en)
        {
            this.db = db;
            this.en = en;
        }
        public IActionResult Index()
        {
            var student = db.Students?.Include(a => a.Addmissions).ThenInclude(s => s.Subject).OrderBy(s => s.StudentId).ToList();
            return View(student);
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.subject = new SelectList(db.Subjects.ToList(), "SubjectId", "SubjectName");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(StudentVM studentVm, int[] subjectId)
        {

            if (ModelState.IsValid)
            {
                var student = new Student()
                {
                    StudentName = studentVm.StudentName,
                    Age = studentVm.Age,
                    DateOfBirth = studentVm.DateOfBirth,
                    Addmited = studentVm.Addmited,
                };
                if (studentVm.imageFile != null)
                {
                    var imgName = DateTime.Now.Ticks.ToString() + Path.GetExtension(studentVm.imageFile.FileName);
                    var imgFile = en.WebRootPath + "/Images/" + imgName;
                    using (var strem = System.IO.File.Create(imgFile))
                    {
                        studentVm.imageFile.CopyTo(strem);
                    }
                    student.image = imgName;
                }
                foreach (var item in subjectId)
                {
                    var a = new Addmission()
                    {
                        Student = student,
                        StudentId = student.StudentId,
                        SubjectId = item,
                    };
                    await db.Addmissions.AddAsync(a);
                }
                //await db.Students.AddAsync(student);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(studentVm);
        }
        public async Task<IActionResult> AddSubject(int? id)
        {
            ViewBag.subject = new SelectList(await db.Subjects.ToListAsync(), "SubjectId", "SubjectName", id ?? 0);
            return PartialView("_addSubject");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.subject = new SelectList(await db.Subjects.ToListAsync(), "SubjectId", "SubjectName");
            if (id != null)
            {
                var student = await db.Students.FindAsync(id);
                var ad = db.Addmissions.Where(s => s.StudentId == student.StudentId).ToList();
                var myst = new StudentVM()
                {
                    StudentId = student.StudentId,
                    StudentName = student.StudentName,
                    Age = student.Age,
                    DateOfBirth = student.DateOfBirth,
                    image = student.image,
                    Addmited = student.Addmited
                };
                myst.Addmissions = ad;
                return View(myst);
            }
            return NotFound();
        }
        [HttpPost]

        public async Task<IActionResult> Edit(StudentVM studentVm, int[] projectid)
        {

            if (ModelState.IsValid)
            {
                var student = await db.Students.FindAsync(studentVm.StudentId);
                var ad = db.Addmissions.Where(s => s.StudentId == student.StudentId).ToList();

                student.StudentName = studentVm.StudentName;
                student.Age = studentVm.Age;

                student.Addmited = studentVm.Addmited;

                if (studentVm.imageFile != null)
                {
                    var imgName = DateTime.Now.Ticks.ToString() + Path.GetExtension(studentVm.imageFile.FileName);
                    var imgFile = en.WebRootPath + "/Images/" + imgName;
                    using (var strem = System.IO.File.Create(imgFile))
                    {
                        studentVm.imageFile.CopyTo(strem);
                    }
                    student.image = imgName;
                }
                else
                {
                    student.image = student.image;
                }
                db.Addmissions.RemoveRange(ad);
                foreach (var item in projectid)
                {
                    var a = new Addmission()
                    {
                        Student = student,
                        StudentId = student.StudentId,
                        SubjectId = item,
                    };
                    await db.Addmissions.AddAsync(a);
                }
                //await db.Students.AddAsync(student);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(studentVm);
        }
        //public async Task<IActionResult> Edit(StudentVM studentVm, int[] subjectId)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var student = await db.Students.FindAsync(studentVm.StudentId);
        //        if (student == null)
        //        {
        //            return NotFound();
        //        }

        //        // Update student properties
        //        student.StudentName = studentVm.StudentName;
        //        student.Age = studentVm.Age;
        //        student.DateOfBirth = studentVm.DateOfBirth;
        //        student.Addmited = studentVm.Addmited;

        //        // Handle image update
        //        if (studentVm.imageFile != null)
        //        {
        //            var imgName = DateTime.Now.Ticks + Path.GetExtension(studentVm.imageFile.FileName);
        //            var imgPath = Path.Combine(en.WebRootPath, "Images", imgName);
        //            using (var stream = new FileStream(imgPath, FileMode.Create))
        //            {
        //                await studentVm.imageFile.CopyToAsync(stream);
        //            }
        //            student.image = imgName;
        //        }

        //        // Update Addmissions
        //        var existingAdmissions = db.Addmissions.Where(a => a.StudentId == student.StudentId).ToList();
        //        db.Addmissions.RemoveRange(existingAdmissions); // Remove old Addmissions

        //        foreach (var id in subjectId)
        //        {
        //            db.Addmissions.Add(new Addmission
        //            {
        //                StudentId = student.StudentId,
        //                SubjectId = id
        //            });
        //        }

        //        await db.SaveChangesAsync(); // Save all changes
        //        return RedirectToAction(nameof(Index));
        //    }

        //    return View(studentVm);
        //}

        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                var student = await db.Students.FindAsync(id);
                var add = await db.Addmissions.Where(a => a.StudentId == student.StudentId).ToListAsync();
                if (student != null)
                {
                    db.Addmissions.RemoveRange(add);
                }
                db.Students.Remove(student);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return NotFound();
        }
    }
}

