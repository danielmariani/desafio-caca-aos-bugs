using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Dima.Core.Common.CustomAttributes
{
    public class DimaPasswordAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (value is string password)
            {
                if (!Regex.IsMatch(password, @"[A-Z]"))
                {
                    return new ValidationResult("A senha deve conter pelo menos uma letra maiúscula.");
                }

                if (!Regex.IsMatch(password, @"[\W_]"))
                {
                    return new ValidationResult("A senha deve conter pelo menos um caractere especial.");
                }

                if (!Regex.IsMatch(password, @"\d"))
                {
                    return new ValidationResult("A senha deve conter pelo menos um número.");
                }

                return ValidationResult.Success ?? 
                    new ValidationResult("O campo Senha é inválido.");
            }

            return new ValidationResult("O campo Senha é inválido.");
        }
    }
}
