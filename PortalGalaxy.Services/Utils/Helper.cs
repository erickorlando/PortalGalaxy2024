namespace PortalGalaxy.Services.Utils;

public static class Helper
{
    public static int GetTotalPages(int totalRows, int pageSize)
    {
        if (totalRows == 0) return 0;

        //return (int)Math.Ceiling((decimal)totalRows / pageSize);

        var total = totalRows / pageSize; // hacemos la division
        if (totalRows % pageSize > 0) // calculamos si tiene residuo
        {
            total++; // le suma en 1
        }

        return total;
    }
}