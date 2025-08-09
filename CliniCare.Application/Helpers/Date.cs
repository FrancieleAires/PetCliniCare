using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CliniCare.Application.Helpers
{
    public class Date
    {
        public static int CalculateAge(DateTime dateOfBirth)
        {
            var today = DateTime.UtcNow.Date;
            var age = today.Year - dateOfBirth.Year;
            if (dateOfBirth.Date > today.AddYears(-age))
            {
                age--;
            }
            return age;
        }
    }
}
