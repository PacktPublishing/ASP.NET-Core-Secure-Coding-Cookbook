using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OnlineBankingApp.Data;
using System;
using System.Linq;

namespace OnlineBankingApp.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new OnlineBankingAppContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<OnlineBankingAppContext>>()))
            {
                if (context.Customer.Any())
                {
                    return;
                }

                context.Customer.AddRange(
                    new Customer
                    {
                        FirstName = "Stanley",
                        MiddleName = "Sybil",
                        LastName = "Jobson",
                        DateOfBirth = DateTime.Parse("10/11/1933"),
                        Email = "stanley.s.jobson@lobortis.ca",
                        Phone = "278133950"
                    },
                    new Customer
                    {
                        FirstName = "Axl",
                        MiddleName = "Lucius",
                        LastName = "Torvalds",
                        DateOfBirth = DateTime.Parse("03/11/1945"),
                        Email = "axl.l.torvalds@ut.net",
                        Phone = "585838762"
                    }
                );
                context.SaveChanges();
            }
        }
    }
}