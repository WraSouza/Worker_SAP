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
