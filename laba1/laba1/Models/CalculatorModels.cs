using System.ComponentModel.DataAnnotations;

namespace laba1.Models
{
    public enum CalculatorOperation
    {
        Add,
        Subtract,
        Multiply,
        Divide
    }

    public class CalculatorModel
    {
        [Display(Name = "Первый операнд (int)")]
        public int? Operand1 { get; set; }

        [Display(Name = "Второй операнд (decimal)")]
        public decimal? Operand2 { get; set; }

        public CalculatorOperation Operation { get; set; }

        [Display(Name = "Результат")]
        public decimal Result { get; set; }
    }
}

