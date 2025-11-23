export interface FundRecord {
  filename: string
  file_lang: string
  isin: string
  fund_name: string
  doc_date: Date | null
  caso_costi: string
  entry_costs_log: string
  entry_costs: number
  exit_costs_log: string
  exit_costs: number
  ongoing_costs: number
  ongoing_costs_log: string
  is_valid: boolean
}

export interface RawFundRecord {
  filename: string
  file_lang: string
  isin: string
  fund_name: string
  doc_date: string | null
  caso_costi: string
  entry_costs_log: string
  entry_costs: number
  exit_costs_log: string
  exit_costs: number
  ongoing_costs: number
  ongoing_costs_log: string
  is_valid: boolean
}
