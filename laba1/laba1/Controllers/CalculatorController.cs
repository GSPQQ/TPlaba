using laba1.Models;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace laba1.Controllers
{
    public class CalculatorController : Controller
    {
        private const decimal ExpectedResult = 10m;

        private const string SessionOperand1 = "Calc.Operand1";
        private const string SessionOperand2 = "Calc.Operand2";
        private const string SessionOperation = "Calc.Operation";
        private const string SessionResult = "Calc.Result";

        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.ExpectedResult = ExpectedResult;
            return View(new CalculatorModel { Operation = CalculatorOperation.Add });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(CalculatorModel model, string? action)
        {
            ViewBag.ExpectedResult = ExpectedResult;

            if (string.Equals(action, "clear", StringComparison.OrdinalIgnoreCase))
            {
                model.Operand1 = null;
                model.Operand2 = null;
                model.Result = 0m;
                model.Operation = CalculatorOperation.Add;
                ModelState.Clear();

                HttpContext.Session.Remove(SessionOperand1);
                HttpContext.Session.Remove(SessionOperand2);
                HttpContext.Session.Remove(SessionOperation);
                HttpContext.Session.Remove(SessionResult);

                return View(model);
            }

            if (model.Operand1 == null)
            {
                ModelState.AddModelError(nameof(model.Operand1), "Поле первого операнда обязательно для заполнения.");
            }

            if (model.Operand2 == null)
            {
                ModelState.AddModelError(nameof(model.Operand2), "Поле второго операнда обязательно для заполнения.");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            decimal a = model.Operand1.GetValueOrDefault();
            decimal b = model.Operand2.GetValueOrDefault();

            switch (model.Operation)
            {
                case CalculatorOperation.Add:
                    model.Result = a + b;
                    break;
                case CalculatorOperation.Subtract:
                    model.Result = a - b;
                    break;
                case CalculatorOperation.Multiply:
                    model.Result = a * b;
                    break;
                case CalculatorOperation.Divide:
                    if (b == 0m)
                    {
                        ModelState.AddModelError(nameof(model.Operand2), "Деление на ноль невозможно.");
                        return View(model);
                    }
                    model.Result = a / b;
                    break;
                default:
                    ModelState.AddModelError(nameof(model.Operation), "Неизвестная операция.");
                    return View(model);
            }

            ModelState.Remove(nameof(model.Result));

            HttpContext.Session.SetString(SessionOperand1, model.Operand1.Value.ToString(CultureInfo.InvariantCulture));
            HttpContext.Session.SetString(SessionOperand2, model.Operand2.Value.ToString(CultureInfo.InvariantCulture));
            HttpContext.Session.SetString(SessionOperation, model.Operation.ToString());
            HttpContext.Session.SetString(SessionResult, model.Result.ToString(CultureInfo.InvariantCulture));

            return View(model);
        }

        [HttpGet]
        public IActionResult Details()
        {
            ViewBag.ExpectedResult = ExpectedResult;

            var model = new CalculatorModel();

            if (TryGetInt32(HttpContext.Session.GetString(SessionOperand1), out var op1))
            {
                model.Operand1 = op1;
            }

            if (TryGetDecimal(HttpContext.Session.GetString(SessionOperand2), out var op2))
            {
                model.Operand2 = op2;
            }

            var opStr = HttpContext.Session.GetString(SessionOperation);
            if (Enum.TryParse<CalculatorOperation>(opStr, out var operation))
            {
                model.Operation = operation;
            }

            if (TryGetDecimal(HttpContext.Session.GetString(SessionResult), out var result))
            {
                model.Result = result;
            }

            return View(model);
        }

        private static bool TryGetInt32(string? value, out int result)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                result = 0;
                return false;
            }

            if (int.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out result))
            {
                return true;
            }

            if (decimal.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var dec) && dec == Math.Truncate(dec))
            {
                result = (int)dec;
                return true;
            }

            result = 0;
            return false;
        }

        private static bool TryGetDecimal(string? value, out decimal result)
        {
            return decimal.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out result);
        }
    }
}

