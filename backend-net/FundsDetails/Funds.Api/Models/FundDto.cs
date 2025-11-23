using System.Text.Json.Serialization;

namespace Funds.Api.Models
{
    public class FundDto
    {

        public required string Isin { get; set; }

        public required string Filename { get; set; }

        [JsonPropertyName("file_lang")]
        public required string File_Lang { get; set; }

        [JsonPropertyName("fund_name")]
        public required string Fund_Name { get; set; }

        [JsonPropertyName("doc_date")]
        public string? Doc_Date { get; set; }

        [JsonPropertyName("caso_costi")]
        public string? Caso_Costi { get; set; }

        [JsonPropertyName("entry_costs_log")]
        public string? Entry_Costs_Log { get; set; }

        [JsonPropertyName("entry_costs")]
        public decimal? Entry_Costs { get; set; }

        [JsonPropertyName("exit_costs_log")]
        public string? Exit_Costs_Log { get; set; }

        [JsonPropertyName("exit_costs")]
        public decimal? Exit_Costs { get; set; }

        [JsonPropertyName("ongoing_costs")]
        public decimal? Ongoing_Costs { get; set; }

        [JsonPropertyName("ongoing_costs_log")]
        public string? Ongoing_Costs_Log { get; set; }

        [JsonPropertyName("is_valid")]
        public bool Is_Valid { get; set; }

    }



}
