namespace MemoryEditingSoftware.Core.Business
{
    public static class Utilities
    {
        // check if the string passed as parameter only contains digits
        public static bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                {
                    return false;
                }
            }

            return true;
        }
    }
}
