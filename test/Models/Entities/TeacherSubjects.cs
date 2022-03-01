using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace test.Models.Entities
{
    public class TeacherSubjects
    {
        [Key]
        public long Id { get; set; }
        public long TeacherId { get; set; }
        public long SubjectsId { get; set; }

        #region Relations

        [ForeignKey("TeacherId")]
        public virtual Teacher? Teacher { get; set; }
        [ForeignKey("SubjectsId")]
        public virtual Subjects? Subjects { get; set; }

        #endregion
    }
}
