using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Data.Context;

public class DataContextFactory : IDesignTimeDbContextFactory<DataContext>
{
    public DataContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
        optionsBuilder.UseSqlite("Data Source = /Users/josipsmac/Desktop/EC utbildning/Csharp/Dashboard_Project_MVC/Data/MyDatabase.db");

        return new DataContext(optionsBuilder.Options);
    }
}