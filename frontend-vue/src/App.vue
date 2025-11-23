<script setup lang="ts">
import { ref, onMounted } from 'vue'
import type { FundRecord, RawFundRecord } from './types'
import DataTable from './components/DataTable.vue'
import EditForm from './components/EditForm.vue'

const records = ref<FundRecord[]>([])

onMounted(async () => {
  try {
    const response = await fetch('/api/data')
    const data = await response.json()

    records.value = data.map(
      (r: RawFundRecord): FundRecord => ({
        ...r,
        doc_date: parseDocDate(r.doc_date),
      }),
    )
  } catch (error) {
    console.error('Errore durante il fetch dei dati:', error)
    alert('Errore durante il caricamento dei dati')
  }
})

const recordToEdit = ref<FundRecord | null>(null)

function handleEditRequest(record: FundRecord) {
  console.log('Richiesta di modifica per:', record)
  recordToEdit.value = { ...record }
}

async function handleUpdate(updatedRecord: FundRecord) {
  console.log("Tentativo di inviare l'aggiornamento al server...", updatedRecord)

  const ris: RawFundRecord = fundRecordToRaw(updatedRecord)

  try {
    const response = await fetch(`/api/data/${updatedRecord.isin}`, {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(ris),
    })

    if (!response.ok) {
      const errorData = await response.text()
      throw new Error(`Errore dal server: ${response.status} - ${errorData}`)
    }

    const responseText = await response.text()
    console.log('Risposta testuale dal server (OK):', responseText)

    const savedRecord = updatedRecord

    const index = records.value.findIndex((r) => r.isin === savedRecord.isin)
    if (index !== -1) {
      records.value[index] = savedRecord
    }

    console.log('Aggiornamento riuscito!', savedRecord)
    cancelEdit()
  } catch (error) {
    console.error("Fallimento dell'operazione di aggiornamento:", error)
    alert(
      `Impossibile salvare le modifiche. Controlla la console per i dettagli.\nErrore: ${(error as Error).message ?? error}`,
    )
  }
}

function cancelEdit() {
  recordToEdit.value = null
}

function parseDocDate(raw: string | null | undefined): Date | null {
  if (!raw) return null
  const match = raw.match(/^(\d{2})\/(\d{2})\/(\d{4})$/)
  if (!match) return null

  const dd = Number(match[1])
  const mm = Number(match[2])
  const yyyy = Number(match[3])

  const date = new Date(yyyy, mm - 1, dd)

  return date
}

function formatDate(date: Date): string {
  const day = String(date.getDate()).padStart(2, '0')
  const month = String(date.getMonth() + 1).padStart(2, '0')
  const year = date.getFullYear()

  return `${day}/${month}/${year}`
}

function fundRecordToRaw(record: FundRecord): RawFundRecord {
  return {
    ...record,
    doc_date: record.doc_date ? formatDate(record.doc_date) : null,
  }
}

async function handlePopulateDb() {
  try {
    const response = await fetch('/admin/import-funds', {
      method: 'POST',
    })

    if (!response.ok) {
      const text = await response.text()
      throw new Error(`Errore import: ${response.status} - ${text}`)
    }
    const res2 = await fetch('/api/data')
    if (!res2.ok) {
      throw new Error(`Errore HTTP ${res2.status} nel ricaricare i dati`)
    }

    const data = await res2.json()

    records.value = data.map(
      (r: RawFundRecord): FundRecord => ({
        ...r,
        doc_date: parseDocDate(r.doc_date),
      }),
    )

    alert('Database popolato/aggiornato correttamente.')
  } catch (error) {
    console.error('Errore durante la popolazione del database:', error)
    alert('Errore durante la popolazione del database, controlla la console.')
  }
}
</script>

<template>
  <h1>Esercizio Vue - Funds Table</h1>
  <DataTable :items="records" @edit="handleEditRequest" @populate="handlePopulateDb" />

  <EditForm v-if="recordToEdit" :record="recordToEdit" @save="handleUpdate" @cancel="cancelEdit" />
</template>

<style scoped></style>
