namespace MilitaryFinder.API.Contracts
{
    public static class ApiRoutes
    {
        public static class FighterAircraft
        {
            public const string GetAll = "api/v1/aircrafts";
            public const string Get = "api/v1/aircraft/{id}";
            public const string Post = "api/v1/aircraft/create";
        }
    }
}
