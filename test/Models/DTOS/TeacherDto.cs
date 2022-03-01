using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace test.Models.DTOS
{
    public class TeacherDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public List<SelectListItem> drpSubjects { get; set; }

        [Display(Name = "Subjects")]
        public long[] SubjectsIds { get; set; }
    }
}
