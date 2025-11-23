using Funds.Api.Models;

namespace Funds.Api
{
    public class ExternalApi
    {

        private readonly HttpClient _httpClient;

        public ExternalApi(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IReadOnlyList<FundDto>> GetFundsAsync()
        {
            
            var url = "https://sapeimarco.github.io/sample-json-api/sample.json";

            var result = await _httpClient.GetFromJsonAsync<List<FundDto>>(url);
            return result ?? [];
        }


    }
}
