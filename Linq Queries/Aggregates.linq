<Query Kind="Expression">
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

// Aggregates
// .Count() counts the number of instances in the collection
// .Sum(x => ...) sums (totals) a numeric field
// .Min(x => ...) finds the minimum value of a collection for a field
// .Max(x => ...) finds the maximum value of a collection for a field
// .Average(x => ...) finds the average value of a numeric field

// IMPORTANT!!!
// Aggregates work ONLY on a collection of values
// Aggregates DO NOT work on a single row

// .Sum, .Min, .Max and .Average must have at least 1 record in their collection
// .Sum and .Average fields must NOT be null

// Syntax: method
// CollectionSet.Aggregate(x => expression)
// CollectionSet.Select(...).aggregate()
// CollectionSet.Count()
// .Count() does not contain an expression

// For Sum, Min, Max, and Average: the result is a single value

// You can use multiple aggregates on a single column
//		.Sum(x => expression).Min(x => expression)


// Find the average playing time (length) of tracks in our music collection

// Thought process:
// Average is an aggregate
// What is the collection? The Tracks table is a collection
// What is the expression? Milliseconds

// Tracks.Average(x => x.Milliseconds) // each x has multiple fields
// Tracks.Select(x => x.Milliseconds).Average() // a single list of numbers
// Tracks.Average() // aborts because to no specific field was referred to on the track record


// List all albums of the 60s showing the Title, Artist, and various aggregates for albums
// containing tracks

// For each album show the number of tracks, the total price of all tracks and the
// average playing length of the album tracks

// Thought process:
// Start at Albums
// Can I get the artist name (.Artist)
// Can I get a collection of tracks for an album (x.Tracks)
// Can I get the number of tracks in the collection (.Count())
// Can I get the total price of the tracks (.Sum())
// Can I get the average of the play length (.Average())

Albums
	.Where(x => x.Tracks.Count() > 0 && (x.ReleaseYear > 1959 && x.ReleaseYear < 1970))
	.Select(x => new
	{
		Title = x.Title,
		Artist = x.Artist.Name,
		NumberOfTracks = x.Tracks.Count(),
		TotalPrice = x.Tracks.Sum(tr => tr.UnitPrice),
		AverageTrackLength = x.Tracks.Select(tr => tr.Milliseconds).Average()
	})