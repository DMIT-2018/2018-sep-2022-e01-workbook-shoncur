<Query Kind="Statements">
  <Connection>
    <ID>79eda3e0-30e1-436a-8b28-e5ad5bf2d597</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Server>.</Server>
    <AllowDateOnlyTimeOnly>true</AllowDateOnlyTimeOnly>
    <DeferDatabasePopulation>true</DeferDatabasePopulation>
    <Database>Chinook</Database>
  </Connection>
</Query>

// Sorting

// Query syntax is much like SQL
// 		orderby field {[ascending]|descending} [,field ....]

// ascending is the default option

// Method syntax is a series of individual methods
// .OrderBy(x => x.field)
// .OrderByDescending(x => x.field)
// .ThenBy(x => x.field)
// .ThenByDescending(x => x.field)

// Find all of the album tracks for the band Queen. Order the track names by the track names alphabetically

Tracks
	.Where(x => x.Album.Artist.Name.Contains("Queen"))
	.OrderBy(x => x.Album.Title)
	.ThenBy(x => x.Name)
	.Dump();