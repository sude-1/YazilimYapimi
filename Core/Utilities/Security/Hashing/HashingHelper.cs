using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.Hashing
{
    public class HashingHelper
    {
        //Hash oluşturmak için
        public static void CreatePasswordHash
            (string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())//hash oluştururken hangi algoritma kullanılsın
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }
        //o hashi doğrulamak için 
        public static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            //doğrularken yine aynı algoritmayı ama oluştururken kullandığımız salt'ı kullanarak doğrulama yapıyoruz 
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i]!=passwordHash[i])
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
