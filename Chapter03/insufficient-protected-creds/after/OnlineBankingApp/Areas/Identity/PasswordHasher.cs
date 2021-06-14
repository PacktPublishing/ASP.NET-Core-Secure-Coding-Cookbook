using Microsoft.AspNetCore.Identity;
using System;
using System.Text;

using OnlineBankingApp.Models;
using System.Security.Cryptography;

using BC = BCrypt.Net.BCrypt;

namespace OnlineBankingApp.Identity
{
    public class PasswordHasher : IPasswordHasher<Customer>
    {
        public string HashPassword(Customer customer, string password)
        { 
            return BC.HashPassword(password);
        }

        public PasswordVerificationResult VerifyHashedPassword(Customer customer,
            string hashedPassword, string password)
        {
            if (BC.Verify(password, hashedPassword))
                return PasswordVerificationResult.Success;
            else
                return PasswordVerificationResult.Failed;
        }
    }

}
