namespace GameRev.Models.Entities;

public class VideogamePlatform
{
    public long VideogameId {get;set;}
    public Videogame Videogame {get;set;}

    public long PlatformId {get;set;}
    public Platform Platform {get;set;}
}