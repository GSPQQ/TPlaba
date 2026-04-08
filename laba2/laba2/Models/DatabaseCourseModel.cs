using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace laba2.Models
{
    public class DatabaseCourseModel
    {
        [DisplayName("Идентификатор")]
        public int Id { get; set; }

        [DisplayName("Название курса")]
        public string Title { get; set; } = string.Empty;

        [DisplayName("Преподаватель")]
        public string Instructor { get; set; } = string.Empty;

        [DisplayName("Количество")]
        public int Credits { get; set; }

        [DisplayName("Дата начала")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DisplayName("Стоимость обучения")]
        public decimal TuitionFee { get; set; }

        [DisplayName("Требуется экзамен")]
        public bool IsExamRequired { get; set; }

        [DisplayName("Семестр")]
        public string Semester { get; set; } = string.Empty;
    }
}
