namespace GameRev.Models.Auth;
using GameRev.Models.Entities;
public class JwtToken
{
    public long Id {get;set;}

    public string Token {get;set;}

    public DateTime IssuedAt {get;set;}

    public User? User {get;set;}
    public long? UserId {get;set;}
}