using System.ComponentModel.DataAnnotations;

namespace test.Models.Entities
{
    public class Subjects
    {
        [Key]
        public long Id { get; set; }
        [MaxLength(50)]
        public string? Name { get; set; }

        public virtual ICollection<TeacherSubjects>? TeacherSubjects { get; set; }
    }
}
