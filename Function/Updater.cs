using AutoUpdaterDotNET;

namespace AutoClickIt.Function
{
    public class Updater
    {
        public static void CheckForUpdates()
        {
            try
            {
                AutoUpdater.Start("https://raw.githubusercontent.com/VindEi/AutoClickIt/master/version.xml");
            }
            catch
            {

            }
        }
    }
}
