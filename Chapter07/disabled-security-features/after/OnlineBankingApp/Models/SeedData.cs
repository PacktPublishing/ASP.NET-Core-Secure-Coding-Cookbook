using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OnlineBankingApp.Data;
using System;
using System.Linq;
using System.Collections.Generic;

using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace OnlineBankingApp.Models
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetService<UserManager<Customer>>();

            var user1 = await userManager.FindByNameAsync("stanley.s.jobson@lobortis.ca");
            if (user1 == null)
            {
                user1 = new Customer
                {
                    FirstName = "Stanley",
                    MiddleName = "Sybil",
                    LastName = "Jobson",
                    DateOfBirth = DateTime.Parse("10/11/1933"),
                    UserName = "stanley.s.jobson@lobortis.ca",
                    Email = "stanley.s.jobson@lobortis.ca",
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
                        },
                    FundTransfers = new List<FundTransfer>{
                        new FundTransfer { 
                                AccountFrom = 1,
                                AccountTo = 2,
                                Amount = 510.00m,
                                TransactionDate = DateTime.Parse("06/12/2021"),
                                Note = "Transfer between accounts"
                            }
                    }

                };
                user1.PasswordHash = userManager.PasswordHasher.HashPassword(user1,"rUj5jtV8jrTyHnx!");

                await userManager.CreateAsync(user1);
            }

            if (user1 == null)
            {
                throw new Exception("The password is probably not strong enough!");
            }

            var user2 = await userManager.FindByNameAsync("axl.l.torvalds@ut.net");
            if (user2 == null)
            {
                user2 = new Customer
                    {
                        FirstName = "Axl",
                        MiddleName = "Lucius",
                        LastName = "Torvalds",
                        DateOfBirth = DateTime.Parse("03/11/1945"),
                        Email = "axl.l.torvalds@ut.net",
                        UserName = "axl.l.torvalds@ut.net",
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
                };
                user2.PasswordHash = userManager.PasswordHasher.HashPassword(user2,"6GKqqtQQTii92ke!");
                await userManager.CreateAsync(user2);
            }

            if (user2 == null)
            {
                throw new Exception("The password is probably not strong enough!");
            }

        }
    }
}