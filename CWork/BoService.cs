using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Globalization;

namespace CWork
{
    public static class BoService
    {

        public static string GetLogUrl(string FileEnvirment,string FileName)
        {
            var year = DateTime.Now.Year;
            var month = DateTime.Now.Month.ToString("00");
            var day = DateTime.Now.Month.ToString();
            string monthName = DateTime.Now.ToString("MMMM", CultureInfo.InvariantCulture);
            var Url = $"{FileEnvirment} Logs/{year}/{month}/{monthName} {day} {FileName}.txt";
            return Url;
        }
    }
}
