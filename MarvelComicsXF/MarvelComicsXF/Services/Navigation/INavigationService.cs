using MarvelComicsXF.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MarvelComicsXF.Services.Navigation
{
    public interface INavigationService
    {
        Task NavigateToPageAsync(string pageName);
        Task NavigateToPageAsync(string pageName, IDictionary<string, object> parameters);
        Task GoBackAsync();
    }
}
