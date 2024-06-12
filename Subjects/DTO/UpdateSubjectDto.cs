using UniVerServer.Subjects.Enums;

namespace UniVerServer.Subjects.DTO;

public class UpdateSubjectDto
{
    // subject information
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Cost { get; set; }
    public int Credits { get; set; } = 20;
    public int Year { get; set; }
    public string ColorHex { get; set; } = "#000000";
    public string Image { get; set; } = "http";
    
    public int ClassRuntime { get; set; } = 120;
    public int ClassRepitions { get; set; } = 8;
    public SubjectTypes Type { get; set; }
}