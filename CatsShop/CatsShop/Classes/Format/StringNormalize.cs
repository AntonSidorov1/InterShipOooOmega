namespace CatsShop
{
    public static class StringNormalize
    {
        public static string Normalize(string text)
        {
            text = text.ToLower();
            text = text.Replace('_', ' ');
            text = text.Replace('-', ' ');
            text = text.Replace('.', ' ');
            text = text.Replace(',', ' ');
            text = text.Trim();
            return text;
        }


        
    }
}
