namespace PokeDex.Api.Contracts
{
    public static class ApiRoutes
    {
        public static class Pokemon
        {
            private const string PokemonNameRouteParameter = "{pokemonName}";
            public const string Root = "/pokemon";
            public const string GetByName = Root + "/" + PokemonNameRouteParameter;
            public const string GetTranslatedByName = Root + "/translated/" + PokemonNameRouteParameter;
        }
    }
}
