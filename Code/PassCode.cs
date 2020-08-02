namespace Code
{
    public static class PassCode
    {
        private static string GetRepeatKey(string s, int n)
        {
            string result = "";
            for (int i = 0; i < s.Length; i++)
            {
                result += ((char)(s[i] ^ s[i % s.Length]));
            }
            return result;
        }

        private static string Enciper(string pass, string key)
        {
            string currentKey = GetRepeatKey(key, pass.Length);
            string res = currentKey[0].ToString();
            for (int i = 0; i < pass.Length; i++)
            {
                res += ((char)(pass[i] ^ currentKey[i % currentKey.Length])).ToString();
            }
            res += currentKey[1].ToString();
            return res;
        }

        private static string Deciper(string pass, string key)
        {
            string tmp = pass.Substring(1, pass.Length - 1);
            pass = tmp;
            string currentKey = GetRepeatKey(key, pass.Length);
            string res = string.Empty;
            for (int i = 0; i < pass.Length; i++)
            {
                res += ((char)(pass[i] ^ currentKey[i % currentKey.Length])).ToString();
            }
            return res;
        }

        public static string Encrypt(string plainText, string key)
            => Enciper(plainText, key);
        public static string Decrypt(string encryptedText, string key)
            => Deciper(encryptedText, key);
    }
}
