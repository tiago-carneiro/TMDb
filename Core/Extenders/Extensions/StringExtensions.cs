namespace TMDb.Core
{
    public static class StringExtensions
    {
        public static string ToImagePath(this string value)
            => $"{ConstantHelper.ImageUrlPref}{value}";
    }
}
