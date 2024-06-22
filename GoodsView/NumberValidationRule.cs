using System.Globalization;
using System.Windows.Controls;

namespace GoodsView
{
    public class NumberValidationRule : ValidationRule
    {
        public int Min { get; set; }

        public int Max { get; set; }
        
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            int number;

            try
            {
                number = int.Parse((string)value);
            }
            catch
            {
                return new ValidationResult(false, "Недопустимые символы");
            }

            if (number < Min || number > Max)
            {
                return new ValidationResult(false, $"Значение должно входить в диапазон от {Min} до {Max}.");
            }

            return new ValidationResult(true, null);
        }
    }
}
