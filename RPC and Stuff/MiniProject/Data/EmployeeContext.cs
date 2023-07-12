using Microsoft.EntityFrameworkCore;
using Projekt.Models;

namespace Projekt.Data
{
    public class EmployeeContext : DbContext
    {
        public EmployeeContext(DbContextOptions<EmployeeContext> options)
        : base(options)
        {
        }

        public DbSet<Employee> EmployeeItems { get; set; }
    }
}
