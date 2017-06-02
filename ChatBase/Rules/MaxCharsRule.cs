using System.Globalization;
using System.Windows.Controls;

namespace ChatBase.Rules {
    public class MaxCharsRule : ValidationRule {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo) {
            ValidationResult result;

            if ((value as string).Length == Constants.MAX_MESSAGE_SIZE) {
                result = new ValidationResult(false, null);
            }
            else {
                result = new ValidationResult(true, null);
            }

            return result;
        }
    }
}
