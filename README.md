# COCHAIN

![cochain-logo](./frontend/public/logo.png)

Software Security and Blockchain course project.

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

1. ## Clona il repository
   Inizia clonando il repository del progetto:

```
git clone https://github.com/NourCheriff/cochain.git
cd cochain
```

2. ## Crea il file `.env`
   All'interno della directory `frontend/`, crea un file denominato `.env` con la seguente struttura:

```
SOLIDITY_VERSION="0.8.28"
CLOUD_BLOCKCHAIN_URL="http://<ip>:<port>"
CLOUD_CHAIN_ID=1337
PRIVATE_KEY_DEPLOYER="<CHIAVE PRIVATA DELL'ACCOUNT METAMASK>"
LOCAL_BLOCKCHAIN_URL="http://127.0.0.1:8545"
LOCAL_CHAIN_ID=1337
```

Queste variabili saranno utilizzate successivamente dal file `hardhat.config.js`.

3. ## Installa le dipendenze frontend
   Spostati nella directory `frontend` e installa le dipendenze dell'applicazione web:

```
cd frontend
npm install
cd ..
```

4. ## Esegui lo script `install.sh`
   Nella directory principale del progetto, esegui lo script `install.sh` specificando il flag `-n` per creare una blockchain locale. Per far ciò, devi prima rendere eseguibile questo file con il comando `chmod`.

```
chmod +x ./install.sh
./install.sh -n
```

Lo script configura i container Docker necessari per il funzionamento dei nodi della blockchain.

5. ## Crea il database dell'applicazione
   Per inizializzare il database, esegui il seguente comando nella directory `CochainAPI`

```
cd CochainAPI
dotnet ef database update --project CochainAPI.Data.Sql --startup-project CochainAPI
```

## Utilizzo

Per avviare l'applicazione, segui questi passaggi:

1. ## Avvia i nodi della blockchain
   Spostati nella directory `onchain` e avvia i containers:

```
cd onchain
docker compose up -d
cd ..
```

2. ## Verifica lo stato della blockchain
   Assicurati che i nodi della blockchain siano attivi eseguendo il seguente comando:

```
curl -X POST --data '{"jsonrpc":"2.0","method":"eth_chainId","params":[],"id":1}' 127.0.0.1:8545
```

3. ## Deploy degli smart contracts
   Nella directory principale, esegui lo script `install.sh` specificando il flag `-d`. Questo permette di effettuare il _deploy_ degli _smart contracts_ sulla blockchain locale:

```
./install.sh -d
```

4. ## Avvia il backend
   In un nuovo terminale, avvia il backend dell'applicazione:

```
cd <path>/cochain/CochainAPI/CochainAPI
dotnet run
```

5. ## Avvia l'applicazione frontend
   In un nuovo terminale, vai nella directory `frontend` e avvia l'applicazione web con il comando:

```
cd <path>/cochain/frontend
npm start
```

6. ## Accedi all'applicazione web
   Apri il browser e accedi all'applicazione all'indirizzo: `http://localhost:4200`
