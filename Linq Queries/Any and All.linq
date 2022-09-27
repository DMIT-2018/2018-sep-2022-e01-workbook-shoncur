<Query Kind="Statements">
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

// Any and All
// These filter tests return a true or false condition
// They work at the complete collection level

// Genres.Count().Dump();
// 25

// Show genres that have tracks which are not on any playlist
Genres
	.Where(g => g.Tracks.Any(tr => tr.PlaylistTracks.Count() == 0))
	.Select(g => g)
	//.Dump()
	;
	
// Show genres that have all their tracks appearing at least once on a playlist
Genres
	.Where(g => g.Tracks.All(tr => tr.PlaylistTracks.Count() > 0))
	.Select(g => g)
	//.Dump()
	;
	
// There maybe times that using a !Any() -> All(!relationship) and !All -> Any(!relationship)

// Using All and Any in comparing 2 collections
// If your collection is NOT a complex record there is a LINQ method called .Except that can be used to solve your query

// Compare the track collection of 2 people using All and Any
// Roberto, Almeida and Michelle Brooks

var almeida = PlaylistTracks
				.Where(x => x.Playlist.UserName.Contains("Almeida"))
				.Select(x => new
				{
					song = x.Track.Name,
					genre = x.Track.Genre.Name,
					id = x.TrackId,
					artist = x.Track.Album.Artist.Name
				})
				.Distinct()
				.OrderBy(x => x.song)
				//.Dump()	110 songs
				;
				
var brooks = PlaylistTracks
				.Where(x => x.Playlist.UserName.Contains("BrooksM"))
				.Select(x => new
				{
					song = x.Track.Name,
					genre = x.Track.Genre.Name,
					id = x.TrackId,
					artist = x.Track.Album.Artist.Name
				})
				.Distinct()
				.OrderBy(x => x.song)
				//.Dump()	88 songs
				;
				
// List the tracks that BOTH Roberto and Michelle like
// Compare 3 datasets together, data in listA that is also in listB
// Assume listA is Roberto and listB is Michelle
// listA is what you wish to report from
// listB is what you wish to compare to

// What songs does Roberto like but not Michelle
// Find Any songs on Robs playlist that are not on Michelles
var c1 = almeida
			.Where(rob => !brooks.Any(mic => mic.id == rob.id))
			.OrderBy(rob => rob.song)
			//.Dump()
			;
			
// What songs do both Michelle and Roberto like
var c2 = almeida
			.Where(rob => !brooks.All(mic => mic.id != rob.id))
			.OrderBy(rob => rob.song)
			//.Dump()
			;
			
// What songs do both Michelle and Roberto like
var c3 = brooks
			.Where(mic => almeida.Any(rob => rob.id == mic.id))
			.Dump()
			;