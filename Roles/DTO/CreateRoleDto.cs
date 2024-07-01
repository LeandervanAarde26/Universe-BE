namespace UniVerServer.Roles.DTO;

public class CreateRoleDto
{
   
    public string Name { get; set; }
    public bool CanAccess { get; set; }
    public bool PaidRole { get; set; }
    public decimal HourlyRate { get; set; }
    public string Identifier { get; private set; }
    public int LastIndex { get; private set; }

    public CreateRoleDto(string name, bool canAccess, bool paidRole, decimal hourlyRate, int lastIndex)
    {
        Name = name;
        CanAccess = canAccess;
        PaidRole = paidRole;
        HourlyRate = hourlyRate;
        LastIndex = lastIndex;
        Identifier = $"R{LastIndex + 1}";
    }
}