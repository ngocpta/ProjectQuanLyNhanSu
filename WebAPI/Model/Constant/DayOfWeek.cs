using System.Collections.Generic;
using System.Linq;

namespace Model.Constant
{
  public class DayOfWeek
  {
    public const string Sun = "SUN";
    public const string Mon = "MON";
    public const string Tue = "TUE";
    public const string Wed = "WED";
    public const string Thu = "THU";
    public const string Fri = "FRI";
    public const string Sat = "SAT";

    public static string Days(string[] days)
    {
      if (days == null || !days.Any()) return "";
      var strDays = days[0];
      return days.Aggregate(strDays, (current, day) => current + "," + day);
    }
  }
}