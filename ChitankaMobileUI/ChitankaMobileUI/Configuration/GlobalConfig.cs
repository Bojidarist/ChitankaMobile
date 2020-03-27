namespace ChitankaMobileUI.Configuration
{
    public static class GlobalConfig
    {
        private static Config cfg;

        public static Config Instance
        {
            get
            {
                if (cfg == null)
                {
                    cfg = new Config();
                }
                return cfg;
            }
        }

        public struct Options
        {
            public static string DSaveFolderId = "dSaveFolderId";
            public static string DFoldersList = "dFoldersList";
            public static string DSaveFolderName = "dSaveFolderName";
        }
    }
}
