namespace MicroondasBennerAPI.Utils
{
    public static class EnvironmentUtils
    {
        private static string GetVariable()
            => Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") 
            ?? Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT")
            ?? "Production";

        public static bool IsDevelopment()
            => GetVariable() == "Development";

        public static string EnvironmentName()
            => GetVariable();
    }
}
