using Microsoft.AspNetCore.Identity;
using System;
using System.Text;

using OnlineBankingApp.Models;
using System.Security.Cryptography;

namespace OnlineBankingApp.Identity
{
    public class PasswordHasher : IPasswordHasher<Customer>
    {
        public string HashPassword(Customer customer, string password)
        { 
            using (var md5 = new MD5CryptoServiceProvider()) {

                var hashedBytes = md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                
                var hashedPassword = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
                return hashedPassword;  
            }

        }

        public PasswordVerificationResult VerifyHashedPassword(Customer customer,
            string hashedPassword, string password)
        {
            var hash = HashPassword(customer, password);

            if (hashedPassword == hash)
                return PasswordVerificationResult.Success;
            else
                return PasswordVerificationResult.Failed;
        }
    }

}
