namespace Web;

public static class PointsHelpers
{
    private static DateTime GetFirstDayOfSeason(DateTime date)
    {
        int year = date.Year;

        switch (date.Month)
        {
            case 12:
            case 1:
            case 2:
                return new DateTime(year, 12, 1);
            case 3:
            case 4:
            case 5:
                return new DateTime(year, 3, 1);
            case 6:
            case 7:
            case 8:
                return new DateTime(year, 6, 1);
            case 9:
            case 10:
            case 11:
                return new DateTime(year, 9, 1);
            default:
                throw new ArgumentOutOfRangeException(nameof(date.Month), "Invalid month value");
        }
    }
    
    private static int GetDaysInCurrentSeason(DateTime date)
    {
        // Get the first day of the current season
        DateTime firstDayOfSeason = GetFirstDayOfSeason(date);

        // Calculate the total number of days from the first day of the season until today
        int totalDays = (int)(date - firstDayOfSeason).TotalDays + 1;

        return totalDays;
    }
    
    public static int Calculate(DateTime today)
    {
        int days = GetDaysInCurrentSeason(today);
        
        var points = new List<double> { 2 };
            
        if (days > 1) {
            points.Add(4.2);
        }
        
        for (int i = 3; i <= days; i++) {
            points.Add(1 + i + 0.6 * points[i - 2] + points[i - 3]);
        }
        
        return (int)points.Sum();
    }
}