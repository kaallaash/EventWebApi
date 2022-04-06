using System.ComponentModel.DataAnnotations;

namespace EventWebAPI.Validators
{
    public class MinimumValueAttribute : ValidationAttribute
    {
        private static int MinimunValue { get; set; }

        public MinimumValueAttribute(int minimunValue)
        {
            MinimunValue = minimunValue;
        }

        public override bool IsValid(object value)
        {
            if (value is int && Convert.ToInt32(value) >= MinimunValue)
            {
                return true;
            }
            return false;
        }
    }
}
