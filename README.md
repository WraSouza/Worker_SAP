# Worker SAP

## 📌 Sobre
O **Worker SAP** é uma aplicação desenvolvida em **.NET 10** que roda como serviço do Windows.  
Sua principal função é automatizar o processamento de arquivos **CSV**, realizando o cadastro de informações no **SAP Business One** via **Service Layer** e organizando os arquivos em pastas específicas.

---

## ⚙️ Funcionalidades
- Leitura automática de arquivos `.csv` em uma pasta definida.
- Cadastro de dados no **SAP B1** utilizando a **Service Layer**.
- Verificação da existência de registros antes do cadastro.
- Movimentação dos arquivos processados para pastas específicas (ex.: sucesso, erro, processados).
- Execução contínua como **Windows Service**, sem necessidade de interação manual.

---

## 🛠️ Tecnologias utilizadas
- **.NET 10**
- **SAP Business One Service Layer**
- **Windows Service**

---

## 📂 Estrutura de funcionamento
1. O serviço monitora uma pasta configurada.
2. Ao identificar um arquivo `.csv`, realiza a leitura dos dados.
3. Faz login no SAP B1 via Service Layer.
4. Verifica se os dados já existem.
5. Cadastra as informações no sistema.
6. Move o arquivo para a pasta correspondente (sucesso/erro).

---

## 🚀 Instalação e uso
1. Clone este repositório:
   ```bash
   git clone https://github.com/seu-usuario/worker-sap.git
2. Configure o projeto no Visual Studio ou outro ambiente compatível com .NET 10
3. Crie/Ajuste os parâmetros de conexão com o SAPB1 para utilizar a Service Layer. Essa configuração pode ser feita no appsettings ou no secrets.json e deve seguir esse formato
```json
"SAPLogin": {
  "UserName": "username",
  "Password": "senha",
  "CompanyDB": "base_de_dados"
}

⚠️ Atenção: nunca compartilhe suas credenciais reais em repositórios públicos. Use variáveis de ambiente ou arquivos de configuração privados.
```
---


 ## 📦 Publicação do Serviço

Antes de criar o serviço no Windows, é necessário **publicar o projeto** para gerar o executável pronto para produção.

1. No Visual Studio, vá em **Build > Publish**.
2. Escolha uma pasta de destino (ex.: `C:\WorkerSAP\publish`).
3. O processo de publicação irá gerar o arquivo `Worker_SAP.exe` e todas as dependências necessárias.

Após a publicação, registre o serviço no Windows:

```bash
sc create WorkerSAP binPath= "C:\WorkerSAP\publish\WorkerSAP.exe"
```

Inicie o serviço
```bash
sc start WorkerSAP
```

## Fluxo de Funcionamento

<img width="457" height="512" alt="image" src="https://github.com/user-attachments/assets/42d75f08-7067-4358-ba56-5c3b27367650" />

## Fluxo Técnico

<img width="696" height="621" alt="FluxoTecnico Worker drawio" src="https://github.com/user-attachments/assets/ad62a8ff-c8f1-4aa1-ae2a-448c3a1ad031" />



  
