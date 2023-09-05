using System;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public class PasswordHasher : MonoBehaviour
{
    public static string HashPassword(string password, string salt)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password + salt);
            byte[] hashBytes = sha256.ComputeHash(passwordBytes);
            return Convert.ToBase64String(hashBytes);
        }
    }
}
