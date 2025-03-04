using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Extenstions
{
    public static class DateTimeExtenstions
    {
        public static int CalcAge(this DateOnly dob)
        {
            var today = DateOnly.FromDateTime(DateTime.UtcNow);
            int age = today.Year - dob.Year;

            // Adjust for birthdays that haven't occurred yet this year
            if (today < new DateOnly(today.Year, dob.Month, Math.Min(dob.Day, DateTime.DaysInMonth(today.Year, dob.Month))))
                age--;

            return age;
        }
    }
}
