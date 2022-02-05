namespace PokeDex.Api.Contracts
{
    public static class ApiRoutes
    {
        private const string PokemonNameRouteParameter = "pokemonName";

        public static class Pokemon
        {
            public const string Root = "pokemon";
            public const string GetByName = PokemonNameRouteParameter;
            public const string GetTranslatedByName = "translated/" + PokemonNameRouteParameter;
        }
    }
}
