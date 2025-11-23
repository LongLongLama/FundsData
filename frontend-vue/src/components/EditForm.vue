<script setup lang="ts">
import { ref, onMounted } from 'vue'
import type { FundRecord } from '../types'
import Dialog from 'primevue/dialog'
import Button from 'primevue/button'
import InputText from 'primevue/inputtext'
import InputNumber from 'primevue/inputnumber'
import Textarea from 'primevue/textarea'
import Calendar from 'primevue/calendar'

const props = defineProps<{
  record: FundRecord
}>()

const emit = defineEmits(['save', 'cancel'])

const localRecord = ref({
  ...props.record,
  doc_date: props.record.doc_date ? new Date(props.record.doc_date) : null,
})

const isModalVisible = ref(false)

onMounted(() => {
  isModalVisible.value = true
})

function handleSaveChanges() {
  emit('save', localRecord.value)
}

function handleCancel() {
  emit('cancel')
}
</script>

<template>
  <Dialog
    v-model:visible="isModalVisible"
    header="Edit fund record"
    modal
    :style="{ width: '60vw', maxWidth: '1100px' }"
    :contentStyle="{ maxHeight: '70vh', overflowY: 'auto' }"
    :breakpoints="{ '960px': '90vw', '640px': '100vw' }"
    @hide="handleCancel"
  >
    <div class="p-fluid grid formgrid gap-3 p-3">
      <div class="col-12 mb-1 border-b surface-border pb-2">
        <h3 class="text-base font-semibold text-color">Document & fund metadata</h3>
        <p class="text-xs text-color-secondary">
          Attributes extracted from the original fund documentation.
        </p>
      </div>

      <div class="col-6">
        <label class="block mb-1 text-sm text-color">Filename</label>
        <InputText
          v-model="localRecord.filename"
          readonly
          class="cursor-not-allowed border hover:border-red-400"
        />
      </div>

      <div class="col-6">
        <label class="block mb-1 text-sm text-color">ISIN</label>
        <InputText
          v-model="localRecord.isin"
          readonly
          class="cursor-not-allowed border hover:border-red-400"
        />
      </div>

      <div class="col-8">
        <label class="block mb-1 text-sm text-color">Fund name</label>
        <InputText
          v-model="localRecord.fund_name"
          readonly
          class="cursor-not-allowed border hover:border-red-400"
        />
      </div>

      <div class="col-4 flex flex-col">
        <label class="block mb-1 text-sm text-color">Is Valid</label>
        <div class="flex items-center gap-2 mt-1">
          <i
            :class="[
              'pi text-lg',
              localRecord.is_valid
                ? 'pi-check-circle text-green-500'
                : 'pi-times-circle text-red-500',
            ]"
          />
          <span class="text-sm text-color">
            {{ localRecord.is_valid ? 'Valid' : 'Invalid' }}
          </span>
        </div>
      </div>

      <div class="col-6">
        <label class="block mb-1 text-sm text-color">File language</label>
        <InputText v-model="localRecord.file_lang" />
      </div>

      <div class="col-6">
        <label class="block mb-1 text-sm text-color">Document date</label>
        <Calendar v-model="localRecord.doc_date" dateFormat="dd/mm/yy" showIcon />
      </div>

      <!-- SECTION: Fees -->
      <div class="col-12 mt-4 mb-1 border-b surface-border pb-2">
        <h3 class="text-base font-semibold text-color">Costs</h3>
        <p class="text-xs text-color-secondary">
          Review and update fee levels and the associated explanatory logs.
        </p>
      </div>

      <div class="col-6">
        <label class="block mb-1 text-sm text-color">Caso Costi</label>
        <InputText v-model="localRecord.caso_costi" />
      </div>

      <div class="col-6">
        <label class="block mb-1 text-sm text-color">Entry Cost</label>
        <InputNumber
          v-model="localRecord.entry_costs"
          mode="decimal"
          :minFractionDigits="2"
          class="w-full"
        />
      </div>

      <div class="col-12">
        <label class="block mb-1 text-sm text-color">Entry Cost log</label>
        <Textarea v-model="localRecord.entry_costs_log" rows="2" autoResize />
      </div>

      <div class="col-6">
        <label class="block mb-1 text-sm text-color">Exit Cost</label>
        <InputNumber
          v-model="localRecord.exit_costs"
          mode="decimal"
          :minFractionDigits="2"
          class="w-full"
        />
      </div>

      <div class="col-6">
        <label class="block mb-1 text-sm text-color">Ongoing Cost</label>
        <InputNumber
          v-model="localRecord.ongoing_costs"
          mode="decimal"
          :minFractionDigits="2"
          class="w-full"
        />
      </div>

      <div class="col-12">
        <label class="block mb-1 text-sm text-color">Exit Cost log</label>
        <Textarea v-model="localRecord.exit_costs_log" rows="2" autoResize />
      </div>

      <div class="col-12">
        <label class="block mb-1 text-sm text-color">Ongoing Cost log</label>
        <Textarea v-model="localRecord.ongoing_costs_log" rows="2" autoResize />
      </div>
    </div>

    <template #footer>
      <Button label="Cancel" class="p-button-text" @click="handleCancel" />
      <Button label="Save changes" @click="handleSaveChanges" />
    </template>
  </Dialog>
</template>
