namespace GameRev.DTOs.Requests;

public class PlatformRequest
{
    public string Name {get;set;}
}

public class UpdatePlatformRequest
{
    public long Id {get;set;}
    public string Name {get;set;}
}