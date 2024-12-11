using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prove
{
    class Program
    {
        static void Main(string[] args)
        {
            //creo la lista di coppie key:value
            Dictionary<string, int> fileTypes = new Dictionary<string, int>();

            //richiamo tutte le path
            string suorcePath = Properties.Settings.Default.suorcePath;
            string destinationPath = Properties.Settings.Default.destinationFilePath;

            //creo lista di tutti i file presenti nella directory
            string[] fileList = Directory.GetFiles(suorcePath);

            //creo ciclo per elaborare ogni file presente nella cartella
            foreach (string file in fileList)
            {

                string extension = Path.GetExtension(file);

                if (fileTypes.ContainsKey(extension))
                {
                    fileTypes[extension]++;
                }

                else
                {
                    fileTypes.Add(extension, +1);
                }
            }

            //creo la cartella dove esportare il file se non esiste
            if (!Directory.Exists(destinationPath))
            {
                Directory.CreateDirectory(destinationPath);
            }


            string reportFile = Path.Combine(destinationPath, Properties.Settings.Default.exportFileName + ".csv");

            //Creo il file di report
            File.Create(reportFile).Dispose();

            //Genero la prima riga dove inserisco i titoli delle colonne
            string appendTitle = "Estensione, Conteggio" + Environment.NewLine;
            File.WriteAllText(reportFile, appendTitle);

            //ciclo per elaborare ogni coppia key:value nella lista ed inserirla nel file di report
            foreach (var extension in fileTypes)
            {
                File.AppendAllText(reportFile, extension.Key + ", " + extension.Value + Environment.NewLine);
            }
        }
    }
}
