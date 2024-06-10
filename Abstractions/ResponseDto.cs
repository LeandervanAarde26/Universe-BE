namespace UniVerServer.Abstractions;

public record ResponseDto(Guid Id, string ActionMessage, Enums.StatusCodes StatusCode);