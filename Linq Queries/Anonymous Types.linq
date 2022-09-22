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

// Using Navigational Properties and Anonymous data set (collection)

// Reference: Student Notes/Demo/eRestaurant/Linq: Query and Method Syntax

// Find all albums released in the 90's (1990-1999)
// Order the albums by ascending year and then alphabetically by album title
// Display the Year, Title, Artist Name, and Release label

// Concerns: a) not all properties of Album are to be displayed
//			 b) the order of the properties are to be displayed in a different sequence
//				then the definition of the properties on the entity Album
//			 c) the Artist Name is NOT on the Album table BUT is on the Artist table

// Solution: Use an anonymous data collection

// The anonymous data instance is defined within the Select by declaredfields (properties)
// The order of the fields on the new defined instance will be done in specifying the 
// properties of the anonymous data collection

Albums
	.Where (x => (x.ReleaseYear >= 1990 && x.ReleaseYear < 2000))
	.OrderBy (x => x.ReleaseYear)
	.ThenBy (x => x.Title)
	.Select (x => new
	{
		Year = x.ReleaseYear,
		Title = x.Title,
		ArtistName = x.Artist.Name,
		ReleaseLabel = x.ReleaseLabel
	})
	.Dump ();

// Note that each method fllows into the next one
// .Where pulls from Albums, .OrderBy pulls from .Where
// Albums -> .Where -> .OrderBy -> .ThenBy -> .Select
// You can put the Orderby after the Select, but would have to change x.ReleaseYear to Year
// 		because it would be pulling from Select, where we have instantiated Year, and are
//		no longer pulling from Where, that was utilizing x.ReleaseYear