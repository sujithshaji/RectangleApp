#region Namespace
using System.Globalization;
using System.Windows.Controls;
#endregion

namespace RectangleApp
{
    #region MaxSizeValidation Class
    /// <summary>
    /// Validation class for rectangle's length/width input value
    /// </summary>
    public class MaxSizeValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            int size;
            //Check whether the input value is valid number
            if (int.TryParse(value.ToString(), out size))
            {
                //Check whether the input value within the value range
                if (size < 0 || size > ApplicationConstants.MaxSize)
                {
                    return new ValidationResult(false, $"Valid value between 0 - {ApplicationConstants.MaxSize}");
                }
            }
            else
            {
                return new ValidationResult(false, "Illegal charactors!");
            }
            return ValidationResult.ValidResult;
        }
    }
    #endregion
}
