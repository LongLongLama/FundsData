using Funds.Api.Models;

namespace Funds.Api
{
    public class FundsImportService
    {
        private readonly ExternalApi _client;
        private readonly FundsDb _repo;

        public FundsImportService(ExternalApi client, FundsDb repo)
        {
            _client = client;
            _repo = repo;
        }

        public async Task<int> ImportAsync()
        {
            var externalFunds = await _client.GetFundsAsync();

            var ris = externalFunds.ToList();

            var totalAffected = 0;

            foreach (var fund in ris)
            {
                var rows = await _repo.UpdateAsync(fund);

                if (rows == 0)
                {
                    rows = await _repo.InsertAsync(fund);
                }

                totalAffected += rows;
            }

            return totalAffected;
        }
    }
}
