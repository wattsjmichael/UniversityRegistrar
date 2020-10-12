using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace UniversityRegistrar.Models
{
  public class UniversityRegistrarContextFactory : IDesignTimeDbContextFactory<UniversityRegistrarContext>
  {

    UniversityRegistrarContext IDesignTimeDbContextFactory<UniversityRegistrarContext>.CreateDbContext(string[] args)
    {
      IConfigurationRoot configuration = new ConfigurationBuilder()
          .SetBasePath(Directory.GetCurrentDirectory())
          .AddJsonFile("appsettings.json")
          .Build();

      var builder = new DbContextOptionsBuilder<UniversityRegistrarContext>();
      var connectionString = configuration.GetConnectionString("DefaultConnection");

      builder.UseMySql(connectionString);

      return new UniversityRegistrarContext(builder.Options);
    }
  }
}