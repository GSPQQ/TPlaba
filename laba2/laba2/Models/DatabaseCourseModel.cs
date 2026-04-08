using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace laba2.Models
{
    // Модель для предметной области "Краеведческий музей"
    public class DatabaseCourseModel
    {
        [DisplayName("Идентификатор")]
        public int Id { get; set; }

        [DisplayName("Название музея")]
        public string Name { get; set; } = string.Empty;

        [DisplayName("Главный хранитель")]
        public string Curator { get; set; } = string.Empty;

        [DisplayName("Дата основания")]
        [DataType(DataType.Date)]
        public DateTime Established { get; set; }

        [DisplayName("Местоположение")]
        public string Location { get; set; } = string.Empty;

        [DisplayName("Стоимость входного билета")]
        [DataType(DataType.Currency)]
        public decimal TicketPrice { get; set; }

        [DisplayName("Доступны экскурсии")]
        public bool IsGuidedToursAvailable { get; set; }

        [DisplayName("Часы работы")]
        public string OpeningHours { get; set; } = string.Empty;

        [DisplayName("Размер коллекции (экспонатов)")]
        public int CollectionSize { get; set; }

        [DisplayName("Описание")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; } = string.Empty;

        [DisplayName("Контактный телефон")]
        [DataType(DataType.PhoneNumber)]
        public string ContactPhone { get; set; } = string.Empty;

        [DisplayName("Веб-сайт")]
        [DataType(DataType.Url)]
        public string Website { get; set; } = string.Empty;
    }
}
