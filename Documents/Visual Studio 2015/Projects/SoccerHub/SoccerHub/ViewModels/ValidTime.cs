using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace SoccerHub.ViewModels
{
    public class ValidTime : ValidationAttribute
    {
        /// <summary>
        /// Returns true if time is valid.
        /// </summary>
        /// <param name="value">The value of the object to validate.</param>
        /// <returns>
        /// true if the specified value is valid; otherwise, false.
        /// </returns>
        public override bool IsValid(object value)
        {

            // Check the format of the given time
            DateTime datetime;
            var isValid = DateTime.TryParseExact(Convert.ToString(value),
                "HH:mm",
                /*CultureInfo.CurrentCulture*/CultureInfo.GetCultureInfo("nl-NL"),
                DateTimeStyles.None,
                out datetime);

            // Check if it is a future time and return
            return (isValid && datetime >= DateTime.Now);
        }
    }
}