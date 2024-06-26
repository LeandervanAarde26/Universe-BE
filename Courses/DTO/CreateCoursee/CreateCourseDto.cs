namespace UniVerServer.Courses.DTO;

public class CreateCourseDto
{
    public Guid SubjectId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; private set; }
    public bool Active { get; set; } = false;
    public bool AcceptingStudents { get; set; } = false;

    public void CalculateEndDate(int classInterval, int classRepitions, DateTime startDate)
    {
        EndDate = startDate.AddDays(classInterval * classRepitions);
    }
    
    // We have a course that has a 7 day interval -> runs once a week
    // We have 8 repitions 
    // How many weeks is this going to be running? 
     // class repitions x interval = 
     
     // Datetime date = currentDate;
     // int Interval = 7; -> 7 days between classes;
     // int repitions = 9 -> Amount of classes; 
    // (1 class (x)every 7 days) (x) 9 times = 63;
    //  which means that we need to add 63 days onto the starting date to calculate the date that it ends.
}