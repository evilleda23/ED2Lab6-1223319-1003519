using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Encryptors
{
    public class RSAEncryptor : IEncryptor
    {
        private readonly string Path;

        public RSAEncryptor(string path)
        {
            Path = path;
        }

        public string GenerateKeys(int p, int q)
        {
            try
            {
                if (IsValid(p, q))
                {
                    string path = "";
                    using var file = new FileStream(path, FileMode.Create);
                    //file.Write(final, 0, final.Length);
                    return path;
                }
                else
                    return "";
            }
            catch
            {
                return "";
            }
        }

        public byte[] ShowCipher(string text, int n, int x)
        {
            return null;
        }

        public string Cipher(byte[] content, Key key, string name)
        {
            try
            {
                if (IsValid(key))
                {
                    string text = ConvertToString(content);
                    byte[] final = ShowCipher(text, key.N, key.X);
                    string path = Path + "\\" + name + ".rsa";
                    using var file = new FileStream(path, FileMode.Create);
                    file.Write(final, 0, final.Length);
                    return path;
                }
                else
                    return "";
            }
            catch
            {
                return "";
            }
        }

        private bool IsValid(Key key)
        {
            if (key.N > 1 && key.X > 1)
                return true;
            else
                return false;
        }

        private bool IsValid(int p, int q)
        {
            if (p > 1 && q > 1)
            {
                if (IsPrime(p) && IsPrime(q))
                    return true;
            }
            return false;
        }

        private bool IsPrime(int n)
        {
            for (int i = 2; i < n; i++)
            {
                if (n % i == 0)
                    return false;
            }
            return true;
        }

        private string ConvertToString(byte[] array)
        {
            string text = "";
            foreach (var item in array)
                text += Convert.ToString(Convert.ToChar(item));
            return text;
        }

        private byte[] ConvertToByteArray(string text)
        {
            byte[] array = new byte[text.Length];
            for (int i = 0; i < text.Length; i++)
                array[i] = Convert.ToByte(text[i]);
            return array;
        }
    }
}
