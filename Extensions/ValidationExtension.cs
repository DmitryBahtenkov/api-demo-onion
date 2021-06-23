using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace WebApplication.Extensions
{
    public static class ValidationExtension
    {
        public static string Validate(this object o)
        {
            var validationContext = new ValidationContext(o);
            var results = new List<ValidationResult>();

            if (!Validator.TryValidateObject(o, validationContext, results, true))
            {
                return string.Join(", ", results.Select(x => x.ErrorMessage));
            }

            return string.Empty;
        }
    }
}