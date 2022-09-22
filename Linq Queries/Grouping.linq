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

// Grouping

// When you create a group it builds two components
// a) Key component (deciding criteria value(s)) defining the group
//		reference this component using the groupname.Key[.propertyname]
//			1 value for key: groupname.Key
//			n values for key: groupname.Key.propertyname
// (property < - > field < - > attribute < - > value)
// b) data of the group (raw instances of the collection)

// Ways to group
// a) by a single column (field, attribute, property) groupname.Key
// b) by a set of columns (anonymous dataset)		  groupname.Key.property
// c) by using an entity (entity name/nav property)	  groupname.Key.property

// Concept processing
// Start with a "pile" of data (original collection prior to grouping)
// Specify the grouping proeprtiers
// Result of the group operation will be to "place the data small piles
//		The piles are dependant on the grouping property(ies) value(s)
//		The individual instances are the data in the smaller piles
//		The entire individual instance of the original collection is place in the smaller pile
//		manipulate each of the "smaller piles useing yo you linq commands

// Grouping is different from ordering
// Ordering is the final resequencing of a collection for display
// Grouping re-organizes a collection into separate, usually smaller
//		collections for further processing (ie aggregates)

// Grouping is an excellent way to organize your ddata especially if you need
//		to process data on a property that is "NOT" a relative Key
//		such as a foreign  key which forms a "natural" group using the navigational properties

// Display albums by ReleaseYear
//		this request does NOT need grouping
//		this request is an ordering of output : OrderBy
// 		this ordering affects only the display

//Albums
//	.OrderBy(a => a.ReleaseYear)
	
// Display albums grouped by ReleaseYear
//		explicit request to breakup the display into desired "piles"
//Albums
//	.GroupBy(a => a.ReleaseYear)
	
// Processing on the groups created by the Group command

// Display the number of albums produced each year
// List only the years which have more than 10 albums
//Albums
//	.GroupBy(a => a.ReleaseYear)
//	.Where(egp => egp.Count() > 10)
//	.Select(eachGroupPile => new
//	{
//		Year = eachGroupPile.Key,
//		NumOfAlbums = eachGroupPile.Count()
//	})
	
// Use a multiple set of properties to form the group
// Included a nested query to report on the small pile group

// Display albums grouped by ReleaseLabel, ReleaseYear. Display the ReleaseYear
// and number of albums. List only the years with 3 or more albums released.
// For each album display the title and release year
Albums
	.GroupBy(a => new 
	{
		a.ReleaseLabel, 
		a.ReleaseYear
	})
	.Where(egp => egp.Count() > 2)
	.Select(eachGroupPile => new
	{
		Label = eachGroupPile.Key.ReleaseLabel,
		Year = eachGroupPile.Key.ReleaseYear,
		NumOfAlbums = eachGroupPile.Count(),
		AlbumItems = eachGroupPile
						.Select(egpInstance => new
						{
							Title = egpInstance.Title,
							Artist = egpInstance.Artist.Name,
							TrackCount = egpInstance.Tracks.Count(),
							YearOfAlbum = egpInstance.ReleaseYear
						})
	})