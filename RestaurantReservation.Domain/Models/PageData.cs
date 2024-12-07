namespace RestaurantReservation.Domain.Models;

public class PageData
{
    public int TotalItemCount { get; set; }
    public int TotalPageCount { get; set; }
    public int PageSize { get; set; }
    public int CurrentPage { get; set; }
    public string PreviousPageLink { get; set; }
    public string NextPageLink { get; set; }

    public PageData(int totalItemCount, int pageSize, int currentPage, Func<int, string> generatePageLink)
    {
        TotalItemCount = totalItemCount;
        PageSize = pageSize;
        CurrentPage = currentPage;
        TotalPageCount = (int)Math.Ceiling(totalItemCount / (double)pageSize);
        
        PreviousPageLink = currentPage > 1 ? generatePageLink(currentPage - 1) : null;
        NextPageLink = currentPage < TotalPageCount ? generatePageLink(currentPage + 1) : null;
    }
}