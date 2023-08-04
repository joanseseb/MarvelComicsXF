using MarvelComicsXF.Models;
using System.Threading.Tasks;

namespace MarvelComicsXF.Services.Api
{
    public interface IMarvelApiService
    {
        Task<ComicDataContainer> GetComicsAsync();
        Task<ComicDataContainer> GetMoreComicsAsync(int offset);
        Task<ComicDataContainer> GetComicsByTitleAsync(string searchText);
        Task<ComicDataContainer> GetMoreComicsByTitleAsync(int offset, string searchText);
        Task<CharacterDataContainer> GetCharactersAsync(string url);
    }
}


