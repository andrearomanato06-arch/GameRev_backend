namespace GameRev.Models.Entities;

using System;
using System.Collections.Generic;

public class Videogame
{
    public long Id {get;set;}

    public string Title {get;set;}

    public string Description {get;set;}

    public string CoverImage {get;set;}

    public int Objectives {get;set;}

    public DateOnly ReleaseDate {get;set;}

    public bool Released {get;set;} = true;

    public List <Platform> Platforms {get;set;} = [];

    public List<Review> Reviews {get;set;} = [];

    public long? AuthorId {get;set;}
    public Author Author {get;set;}

    public List<VideogamePlatform> VideogamePlatforms {get;set;} = [];
}