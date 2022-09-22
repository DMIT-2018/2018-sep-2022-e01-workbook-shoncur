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

// This environment expects the use of C# statement grammar
// the results of a query is NOT automatically displayed as is, to display the results you need to .Dump() the variable

var qsyntaxlist = from arowoncollection in Albums
					select arowoncollection;
//qsyntaxlist.Dump();

var msyntaxlist = Albums
					.Select(arowoncollection => arowoncollection);
//msyntaxlist.Dump();

var QueenAlbums = Albums
					.Where(a => a.Artist.Name.Contains("Queen"))
					.Dump();