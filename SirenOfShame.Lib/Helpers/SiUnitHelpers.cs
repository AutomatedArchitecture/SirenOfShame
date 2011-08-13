namespace SirenOfShame.Lib.Helpers
{
    public static class SiUnitHelpers
    {
        public static string ToBinaryString(uint i)
        {
            if (i > 1024 * 1024 * 1024)
            {
                return (i / (1024 * 1024 * 1024.0)).ToString("#,###.##") + " Gi";
            }
            if (i > 1024 * 1024)
            {
                return (i / (1024 * 1024.0)).ToString("#,###.##") + " Mi";
            }
            if (i > 1024)
            {
                return (i / 1024.0).ToString("#,###.##") + " Ki";
            }
            return i.ToString("#,###.##");
        }
    }
}
