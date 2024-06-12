using UniVerServer.Subjects.Enums;

namespace UniVerServer.Subjects.DTO;

public class GetSubjectDto
{
    public Guid Id { get; set; }
    public string Identifier { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Cost { get; set; }
    public int Credits { get; set; } 
    public int Year { get; set; }
    public string LecturerName { get; set; }
    public Guid LecturerId { get; set; }
    //Extras (nice to have)
    public string ColorHex { get; set; }
    public string Image { get; set; } 
    // running information
    public bool Active { get; set; } = false;
    public int ClassRuntime { get; set; } = 120;
    public int ClassRepitions { get; set; } = 8;
    public string Type { get; set; }
    public string DateCreated { get; set; }
}