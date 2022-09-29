**Notes**

*CQRS - Command and Query Responsibility Segregation*

![Playlist Management screenshot](PLayList&nbspManagement&nbspscreen&nbspshot.PNG)

*Query*
This is to pull data out of the database, onto the form:
```c#
public class SongItem {
  public string Song {get;set;}
  public string Album {get;set;}
  public string Artist {get;set;}
  public double Length {get;set;}
  public demical Price {get;set;}
  public int TrackID {get;set;} // This is a hidden field
}

public class PlaylistItem {
  public int TrackNumber {get;set;}
  public string Song {get;set;}
  public double Length {get;set;}
  public int TrackID {get;set;} // This is a hidden field, it will be used to delete a record
}
```
