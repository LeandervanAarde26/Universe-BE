using UniVerServer.Courses.DTO;

namespace UniVerServer.Courses.Extensions;

public static class DateExtensions
{
    public static DateTime CalculateEndDate(this DateTime startDate, int classInterval, int classRepitions)
    {
        return startDate.AddDays(classInterval * classRepitions);
    }
}