using UniVerServer.Abstractions;
using UniVerServer.FinancialTransactions.Enums;

namespace UniVerServer.FinancialTransactions.Models;

public class FinancialTransaction : BaseModel
{
    public Users.Models.Users User { get; set; }
    public Guid UserId { get; set; }
    public TransactionType TransactionType { get; set; }
    public DateTime TransactionDate { get; set; }
    public DateTime DueDate { get; set; }
}
 // Payments need to happen on the 25th of each month 
 // Calculations need to run in a CRON JOB.
 // TABLE NEEDS TO BE ADDED for closing off the month.