using System.Threading;
using System.Threading.Tasks;

namespace PokeDex.Api.Services
{
    public interface ITranslationService
    {
        Task<string> TranslateTextAsync(string text, string translationName, CancellationToken cancellationToken);
    }
}