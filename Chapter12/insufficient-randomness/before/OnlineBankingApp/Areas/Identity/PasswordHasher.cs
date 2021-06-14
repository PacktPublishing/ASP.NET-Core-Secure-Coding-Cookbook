using Microsoft.AspNetCore.Identity;
using System;
using System.Text;

using OnlineBankingApp.Models;
using System.Security.Cryptography;

using Konscious.Security.Cryptography;

namespace OnlineBankingApp.Identity
{
    public class PasswordHasher : IPasswordHasher<Customer>
    {
        private const int DigestBytes = 64;
        private const int Threads = 8;
        private const int Memory = 524288;
        private const int Iterations = 8;
        private const int SaltBytes = 32;
        
        public string HashPassword(Customer user, string password)
        {
            if (password == null)
            {
                throw new ArgumentNullException(nameof(password));
            }

            return GetHash(password, null);
        }
        
        public PasswordVerificationResult VerifyHashedPassword(Customer user, string hashedPassword, string providedPassword)
        {
            if (hashedPassword == null)
            {
                throw new ArgumentNullException(nameof(hashedPassword));
            }

            if (providedPassword == null)
            {
                throw new ArgumentNullException(nameof(providedPassword));
            }

            var hashBytes = Convert.FromBase64String(hashedPassword);
            var saltArr = new byte[SaltBytes];
            Buffer.BlockCopy(hashBytes, 1, saltArr, 0, SaltBytes);

            return hashedPassword.Equals(GetHash(providedPassword, saltArr))
                ? PasswordVerificationResult.Success
                : PasswordVerificationResult.Failed;
        }
        
        private string GetHash(string password, byte[] salt)
        {
            var saltBytes = salt ?? CreateSalt();
            var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password))
            {
                Salt = saltBytes,
                DegreeOfParallelism = Threads,
                Iterations = Iterations,
                MemorySize = Memory
            };

            var digestBytes = argon2.GetBytes(DigestBytes);

            var resultBytes = new byte[1 + SaltBytes + DigestBytes];
            Buffer.BlockCopy(new[] {(byte) 0x00}, 0, resultBytes, 0, 1);
            Buffer.BlockCopy(saltBytes, 0, resultBytes, 1, SaltBytes);
            Buffer.BlockCopy(digestBytes, 0, resultBytes, 1 + SaltBytes, DigestBytes);
            return Convert.ToBase64String(resultBytes);
        }
        
        private byte[] CreateSalt()
        {
            var buffer = new byte[SaltBytes];
            Random rnd = new Random();
            rnd.NextBytes(buffer);
            return buffer;
        }
    }

}
