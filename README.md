# 📊 Funds Manager – Vue 3 + .NET + SQL Server + Docker

Un progetto full-stack completo sviluppato come esercizio tecnico che include:

- **Frontend** in Vue 3 + PrimeVue  
- **Backend** in C# (.NET 10) + Minimal API  
- **Database** SQL Server con Dapper  
- **Routing & Proxy** tramite Nginx  
- **Deploy locale** tramite Docker Compose  

---

## 🚀 Funzionalità principali

### **Frontend (Vue + PrimeVue)**
- Tabella dei fondi con:
  - filtri avanzati
  - ricerca globale
  - ordinamento
- Form di modifica singolo record
- UI con PrimeVue (in precedenza BootstrapVue Next)
- Bottone **“Popola Database”** per richiedere al backend l'import dei dati iniziali
- Proxy Nginx che:
  - serve i file statici del frontend
  - inoltra le chiamate API al backend

---

## 🖥️ Backend (.NET 10 Minimal API)

- REST API per:
  - recuperare tutti i fondi
  - aggiornare un fondo tramite ISIN
  - importare e sincronizzare i dati iniziali
- Chiamata a fonte esterna:  
  `https://sapeimarco.github.io/sample-json-api/sample.json`
- Mapping JSON → Fund model
- Persistenza tramite:
  - SQL Server
  - Dapper
  - Query SQL

---

## 🗄️ Database

È presente una sola tabella:

### **Funds**
Contiene i campi provenienti dal JSON remoto, tra cui:

- `isin` (PK)
- `filename`
- `file_lang`
- `fund_name`
- `doc_date`
- cost logs e valori numerici
- flag `is_valid` 

L’import automatico aggiorna i record se già presenti o li inserisce se nuovi.

---

## 🌐 Import dei dati

Nel frontend è presente il pulsante **"Popola database"**.

👉 Quando lo clicchi:
1. il frontend chiama `/api/admin/import-funds`
2. il backend scarica la lista fondi dal JSON remoto
3. li mappa in oggetti `Fund`
4. esegue `UPDATE` oppure `INSERT` nel database
5. il frontend ricarica la tabella

---

## 🐳 Avvio completo tramite Docker

Il progetto include una configurazione **docker-compose** che lancia:

- frontend (Vue + Nginx)
- backend (.NET)
- SQL Server su Linux

### 🏁 Avvio del sistema

Da terminale, nella cartella del progetto:

```sh
docker-compose up --build -V`
```
La flag -V assicura che i volumi vengano ricreati e che il database sia pulito.

#🌍 Come usare l'app

Una volta che Docker ha avviato tutti i servizi:

➡️ Apri il browser su [http://localhost:5173](http://localhost:5173)

