using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Globalization;

namespace CWork
{
    public static class BoService
    {

        public static string GetLogUrl()
        {
            var year = DateTime.Now.Year;
            var month = DateTime.Now.Month.ToString("00");
            var day = DateTime.Now.Month.ToString("00");
            string monthName = DateTime.Now.ToString("MMMM", CultureInfo.InvariantCulture);
            var Url = $"Logs/{year}/{month}/{monthName} {day} Logs.txt";
            return Url;
        }
    }
}
