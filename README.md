# Notes

## CQRS - Command and Query Responsibility Segregation

![Playlist Management screenshot](PLayList&nbspManagement&nbspscreen&nbspshot.PNG)

### Query

This is to pull data out of the database, onto the form:
```c#
public class SongItem {
  public string Song {get;set;}
  public string Album {get;set;}
  public string Artist {get;set;}
  public double Length {get;set;}
  public demical Price {get;set;}
  public int TrackID {get;set;} // This is a hidden field
}

public class PlaylistItem {
  public int TrackNumber {get;set;}
  public string Song {get;set;}
  public double Length {get;set;}
  public int TrackId {get;set;} // This is a hidden field, it will be used to delete a record
}
```

### Command

This is to add data to the database, from the form:
```c#
public void AddTrack(PlaylistName, TrackId)

public void RemoveTrack(PlaylistName, List<int>)

public void MoveTrack(PlaylistName)

public class ReorgTrackItem {
  public int TrackId {get;set;}
  public int currentTrackNumber {get;set;}
  public int reorgTrackNumber {get;set;}
}
```

## Notes for Purchasing Subsystem
Pattern for Service Method
- Checked that incoming parameters had values unless nullable (ArgumentNullException)
- Collection of BusinessRule violations
  - Check Rule 1
  - Check Rule 2
  - etc...
- Staging logic
  - Create instances
  - Staging Add/Update/Delete
  - Set of records
- Test errors
  - Yes - Aggregate Exception
  - No - SaveChanges()
  
Query Models (as given by Don)
For dropdown:
- VendorId, Name, Phone, PO (PurchaseOrder), Subtotal, GST, Total

For ActiveOrder and SuggestOrder
- POId, StockItemId, Description, QOH, QOO, QTO, ROL, Price
  - Comes back as a List<T>
  
Vendor Inventory Table
- StockItemId, Description, QOH, List<T> ROL, QOO, Buffer, Price

Service Methods:
A) Vendors_GetList()
B) PurchaseOrder_GetActiveOrder(vendorid)
C) PurchaseOrderDetails_GetOrder(vendorid, List<C>)

TRX Method:
PurchaseOrder_Update(employeeid, vendorid, subtotal, gst, poid, List<C>)
PurchaseOrder_Place(employeeid, vendorid, subtotal, gst, poid, List<C>)
PurchaseOrder_Delete(employeeid, poid, vendorid)
