namespace MilitaryFinder.API.Contracts.V1
{
    public static class ApiRoutes
    {
        private const string Root = "api";
        private const string Version = "v1";

        private const string Base = Root + "/" + Version;

        public static class FighterAircraft
        {
            public const string GetAll = Base + "/aircrafts";
            public const string Get    = Base + "/aircraft/{id}";
            public const string Post   = Base + "/aircraft/create";
        }
    }
}
