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
    }
}
