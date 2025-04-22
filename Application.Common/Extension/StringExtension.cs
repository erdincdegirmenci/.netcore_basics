namespace Application.Common.Extension
{
    public static class StringExtension
    {
        public static int ToInt(this string value)
        {
            int returnValue = 0;
            if (string.IsNullOrEmpty(value))
                return returnValue;

            int.TryParse(value, out returnValue);

            return returnValue;
        }
    }
}
