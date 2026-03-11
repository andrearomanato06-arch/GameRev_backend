namespace GameRev.DTOs.Filters;

public class VideogameSearchFilter
{
    public string? Title {get;set;}

    public string? Platform {get;set;}

    public int? Year {get;set;}

    public int? Objectives {get;set;}

    public double? RatingStart {get;set;} = null;

    public double? RatingEnd {get;set;} = null;
}