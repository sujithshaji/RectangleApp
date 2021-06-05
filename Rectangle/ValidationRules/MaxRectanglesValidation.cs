#region Namespace
using System.Globalization;
using System.Windows.Controls;
#endregion

namespace RectangleApp
{
    #region MaxRectanglesValidation Class
    /// <summary>
    /// Validation class for Number of rectangles input value
    /// </summary>
    public class MaxRectanglesValidation : ValidationRule
    {
        /// <summary>
        /// Validate method
        /// </summary>
        /// <param name="value"></param>
        /// <param name="cultureInfo"></param>
        /// <returns></returns>
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            int size;
            //Check whether the input value is valid number
            if (int.TryParse(value.ToString(), out size))
            {
                //Check whether the input value within the value range
                if (size < 0 || size > ApplicationConstants.MaxRectangles)
                {
                    return new ValidationResult(false, $"Valid value between 0 - {ApplicationConstants.MaxRectangles}");
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
