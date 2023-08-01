using MarvelComicsXF.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MarvelComicsXF.Services
{
    public interface IMarvelApiService
    {
        Task<ComicDataContainer> GetComicsAsync();
        Task<ComicDataContainer> GetMoreComicsAsync(int offset);
        Task<ComicDataContainer> GetComicsByTitleAsync(string searchText);
        Task<ComicDataContainer> GetMoreComicsByTitleAsync(int offset, string searchText);
    }
}


