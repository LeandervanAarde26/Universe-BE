using System.Text.RegularExpressions;
using UniVerServer.Subjects.Enums;

namespace UniVerServer.Subjects.DTO;

public class CreateSubjectDto
{
    public string Identifier { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Cost { get; set; }
    public int Credits { get; set; } = 20;
    public Guid LecturerId { get; set; } 
    //Extras (nice to have)
    public string ColorHex { get; set; } = "#000000";
    public string Image { get; set; } = "http";
    // running information
    public bool Active { get; set; } = false;
    public int ClassRuntime { get; set; } = 120;
    public int ClassRepitions { get; set; } = 8;
    public SubjectTypes Type { get; set; }
    public int Year { get; set; }

    public void GenerateSubjectCode()
    {
        var deconstructedString = Regex.Replace(Identifier, @"[0-9\-]", string.Empty);
        char moduleOrMajor = Type == SubjectTypes.MODULE ? 
            '1' : 
            '0';
        var result = $"{deconstructedString}{Year}0{moduleOrMajor}";
        Identifier = result;
    }
}