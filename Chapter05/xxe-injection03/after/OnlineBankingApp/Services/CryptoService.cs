using System;
using System.IO;
using System.Text;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;

namespace OnlineBankingApp.Services
{
    public class CryptoService : ICryptoService
    {
        private static readonly SecureRandom Random = new SecureRandom();

        private const int SaltBitSize = 128;
        private const int Iterations = 10000;
        private const int NonceBitSize = 128;
        private const int KeyBitSize = 256;
        private const int MacBitSize = 128;

        public string Encrypt(string strToEncrypt, string passPhrase)
        {
            var plainText = Encoding.UTF8.GetBytes(strToEncrypt);
            var generator = new Pkcs5S2ParametersGenerator();
            var salt = new byte[SaltBitSize / 8];
            Random.NextBytes(salt);

            generator.Init(
                PbeParametersGenerator.Pkcs5PasswordToBytes(passPhrase.ToCharArray()),
                salt,
                Iterations);

            var key = (KeyParameter)generator.GenerateDerivedMacParameters(KeyBitSize);

            var nonSecretPayload = new byte[] { };
            var payload = new byte[salt.Length];
            Array.Copy(nonSecretPayload, payload, nonSecretPayload.Length);
            Array.Copy(salt, 0, payload, nonSecretPayload.Length, salt.Length);

            var nonce = new byte[NonceBitSize / 8];
            Random.NextBytes(nonce, 0, nonce.Length);

            var cipher = new GcmBlockCipher(new AesEngine());
            var parameters = new AeadParameters(
                new KeyParameter(key.GetKey()), MacBitSize, nonce, payload);
            cipher.Init(true, parameters);

            var cipherText = new byte[cipher.GetOutputSize(plainText.Length)];
            var len = cipher.ProcessBytes(plainText, 0, plainText.Length, cipherText, 0);
            cipher.DoFinal(cipherText, len);

            using var combinedStream = new MemoryStream();
            using var binaryWriter = new BinaryWriter(combinedStream);
            binaryWriter.Write(payload);
            binaryWriter.Write(nonce);
            binaryWriter.Write(cipherText);

            return Convert.ToBase64String(combinedStream.ToArray());
        }

        public string Decrypt(string strEncrypted, string passPhrase)
        {
            var cipherText = Convert.FromBase64String(strEncrypted);
            var generator = new Pkcs5S2ParametersGenerator();
            var salt = new byte[SaltBitSize / 8];
            Array.Copy(cipherText, 0, salt, 0, salt.Length);

            generator.Init(
                PbeParametersGenerator.Pkcs5PasswordToBytes(passPhrase.ToCharArray()),
                salt,
                Iterations);

            var key = (KeyParameter)generator.GenerateDerivedMacParameters(KeyBitSize);

            using var cipherStream = new MemoryStream(cipherText);
            using var cipherReader = new BinaryReader(cipherStream);

            var payload = cipherReader.ReadBytes(salt.Length);
            var nonce = cipherReader.ReadBytes(NonceBitSize / 8);

            var cipher = new GcmBlockCipher(new AesEngine());
            var parameters = new AeadParameters(
                new KeyParameter(key.GetKey()), MacBitSize, nonce, payload);
            cipher.Init(false, parameters);

            var readBytes = cipherReader.ReadBytes(strEncrypted.Length - salt.Length - nonce.Length);
            var plainTextBytes = new byte[cipher.GetOutputSize(readBytes.Length)];

            var len = cipher.ProcessBytes(readBytes, 0, readBytes.Length, plainTextBytes, 0);
            cipher.DoFinal(plainTextBytes, len);

            return Encoding.UTF8.GetString(plainTextBytes);
        }
    }

    public interface ICryptoService
    {
        string Encrypt(string strToEncrypt, string passPhrase);
        string Decrypt(string strEncrypted, string passPhrase);
    }    

}