using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace ChatBase.Rules {
    public class IPRule : ValidationRule {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo) {
            string ipRegex = "(25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9]?[0-9])\\.(25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9]?[0-9])\\.(25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9]?[0-9])\\.(25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9]?[0-9])";

            ValidationResult result;
            Regex regex = new Regex(ipRegex);

            bool isIPAddress = regex.IsMatch(value as string);

            if (isIPAddress) {
                result = new ValidationResult(true, null);
            }
            else {
                result = new ValidationResult(false, null);
            }

            return result;
        }
    }
}
