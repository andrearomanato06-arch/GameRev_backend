namespace GameRev.Models.Entities;

using System;
using System.Collections.Generic;
using GameRev.DTOs.Requests;

public class Author
{
    public long Id {get;set;}

    public string Name {get;set;}
    
    public List<Videogame> Videogames {get;set;} = [] ;
}