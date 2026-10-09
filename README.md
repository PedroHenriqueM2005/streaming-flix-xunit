# streaming-flix-xunit


## Visão geral

**StreamingFlix** é uma aplicação de console em **.NET 10** que implementa as regras de negócio de uma plataforma de streaming:

- **Classificação de planos** por quantidade de telas simultâneas (BÁSICO, PADRÃO e PREMIUM);
- **Cálculo de mensalidade com desconto** por fidelidade (10% para contratos de 6 a 11 meses e 20% para 12 meses ou mais);
- **Validação de acesso a conteúdo adulto** (idade ≥ 18 e controle parental desativado).

Toda a lógica fica na classe `PlanoStreamingService` (projeto `StreamingFlix.App`) e é 100% coberta por **testes unitários parametrizados** com **xUnit** (`[Theory]` + `[InlineData]`) no projeto `StreamingFlix.Tests`.

> Atividade em equipe da disciplina **Garantia da Qualidade de Software** — Prof. Daniel (pela ultima vez, não é o vorcaro) Henrique Matos de Paiva

| Papel | Responsabilidade |
|-------|------------------|
| **Desenvolvedor 1** (Backend / Core) | Configuração da solução via .NET CLI e implementação das regras em `PlanoStreamingService.cs`(QA / Testes) | Suíte de testes parametrizados `PlanoStreamingServiceTests.cs` com `[Theory]` + `[InlineData]` |
| **Desenvolvedor 3** (Docs / DevOps) | Repositório remoto, `.gitignore`, licença MIT e documentação (`README.md`) |


## Estrutura da solução

```
streaming-flix-xunit/
├── StreamingFlix.slnx                  # Solução (gerada por dotnet new sln)
├── StreamingFlix.App/                  # Projeto console (código de produção)
│   ├── StreamingFlix.App.csproj
│   ├── PlanoStreamingService.cs        # Regras de negócio dos planos
│   └── Program.cs
└── StreamingFlix.Tests/                # Projeto de testes unitários (xUnit)
    ├── StreamingFlix.Tests.csproj
    └── PlanoStreamingServiceTests.cs   # Testes com [Theory] + [InlineData]
```

## Requisitos técnicos

| Requisitos:
|-----------|--------|
| [.NET SDK](https://dotnet.microsoft.com/download/dotnet/10.0) | **10.0** (target `net10.0`) |
| xUnit | 2.9.3 |
| Microsoft.NET.Test.Sdk | 17.14.1 |
| xunit.runner.visualstudio | 3.1.4 |
| coverlet.collector | 6.0.4 |

## Como clonar e rodar a aplicação

```bash
# 1. Clone o repositório
git clone https://github.com/SEU-USUARIO/streaming-flix-xunit.git

# 2. Entre na pasta do projeto
cd streaming-flix-xunit

# 3. Restaure as dependências e compile a solução
dotnet build

# 4. Execute a aplicação de console
dotnet run --project StreamingFlix.App
```

##  Como executar os testes unitários (CLI)


Na raiz do repositório, execute:

```bash
dotnet test
```

Saída esperada:

```
Passed!  - Failed: 0, Passed: 9, Skipped: 0, Total: 9
```

Cada `[InlineData]` roda como um **teste individual** no relatório — por exemplo:

```
✓ PodeAcessarConteudoAdulto_VariosCasos_RetornaElegibilidadeCorreta(idade: 20, controleParentalAtivo: False, acessoEsperado: True)
```

## Cobertura dos testes parametrizados

A suíte `PlanoStreamingServiceTests` utiliza **3 métodos `[Theory]`** com **3 `[InlineData]` cada**, totalizando **9 casos de teste** que cobrem todos os métodos e regras do serviço:

**Teste 1 — `ObterClassificacaoPorQualidade` (retorno `string`):**

| `[InlineData]` | telasSimultaneas | classificação esperada |
|---------------|------------------|------------------------|
| 1 | `1` | `"BÁSICO"` |
| 2 | `2` | `"PADRÃO"` |
| 3 | `4` | `"PREMIUM"` |

**Teste 2 — `CalcularMensalidadeComDesconto` (retorno `int`):**

| `[InlineData]` | valorBase | mesesContratados | mensalidade esperada | cenário |
|---------------|-----------|------------------|----------------------|---------|
| 1 | `50` | `1` | `50` | Sem desconto (< 6 meses) |
| 2 | `50` | `6` | `45` | 10% de desconto (6 a 11 meses) |
| 3 | `50` | `12` | `40` | 20% de desconto (≥ 12 meses) |

**Teste 3 — `PodeAcessarConteudoAdulto` (retorno `bool`):**

| `[InlineData]` | idade | controleParentalAtivo | acesso esperado | cenário |
|---------------|-------|-----------------------|-----------------|---------|
| 1 | `20` | `false` | `true` | Maior de idade, sem restrição |
| 2 | `20` | `true` | `false` | Maior de idade, com restrição |
| 3 | `16` | `false` | `false` | Menor de idade |

## Como a solução foi criada (.NET CLI)

```bash
dotnet new sln -n StreamingFlix
dotnet new console -n StreamingFlix.App -f net10.0
dotnet new xunit -n StreamingFlix.Tests -f net10.0
dotnet sln add StreamingFlix.App/StreamingFlix.App.csproj
dotnet sln add StreamingFlix.Tests/StreamingFlix.Tests.csproj
dotnet add StreamingFlix.Tests/StreamingFlix.Tests.csproj reference StreamingFlix.App/StreamingFlix.App.csproj
```

## Licença

Este projeto está licenciado sob a [MIT License](LICENSE).
