namespace RestaurantReservation.Domain.Models;

public class PaginatedList<T>
{
    public List<T> Items { get; set; }
    public PageData PageData { get; set; }
    
    public PaginatedList(List<T> items, PageData pageData)
    {
        Items = items;
        PageData = pageData;
    }
}