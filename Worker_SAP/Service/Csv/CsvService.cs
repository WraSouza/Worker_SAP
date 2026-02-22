using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using System.Text;
using Worker_SAP.Model;
using Worker_SAP.Repository.Csv;

namespace Worker_SAP.Service.Csv
{
    public class CsvService(ICsvRepository csvRepository) : ICsvService
    {
        private static readonly string _pastaRaiz = @"C:\Users\wladimir.souza\Documents\DesafioPowerOne";

        private static readonly string _processed = Path.Combine(_pastaRaiz, "processed");

        private static readonly string _error = Path.Combine(_pastaRaiz, "error");

        private readonly string _caminhoComErro = "Arquivos Para Reprocessar";

        DateTime inicio = DateTime.Now;       

        public async Task ProcessarArquivoAsync(string caminhoArquivo)
        {
            int lidos = 0;
            int inseridos = 0;
            int ignorados = 0;
            int comErro = 0;
            int contadorLinha = 1;
            List<Item> itensComErro = [];
            List<int> linhaCsv = [];

            var itens = csvRepository.LerRegistros(caminhoArquivo);
            lidos = itens.Count();

            foreach (var item in itens)
            {
                contadorLinha++;

                if (string.IsNullOrEmpty(item.ItemName) || string.IsNullOrEmpty(item.ItemCode) || string.IsNullOrEmpty(item.UnidadeMedida))
                {
                    comErro++;

                    itensComErro.Add(item);                   

                    linhaCsv.Add(contadorLinha);

                    continue;
                }

                inseridos++;

            }

            if (comErro > 0)
            {               
                GerarLog(caminhoArquivo, inicio, lidos, inseridos, ignorados, comErro, "Campos Obrigatórios Não Preenchidos Totalmente", linhaCsv);
                MoverArquivo(caminhoArquivo, _error);
                CriarArquivoCsvItensErro(itensComErro);
            }

            if (comErro == 0 && lidos == inseridos)
            {
                GerarLog(caminhoArquivo, inicio, lidos, inseridos, ignorados, comErro, "Realizado Com Sucesso", linhaCsv);
                MoverArquivo(caminhoArquivo, _processed);
            }

        }

        private void CriarArquivoCsvItensErro(List<Item> itensComErro)
        {
            string caminhoCompleto = Path.Combine(_error, _caminhoComErro);

            if (!Directory.Exists(caminhoCompleto))
            {
                Directory.CreateDirectory(caminhoCompleto);
            }

            string caminhoDestino = Path.Combine(caminhoCompleto, $"ERROS_{DateTime.Now:yyyyMMdd}.csv");

            var config = new CsvConfiguration(CultureInfo.InvariantCulture) { Delimiter = ";" };

            using (var writer = new StreamWriter(caminhoDestino))
            using (var csv = new CsvWriter(writer, config))
            {                
                csv.WriteRecords(itensComErro);
            }

        }

        private static void MoverArquivo(string caminhoArquivo, string destino)
        {
            var nomeArquivo = Path.GetFileName(caminhoArquivo);

            var caminhoDestino = Path.Combine(destino, nomeArquivo);

            if (File.Exists(caminhoDestino))
            {
                File.Delete(caminhoDestino);
            }
            File.Move(caminhoArquivo, caminhoDestino);
        }

        private static void GerarLog(string arquivo, DateTime inicio, int lidos, int inseridos, int ignorados, int erros, string status, List<int> linhaCsv)
        {
            string pastaLogs = @"C:\Users\wladimir.souza\Documents\DesafioPowerOne\logs";

            if(linhaCsv.Count == 0)
            {
                linhaCsv.Add(0);
            }

            // Nome do arquivo de log baseado na data para não sobrescrever
            string nomeLog = $"log_{DateTime.Now:yyyyMMdd_HHmm}.txt";
            string caminhoLog = Path.Combine(pastaLogs, nomeLog);

            // Montando o conteúdo conforme exigências
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("--------------------------------------------------");
            sb.AppendLine($"DATA/HORA: {DateTime.Now:dd/MM/yyyy HH:mm:ss}");
            sb.AppendLine($"ARQUIVO: {arquivo}");
            sb.AppendLine($"STATUS: {status}");
            sb.AppendLine($"INÍCIO: {inicio:HH:mm:ss}");
            sb.AppendLine($"FIM: {DateTime.Now:HH:mm:ss}");
            sb.AppendLine($"LINHA(S) COM FALHA(S): {string.Join(", ", linhaCsv)}");
            sb.AppendLine($"- Registros Lidos: {lidos}");
            sb.AppendLine($"- Registros Inseridos: {inseridos}");
            sb.AppendLine($"- Registros Ignorados: {ignorados}");
            sb.AppendLine($"- Registros com Erro: {erros}");
            sb.AppendLine("--------------------------------------------------\n");
            
            File.AppendAllText(caminhoLog, sb.ToString());
        }
    }
}
