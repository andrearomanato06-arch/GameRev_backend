namespace GameRev.DTOs.Requests;

public class AuthorRequest
{
    public string Name {get;set;}
}

public class UpdateAuthor
{
    public long Id {get;set;}

    public string? Name {get;set;}
}