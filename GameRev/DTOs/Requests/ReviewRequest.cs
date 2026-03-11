namespace GameRev.DTOs.Requests;

public class ReviewRequest
{
    public double? Rating {get;set;} = null;

    public string? Description {get;set;} = null;

    public DateTime ReviewDate {get;set;} = DateTime.Now;

    public long VideogameId {get;set;}

    public long UserId {get;set;}
}

public class UpdateReviewRequest
{
    public long Id {get;set;}

    public double? Rating {get;set;} = null;

    public string? Description {get;set;} = null;

    public DateTime? ReviewDate {get;set;} = DateTime.Now;

    public long? VideogameId {get;set;}

    public long? UserId {get;set;}

}