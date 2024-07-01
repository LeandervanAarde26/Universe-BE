using UniVerServer.Users.DTO;

namespace UniVerServer.Subjects.DTO;

public class GetSingleSubjectDto
{
    public Guid Id { get; set; }
    public string Identifier { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Cost { get; set; }
    public int Credits { get; set; } 
    public int Year { get; set; }
    //Extras (nice to have)
    public string ColorHex { get; set; }
    public string Image { get; set; } 
    // running information
    public bool Active { get; set; }
    public int ClassRuntime { get; set; } 
    public int ClassRepitions { get; set; }
    public int ClassDayIntervals { get; set; } = 7;
    public string Type { get; set; }
    public string DateCreated { get; set; }
    public LecturerInformation Lecturer { get; set; }
}
