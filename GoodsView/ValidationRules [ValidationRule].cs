using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace GoodsView
{
    public class NumberValidationRule : ValidationRule
    {
        private int min;
        public int Min
        {
            get => min;
            set => min = value;
        }

        private int max;
        public int Max
        {
            get => max;
            set => max = value;
        }

        public NumberValidationRule(int max, int min)
        {
            Max = max;
            Min = min;
        }
        public NumberValidationRule()
        {

        }
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

            if ((number < Min) || (number > Max))
            {
                return new ValidationResult(false, $"Значение должно входить в диапазон от {Min} до {Max}.");
            }
            else
                return new ValidationResult(true, null);

        }
    }
}
