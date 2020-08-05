namespace Code
{
    public static class PassCode
    {
        private static string GetNewKey(string s, int n)
        {
            string result = "";
            while(n > 0){
                for (int i = 0; i < s.Length; i++)
                {
                    result += ((char)(s[i] ^ s[s.Length - 1 - i]));
                }
                n--;
            }
            return result;
        }

        private static string Enciper(string pass, string key)
        {
            string currentKey = GetNewKey(key, pass.Length);
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
            if (pass.Length < 1) return "";
            string tmp = pass.Substring(1, pass.Length - 1);
            pass = tmp;
            string currentKey = GetNewKey(key, pass.Length);
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
