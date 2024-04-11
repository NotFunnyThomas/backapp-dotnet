namespace BackApp.Model
{
    public static class StringPlus
    {
        public static string ConcatNewStringForNumber(this string strBase, int numberValue)
        {
            return $"b: {strBase}/{numberValue}";
        }

    }
}
