# Progetto di Applicazioni Web 2024 (No PISSIR)
Applicazione web per la gestione di parcheggi dotati di MWBot per la ricarica di auto elettriche, realizzata in C# tramite il framework .NET; interfaccia grafica realizzata con CSHTML e CSS utilizzando l'approccio Razor Pages.

## Link repository github
https://github.com/robmala/ProgettoAPPWEB24

## Installazione da github
Clonare la repository da github cliccando sul tasto "<> Code " e copiando il link HTTPS.  
Aprire Visual Studio e selezionare la voce "Clona un repository", incollare il link, selezionare la cartella dove si vuole clonare il progetto e cliccare sul tasto "Clona".

## Installazione da cartella compressa
Decomprimere la cartella del progetto e posizionarla nel percorso preferito. Aprire la cartella decompressa e fare doppio click con il tasto sinistro del mouse sul file "ProgettoAPPWEB24.sln" per aprirlo con Visual Studio.

## Setup
Prima di avviare l'applicazione assicurarsi che siano presenti i segreti utente del progetto: (dentro Visual Studio) tasto destro sulla voce "ProgettoAPPWEB24", selezionare "Gestisci segreti utente" ( o "Manage user secrets"), se il file che si apre risulta vuoto copiarci all'interno i dati presenti nel file "segreti.txt" presente nella cartella del progetto.

## Avvio
Far partire l'applicazione usando il protocollo https assicurandosi che venga aperta sulla porta 7174 o 5216.  
Se non dovesse aprirsi su una delle porte indicate, modificare il file "launchSettings.json" sotto la cartella "Properties" aggiungendo nel blocco "https": { "applicationUrl": "https://localhost:7174;https://localhost:5216" }.
Salvare il file e far partire l'applicazione.

```
## Usage
Utente di prova:
- Admin: email = admin@admin.com; password = Pa$$w0rd
- Utente base: registrarsi sull'applicazione come utente del parcheggio
```

## Author
Roberto Malavasi (20033809) - Università del Piemonte Orientale
