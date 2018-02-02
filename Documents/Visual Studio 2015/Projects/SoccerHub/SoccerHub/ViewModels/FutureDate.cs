using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace SoccerHub.ViewModels
{
    public class FutureDate : ValidationAttribute
    {
        /// <summary>
        /// Returns true if date is valid.
        /// </summary>
        /// <param name="value">The value of the object to validate.</param>
        /// <returns>
        /// true if the specified value is valid; otherwise, false.
        /// </returns>
        public override bool IsValid(object value)
        {

            // Check the format of the given date
            DateTime datetime;
            var isValid = DateTime.TryParseExact(Convert.ToString(value),
                "d-MM-yyyy",
                /*CultureInfo.CurrentCulture*/CultureInfo.GetCultureInfo("nl-NL"),
                DateTimeStyles.None,
                out datetime);

            // Check if it is a future date and return
            return (isValid && datetime >= DateTime.Now);
        }
    }
}