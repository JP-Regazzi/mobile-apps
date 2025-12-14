# AndroidApps (.NET MAUI) — Curso Software para Smartphone (UFRJ) — 2025.2

Este repositório reúne **6 aplicativos mobile** desenvolvidos em **.NET MAUI** durante a disciplina **Software para Smartphone** da **UFRJ**, no período **2025.2**. O objetivo do projeto é praticar conceitos essenciais de desenvolvimento para smartphones, incluindo navegação entre páginas, UI com XAML, binding, padrão de arquitetura, persistência local com SQLite e construção de apps completos do tipo CRUD.

## Tecnologias e ambiente

- **.NET MAUI**
- **Visual Studio 2022**
- **Emulador Android: Pixel 7**
- Persistência local: **SQLite**

---

## Estrutura do repositório

O repositório contém uma **solução** com os projetos correspondentes a cada aplicativo. Cada app pode ser executado individualmente selecionando-o como *Startup Project* no Visual Studio.

---

## Aplicativos incluídos (6)

A seguir está um resumo do que cada app faz e quais conceitos ele reforça.

### 1) App Número da Sorte
Aplicativo simples para gerar número aleatórios, reforçando:
- Estrutura básica de um app MAUI
- Componentes de UI (botões, labels, layouts)
- Manipulação de eventos

### 2) App Sorteio
Aplicativo demonstrativo de  em navegação e composição de telas, reforçando:
- Diferentes tipos de páginas no MAUI
- Navegação entre páginas
- Organização de UI por responsabilidades

### 3) App Shopping
Aplicativo no estilo “catálogo/galeria”, reforçando:
- Listas e apresentação de itens
- Uso de imagens e recursos
- Experiência de navegação e detalhes de item

### 4) App Jogo da Forca (MV / MVVM)
Aplicativo voltado a arquitetura e separação de responsabilidades, reforçando:
- Organização em camadas
- Binding
- Estrutura baseada em View / ViewModel (quando aplicável)

### 5) App Task CRUD (MVVM + SQLite)
Aplicativo completo para gerenciamento de tarefas com persistência local, reforçando:
- **CRUD** (Create/Read/Update/Delete)
- **SQLite** para persistência local
- **MVVM** (ViewModel, Binding, Commands)
- Fluxo de inclusão/edição/exclusão

### 6) App Academia
Aplicativo para controle de dados do contexto de academia, reforçando:
- Organização de telas e componentes
- Fluxos típicos de cadastro/consulta
- Boas práticas de organização do projeto
---

## Como executar o projeto

### Pré-requisitos
- **Visual Studio 2022** instalado
- Workload de desenvolvimento com **.NET MAUI**
- Android SDK configurado (instalado via Visual Studio)
- Um emulador Android disponível (foi utilizado o **Pixel 7**)

### Passo a passo
1. Clone este repositório:
   - git clone https://github.com/JP-Regazzi/mobile-apps.git
3. Abra a solução (.sln) no Visual Studio 2022
4. No painel Solution Explorer, escolha o app que deseja executar
5. Defina o projeto como inicial:
   - Clique com o botão direito no projeto do app → Set as Startup Project
6. Selecione o destino Android Emulator e escolha o emulador (Pixel 7)
7. Execute:
   - Pressione F5 ou clique em Run
   
