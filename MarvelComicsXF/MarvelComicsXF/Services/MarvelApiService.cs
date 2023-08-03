using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using MarvelComicsXF.Models;

namespace MarvelComicsXF.Services
{
    public class MarvelApiService : IMarvelApiService
    {
        private const string BaseUrl = "https://gateway.marvel.com/v1/public/";
        private const string PublicKey = "361b3bec7148a304fc196940341a1b4e"; // Replace with your Marvel API public key
        private const string PrivateKey = "8fd4c61f8260e0d2a849838951e5054038676655"; // Replace with your Marvel API private key

        private readonly HttpClient httpClient;

        public MarvelApiService()
        {
            httpClient = new HttpClient();
        }

        #region [ Md5Hash ]
        private static string CalculateMd5Hash(string input)
        {
            using (var md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                var stringBuilder = new System.Text.StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    stringBuilder.Append(hashBytes[i].ToString("x2"));
                }
                return stringBuilder.ToString();
            }
        }


        #endregion

        public async Task<ComicDataContainer> GetComicsAsync()
        {
            var timestamp = DateTime.Now.Ticks.ToString();
            var hash = CalculateMd5Hash(timestamp + PrivateKey + PublicKey);

            var apiUrl = $"{BaseUrl}comics?apikey={PublicKey}&ts={timestamp}&hash={hash}";

            var response = await httpClient.GetAsync(apiUrl);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<MarvelComicApiResponse>(content);
                return result.Data;
            }

            return null;
        }

        public async Task<ComicDataContainer> GetComicsByTitleAsync(string searchText)
        {
            var timestamp = DateTime.Now.Ticks.ToString();
            var hash = CalculateMd5Hash(timestamp + PrivateKey + PublicKey);

            var apiUrl = $"{BaseUrl}comics?apikey={PublicKey}&ts={timestamp}&hash={hash}&title={searchText}";

            var response = await httpClient.GetAsync(apiUrl);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<MarvelComicApiResponse>(content);
                return result.Data;
            }

            return null;
        }

        public async Task<ComicDataContainer> GetMoreComicsAsync(int offset)
        {
            var timestamp = DateTime.Now.Ticks.ToString();
            var hash = CalculateMd5Hash(timestamp + PrivateKey + PublicKey);
            var currentOffSet = offset.ToString();

            var apiUrl = $"{BaseUrl}comics?apikey={PublicKey}&ts={timestamp}&hash={hash}&offset={currentOffSet}";

            var response = await httpClient.GetAsync(apiUrl);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<MarvelComicApiResponse>(content);
                return result.Data;
            }

            return null;
        }

        public async Task<ComicDataContainer> GetMoreComicsByTitleAsync(int offset, string searchText)
        {
            var timestamp = DateTime.Now.Ticks.ToString();
            var hash = CalculateMd5Hash(timestamp + PrivateKey + PublicKey);
            var currentOffSet = offset.ToString();

            var apiUrl = $"{BaseUrl}comics?apikey={PublicKey}&ts={timestamp}&hash={hash}&offset={currentOffSet}&title={searchText}";

            var response = await httpClient.GetAsync(apiUrl);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<MarvelComicApiResponse>(content);
                return result.Data;
            }

            return null;
        }

        public async Task<CharacterDataContainer> GetCharactersAsync(string url)
        {
            var timestamp = DateTime.Now.Ticks.ToString();
            var hash = CalculateMd5Hash(timestamp + PrivateKey + PublicKey);

            var httpsurl = "https" + url.Substring(4);
            var apiUrl = $"{httpsurl}?apikey={PublicKey}&ts={timestamp}&hash={hash}";

            var response = await httpClient.GetAsync(apiUrl);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                var result = JsonConvert.DeserializeObject<MarvelCharacterApiResponse>(content);
                return result.Data;
            }

            return null;
        }
    }

    public class MarvelComicApiResponse
    {
        public ComicDataContainer Data { get; set; }
    }
    public class MarvelCharacterApiResponse
    {
        public CharacterDataContainer Data { get; set; }
    }


}


