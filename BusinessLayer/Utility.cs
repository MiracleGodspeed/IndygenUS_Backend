using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace BusinessLayer
{
    public static class Utility
    {
        public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public static string GenerateSlug(this string phrase)
        {
            string str = phrase.RemoveAccent().ToLower();
            // invalid chars           
            str = Regex.Replace(str, @"[^a-z0-9\s-]", "");
            // convert multiple spaces into one space   
            str = Regex.Replace(str, @"\s+", " ").Trim();
            // cut and trim 
            str = str.Substring(0, str.Length <= 45 ? str.Length : 45).Trim();
            str = Regex.Replace(str, @"\s", "");
            return str;
        }
        public static string RemoveAccent(this string txt)
        {
            byte[] bytes = System.Text.Encoding.GetEncoding("Cyrillic").GetBytes(txt);
            return System.Text.Encoding.ASCII.GetString(bytes);
        }


        public static byte[] EncryptAesManaged(string raw)
        {
            try
            {
                byte[] encrypted;
                using (AesManaged aes = new AesManaged())
                {
                    encrypted = Encrypt(raw, aes.Key, aes.IV);
                    
                }
                return encrypted;

            }
            catch (Exception exp)
            {
                //Console.WriteLine(exp.Message);
            }
            return null;

        }
        public static string DecryptAesManaged(byte[] raw)
        {
            try
            {
                string decrypted;
                //byte[] btb = Encoding.UTF8.GetBytes(raw);
                using (AesManaged aes = new AesManaged())
                {
                   
                   decrypted = Decrypt(raw, aes.Key, aes.IV);

                }
                return decrypted;

            }
            catch (Exception exp)
            {
                //Console.WriteLine(exp.Message);
            }
            return null;
        }

        static byte[] Encrypt(string plainText, byte[] Key, byte[] IV)
        {
            byte[] encrypted;
            using (AesManaged aes = new AesManaged())
            {
                ICryptoTransform encryptor = aes.CreateEncryptor(Key, IV);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(cs))
                            sw.Write(plainText);
                        encrypted = ms.ToArray();
                    }
                }
            }
            return encrypted;
        }
        static string Decrypt(byte[] cipherText, byte[] Key, byte[] IV)
        {
            string plaintext = null;
            using (AesManaged aes = new AesManaged())
            {
                ICryptoTransform decryptor = aes.CreateDecryptor(Key, IV);
                using (MemoryStream ms = new MemoryStream(cipherText))
                {
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader reader = new StreamReader(cs))
                            plaintext = reader.ReadToEnd();
                    }
                }
            }
            return plaintext;
        }
        //public static string DecryptAesManaged(byte[] encrypted)
        //{
        //    try
        //    {
        //        using (AesManaged aes = new AesManaged())
        //        {
        //            // Encrypt string    
        //            //byte[] encrypted = Encrypt(raw, aes.Key, aes.IV);
        //            //var ff = $"Encrypted data:{Encoding.UTF8.GetString(encrypted)}";
        //            // Decrypt the bytes to a string.
        //            string decrypted = Decrypt(encrypted, aes.Key, aes.IV);

        //        }
        //        return decry
        //    }
        //    catch (Exception exp)
        //    {
        //        //Console.WriteLine(exp.Message);
        //    }
        //    Console.ReadKey();
        //}
        //public static byte[] Encrypt(string plainText)
        //{
        //    byte[] encrypted;
        //    using (AesManaged aes = new AesManaged())
        //    {
        //        ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
        //        using (MemoryStream ms = new MemoryStream())
        //        {
        //            using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
        //            {
        //                using (StreamWriter sw = new StreamWriter(cs))
        //                    sw.Write(plainText);
        //                encrypted = ms.ToArray();
        //            }
        //        }
        //    }
        //    return encrypted;
        //}
        //public static string Decrypt(byte[] cipherText)
        //{
        //    string plaintext = null;
        //    using (AesManaged aes = new AesManaged())
        //    {
        //        ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
        //        using (MemoryStream ms = new MemoryStream(cipherText))
        //        {
        //            using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
        //            {
        //                using (StreamReader reader = new StreamReader(cs))
        //                    plaintext = reader.ReadToEnd();
        //            }
        //        }
        //    }
        //    return plaintext;
        //}
    }
}
