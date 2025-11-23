<script setup lang="ts">
import type { FundRecord } from '../types'

import { ref } from 'vue'
import DataTable from 'primevue/datatable'
import Column from 'primevue/column'
import Button from 'primevue/button'
import InputText from 'primevue/inputtext'
import Select from 'primevue/select'
import Calendar from 'primevue/calendar'
import Checkbox from 'primevue/checkbox'

import { FilterMatchMode } from '@primevue/core/api'

defineProps<{
  items: FundRecord[]
}>()

const emit = defineEmits(['edit', 'populate'])

const filters = ref()

const initFilters = () => {
  filters.value = {
    global: { value: null, matchMode: FilterMatchMode.CONTAINS },
    fund_name: { value: null, matchMode: FilterMatchMode.CONTAINS },
    filename: { value: null, matchMode: FilterMatchMode.CONTAINS },
    isin: { value: null, matchMode: FilterMatchMode.CONTAINS },
    file_lang: { value: null, matchMode: FilterMatchMode.EQUALS },
    is_valid: { value: null, matchMode: FilterMatchMode.EQUALS },
    doc_date: { value: null, matchMode: FilterMatchMode.DATE_IS },
  }
}

initFilters()

const clearFilter = () => {
  initFilters()
}

const formatCurrency = (value: number) => {
  if (value === null || value === undefined) return ''
  return new Intl.NumberFormat('it-IT', { style: 'currency', currency: 'EUR' }).format(value)
}
</script>

<template>
  <div class="card">
    <DataTable
      v-model:filters="filters"
      :value="items"
      scrollable
      showGridlines
      paginator
      :rows="10"
      dataKey="isin"
      filterDisplay="menu"
      :globalFilterFields="['fund_name', 'isin', 'filename']"
    >
      <template #header>
        <div class="flex justify-between items-center gap-4">
          <div class="flex gap-2">
            <Button
              type="button"
              icon="pi pi-filter-slash"
              label="Clear"
              variant="outlined"
              @click="clearFilter()"
            />
            <Button
              type="button"
              icon="pi pi-database"
              label="Popola/Aggiorna database"
              severity="secondary"
              @click="emit('populate')"
            />
          </div>

          <span class="p-input-icon-left">
            <i class="pi pi-search" />
            <InputText v-model="filters['global'].value" placeholder="Cerca ovunque..." />
          </span>
        </div>
      </template>

      <template #empty> No record found. </template>

      <Column header="Edit" style="min-width: 8rem">
        <template #body="slotProps">
          <Button
            icon="pi pi-pencil"
            class="p-button-rounded p-button-text"
            @click="emit('edit', slotProps.data)"
          />
        </template>
      </Column>

      <Column field="filename" header="Filename" :sortable="true" style="min-width: 14rem">
        <template #filter="{ filterModel }">
          <InputText v-model="filterModel.value" type="text" placeholder="Cerca per filename..." />
        </template>
      </Column>

      <Column field="fund_name" header="Nome Fondo" :sortable="true" style="min-width: 16rem">
        <template #filter="{ filterModel }">
          <InputText
            v-model="filterModel.value"
            type="text"
            placeholder="Cerca per nome fondo..."
          />
        </template>
      </Column>

      <Column field="isin" header="ISIN" :sortable="true" style="min-width: 12rem">
        <template #filter="{ filterModel }">
          <InputText v-model="filterModel.value" type="text" placeholder="Cerca per ISIN..." />
        </template>
      </Column>

      <Column field="file_lang" header="Lingua" :sortable="true" style="min-width: 8rem">
        <template #filter="{ filterModel }">
          <Select
            v-model="filterModel.value"
            :options="['IT', 'EN', 'FR', 'DE']"
            placeholder="Seleziona"
            showClear
          />
        </template>
      </Column>

      <Column
        field="doc_date"
        header="Data"
        :sortable="true"
        dataType="date"
        style="min-width: 12rem"
      >
        <template #body="{ data }">
          {{ data.doc_date.toLocaleDateString('it-IT') }}
        </template>
        <template #filter="{ filterModel }">
          <Calendar v-model="filterModel.value" dateFormat="dd/mm/yy" placeholder="dd/mm/yyyy" />
        </template>
      </Column>

      <Column
        field="is_valid"
        header="Valido"
        :sortable="true"
        dataType="boolean"
        style="min-width: 10rem"
      >
        <template #body="{ data }">
          <i
            class="pi"
            :class="{
              'pi-check-circle text-green-500': data.is_valid,
              'pi-times-circle text-red-500': !data.is_valid,
            }"
          ></i>
        </template>
        <template #filter="{ filterModel }">
          <label for="isvalid-filter" class="font-bold"> Is Valid </label>
          <Checkbox
            v-model="filterModel.value"
            :indeterminate="filterModel.value === null"
            binary
            inputId="isvalid-filter"
          />
        </template>
      </Column>

      <Column
        field="caso_costi"
        header="Caso Costi"
        :sortable="true"
        style="min-width: 10rem"
      ></Column>

      <Column field="entry_costs" header="Entry Costs" :sortable="false" style="min-width: 10rem">
        <template #body="{ data }">
          {{ formatCurrency(data.entry_costs) }}
        </template>
      </Column>

      <Column field="exit_costs" header="Exit Costs" :sortable="false" style="min-width: 10rem">
        <template #body="{ data }">
          {{ formatCurrency(data.exit_costs) }}
        </template>
      </Column>

      <Column
        field="ongoing_costs"
        header="Ongoing Costs"
        :sortable="false"
        style="min-width: 10rem"
      >
        <template #body="{ data }">
          {{ formatCurrency(data.ongoing_costs) }}
        </template>
      </Column>

      <Column field="entry_costs_log" header="Entry Log" style="min-width: 15rem"></Column>
      <Column field="exit_costs_log" header="Exit Log" style="min-width: 15rem"></Column>
      <Column field="ongoing_costs_log" header="Ongoing Log" style="min-width: 15rem"></Column>
    </DataTable>
  </div>
</template>
