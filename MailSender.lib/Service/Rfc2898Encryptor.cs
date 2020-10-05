using MailSender.lib.Interfaces;
using System;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace MailSender.lib.Service
{
    public class Rfc2898Encryptor : IEncryptorService
    {
        private static readonly byte[] SALT =
        {
            0x26, 0xdc, 0xff, 0x00,
            0xad, 0xed, 0x7a, 0xee,
            0xc5, 0xfe, 0x07, 0xaf,
            0x4d, 0x08, 0x22, 0x3c
        };

        public Encoding Encoding { get; set; } = Encoding.UTF8;

        private static ICryptoTransform GetEncryptAlgorithm(string password)
        {
            var pdb = new Rfc2898DeriveBytes(password, SALT);
            var algorithm = Rijndael.Create();
            algorithm.Key = pdb.GetBytes(32);
            algorithm.IV = pdb.GetBytes(16);
            return algorithm.CreateEncryptor();
        }
        private static ICryptoTransform GetDecryptAlgorithm(string password)
        {
            var pdb = new Rfc2898DeriveBytes(password, SALT);
            var algorithm = Rijndael.Create();
            algorithm.Key = pdb.GetBytes(32);
            algorithm.IV = pdb.GetBytes(16);
            return algorithm.CreateDecryptor();
        }

        public string Decrypt(string str, string password)
        {
            var encoding = Encoding ?? Encoding.UTF8;
            var cryptedBytes = Convert.FromBase64String(str);
            var bytes = Decrypt(cryptedBytes, password);
            return encoding.GetString(bytes);
        }

        public string Encrypt(string str, string password)
        {
            var encoding = Encoding ?? Encoding.UTF8;
            var bytes = encoding.GetBytes(str);
            var cryptedBytes = Encrypt(bytes, password);
            return Convert.ToBase64String(cryptedBytes);
        }

        public byte[] Encrypt(byte[] data, string password)
        {
            var algorithm = GetEncryptAlgorithm(password);
            using (var stream = new MemoryStream())
            using (var cryptoStream = new CryptoStream(stream, algorithm, CryptoStreamMode.Write)) 
            {
                cryptoStream.Write(data, 0, data.Length);
                cryptoStream.FlushFinalBlock();
                return stream.ToArray();
            }
        }

        public byte[] Decrypt(byte[] data, string password)
        {
            var algorithm = GetDecryptAlgorithm(password);
            using (var stream = new MemoryStream())
            using (var cryptoStream = new CryptoStream(stream, algorithm, CryptoStreamMode.Write))
            {
                cryptoStream.Write(data, 0, data.Length);
                cryptoStream.FlushFinalBlock();
                return stream.ToArray();
            }
        }


    }
}
