internal static class FontFixTool
{
    public static string Fix(string text)
    {
        if (ThaiFontAdjuster.IsThaiString(text))
            return ThaiFontAdjuster.Adjust(text);
        return text;
    }
}
