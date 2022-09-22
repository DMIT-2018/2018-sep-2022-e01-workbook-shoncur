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

// List all albums by release label
// Any album with no label should be indicated as Unknown
// List Title, Label, Artist NAme

// Understand the problem
// Collection: Albums
// Selective data: Anonymous data set
// Label (nullable): either Unknown or Label Name *****
// OrderBy ReleaseLabel

// Design
// Albums
// Select (new{})
// Fields: Title
//		   Label - Ternary Operator (condition/s ? true value : false value)
//		   Artist.Name

Albums
	.Select (x => new
	{
		Title = x.Title,
		Label = x.ReleaseLabel == null ? "Unknown" : x.ReleaseLabel,
		ArtistName = x.Artist.Name
	})
	.OrderBy (x => x.Label);
	//.Dump()
	
// List all albums showing the Title, Artist Name, Year and decade of release using
// oldies, 70s, 80s, 90s, or modern
// Order by decade

Albums
	.Select (x => new
	{
		Title = x.Title,
		ArtistName = x.Artist.Name,
		Year = x.ReleaseYear,
		Decade = x.ReleaseYear < 1970 ? "Oldies" :
				 x.ReleaseYear < 1980 ? "70s" : 
				 x.ReleaseYear < 1990 ? "80s" : 
				 x.ReleaseYear < 2000 ? "90s" : 
				 "Modern"
	})
	.OrderBy(x => x.Year)
	.Dump();