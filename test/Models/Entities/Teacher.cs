using System.ComponentModel.DataAnnotations;

namespace test.Models.Entities
{
    public class Teacher
    {
        [Key]
        public long Id { get; set; }
        [MaxLength (50)]
        public string? Name { get; set; }
        public DateTimeOffset DateTimeInLocalTime { get; set; }
        public DateTimeOffset DateTimeInUTC { get; set; }

        public virtual ICollection<TeacherSubjects>? TeacherSubjects { get; set; }

    }
}
