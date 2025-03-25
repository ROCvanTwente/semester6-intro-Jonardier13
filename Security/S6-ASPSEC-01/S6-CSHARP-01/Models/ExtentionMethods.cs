namespace S6_CSHARP_01.Models
{
    public static class ExtentionMethods
    {
        public static string FirstCharToUpper(string tekst)
        {
            if (string.IsNullOrEmpty(tekst))
                return tekst; // retourneer lege string of null als de input leeg is.

            // De eerste letter wordt omgezet naar hoofdletter, de rest naar kleine letters.
            return char.ToUpper(tekst[0]) + tekst.Substring(1).ToLower();
        }

        public static DateTime AddMinutes(this DateTime dateTime, int minutes)
        {
            // Voeg de opgegeven minuten toe aan de oorspronkelijke datum
            return dateTime.AddMinutes(minutes);
        }

    }
}
