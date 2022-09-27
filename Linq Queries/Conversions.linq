<Query Kind="Program">
  <Connection>
    <ID>3994f98f-1659-45eb-b936-a1fea3f65c1d</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Server>.</Server>
    <AllowDateOnlyTimeOnly>true</AllowDateOnlyTimeOnly>
    <DeferDatabasePopulation>true</DeferDatabasePopulation>
    <Database>Chinook</Database>
  </Connection>
</Query>

void Main()
{
	// Conversions
	// Collection we will look at are IQueryable, IEnumerable and List
	
	// Display all albums and their tracks. Display the album title, artist name, and album tracks.
	// For each track show the song name and playtime. Show only album with 25 or more tracks.
	
	List<AlbumTracks> albumlist = Albums //.ToList if you wanted it in memory
					.Where(a => a.Tracks.Count() >= 25)
					.Select(a => new AlbumTracks
					{
						AlbumTitle = a.Title,
						ArtistName = a.Artist.Name,
						Tracks = a.Tracks
									.Select(tr => new SongItem
									{
										SongName = tr.Name,
										PlayTime = tr.Milliseconds / 1000.0
									})
									.ToList()
					})
					.ToList()
					//.Dump()
					;
					
	// Using .FirstOrDefault()
	// First saw in 1517 when checked to see if a record existed in a BLL service method
	
	// Find the first album by Deep Purple
	var artistparam = "Deep Purple";
	var resultsFOD = Albums
					.Where(a => a.Artist.Name.Equals(artistparam))
					.Select(a => a)
					.OrderBy(a => a.ReleaseYear)
					.FirstOrDefault()
					//.Dump()
					;
	//if (resultsFOD != null)
	//{
	//	resultsFOD.Dump();
	//}
	//else
	//{
	//	Console.WriteLine($"No albums found for artist {artistparam}");
	//}
	
	// Distinct()
	// Remove duplicate reported lines
	
	// Get a list of customer countries
	var resultsDistinct = Customers
							.OrderBy(c => c.Country)
							.Select(c => c.Country)
							.Distinct()
							//.Dump()
							;
							
	// .Take() and .Skip()
	// In CPSC1517, when you want to use the supplied Paginator
	// the query method was to return ONLY the needed records for the display NOT the entire collection
	// a) the query was executed returning a collection of size x
	// b) obtained the total count (x) of return records
	// c) calculated the number of records to skip (pagenumber - 1) * pagesize
	// d) on the return method statement you used
	//			return variablename.Skip(rowsSkipped).Take(pagesize).ToList()
	
	// Union
	// Rules in LINQ are the same as SQL
	// Result is the same as SQL, combine separate collections into one
	// Syntax	(queryA).Union(queryB))
	// Rules:
	//	number of columns teh same
	// 	column datatypes must be the same
	//	ordering should be done as a method after the last Union
	
	var resultsUnionA = (Albums
						.Where(x => x.Tracks.Count() == 0 )
						.Select(x => new
						{
							title = x.Title,
							totalTracks = 0,
							totalCost = 0.00m,
							averageLength = 0.00d
						})
						.OrderBy(x => x.totalTracks))
						.Union(Albums
							.Where(x => x.Tracks.Count() > 0 )
							.Select(x => new
							{
								title = x.Title,
								totalTracks = x.Tracks.Count(),
								totalCost = x.Tracks.Sum(tr => tr.UnitPrice),
								averageLength = x.Tracks.Average(tr => tr.Milliseconds)
							})	
						.OrderBy(x => x.totalTracks))
						.Dump()
						;
}

// You can define other methods, fields, classes and namespaces here

public class SongItem
{
	public string SongName{get;set;}
	public double PlayTime{get;set;}
}

public class AlbumTracks
{
	public string AlbumTitle{get;set;}
	public string ArtistName{get;set;}
	public List<SongItem> Tracks{get;set;}
}