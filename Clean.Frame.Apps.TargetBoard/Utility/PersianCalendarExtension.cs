namespace Clean.Frame.Apps.TargetBoard.Utility
{
    public class PersianCalendarExtension
    {
        public static string GetMonthName(int monthNumber)
        {
            string MonthName = "";
            switch (monthNumber)
            {
                case 1:
                    MonthName = "فروردین";
                    break;
                case 2:
                    MonthName = "اردیبهشت";
                    break;
                case 3:
                    MonthName = "خرداد";
                    break;
                case 4:
                    MonthName = "تیر";
                    break;
                case 5:
                    MonthName = "مرداد";
                    break;
                case 6:
                    MonthName = "شهریور";
                    break;
                case 7:
                    MonthName = "مهر";
                    break;
                case 8:
                    MonthName = "آبان";
                    break;
                case 9:
                    MonthName = "آذر";
                    break;
                case 10:
                    MonthName = "دی";
                    break;
                case 11:
                    MonthName = "بهمن";
                    break;
                case 12:
                    MonthName = "اسفند";
                    break;
            }
            return MonthName;
        }
        public static  List<string> PersianMonthNames()
        {
            return new List<string>() { "فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور", "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند" };
        }
    }
}
