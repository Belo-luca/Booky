using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

/*
Ecco alcune idee per migliorare il programma **Booky** senza entrare nel dettaglio del codice:

### **Funzionalità da Aggiungere**  

4. **Wishlist e Prestiti**  
   - Aggiungere una sezione per i libri desiderati (da comprare in futuro)  
   - Tracciare i libri prestati (con nome della persona e data)  

### **Correzioni Importanti**  
1. **Bug nella Ricerca**  
   - Alcune funzioni di ricerca (es. per autore/genere) non mostrano tutti i risultati.  
   - La modifica di un libro non si salva correttamente nel file.  

2. **Problemi di Sicurezza**  
   - Le password sono salvate in chiaro (nessuna cifratura).  
   - Non ci sono controlli su input malevoli (es. caratteri speciali nei titoli).  

3. **Migliorie UI**  
   - Messaggi di conferma prima di eliminare un libro.  
   - Paginazione nella visualizzazione (es. "Mostra 10 libri per volta").  

4. **Ordinamento**  
   - Alcuni algoritmi di ordinamento non funzionano correttamente.  
   - Aggiungere ordinamento ascendente/descendente.  

5. **Gestione Errori**  
   - Messaggi più chiari se il file non esiste o è corroto.  
   - Evitare crash con input non validi (es. lettere al posto di numeri).  

### **Idee Extra (Facoltative)**  
- **Commenti e Note**  
  - Permettere all’utente di aggiungere annotazioni personali ai libri.  
- **Grafici**  
  - Mostrare un istogramma dei generi preferiti.*/
internal class Program
{
    struct descrizione_libro
    {
        public string titolo;
        public string autore;
        public int anno;
        public string genere;
        public float prezzo;
        public int voto;
    }

    private static void Main(string[] args)
    {
        bool check = false;
        string file = accedi(ref check);
        file = file + ".txt";
        try
        {
            using (StreamReader sw = new StreamReader(file, true))
            {
            }
        }
        catch
        {
            using (FileStream fs = File.Create(file))
            {
            }
        }

        Console.WriteLine("Benvenuto in booky, la tua libreria a portata di click");
        Console.WriteLine(" ____   ____    ____    __  __  __    __");
        Console.WriteLine(" | __) / __ \\  / __ \\  | | / /  \\ \\__/ /");
        Console.WriteLine(" | _ \\ | || |  | || |  | |/ /    \\    /");
        Console.WriteLine(" ||_) || || |  | || |  | |\\ \\     |  |");
        Console.WriteLine(" | __/ \\____/  \\____/  |_| \\_\\    |__|");
        int scelta = 0;
        int contatore = 0;
        using (StreamReader sr = new StreamReader(file))
        {
            string riga;
            while ((riga = sr.ReadLine()) != null)
            {
                contatore++;
            }
        }
        descrizione_libro[] libri = new descrizione_libro[1000+contatore];
        int N = caricaLibri(libri, file);

        while (scelta != 7)
        {
            Utente ciao;
            ciao.lost_password_1 = "bello";


            Console.WriteLine("\nScegli un'opzione:");
            Console.WriteLine("10. Come funziona");
            Console.WriteLine("1. Aggiungi un libro");
            Console.WriteLine("2. Visualizza tutti i libri");
            Console.WriteLine("3. Ricerca un libro");
            Console.WriteLine("4. Modifica un libro");
            Console.WriteLine("5. Elimina un libro");
            Console.WriteLine("6. Riordina libri");
            Console.WriteLine("7. Statistiche");
            Console.WriteLine("8. La tua wishlist dei libri che desideri");

            Console.Write("Inserisci la tua scelta: ");
            if (!int.TryParse(Console.ReadLine(), out scelta))
            {
                Console.WriteLine("Inserisci un numero valido.");
                continue;
            }

            switch (scelta)
            {
                case 10:
                    regolamento();
                    break;
                case 1:
                    AggiungiLibro(libri, file, ref N);
                    break;
                case 2:
                    VisualizzaLibri(libri, N);
                    break;
                case 3:
                    RicercaLibro(libri, N);
                    break;
                case 4:
                    ModificaLibro(libri, N);
                    break;
                case 5:
                    EliminaLibro(libri, file, ref N);
                    break;
                case 6:
                    RiordinaLibri(libri, N);
                    break;
                case 7:
                    Statistische(libri, N);
                    break;
                case 8:
                    TrovaGenerePreferito(libri, N);
                    break;
                default:
                    Console.WriteLine("Scelta non valida. Riprova.");
                    break;
            }
        }
    }
    #region Booky_libri
    static void AggiungiLibro(descrizione_libro[] biblioteca, string nomeFile, ref int N)
    {
        if (N >= biblioteca.Length)
        {
            Console.WriteLine("Hai raggiunto il limite massimo di libri.");
            return;
        }

        descrizione_libro nuovoLibro;
        Console.WriteLine("Aggiungi un libro");

        Console.Write("Titolo: ");
        bool controllo = false;
        do
        {
            nuovoLibro.titolo = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(nuovoLibro.titolo))
            {
                Console.WriteLine("Il titolo non può essere vuoto.");
            }
            else
            {
                controllo = true;
            }
        } while (!controllo);
        biblioteca[N].titolo = nuovoLibro.titolo;

        Console.Write("Autore: ");
        bool controllo2 = false;
        do
        {
            nuovoLibro.autore = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(nuovoLibro.autore))
            {
                Console.WriteLine("L'autore non può essere vuoto.");
            }
            else
            {
                controllo2 = true;
            }
        } while (!controllo2);
        biblioteca[N].autore = nuovoLibro.autore;

        Console.Write("Anno: ");
        nuovoLibro.anno = 0;
        int anno = nuovoLibro.anno;
        bool controllo3 = false;
        do
        {
            string anno2 = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(anno2))
            {
                Console.WriteLine("Inserisci un numero valido.");
            }
            else
            {
                if (!int.TryParse(anno2, out nuovoLibro.anno))
                {
                    Console.WriteLine("Inserisci un numero valido.");
                }
                else
                {
                    controllo3 = true;
                }
            }
        } while (!controllo3);
        biblioteca[N].anno = anno;

        Console.Write("Genere: ");
        bool controllo4 = false;
        do
        {
            nuovoLibro.genere = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(nuovoLibro.genere))
            {
                Console.WriteLine("Il genere non può essere vuoto.");
            }
            else
            {
                controllo4 = true;
            }
        } while (!controllo4);
        biblioteca[N].genere = nuovoLibro.genere;

        Console.Write("Prezzo: ");
        nuovoLibro.prezzo = 0;
        float prezzo = nuovoLibro.prezzo;
        bool controllo5 = false;
        do
        {
            string prezzo2 = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(prezzo2))
            {
                Console.WriteLine("Inserisci un numero valido.");
            }
            else
            {
                if (!float.TryParse(prezzo2, out nuovoLibro.prezzo))
                {
                    Console.WriteLine("Inserisci un numero valido.");
                }
                else
                {
                    controllo5 = true;
                }
            }
        } while (!controllo5);
        biblioteca[N].prezzo = nuovoLibro.prezzo;

        Console.Write("Voto: ");
        nuovoLibro.voto = 0;
        int voto = nuovoLibro.voto;
        bool controllo6 = false;
        do
        {
            string prezzo2 = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(prezzo2))
            {
                Console.WriteLine("Inserisci un numero valido.");
            }
            else
            {
                if (!int.TryParse(prezzo2, out nuovoLibro.voto))
                {
                    Console.WriteLine("Inserisci un numero valido.");
                }
                else
                {
                    controllo6 = true;
                }
            }
        } while (!controllo6);
        biblioteca[N].voto = nuovoLibro.voto;

        using (StreamWriter sw = new StreamWriter(nomeFile, true))
        {
            sw.WriteLine($"{nuovoLibro.titolo};{nuovoLibro.autore};{nuovoLibro.anno};{nuovoLibro.genere};{nuovoLibro.prezzo};{nuovoLibro.voto}");
        }
        Console.WriteLine("Libro aggiunto con successo.");
        N++;
    }

    static int caricaLibri(descrizione_libro[] libri, string nomeFile)
    {
        int i = 0;
        using (StreamReader sr = new StreamReader(nomeFile))
        {
            string riga;
            while ((riga = sr.ReadLine()) != null)
            {
                string[] dati = riga.Split(';');
                libri[i].titolo = dati[0];
                libri[i].autore = dati[1];
                libri[i].anno = int.Parse(dati[2]);
                libri[i].genere = dati[3];
                libri[i].prezzo = float.Parse(dati[4]);
                libri[i].voto = int.Parse(dati[5]);
                i++;
            }
        }
        return i;
    }

    static void VisualizzaLibri(descrizione_libro[] biblioteca, int N)
    {
        Console.WriteLine("\nVisualizza tutti i libri");
        if (N == 0)
        {
            Console.WriteLine("Nessun libro presente.");
            return;
        }

        for (int i = 0; i < N; i++)
        {
            Console.WriteLine($"\nLibro {i + 1}");
            Console.WriteLine($"Titolo: {biblioteca[i].titolo}");
            Console.WriteLine($"Autore: {biblioteca[i].autore}");
            Console.WriteLine($"Anno: {biblioteca[i].anno}");
            Console.WriteLine($"Genere: {biblioteca[i].genere}");
            Console.WriteLine($"Prezzo: {biblioteca[i].prezzo} euro");
            Console.WriteLine($"Voto: {biblioteca[i].voto}/10");
        }
    }

    static void regolamento()
    {
        Console.WriteLine("\nCome funziona booky?");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Aggiungi libro");
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("Puoi aggiungere massimo 1000 libri per volta. Le informazioni richieste sono: titolo, autore, anno, genere, prezzo e voto.");

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Visualizza libro");
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("Puoi visualizzare tutti i libri che hai inserito con le diverse informazioni.");

        Console.ForegroundColor= ConsoleColor.Red;
        Console.WriteLine("Ricerca libro");
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("Puoi cercare i libri per titolo, autore, genere, prezzo e voto; sia maggiore di un tot che un minore.");

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Modifica libro");
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("Puoi modificare le informazioni di un libro già presente, rimettendole tutte da capo.");

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Elimina libro");
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("Puoi eliminare un libro già presente, ma non puoi recuperarlo.");

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Riordina libro");
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("Puoi riordinare i libri in base a titolo, autore, anno, genere, prezzo e voto.");

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Esci");
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("Puoi uscire dal programma in qualsiasi momento. I tuoi dati verranno salvati automaticamente.");


        Console.WriteLine("Per iniziare, scegli un'opzione dal menu principale.");
        Console.WriteLine("Buon divertimento!");
    }

    static void RicercaLibro(descrizione_libro[] libri, int N)
    {
        Console.WriteLine("Per cosa vuoi cercare i libri");
        Console.WriteLine("1. Nome del libro");
        Console.WriteLine("2. Nome dell'autore");
        Console.WriteLine("3. Genere");
        Console.WriteLine("4. Fascio di prezzi");
        Console.WriteLine("5. Fascio di voti");
        Console.WriteLine("6. Fascio di anni");
        int scelta = 0;
        bool controllo = false;
        do
        {
            string check = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(check))
            {
                Console.WriteLine("Inserisci un numero valido.");
            }
            else
            {
                if (!int.TryParse(check, out scelta))
                {
                    Console.WriteLine("Inserisci un numero valido.");
                }
                else
                {
                    controllo = true;
                }
            }
        } while (!controllo);
        string opzione = "";
        switch (scelta) 
        {
            case 1:
                Console.WriteLine("Quale è il titolo del libro che cerchi");
                bool controllo1 = false;
                string nome;
                do
                {
                    nome = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(nome))
                    {
                        Console.WriteLine("Il nome non può essere vuoto.");
                    }
                    else
                    {
                        controllo1 = true;
                    }
                } while (!controllo1);
                opzione = "Titolo";
                Trovalibro_nome(nome ,libri, N, opzione);
                break;
            case 2:
                Console.WriteLine("Quale è il nome dell'autore che cerchi");
                bool controllo2 = false;
                string autore;
                do
                {
                    autore = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(autore))
                    {
                        Console.WriteLine("Il nome non può essere vuoto.");
                    }
                    else
                    {
                        controllo2 = true;
                    }
                } while (!controllo2);
                opzione = "Autore";
                Trovalibro_nome(autore, libri, N, opzione);
                break;
            case 3:
                Console.WriteLine("Quale è il genere");
                bool controllo3 = false;
                string genere;
                do
                {
                    genere = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(genere))
                    {
                        Console.WriteLine("Il nome non può essere vuoto.");
                    }
                    else
                    {
                        controllo3 = true;
                    }
                } while (!controllo3);
                opzione = "Genere";
                Trovalibro_nome(genere, libri, N, opzione);
                break;
            case 4:
                Console.WriteLine("Quale è il prezzo minimo che deve avere");
                float prezzo = 0;
                bool controllo4 = false;
                do
                {
                    string check = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(check))
                    {
                        Console.WriteLine("Inserisci un numero valido.");
                    }
                    else
                    {
                        if (!float.TryParse(check, out prezzo))
                        {
                            Console.WriteLine("Inserisci un numero valido.");
                        }
                        else
                        {
                            controllo4 = true;
                        }
                    }
                } while (!controllo4);
                Console.WriteLine("Quale è il prezzo massimo che deve avere");
                int prezzo2 = 0;
                bool controllo5 = false;
                do
                {
                    string check = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(check))
                    {
                        Console.WriteLine("Inserisci un numero valido.");
                    }
                    else
                    {
                        if (!int.TryParse(check, out prezzo2))
                        {
                            Console.WriteLine("Inserisci un numero valido.");
                        }
                        else
                        {
                            controllo5 = true;
                        }
                    }
                } while (!controllo5);
                Trovalibro_prezzo(prezzo, libri, N,prezzo2);
                break;
            case 5:
                Console.WriteLine("Quale è il voto minimo che deve avere? ");
                int voto = 0;
                bool controllo6 = false;
                do
                {
                    string check = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(check))
                    {
                        Console.WriteLine("Inserisci un numero valido.");
                    }
                    else
                    {
                        if (!int.TryParse(check, out voto))
                        {
                            Console.WriteLine("Inserisci un numero valido.");
                        }
                        else
                        {
                            controllo6 = true;
                        }
                    }
                } while (!controllo6);
                Console.WriteLine("Quale è il voto massimo che deve avere? ");
                int voto2 = 0;
                bool controllo7 = false;
                do
                {
                    string check = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(check))
                    {
                        Console.WriteLine("Inserisci un numero valido.");
                    }
                    else
                    {
                        if (!int.TryParse(check, out voto2))
                        {
                            Console.WriteLine("Inserisci un numero valido.");
                        }
                        else
                        {
                            controllo7 = true;
                        }
                    }
                } while (!controllo7);
                Trovalibro_voto(voto, libri, N, voto2);
                break; 
            case 6:
                Console.WriteLine("Quale è l'anno minimo che deve avere? ");
                int anno = 0;
                bool controllo8 = false;
                do
                {
                    string check = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(check))
                    {
                        Console.WriteLine("Inserisci un numero valido.");
                    }
                    else
                    {
                        if (!int.TryParse(check, out anno))
                        {
                            Console.WriteLine("Inserisci un numero valido.");
                        }
                        else
                        {
                            controllo8 = true;
                        }
                    }
                } while (!controllo8);
                Console.WriteLine("Quale è il voto massimo che deve avere? ");
                int anno2 = 0;
                bool controllo9 = false;
                do
                {
                    string check = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(check))
                    {
                        Console.WriteLine("Inserisci un numero valido.");
                    }
                    else
                    {
                        if (!int.TryParse(check, out anno2))
                        {
                            Console.WriteLine("Inserisci un numero valido.");
                        }
                        else
                        {
                            controllo9 = true;
                        }
                    }
                } while (!controllo9);
                Trovalibro_anno(anno, libri, N, anno2);
                break;


        }
    }

    #region Trova libri
    static void Trovalibro_nome(string nome, descrizione_libro[] biblioteca, int N, string opzione)
    {
        if (opzione == "Titolo")
        {
            for (int i = 0; i < N; i++)
            {
                if (nome == biblioteca[i].titolo)
                {
                    Console.WriteLine("titolo: " + biblioteca[i].titolo);
                    Console.WriteLine("autore: " + biblioteca[i].autore);
                    Console.WriteLine("anno: " + biblioteca[i].anno);
                    Console.WriteLine("genere: " + biblioteca[i].genere);
                    Console.WriteLine("prezzo: " + biblioteca[i].prezzo);
                    Console.WriteLine("voto: " + biblioteca[i].voto);
                }
            }
        } else if (opzione == "Autore") {
            for (int i = N; i < N; i++)
            {
                if (nome == biblioteca[i].autore)
                {
                    Console.WriteLine("titolo: " + biblioteca[i].titolo);
                }
            }
        } else if (opzione == "Genere")
        {
            for (int i = N; i < N; i++)
            {
                if (nome == biblioteca[i].genere)
                {
                    Console.WriteLine("titolo: " + biblioteca[i].titolo);
                }
            }
        }
    }
    static void Trovalibro_prezzo(float prezzo, descrizione_libro[] biblioteca, int N, float prezzo2)
    {
        for (int i = 0; i < N; i++)
        {
            if (biblioteca[i].prezzo>prezzo && biblioteca[i].prezzo < prezzo2)
            {
                Console.WriteLine("titolo: " + biblioteca[i].titolo + " scritto da " + biblioteca[i].autore);
            }
        }
    }
    static void Trovalibro_voto(int voto, descrizione_libro[] biblioteca, int N, int voto2)
    {
        for (int i = 0; i < N; i++)
        {
            if (biblioteca[i].voto > voto && biblioteca[i].voto < voto2)
            {
                Console.WriteLine("titolo: " + biblioteca[i].titolo + " scritto da " + biblioteca[i].autore);
            }
        }
    }
    static void Trovalibro_anno(int anno, descrizione_libro[] biblioteca, int N, int anno2)
    {
        for (int i = 0; i < N; i++)
        {
            if (biblioteca[i].anno > anno && biblioteca[i].anno < anno2)
            {
                Console.WriteLine("titolo: " + biblioteca[i].titolo + " scritto da " + biblioteca[i].autore);
            }
        }
    }
    #endregion
    static void ModificaLibro(descrizione_libro[] biblioteca, int N)
    {
        Console.WriteLine("Quale libro vuoi modificare?");
        bool controllo1 = false;
        string titolo;
        do
        {
            titolo = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(titolo))
            {
                Console.WriteLine("Il nome non può essere vuoto.");
            }
            else
            {
                controllo1 = true;
            }
        } while (!controllo1);
        for (int i = 0; i < N; i++)
        {
            if (biblioteca[i].titolo == titolo)
            {
                descrizione_libro nuovoLibro;
                Console.WriteLine("Inserisci: ");

                Console.Write("Titolo: ");
                bool controllo = false;
                do
                {
                    nuovoLibro.titolo = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(nuovoLibro.titolo))
                    {
                        Console.WriteLine("Il titolo non può essere vuoto.");
                    }
                    else
                    {
                        controllo = true;
                    }
                } while (!controllo);
                biblioteca[i].titolo = nuovoLibro.titolo;

                Console.Write("Autore: ");
                bool controllo2 = false;
                do
                {
                    nuovoLibro.autore = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(nuovoLibro.autore))
                    {
                        Console.WriteLine("L'autore non può essere vuoto.");
                    }
                    else
                    {
                        controllo2 = true;
                    }
                } while (!controllo2);
                biblioteca[i].autore = nuovoLibro.autore;

                Console.Write("Anno: ");
                nuovoLibro.anno = 0;
                int anno = nuovoLibro.anno;
                bool controllo3 = false;
                do
                {
                    string anno2 = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(anno2))
                    {
                        Console.WriteLine("Inserisci un numero valido.");
                    }
                    else
                    {
                        if (!int.TryParse(anno2, out nuovoLibro.anno))
                        {
                            Console.WriteLine("Inserisci un numero valido.");
                        }
                        else
                        {
                            controllo3 = true;
                        }
                    }
                } while (!controllo3);
                biblioteca[i].anno = anno;

                Console.Write("Genere: ");
                bool controllo4 = false;
                do
                {
                    nuovoLibro.genere = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(nuovoLibro.genere))
                    {
                        Console.WriteLine("Il genere non può essere vuoto.");
                    }
                    else
                    {
                        controllo4 = true;
                    }
                } while (!controllo4);
                biblioteca[i].genere = nuovoLibro.genere;

                Console.Write("Prezzo: ");
                nuovoLibro.prezzo = 0;
                float prezzo = nuovoLibro.prezzo;
                bool controllo5 = false;
                do
                {
                    string prezzo2 = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(prezzo2))
                    {
                        Console.WriteLine("Inserisci un numero valido.");
                    }
                    else
                    {
                        if (!float.TryParse(prezzo2, out nuovoLibro.prezzo))
                        {
                            Console.WriteLine("Inserisci un numero valido.");
                        }
                        else
                        {
                            controllo5 = true;
                        }
                    }
                } while (!controllo5);
                biblioteca[i].prezzo = nuovoLibro.prezzo;

                Console.Write("Voto: ");
                nuovoLibro.voto = 0;
                int voto = nuovoLibro.voto;
                bool controllo6 = false;
                do
                {
                    string prezzo2 = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(prezzo2))
                    {
                        Console.WriteLine("Inserisci un numero valido.");
                    }
                    else
                    {
                        if (!int.TryParse(prezzo2, out nuovoLibro.voto))
                        {
                            Console.WriteLine("Inserisci un numero valido.");
                        }
                        else
                        {
                            controllo6 = true;
                        }
                    }
                } while (!controllo6);
                biblioteca[i].voto = nuovoLibro.voto;
            }
        }
    }

    static void EliminaLibro(descrizione_libro[] biblioteca, string nomeFile, ref int N)
    {
        Console.WriteLine("Quale libro vuoi eliminare?");
        bool controllo1 = false;
        string titolo;
        do
        {
            titolo = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(titolo))
            {
                Console.WriteLine("Il nome non può essere vuoto.");
            }
            else
            {
                controllo1 = true;
            }
        } while (!controllo1);
        for (int i = 0; i < N; i++)
        {
            if (biblioteca[i].titolo == titolo)
            {
                for (int j = i; j < N - 1; j++)
                {
                    biblioteca[j] = biblioteca[j + 1];
                }
                N--;
                break;
            }
        }
    }

    #region statistiche
    static void Statistische(descrizione_libro[] biblioteca, int N)
    {
        Console.WriteLine("Statistiche della libreria:");
        Console.WriteLine("Il numero di libri è: " + N);
        float prezzoMedio = prezzo_medio(biblioteca, N);
        Console.WriteLine("Il prezzo medio dei libri è: " + prezzoMedio + " euro");
        Console.WriteLine("Il libro più vecchio è: " + TrovaLibroPiuVecchio(biblioteca, N).titolo);
        Console.WriteLine("Il libro più nuovo è: " + TrovaLibro_Nuovo(biblioteca, N).titolo);
        Console.WriteLine("Il libro con il voto più alto è: " + TrovaLibroPiuVotato(biblioteca, N).titolo);
        Console.WriteLine("Il libro con il voto più basso è: " + TrovaLibroMenoVotato(biblioteca, N).titolo);
        Console.WriteLine("Il genere preferito è: " + TrovaGenerePreferito(biblioteca,N));
    }

    #region Statistiche_funzioni

    static float prezzo_medio(descrizione_libro[] biblioteca, int N)
    {
        float somma = 0;
        for (int i = 0; i < N; i++)
        {
            somma += biblioteca[i].prezzo;
        }
        return somma / N;
    }

    static descrizione_libro TrovaLibroPiuVecchio(descrizione_libro[] biblioteca, int N)
    {
        descrizione_libro piuVecchio = biblioteca[0];
        for (int i = 1; i < N; i++)
        {
            if (biblioteca[i].anno < piuVecchio.anno)
            {
                piuVecchio = biblioteca[i];
            }
        }
        return piuVecchio;
    }
    
    static descrizione_libro TrovaLibro_Nuovo(descrizione_libro[] biblioteca, int N)
    {
        descrizione_libro piu_nuovo = biblioteca[0];
        for (int i = 1; i < N; i++)
        {
            if (biblioteca[i].anno > piu_nuovo.anno)
            {
                piu_nuovo = biblioteca[i];
            }
        }
        return piu_nuovo;
    }

    static descrizione_libro TrovaLibroPiuVotato(descrizione_libro[] biblioteca, int N)
    {
        descrizione_libro piuVotato = biblioteca[0];
        for (int i = 1; i < N; i++)
        {
            if (biblioteca[i].voto > piuVotato.voto)
            {
                piuVotato = biblioteca[i];
            }
        }
        return piuVotato;
    }

    static descrizione_libro TrovaLibroMenoVotato(descrizione_libro[] biblioteca, int N)
    {
        descrizione_libro menoVotato = biblioteca[0];
        for (int i = 1; i < N; i++)
        {
            if (biblioteca[i].voto < menoVotato.voto)
            {
                menoVotato = biblioteca[i];
            }
        }
        return menoVotato;
    }

    static string TrovaGenerePreferito(descrizione_libro[] biblioteca, int N)
    {
        string[] strings = new string[N];
        for (int i = 0; i < N; i++)
        {
            strings[i] = biblioteca[i].genere;
        }
        string[] strings2 = strings.Distinct().ToArray();
        descrivi[] descrizioni = new descrivi[strings2.Length];
        for (int i = 0; i < strings2.Length; i++)
        {
            for (int j = 0; j < N; j++)
            {
                if (strings2[i] == biblioteca[j].genere)
                {
                    descrizioni[i].genere = strings2[i];
                    descrizioni[i].count++;
                }
            }
        }
        string generePreferito = descrizioni[0].genere;
        int maxCount = descrizioni[0].count;
        for (int i = 1; i<strings2.Length; i++)
        {
            if (descrizioni[i].count > maxCount)
            {
                maxCount = descrizioni[i].count;
                generePreferito = descrizioni[i].genere;
            }
        }
        return generePreferito;
    }

    public struct descrivi
    {
        public string genere;
        public int count;
    }
    #endregion

    #endregion
    #region Riordina libri
    static void RiordinaLibri(descrizione_libro[] biblioteca, int N)
    {
        Console.WriteLine("Per cosa vuoi riordinare i libri?");
        Console.WriteLine("1. Titolo");
        Console.WriteLine("2. Autore");
        Console.WriteLine("3. Anno");
        Console.WriteLine("4. Genere");
        Console.WriteLine("5. Prezzo");
        Console.WriteLine("6. Voto");
        int scelta = 0;
        bool controllo = false;
        do
        {
            string check = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(check))
            {
                Console.WriteLine("Inserisci un numero valido.");
            }
            else
            {
                if (!int.TryParse(check, out scelta))
                {
                    Console.WriteLine("Inserisci un numero valido.");
                }
                else
                {
                    controllo = true;
                }
            }
        } while (!controllo);
        switch (scelta)
        {
            case 1:
                RiordinaTitolo(biblioteca, N);
                break;
            case 2:
                RiordinaAutore(biblioteca, N);
                break;
            case 3:
                RiordinaAnno(biblioteca, N);
                break;
            case 4:
                RiordinaGenere(biblioteca, N);
                break;
            case 5:
                RiordinaPrezzo(biblioteca, N);
                break;
            case 6:
                RiordinaVoto(biblioteca, N);
                break;
        }
    }

    static void RiordinaTitolo(descrizione_libro[] biblioteca, int N)
    {
        bool scambio = true;
        for (int i = 0; i < N && scambio; i++)
        {
            scambio = false;
            for (int j = 0; j < N - i -1; j++)
            {
                if (biblioteca[i].titolo.CompareTo(biblioteca[j].titolo) > 0)
                {
                    descrizione_libro temp = biblioteca[i];
                    biblioteca[i] = biblioteca[j];
                    biblioteca[j] = temp;
                }
            }
        }
    }

    static void RiordinaAutore(descrizione_libro[] biblioteca, int N)
    {
        bool scambio = true;
        for (int i = 0; i < N && scambio; i++)
        {
            scambio = false;
            for (int j = 0; j < N - i - 1; j++)
            {
                if (biblioteca[i].autore.CompareTo(biblioteca[j].autore) > 0)
                {
                    descrizione_libro temp = biblioteca[i];
                    biblioteca[i] = biblioteca[j];
                    biblioteca[j] = temp;
                }
            }
        }
    }

    static void RiordinaAnno(descrizione_libro[] biblioteca, int N)
    {
        bool scambio = true;
        for (int i = 0; i < N && scambio; i++)
        {
            scambio = false;
            for (int j = 0; j < N - i - 1; j++)
            {
                if (biblioteca[i].anno.CompareTo(biblioteca[j].anno) > 0)
                {
                    descrizione_libro temp = biblioteca[i];
                    biblioteca[i] = biblioteca[j];
                    biblioteca[j] = temp;
                }
            }
        }
    }

    static void RiordinaGenere(descrizione_libro[] biblioteca, int N)
    {
        bool scambio = true;
        for (int i = 0; i < N && scambio; i++)
        {
            scambio = false;
            for (int j = 0; j < N - i - 1; j++)
            {
                if (biblioteca[i].genere.CompareTo(biblioteca[j].genere) > 0)
                {
                    descrizione_libro temp = biblioteca[i];
                    biblioteca[i] = biblioteca[j];
                    biblioteca[j] = temp;
                }
            }
        }
    }

    static void RiordinaPrezzo(descrizione_libro[] biblioteca, int N)
    {
        bool scambio = true;
        for (int i = 0; i < N && scambio; i++)
        {
            scambio = false;
            for (int j = 0; j < N - i - 1; j++)
            {
                if (biblioteca[i].prezzo.CompareTo(biblioteca[j].prezzo) > 0)
                {
                    descrizione_libro temp = biblioteca[i];
                    biblioteca[i] = biblioteca[j];
                    biblioteca[j] = temp;
                }
            }
        }
    }

    static void RiordinaVoto(descrizione_libro[] biblioteca, int N)
    {
        bool scambio = true;
        for (int i = 0; i < N && scambio; i++)
        {
            scambio = false;
            for (int j = 0; j < N - i - 1; j++)
            {
                if (biblioteca[i].voto.CompareTo(biblioteca[j].voto) > 0)
                {
                    descrizione_libro temp = biblioteca[i];
                    biblioteca[i] = biblioteca[j];
                    biblioteca[j] = temp;
                }
            }
        }
    }

    #endregion

    #endregion



    #region Booky_utenti

    static void registrati(ref int N, Utente[] utenti)
    {
        bool controllo;
        string nomeutente;
        #region NOME_UTENTE
        do
        {
            controllo = false;
            Console.WriteLine("Inserisci il nome dell'utente");
            bool controllo1 = false;
            do
            {
                nomeutente = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(nomeutente))
                {
                    Console.WriteLine("Il nome non può essere vuoto.");
                }
                else
                {
                    controllo1 = true;
                }
            } while (!controllo1);
            for (int i = 0; i < N; i++)
            {
                if (nomeutente.ToLower() == (utenti[i].nome_utente).ToLower())
                {
                    controllo = true;
                    break;
                }
            }
            utenti[N].nome_utente = nomeutente;
        } while (controllo);
        #endregion
        #region PASSWORD
        bool corretto = true;
        do
        {
            Console.WriteLine("Inserisci la password");
            string password = "";
            string password_conferma = "";

            do
            {
                ConsoleKeyInfo tasto = Console.ReadKey(true);

                if (tasto.Key == ConsoleKey.Enter && password != "")
                {
                    Console.WriteLine();
                    break;
                }
                else if (tasto.Key == ConsoleKey.Backspace)
                {
                    if (password.Length > 0)
                    {
                        password = password.Substring(0, password.Length - 1);
                        Console.Write("\b \b");
                    }
                }
                else if (!char.IsControl(tasto.KeyChar))
                {
                    char carattere = tasto.KeyChar;
                    password += carattere;
                    Console.Write("*");
                }
            } while (true);
            Console.WriteLine("Inserisci la password");
            do
            {
                ConsoleKeyInfo tasto = Console.ReadKey(true);

                if (tasto.Key == ConsoleKey.Enter && password_conferma != "")
                {
                    Console.WriteLine();
                    break;
                }
                else if (tasto.Key == ConsoleKey.Backspace)
                {
                    if (password_conferma.Length > 0)
                    {
                        password_conferma = password_conferma.Substring(0, password_conferma.Length - 1);
                        Console.Write("\b \b");
                    }
                }
                else if (!char.IsControl(tasto.KeyChar))
                {
                    char carattere = tasto.KeyChar;
                    password_conferma += carattere;
                    Console.Write("*");
                }
            } while (true);
            if (password != password_conferma)
            {
                Console.WriteLine("Le password non coincidono");
                corretto = false;
            }
            else
            {
                corretto = true;
            }
            utenti[N].password = password;
        } while (corretto == false);
        #endregion
        #region RECUPERO PASSWORD
        int scelto = 0;
        do
        {
            Console.WriteLine("inserisci il numero della a cui domanda vuoi rispondere? \n 1. inserisci il nome del/la tuo/a migliore amico/a d'infanzia \n 2. inserisci il nome del tuo animale domestico \n 3. inserisci il nome di tua mamma \n 0.uscire");
            scelto = Convert.ToInt32(Console.ReadLine());
            switch (scelto)
            {
                case 1:
                    Console.WriteLine("inserisci il nome del/la tuo/a migliore amico/a d'infanzia");
                    utenti[N].lost_password_1 = Console.ReadLine();
                    break;
                case 2:
                    Console.WriteLine("inserisci il nome del tuo animale domestico");
                    utenti[N].lost_password_2 = Console.ReadLine();
                    break;
                case 3:
                    Console.WriteLine("inserisci il nome di tua mamma");
                    utenti[N].lost_password_3 = Console.ReadLine();
                    break;
            }
        } while (scelto == 0 && ((utenti[N].lost_password_1).Trim() != "") | ((utenti[N].lost_password_2).Trim() != "") | ((utenti[N].lost_password_1).Trim() != ""));
        for (int i = 0; i < N; i++)
        {
            if ((utenti[N].lost_password_1).Trim() == "")
            {
                utenti[N].lost_password_1 = "Nessuna risposta";
            }
            else if ((utenti[N].lost_password_2).Trim() == "")
            {
                utenti[N].lost_password_2 = "Nessuna risposta";
            }
            else if ((utenti[N].lost_password_3).Trim() == "")
            {
                utenti[N].lost_password_3 = "Nessuna risposta";
            }
        }
        #endregion
        N++;
    }


    #region struct info
    struct Utente
    {
        public string nome_utente;
        public string password;
        public string lost_password_1;
        public string lost_password_2;
        public string lost_password_3;
        public int tentativi_password;
    }
    #endregion

    static int caricautenti(string nome, Utente[] utenti)
    {
        int i = 0;
        if (!File.Exists(nome)) return i;
        #region prende il file e lo legge
        try
        {
            foreach (var riga in File.ReadLines(nome))
            {
                string[] dati = riga.Split(';');
                if (dati.Length >= 6)
                {
                    utenti[i].nome_utente = dati[0];
                    utenti[i].password = dati[1];
                    utenti[i].lost_password_1 = dati[2];
                    utenti[i].lost_password_2 = dati[3];
                    utenti[i].lost_password_3 = dati[4];
                    utenti[i].tentativi_password = Convert.ToInt32(dati[5]);
                    i++;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Errore durante la lettura: " + ex.Message);
        }
        #endregion

        return i;
    }

    static void Registrati(ref int N, Utente[] utenti, string nome_file)
    {
        #region nome per la registrazione
        Console.Write("Inserisci il nome utente: ");
        string nuovo_nome;
        while (true)
        {
            nuovo_nome = Console.ReadLine();
            bool esiste = false;
            for (int i = 0; i < N; i++)
            {
                if (utenti[i].nome_utente.Equals(nuovo_nome, StringComparison.OrdinalIgnoreCase))
                {
                    esiste = true;
                    break;
                }
            }
            if (esiste)
                Console.Write("Nome già usato, inserisci un altro: ");
            else
                break;
        }
        #endregion

        #region pws
        string password = LeggiPassword("Inserisci la password: ");
        string conferma = LeggiPassword("Conferma la password: ");
        while (password != conferma)
        {
            Console.WriteLine("Le password non coincidono. Riprova.");
            password = LeggiPassword("Inserisci la password: ");
            conferma = LeggiPassword("Conferma la password: ");
        }
        #endregion

        #region domande recupero password
        string[] risposte = new string[3];
        do {
            Console.WriteLine("Per il recupero della password, rispondi alle seguenti domande:");
            Console.WriteLine("Inserisci il nome del/la tuo/a amico/a d'infanzia");
            risposte[0] = Console.ReadLine();
            Console.WriteLine("Inserisci il nome del tuo animale domestico");
            risposte[1] = Console.ReadLine();
            Console.WriteLine("Inserisci il nome di tua madre");
            risposte[2] = Console.ReadLine();
        } while ((risposte[0] == null || risposte[0].Trim() == "") && (risposte[1] == null || risposte[1].Trim() == "") && (risposte[2] == null || risposte[2].Trim() == ""));
        #endregion

        #region crea la descrizione
        utenti[N] = new Utente
        {
            nome_utente = nuovo_nome,
            password = password,
            lost_password_1 = risposte[0],
            lost_password_2 = risposte[1],
            lost_password_3 = risposte[2],
            tentativi_password = 0
        };
        #endregion
        N++;

        #region scrive il file
        SalvaUtenti(nome_file, utenti, N);
        Console.WriteLine("Registrazione completata.");
        #endregion
    }

    static void Accedi(ref int N, Utente[] utenti, string nome_file, ref string nome_persona, ref bool check)
    {
        #region nome per l'accesso
        Console.Write("Inserisci il nome utente: ");
        string nome = Console.ReadLine();
        int indice = -1;
        if (nome == "Amministratore101")
        {
            Adiminstrator(nome, N);
            return;
        }
        for (int i = 0; i < N; i++)
        {
            if (utenti[i].nome_utente == nome)
            {
                indice = i;
                break;
            }
        }
        if (indice == -1)
        {
            Console.WriteLine("Utente non trovato.");
            return;
        }
        #endregion

        #region password
        if (utenti[indice].tentativi_password >= 3) 
        {
            SalvaUtenti(nome_file, utenti, N);
            Console.WriteLine("Account bloccato. Recupera la password.");
            RecuperaPassword(utenti, indice, nome_file, N);
            return;
        }
        string input = LeggiPassword("Inserisci la password: ");
        #endregion
        #region verifica password
        if (input == utenti[indice].password)
        {
            nome_persona = utenti[indice].nome_utente;
            check = true;
            utenti[indice].tentativi_password = 0;
        }
        else
        {
            utenti[indice].tentativi_password++;
            Console.WriteLine("Password errata. Tentativi rimasti: " + (3 - utenti[indice].tentativi_password));
            if (utenti[indice].tentativi_password >= 3)
            {
                Console.WriteLine("Account bloccato. Recupera la password.");
                RecuperaPassword(utenti, indice, nome_file, N);
            }
        }
        SalvaUtenti(nome_file, utenti, N);
        #endregion
    }

    static void RecuperaPassword(Utente[] utenti, int i, string nome, int N)
    {
        #region crea_gruppi_di_domande
        string[] domande = {
            utenti[i].lost_password_1,
            utenti[i].lost_password_2,
            utenti[i].lost_password_3
        };
        string[] prompt = {
            "Nome del/la tuo/a migliore amico/a d'infanzia",
            "Nome del tuo animale domestico",
            "Nome di tua madre"
        };
        #endregion

        #region controlla_domande
        for (int j = 0; j < domande.Length; j++)
        {
            if (!string.IsNullOrWhiteSpace(domande[j]))
            {
                Console.Write(prompt[j] + ": ");
                string risposta = Console.ReadLine();
                if (risposta == domande[j])
                {
                    Console.WriteLine("Password: " + utenti[i].password);
                    utenti[i].tentativi_password = 0;
                    SalvaUtenti(nome, utenti, N);
                    return;
                }
            }
        }
        Console.WriteLine("Recupero fallito.");
        #endregion
    }

    static string LeggiPassword(string prompt)
    {
        Console.Write(prompt);
        string password = "";
        ConsoleKeyInfo key;
        do
        {
            key = Console.ReadKey(true);
            if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
            {
                password += key.KeyChar;
                Console.Write("*");
            }
            else if (key.Key == ConsoleKey.Backspace && password.Length > 0)
            {
                password = password[0..^1];
                Console.Write("\b \b");
            }
        } while (key.Key != ConsoleKey.Enter);
        Console.WriteLine();
        return password;
    }

    static void SalvaUtenti(string nomeFile, Utente[] utenti, int N)
    {
        using StreamWriter sw = new StreamWriter(nomeFile);

        for(int i = 0; i < N; i++)
        {
            string line = $"{utenti[i].nome_utente};{utenti[i].password};{utenti[i].lost_password_1};{utenti[i].lost_password_2};{utenti[i].lost_password_3};{utenti[i].tentativi_password}";
            sw.WriteLine(line);
        }

    }

    static void Adiminstrator(string nome, int N)
    {
        using (StreamReader sr = new StreamReader("utenti.txt"))
        {
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                //funzione split
                string[] spliit = line.Split(";");
                Console.WriteLine("nome utente:" + spliit[0]);
                Console.WriteLine("password utente:" + spliit[1]);
                if (spliit[2] == "")
                    Console.WriteLine("L'utente non ha fornito questo metodo di recupero password");
                else
                    Console.WriteLine("Il primo nome è:" + spliit[2]);
                if (spliit[3] == "")
                    Console.WriteLine("L'utente non ha fornito il secondo metodo di recupero password");
                else
                    Console.WriteLine("Il secondo nome è:" + spliit[3]);
                if (spliit[4] == "")
                    Console.WriteLine("L'utente non ha fornito il terzo metodo di recupero password");
                else
                    Console.WriteLine("Il terzo nome è:" + spliit[4]);
                if (spliit[5] == "3")
                    Console.WriteLine("L'utente ha bloccato l'account");
                else
                    Console.WriteLine("L'utente non ha bloccato l'account");
                Console.WriteLine("");
            }
        }
        return;
    }

    static string accedi(ref bool check)
    {
       string nome_persona = "";
       while (!check)
            {
            #region variabili
            string file_utenti = "utenti.txt";
            Utente[] utenti = new Utente[1000];
            int N = caricautenti(file_utenti, utenti);
            #endregion

            #region gestione_utenti
            Console.WriteLine("Hai già l'account? (yes/no)");
            string risposta = Console.ReadLine()?.Trim().ToLower();

            if (risposta == "yes")
            {
                Accedi(ref N, utenti, file_utenti, ref nome_persona, ref check);
            }
            else if (risposta == "no")
            {
                Registrati(ref N, utenti, file_utenti);
            }
       }
        #endregion
        return nome_persona;
    }
    #endregion
}