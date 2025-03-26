using System;
using Microsoft.EntityFrameworkCore;

namespace TheEmployeeAPI;

public class AppDbContext : DbContext // Inherits from DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) // Constructor for options for configuring the DbContext, usually the dataset
    {
    }

    public DbSet<Employee> Employees { get; set; } // Property - Collection of Employee entities that are mapped to the Employees table in the database

}
