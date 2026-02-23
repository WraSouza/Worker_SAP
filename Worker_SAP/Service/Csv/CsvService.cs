using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using System.Text;
using Worker_SAP.Model;
using Worker_SAP.Repository.Csv;
using Worker_SAP.Repository.Sap.BP;
using Worker_SAP.Repository.Sap.ItemSAP;
using Worker_SAP.Service.AuthService;

namespace Worker_SAP.Service.Csv
{
    public class CsvService(ICsvRepository csvRepository, IItemSAPRepository itemRepository,IBusinessPartnerRepository bpRepository, IAuthService authService) : ICsvService
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
            List<BusinessPartner> bpsComErro = [];
            List<int> linhaCsv = [];

           string nomeArquivo = Path.GetFileName(caminhoArquivo).ToLower();           

            //Chamar AuthService para fazer login
            LoginResponse loginResponse = await authService.LoginAsync();            

            if (loginResponse != null)
            {
                if (nomeArquivo.Contains("items"))
                {
                    var itens = csvRepository.LerRegistros<Item>(caminhoArquivo);
                    lidos = itens.Count();

                    itemRepository.ConfigurarSessao(loginResponse.SessionId);                   

                    foreach (var item in itens)
                    {
                        contadorLinha++;

                        bool exists = await itemRepository.VerificarExistenciaItem(item.ItemCode);                        

                        if (exists)
                        {
                            ignorados++;
                            
                            continue;
                        }                       

                        if (string.IsNullOrEmpty(item.ItemName) || string.IsNullOrEmpty(item.ItemCode) || string.IsNullOrEmpty(item.SalesUnit))
                        {
                            comErro++;

                            itensComErro.Add(item);

                            linhaCsv.Add(contadorLinha);

                            continue;
                        }

                        Item novoItem = new Item(item.ItemCode, item.ItemName);

                        await itemRepository.AdicionarItemAsync(novoItem);

                        inseridos++;

                    }

                    if (comErro > 0)
                    {                        
                        CriarArquivoCsvItensErro(itensComErro);
                    }

                    await ProcessarItensAsync(caminhoArquivo,inicio,lidos,inseridos,ignorados,comErro,linhaCsv); 

                }
                else if (nomeArquivo.Contains("businesspartner"))
                {
                    var itens = csvRepository.LerRegistros<BusinessPartner>(caminhoArquivo);
                    lidos = itens.Count();

                    bpRepository.ConfigurarSessao(loginResponse.SessionId);

                    foreach (var item in itens)
                    {
                        contadorLinha++;

                        bool exists = await bpRepository.VerificarExistenciaBPAsync(item.CardCode);

                        if (exists)
                        {
                            ignorados++;

                            continue;
                        }

                        if (PossuiCamposVazios(item))
                        {
                            comErro++;

                            bpsComErro.Add(item);

                            linhaCsv.Add(contadorLinha);

                            continue;
                        }

                        BusinessPartner businessPartner = new BusinessPartner(item.CardCode
                                                                               ,item.CardName
                                                                               ,item.TipoDocumento
                                                                               ,item.Documento
                                                                               ,item.Rua
                                                                               ,item.Numero
                                                                               ,item.Bairro
                                                                               ,item.Cidade
                                                                               ,item.Estado
                                                                               ,item.Pais);

                        //await bpRepository.AdicionarBPAsync(businessPartner);

                        inseridos++;
                    }

                    if (comErro > 0)
                    {
                        CriarArquivoCsvErro(bpsComErro,"BusinessPartner");
                    }

                    await ProcessarItensAsync(caminhoArquivo, inicio, lidos, inseridos, ignorados, comErro, linhaCsv);
                }
                else if (nomeArquivo.Contains("salesorder"))
                {
                    var itens = csvRepository.LerRegistros<SalesOrder>(caminhoArquivo);
                    lidos = itens.Count();

                    foreach (var item in itens)
                    {
                        
                    }
                }
            }
            else
            {
                GerarLog(caminhoArquivo, inicio, lidos, inseridos, ignorados, comErro, "Não Foi Possível Realizar Login no Sistema", linhaCsv);
                MoverArquivo(caminhoArquivo, _error);
            }

        }

        public static bool PossuiCamposVazios(object obj)
        {
            // Pega todas as propriedades públicas da classe
            var propriedades = obj.GetType().GetProperties();

            foreach (var prop in propriedades)
            {
                // Pega o valor da propriedade no objeto atual
                var valor = prop.GetValue(obj);

                // Se for string, verifica se está vazia. Se for outro objeto, verifica se está nulo.
                if (valor == null || (valor is string s && string.IsNullOrWhiteSpace(s)))
                {
                    return true; 
                }
            }

            return false; 
        }

        private async Task ProcessarItensAsync(string caminho, DateTime inicio, int lidos, int inseridos,int ignorados, int comErro,List<int> linhaCsv)
        {
            if (comErro > 0)
            {
                GerarLog(caminho, inicio, lidos, inseridos, ignorados, comErro, "Campos Obrigatórios Não Preenchidos Totalmente", linhaCsv);
                MoverArquivo(caminho, _error);
            }
            else
            {
                GerarLog(caminho, inicio, lidos, inseridos, ignorados, comErro, "Realizado Com Sucesso", linhaCsv);
                MoverArquivo(caminho, _processed);
            }

        }

        private void CriarArquivoCsvErro<T>(List<T> registrosComErro, string tipoEntidade) where T : class
        {
            // 1. Define o caminho da pasta
            string caminhoCompleto = Path.Combine(_error, _caminhoComErro);

            if (!Directory.Exists(caminhoCompleto))
            {
                Directory.CreateDirectory(caminhoCompleto);
            }

            // 2. Cria um nome de arquivo dinâmico (ex: ERROS_Items_20260223.csv)
            string nomeArquivo = $"ERROS_{tipoEntidade}_{DateTime.Now:yyyyMMdd_HHmm}.csv";
            string caminhoDestino = Path.Combine(caminhoCompleto, nomeArquivo);

            var config = new CsvConfiguration(CultureInfo.InvariantCulture) { Delimiter = ";" };

            using (var writer = new StreamWriter(caminhoDestino))
            using (var csv = new CsvWriter(writer, config))
            {
                // O WriteRecords aceita IEnumerable<T> automaticamente
                csv.WriteRecords(registrosComErro);
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

            if (linhaCsv.Count == 0)
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
