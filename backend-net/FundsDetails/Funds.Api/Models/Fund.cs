namespace Funds.Api.Models
{
    public class Fund
    {

        public required string Isin { get; set; }

        public required string Filename { get; set; } 
        public required string FileLang { get; set; }
        public required string FundName { get; set; } 
        public DateTime? DocDate { get; set; }

        public string? CasoCosti { get; set; }

        public string? EntryCostsLog { get; set; }
        public decimal? EntryCosts { get; set; }

        public string? ExitCostsLog { get; set; }
        public decimal? ExitCosts { get; set; }

        public decimal? OngoingCosts { get; set; }
        public string? OngoingCostsLog { get; set; }
        public bool IsValid { get; set; }


    }
}
