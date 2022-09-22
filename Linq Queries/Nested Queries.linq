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
	// Nested queries
	// Sometimes referred to as subqueries
	// Simply put: it is a query within a query [....]
	
	// List all sales support employees showing their
	// fullname (last, first), title, and phone
	// For each employee, show a list of customers they support
	// Show the customer fullname (last, first), city and state
	
	// Employee 1, Title, Phone
	// 		customer 2000, city, state
	// 		customer 2109, city, state
	// 		customer 5000, city, state
	// Employee 2, Title, Phone
	// 		customer 301, city, state
	// There are 2 separate lists that need to be within one final dataset collection
	// List of employees
	// List of customers
	// Concern: the lists are intermixed!!
	
	// C# POV in a class definition
	// First: this is a composite class
	// 		the class is describing an employee
	// 		each instance of the employee will have a list of employee customers
	
	// Class EmployeeList
	//		FullName (property)
	//		Title (property)
	//	 	Phone (property)
	// 		Collection of Customers (property: List<T>)
	
	// Class CustomerList
	// 		FullName (property)
	// 		City (property)
	// 		State (property)
	
	var results = Employees
					.Where(e => e.Title.Contains("Sales Support"))
					.Select(e => new EmployeeItem
					{
						FullName = e.LastName + ", " + e.FirstName,
						Title = e.Title,
						Phone = e.Phone,
						CustomerList = e.SupportRepCustomers
										.Select(c => new CustomerItem
										{
											FullName = c.LastName + ", " + c.FirstName,
											City = c.City,
											State = c.State
										})
					});
	results.Dump();
	
	// List all albums that are from 1990
	// Display the album title and artist name
	// For each album, display it's tracks and genres
	
	var albumResults = Albums
						.Where(a => a.ReleaseYear == 1990)
						.Select(a => new
						{
							Title = a.Title,
							Artist = a.Artist.Name,
							Tracks = a.Tracks
										.Select(t => new
										{
											Name = t.Name,
											Genre = t.Genre.Name
										})
						});
	albumResults.Dump();
}

public class CustomerItem
{
	public string FullName {get; set;}
	public string City {get; set;}
	public string State {get; set;}
}

public class EmployeeItem
{
	public string FullName {get; set;}
	public string Title {get; set;}
	public string Phone {get; set;}
	public IEnumerable<CustomerItem> CustomerList {get; set;}
}

// You can define other methods, fields, classes and namespaces here