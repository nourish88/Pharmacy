using Microsoft.AspNetCore.Mvc;

namespace Pharmacy.Shared.Exceptions.HttpProblemDetails;

public class BusinessProblemDetails:ProblemDetails
{
    public BusinessProblemDetails(string detail)
    {
        Title = "Rule Violation";
        Detail = detail;
        Status = StatusCodes.Status400BadRequest;
        Type = "https://example.com/probs/business";
    }
}
