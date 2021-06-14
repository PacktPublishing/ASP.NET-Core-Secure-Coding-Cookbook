using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OnlineBankingApp.Data;
using System;
using System.Linq;
using System.Collections.Generic;

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
                        //Phone = "278133950",
                        Accounts = new List<Account>{ 
                            new Account { 
                                    Name = "Savings",
                                    AccountType = AccountType.Savings, 
                                    Balance = 1250.55m 
                                } , 
                            new Account { 
                                    Name = "Checking",
                                    AccountType = AccountType.Checking, 
                                    Balance = 2340.10m
                                }  
                            }
                    },
                    new Customer
                    {
                        FirstName = "Axl",
                        MiddleName = "Lucius",
                        LastName = "Torvalds",
                        DateOfBirth = DateTime.Parse("03/11/1945"),
                        Email = "axl.l.torvalds@ut.net",
                        //Phone = "585838762",
                        Accounts = new List<Account>{ 
                            new Account { 
                                    Name = "Savings",
                                    AccountType = AccountType.Savings, 
                                    Balance = 15030.00m 
                                } , 
                            new Account { 
                                    Name = "Checking",
                                    AccountType = AccountType.Checking, 
                                    Balance = 2010.35m
                                }  
                        }
                        // FundTransfers = new List<FundTransfer>{
                        //     new FundTransfer
                        //     {
                        //         AccountFrom = 1,
                        //         AccountTo = 2,
                        //         Amount = 50.00m,
                        //         TransactionDate = DateTime.Parse("07/30/2021")
                        //     },
                        //     new FundTransfer
                        //     {
                        //         AccountFrom = 1,
                        //         AccountTo = 2,
                        //         Amount = 100.00m,
                        //         TransactionDate = DateTime.Parse("08/05/2021")
                        //     }
                        // }
                    }

                );

                context.SaveChanges();

                // context.FundTransfer.AddRange(
                //     new FundTransfer
                //     {
                //         CustomerID = 1,
                //         AccountFrom = 1,
                //         AccountTo = 2,
                //         Amount = 50.00m,
                //         TransactionDate = DateTime.Parse("07/30/2021")
                //     },
                //     new FundTransfer
                //     {
                //         CustomerID = 1,
                //         AccountFrom = 1,
                //         AccountTo = 2,
                //         Amount = 100.00m,
                //         TransactionDate = DateTime.Parse("08/05/2021")
                //     }
                //);                
                context.SaveChanges();
            }
        }
    }
}