using System.ComponentModel.DataAnnotations;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using TheEmployeeAPI;
using TheEmployeeAPI.Abstractions;
using TheEmployeeAPI.Employees;

var builder = WebApplication.CreateBuilder(args);

var employees = new List<Employee>
{
    new Employee { 
        Id = 1, 
        FirstName = "John", 
        LastName = "Doe", 
        Benefits = new List<EmployeeBenefits> { 
            new EmployeeBenefits { BenefitType = BenefitType.Health, Cost = 100},
            new EmployeeBenefits { BenefitType = BenefitType.Dental, Cost = 50 }
        }
    },
        new Employee { 
        Id = 2, 
        FirstName = "Bobby", 
        LastName = "Brown", 
        Benefits = new List<EmployeeBenefits> { 
            new EmployeeBenefits { BenefitType = BenefitType.Dental, Cost = 50 }
        }
    }
};

var employeeRepository = new EmployeeRepository();
foreach (var e in employees) {
    employeeRepository.Create(e);
}

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "TheEmployeeAPI.xml"));
});

builder.Services.AddSingleton<IRepository<Employee>>(employeeRepository); // Added as singleton because in memory, when working with real DB this will not be the case
builder.Services.AddProblemDetails();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();
builder.Services.AddControllers(options =>
{
    options.Filters.Add<FluentValidationFilter>();
});
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.UseHttpsRedirection();

app.Run();

public partial class Program {}
