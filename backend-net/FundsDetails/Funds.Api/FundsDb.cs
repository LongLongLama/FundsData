using Dapper;
using Funds.Api.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Globalization;





namespace Funds.Api

{
    public class FundsDb
    {
        private readonly string _connectionString;
        public FundsDb(IConfiguration configuration)
        {

            _connectionString = configuration.GetConnectionString("FundsDb") ?? throw new InvalidOperationException("Connection string not found.");
        }


        public IDbConnection CreateConnection() => new SqlConnection(_connectionString);

        public async Task<IEnumerable<FundDto>> GetAllAsync()
        {
            const string sql = @"SELECT
                                Isin,
                                Filename,
                                File_Lang,
                                Fund_Name,
                                Doc_Date,
                                Caso_Costi,
                                Entry_Costs_Log,
                                Entry_Costs,
                                Exit_Costs_Log,
                                Exit_Costs,
                                Ongoing_Costs,
                                Ongoing_Costs_Log,
                                Is_Valid
                                FROM dbo.Funds";

            using var conn = CreateConnection();
            return await conn.QueryAsync<FundDto>(sql);
        }


        public async Task<int> UpdateAsync(FundDto fund)
        {
            const string sql = @"UPDATE dbo.Funds
                            SET
                            Filename        = @Filename,
                            File_Lang        = @File_Lang,
                            Fund_Name        = @Fund_Name,
                            Doc_Date         = @Doc_Date,
                            Caso_Costi       = @Caso_Costi,
                            Entry_Costs_Log   = @Entry_Costs_Log,
                            Entry_Costs      = @Entry_Costs,
                            Exit_Costs_Log    = @Exit_Costs_Log,
                            Exit_Costs       = @Exit_Costs,
                            Ongoing_Costs    = @Ongoing_Costs,
                            Ongoing_Costs_Log = @Ongoing_Costs_Log,
                            Is_Valid         = @Is_Valid
                            WHERE Isin = @Isin;
                            ";

            using var conn = CreateConnection();
            return await conn.ExecuteAsync(sql, fund);
        }

        public async Task<int> InsertAsync(FundDto fund)
        {
            const string sql = @"
                                INSERT INTO dbo.Funds (
                                    Isin,
                                    Filename,
                                    File_Lang,
                                    Fund_Name,
                                    Doc_Date,
                                    Caso_Costi,
                                    Entry_Costs_Log,
                                    Entry_Costs,
                                    Exit_Costs_Log,
                                    Exit_Costs,
                                    Ongoing_Costs,
                                    Ongoing_Costs_Log,
                                    Is_Valid
                                )
                                VALUES (
                                    @Isin,
                                    @Filename,
                                    @File_Lang,
                                    @Fund_Name,
                                    @Doc_Date,
                                    @Caso_Costi,
                                    @Entry_Costs_Log,
                                    @Entry_Costs,
                                    @Exit_Costs_Log,
                                    @Exit_Costs,
                                    @Ongoing_Costs,
                                    @Ongoing_Costs_Log,
                                    @Is_Valid
                                );";

            using var conn = CreateConnection();
            return await conn.ExecuteAsync(sql, fund);
        }

        //public static Fund FromDto(FundDto dto)
        //{
        //    return new Fund
        //    {
        //        Isin = dto.Isin,
        //        Filename = dto.Filename,
        //        FileLang = dto.File_Lang,
        //        FundName = dto.Fund_Name,
        //        DocDate = ParseDocDate(dto.Doc_Date),
        //        CasoCosti = dto.Caso_Costi,
        //        EntryCostsLog = dto.Entry_Costs_Log,
        //        EntryCosts = dto.Entry_Costs,
        //        ExitCostsLog = dto.Exit_Costs_Log,
        //        ExitCosts = dto.Exit_Costs,
        //        OngoingCosts = dto.Ongoing_Costs,
        //        OngoingCostsLog = dto.Ongoing_Costs_Log,
        //        IsValid = dto.Is_Valid
        //    };
        //}

        //private static DateTime? ParseDocDate(string? raw)
        //{
        //    if (string.IsNullOrWhiteSpace(raw)) return null;

        //    if (DateTime.TryParseExact(
        //            raw,
        //            "dd/MM/yyyy",
        //            CultureInfo.InvariantCulture,
        //            DateTimeStyles.None,
        //            out var dt))
        //    {
        //        return dt;
        //    }

        //    return null;
        //}


    }
}
