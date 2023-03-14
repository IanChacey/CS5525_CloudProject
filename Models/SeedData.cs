using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TimeKeepingApp.Data;

namespace TimeKeepingApp.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<ApplicationDbContext>>()))
            {
                if (context.Shift.Any() && context.Employee.Any())
                {
                    return;   // DB has been seeded
                }
                else if (!context.Employee.Any() || !context.Shift.Any())
                {
                    if (!context.Shift.Any())
                    {
                        context.Shift.AddRange(
                            new Shift
                            {
                                EmployeeID = "a2080bdb-5c48-48e6-b4e2-775bc77d91d5",
                                ShiftStart = DateTime.Parse("2023-2-12 12:00"),
                                ShiftEnd = DateTime.Parse("2023-2-12 16:00"),
                                Location = "On Site"
                            },
                            new Shift
                            {
                                EmployeeID = "0002",
                                ShiftStart = DateTime.Parse("2023-2-12 08:00"),
                                ShiftEnd = DateTime.Parse("2023-2-12 12:00"),
                                Location = "On Site"
                            }

                        );
                    }

                    if (!context.Employee.Any())
                    {
                        context.Employee.AddRange(
                            new Employee
                            {
                                EmployeeID = "a2080bdb-5c48-48e6-b4e2-775bc77d91d5",
                                Department = "Managment",
                                Role = "Manager",
                                EmployeeName = "David",
                                HourlyWage = 16.43f
                            },
                            new Employee
                            {
                                EmployeeID = "0002",
                                Department = "Retail",
                                Role = "Salesman",
                                EmployeeName = "Kyle",
                                HourlyWage = 12.74f
                            }
                        );
                    }
                }
                context.SaveChanges();
            }
        }
    }
}
