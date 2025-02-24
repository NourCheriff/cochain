namespace CochainAPI.Model.Helper.Extender
{
    public static class ToBase64String
    {
        public static string BytesToBase64String(this byte[] byteArray)
        {
            return byteArray != null ? Convert.ToBase64String(byteArray) : string.Empty;
        }
    }
}
