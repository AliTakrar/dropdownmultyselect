using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using test.Models.Context;
using test.Models.DTOS;
using test.Models.Entities;

namespace test.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;

        public HomeController(ILogger<HomeController> logger , ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var teacherList = _db.Teachers.ToList();

            return View(teacherList);
        }
        [HttpGet]
        public IActionResult AddTeacher(long? Id)
        {
            TeacherDto model = new TeacherDto(); List<long> subjectsIds = new List<long>();
            if (Id.HasValue)
            {
                //Get teacher   
                var teacher = _db.Teachers.Include("TeacherSubjects").FirstOrDefault(x => x.Id == Id.Value);
                //Get teacher subjects and add each subjectId into subjectsIds list  
                teacher.TeacherSubjects.ToList().ForEach(result => subjectsIds.Add(result.SubjectsId));

                //bind model   
                model.drpSubjects = _db.Subjects.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
                model.Id = teacher.Id;
                model.Name = teacher.Name;
                model.SubjectsIds = subjectsIds.ToArray();
            }
            else
            {
                model = new TeacherDto();
                model.drpSubjects = _db.Subjects.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();
            }

            return View(model);
        }
        [HttpPost]
        public IActionResult AddTeacher(TeacherDto model)
        {
            Teacher teacher = new Teacher();
            List<TeacherSubjects> teacherSubjects = new List<TeacherSubjects>();


            if (model.Id > 0)
            {
                //first find teacher subjects list and then remove all from db   
                teacher = _db.Teachers.Include("TeacherSubjects").FirstOrDefault(x => x.Id == model.Id);
                teacher.TeacherSubjects.ToList().ForEach(result => teacherSubjects.Add(result));
                _db.TeacherSubjects.RemoveRange(teacherSubjects);
                _db.SaveChanges();

                //Now update teacher details  
                teacher.Name = model.Name;
                if (model.SubjectsIds.Length > 0)
                {
                    teacherSubjects = new List<TeacherSubjects>();

                    foreach (var subjectid in model.SubjectsIds)
                    {
                        teacherSubjects.Add(new TeacherSubjects { SubjectsId = subjectid, TeacherId = model.Id });
                    }
                    teacher.TeacherSubjects = teacherSubjects;
                }
                _db.SaveChanges();

            }
            else
            {
                teacher.Name = model.Name;
                teacher.DateTimeInLocalTime = DateTime.Now;
                teacher.DateTimeInUTC = DateTime.UtcNow;
                if (model.SubjectsIds.Length > 0)
                {
                    foreach (var subjectid in model.SubjectsIds)
                    {
                        teacherSubjects.Add(new TeacherSubjects { SubjectsId = subjectid, TeacherId = model.Id });
                    }
                    teacher.TeacherSubjects = teacherSubjects;
                }
                _db.Teachers.Add(teacher);
                _db.SaveChanges();
            }
            return RedirectToAction("index");
        }


    }
}