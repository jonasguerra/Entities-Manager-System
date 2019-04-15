using System;
using System.ComponentModel.DataAnnotations;

namespace EntitiesManagerSystem.Custom_Atribute
{
    public class RequiredIfAttribute : ValidationAttribute
    {
        private string PropertyName { get; set; }
        private string ErrorMessage { get; set; }
        private bool Conditional { get; set; }

        public RequiredIfAttribute(string propertyName, bool conditional, string errorMessage)
        {
            this.PropertyName = propertyName;
            this.Conditional = conditional;
            this.ErrorMessage = errorMessage;
        }

        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            Object instance = context.ObjectInstance;
            Type type = instance.GetType();
            Object proprtyvalue = type.GetProperty(PropertyName).GetValue(instance, null);
            if (proprtyvalue.ToString() == Conditional.ToString() && value == null)
            {
                return new ValidationResult(ErrorMessage);
            }
            return ValidationResult.Success;
        }
    }
}