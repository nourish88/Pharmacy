namespace Pharmacy.Application.Features.Groups.Queries;

public class GetGroupsResponse
{
        

    public int TotalRecords { get; set; }
    public List<GroupViewModel> Groups { get; set; } = new();

}
public class GetAllGroupsResponse
{
    public List<AllGroupViewModel> Groups { get; set; } = new();

}
public sealed record GroupViewModel(int Value, string? Label);
public sealed record AllGroupViewModel(int Id, string? Name);