using System.ComponentModel.DataAnnotations;

namespace Extensions.Base.ValidationAttributes
{
    public class PersonalCodeAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            var personalCode = value as string;

            if (String.IsNullOrEmpty(personalCode))
                return false;

            int controlNumber;
            try
            {
                controlNumber = int.Parse(personalCode[10].ToString());
            }
            catch
            {
                return false;
            }

            var multipliers = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 1 };
            var multipliers2 = new int[] { 3, 4, 5, 6, 7, 8, 9, 1, 2, 3 };

            var sum = 0;
            for (var i = 0; i < 10; i++)
                sum += int.Parse(personalCode[i].ToString()) * multipliers[i];

            var rem = sum % 11;

            if (rem < 10) return rem == controlNumber;
            if (rem == 10)
            {
                sum = 0;
                for (var i = 0; i < 10; i++)
                    sum += int.Parse(personalCode[i].ToString()) * multipliers2[i];

                rem = sum % 11;

                return rem == controlNumber;
            }

            return false;
        }
    }
}
