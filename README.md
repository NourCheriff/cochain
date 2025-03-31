# COCHAIN

![cochain-logo](./frontend/public/logo.png)

# Table of contents

1. [Prerequisiti](#prerequisiti)
2. [Setup](#setup)
3. [Utilizzo](#utilizzo)

## Prerequisiti

Per eseguire la nostra applicazione è necessario aver installato:

- [Node.js/npm](https://nodejs.org/en/download) versione 18 o superiore (la versione raccomandata è la 22)
- [Docker](https://docs.docker.com/engine/install/)
- [PostgreSQL](https://www.postgresql.org/download/)
- [.NET 8.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [ASP.NET Core Runtime 8.0](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [Metamask-extension](https://metamask.io/download) per la gestione degli account blockchain
- [.NET Core CLI tools](https://learn.microsoft.com/en-us/ef/core/get-started/overview/install#get-the-net-core-cli-tools)

Inoltre, è fondamentale avere un account attivo su [Metamask](https://portfolio.metamask.io/), in quanto il progetto prevede l'utilizzo di wallet blockchain per l'interazione con la rete.

## Setup

### 1. Clona il repository
   Inizia clonando il repository del progetto:

```bash
    git clone https://github.com/NourCheriff/cochain.git
    cd cochain
```

### 2. Crea il file `.env`
   All'interno della directory `frontend/`, crea un file denominato `.env` con la seguente struttura:

```
    SOLIDITY_VERSION="0.8.28"
    CLOUD_BLOCKCHAIN_URL="http://<ip>:<port>"
    CLOUD_CHAIN_ID=1337
    LOCAL_BLOCKCHAIN_URL="http://127.0.0.1:8545"
    LOCAL_CHAIN_ID=1337
    PRIVATE_KEY_DEPLOYER="<CHIAVE PRIVATA DELL'ACCOUNT DEPLOYER>"
```

Queste variabili saranno utilizzate successivamente dal file `hardhat.config.js`.

### 3. Installa le dipendenze frontend
   Spostati nella directory `frontend` e installa le dipendenze dell'applicazione web:

```bash
    cd frontend
    npm install
    cd ..
```

### 4. Installa le dipendenze backend
    Spostati nella directory `CochainAPI` e installa le dipendenze richieste:

```bash
    cd CochainAPI
    dotnet restore
```

### 5. Imposta la stringa di connessione al database
    Per consentire all'applicazione backend di interagire con il database, è necessario configurare la stringa di connessione.
    
    1. Apri il file `appsettings.json`, presente nella directory `CochainAPI/CochainAPI`

    2. Trova la sezione relativa alla connessione con il database:
```
    "ConnectionStrings": {
        "CochainDB": "Host=localhost;Port=<PORTA_DB>;Database=<NOME_DATABASE>;Username=<USERNAME_DB>;Password=<PASSWORD_DB>"
    },
```

    3. Sostituisci i seguenti segnaposto con i valori corretti:
    - `<PORTA_DB>` con la porta del database, solitamente è `5432`.
    - `<USERNAME_DB>` con il nome utente del database.
    - `<PASSWORD_DB>` con la password del database.

    Dopo aver aggiornato il file, il backend potrà connettersi correttamente al database.
    

### 6. Crea il database dell'applicazione
   Per inizializzare il database, esegui il seguente comando nella directory `CochainAPI`

```bash
    dotnet ef database update \
        --project CochainAPI.Data.Sql \
        --startup-project CochainAPI
```


### 7. Esegui lo script `install.sh`
   Nella directory principale del progetto, esegui lo script `install.sh` specificando il flag `-n` per creare una blockchain locale. Per far ciò, devi prima rendere eseguibile questo file con il comando `chmod`.

```bash
    chmod +x ./install.sh
    ./install.sh -n
```

Lo script configura i container Docker necessari per il funzionamento dei nodi della blockchain.

## Utilizzo

Per avviare l'applicazione, segui questi passaggi:

### 1. Avvia i nodi della blockchain
   Spostati nella directory `onchain` e avvia i containers:

```bash
    cd onchain
    docker compose up -d
    cd ..
```

### 2. Verifica lo stato della blockchain
   Assicurati che i nodi della blockchain siano attivi eseguendo il seguente comando:

```bash
    curl -X POST \ 
        --data '{"jsonrpc":"2.0","method":"eth_chainId","params":[],"id":1}' \
        127.0.0.1:8545
```

### 3. Deploy degli smart contracts
   Nella directory principale, esegui lo script `install.sh` specificando il flag `-d`. Questo permette di effettuare il _deploy_ degli _smart contracts_ sulla blockchain locale:

```bash
    ./install.sh -d
```

L'output di questo comando sarà nel seguente formato:

```bash
    Activity deployed to: <SMART_CONTRACT_ADDRESS>
    CarbonCredits deployed to: <SMART_CONTRACT_ADDRESS>
```

### 4. Verifica lo stato degli smart contracts deployati
    Assicurati che gli smart contracts siano stato deployati correttamente nella blockchain eseguendo il comando:

```bash
    curl -X POST http://localhost:8545 \
         -H "Content-Type: application/json" \
         -d '{
            "jsonrpc": "2.0",
            "method": "eth_getCode",
            "params": [<SMART_CONTRACT_ADDRESS>, "latest"],
            "id": 1
            }'
```

    Sostituendo il segnaposto <SMART_CONTRACT_ADDRESS> con l'indirizzo ottenuto nel passaggio precedente.

### 5. Avvia il backend
    Per avviare l'applicazione backend, segui questi passaggi:

    1. Apri un nuovo terminale e spostati nella directory `CochainAPI/CochainAPI`:

```bash
    cd <path>/cochain/CochainAPI/CochainAPI
```

    2. Prima di eseguire l'applicazione, imposta la password della mail per l'invio dell'OTP con il seguente comando:

```bash
    export emailinapppassword=<PASSWORD_EMAIL>
```
    Sostituendo `PASSWORD_EMAIL` con la password attuale della mail utilizzata per l'invio dell'OTP.

    3. Avvia l'applicazione backend eseguendo:

```bash
    dotnet run
```

> [!IMPORTANT]  
> È importante eseguire tutti questi comandi nello stesso terminale aperto, altrimenti la variabile di ambiente contenente la password della mail non sarà disponibile quando si avvia il backend. 

### 6. Avvia l'applicazione frontend
   In un nuovo terminale, spostati nella directory `frontend` e avvia l'applicazione web con il comando:

```bash
    cd <path>/cochain/frontend
    npm start
```

### 7. Accedi all'applicazione web
   Apri il browser e accedi all'applicazione all'indirizzo: `http://localhost:4200`
