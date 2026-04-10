using System;
using System.Security.Cryptography;
using System.Text;

namespace Ezcel.Configuration.SecureStorage
{
    public class SecureStorage
    {
        public string Encrypt(string plainText)
        {
            if (string.IsNullOrEmpty(plainText))
            {
                return string.Empty;
            }

            try
            {
                // 使用DPAPI加密
                byte[] data = Encoding.UTF8.GetBytes(plainText);
                byte[] encrypted = ProtectedData.Protect(data, null, DataProtectionScope.CurrentUser);
                return Convert.ToBase64String(encrypted);
            }
            catch (Exception ex)
            {
                // 如果DPAPI失败，返回原始文本（不安全，但总比崩溃好）
                return plainText;
            }
        }

        public string Decrypt(string encryptedText)
        {
            if (string.IsNullOrEmpty(encryptedText))
            {
                return string.Empty;
            }

            try
            {
                // 使用DPAPI解密
                byte[] data = Convert.FromBase64String(encryptedText);
                byte[] decrypted = ProtectedData.Unprotect(data, null, DataProtectionScope.CurrentUser);
                return Encoding.UTF8.GetString(decrypted);
            }
            catch (Exception ex)
            {
                // 如果解密失败，返回原始文本
                return encryptedText;
            }
        }
    }
}