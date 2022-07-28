namespace TheTallTankardTavern.Helpers
{
    public static class ItemHelper
    {
        public static string ToItemID(this string inventoryId)
        {
            return inventoryId.Substring(0, inventoryId.IndexOf("+"));
        }
    }
}
