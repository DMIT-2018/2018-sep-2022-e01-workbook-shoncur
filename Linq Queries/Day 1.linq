<Query Kind="Expression">
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

Albums

// Query syntax to list all records in an entity (table, collection)
from arowoncollection in Albums
select arowoncollection

// Method syntax to list all records in an entity
Albums
	.Select (arowoncollection => arowoncollection)
	
// Where
// Filter method
// The conditions are setup as you would in C#
// Beware that Linqpad may NOT like some C# syntax (DateTime)
// Beware that Linq is converted to Sql which may not like certain C# syntax because Sql could not convert

// Syntax
// Notice that the method syntax makes use of the Lambda expressions
// Lambdas are common when performing Linq with the method syntax
// .Where(lambda expression)
// .Where(x => condition [logical operator]
// .Where(x => Bytes > 350000)

// Method syntax
Tracks
	.Where(x => x.Bytes > 1000000000)
	
// Query syntax
from x in Tracks
where x.Bytes > 1000000000
select x

// Find all the albums of the artist Queen
// Concerns: the artist name is in another table, in an sql Select you would be using an inner Join
//												  in Linq you do NOT need to specify your inner Join
// 												  instead, use the "navigational properties" of your entity to generate the relationship
Albums
	.Where(a => a.Artist.Name.Contains("Queen"))