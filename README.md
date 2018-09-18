# Introdução
Projeto para avaliação de candidaturas de uma empresa.

Construído em .NET Core, utilizando a abordagem DDD (Design Domain Driven), criando uma arquitetura própria.
Utilizando ubiquous language (linguagem onipresente).

## Estrutura da solução
* SmartHR
* SmartHR.Application
* SmartHR.Domain
* SmartHR.Infra.CrossCutting
* SmartHR.Infra.Data
* SmartHR.Service

## Utilização:
O Projeto foi construído com o VSCode, e é orquestrado em um container do Docker.

## Endpoints

Ao executar a aplicação, acesse `/api` _(localhost:porta/api)_ para inserir alguns dados de teste.

Por padrão as operações de CRUD vão variar conforme o método do protocolo HTTP enviado.

| Método | Descrição | Exemplo | Params
|------------ |--------|--------|--------  
| `GET` | Obter (parâmetro opcional `ID` pode ser enviado) | `/api/vagas/1` | `/1 :int`
| `POST` | Criar um objeto da entidade em questão | `/api/vagas` | `application/json` 
| `PUT` | Alterar o objeto da entidade, enviando o `ID` | `/api/vagas` | `application/json { id: 1, ... }` 
| `DELETE` | Excluir um objeto da entidade, enviando o `ID` | `/api/vagas/1` | `/1 :int` 
     

### Vagas

| Método | Rota | Descrição |
|------------ |--------|--------  
| `POST` | `/api/v1/vagas` | Criar vaga com objeto JSON (sem enviar o ID no JSON) |
| `PUT` | `/api/v1/vagas` | Alterar vaga com objeto JSON (enviando o ID no JSON) |
| `DELETE` | `/api/v1/vagas/<vaga_id>` | Excluir vaga a partir do ID |
| `GET` | `/api/v1/vagas` | Obter todas as vagas |
| `GET` | `/api/v1/vagas/<vaga_id>` | Obter vaga específica pelo ID |
| `GET` | `/api/v1/vagas/<vaga_id>/candidaturas` | Obter candidatos de uma vaga específica |
| `GET` | `/api/v1/vagas/<vaga_id>/candidaturas/ranking` | Obter candidatos de uma vaga específica ordenados pelo Score |

JSON esperado:

| Propriedade | Tipo | Observação |
|------------ |--------|--------  
| `id` | `int` | Se enviado, atualizará o objeto, senão, criará. |
| `empresa` | `string` |  |
| `titulo` | `string` | |
| `descricao` | `string` | |
| `localizacao` | `string` | Pelos requisitos, só podem ser enviadas as letras: A, B, C, D, E ou F |
| `nivel` | `int` | |

Exemplo : 
        ```
        javascript
          {
                "id": 1,
                "empresa": "Teste (Atualizando)",
                "titulo": "Vaga teste",
                "descricao": "Criar os mais diferentes tipos de teste",
                "localizacao": "A",
                "nivel": 3
          }
          ```

### Pessoas (Candidatos)

| Método | Rota | Descrição |
|------------ |--------|--------  
| `POST` | `/api/v1/pessoas` | Criar pessoa com objeto JSON (sem enviar o ID no JSON) |
| `PUT` | `/api/v1/pessoas` | Alterar pessoa com objeto JSON (enviando o ID no JSON) |
| `DELETE` | `/api/v1/pessoas/<pessoa_id>` | Excluir pessoa a partir do ID |
| `GET` | `/api/v1/pessoas` | Obter todas as pessoas |
| `GET` | `/api/v1/pessoas/<pessoa_id>` | Obter pessoa específica pelo ID |

JSON esperado:

| Propriedade | Tipo | Observação |
|------------ |--------|--------  
| `id` | `int` | Se enviado, atualizará o objeto, senão, criará. |
| `nome` | `string` |  |
| `profissao` | `string` | |
| `localizacao` | `string` | Pelos requisitos, só podem ser enviadas as letras: A, B, C, D, E ou F |
| `nivel` | `int` | |

Exemplo :
```javascript
{
    "id": 1,
    "empresa": "Teste (Atualizando)",
    "titulo": "Vaga teste",
    "descricao": "Criar os mais diferentes tipos de teste",
    "localizacao": "A",
    "nivel": 3
}
```



### Candidaturas

| Método | Rota | Descrição |
|------------ |--------|--------  
| `POST` | `/api/v1/candidaturas` | Criar pessoa com objeto JSON |
| `DELETE` | `/api/v1/candidaturas/<vaga_id>/<pessoa_id>` | Excluir pessoa a partir do ID da vaga e ID da pessoa |
| `GET` | `/api/v1/candidaturas` | Obter todas as candidaturas |

JSON esperado:

| Propriedade | Tipo | Observação |
|------------ |--------|--------  
| `id_vaga` | `int` |  |
| `id_pessoa` | `int` |  |

Exemplo :
```javascript
{
     "id_vaga": 1,
     "id_pessoa": 2
}
```

### Observações de Código:

#### Includes
Foram utilizadas técnicas de include (que é como um Join no SQL) unindo Linq, expressões lambda e o Entity Framework Core.

Um exemplo, utilizado em várias partes do código:
```csharp
public Vaga ObterPorID(int id)
{
    return _repository.Obter(wh => wh.Id == id, i => i.Candidaturas);
}
```
No arquivo `RepositoryBase`, existe a função

```csharp
    Obter(Expression<Func<TEntity, bool>> condicao, params Expression<Func<TEntity, object>>[] includes)
```
    
Onde `TEntity` será a entidade que estivermos trabalhando no momento, no caso do exemplo, `TEntity` = **Vaga**.

No primeiro parâmetro, a função espera um tipo `Expression<Func<TEntity, bool>>`, logo, trabalhamos com as propriedades da entidade.
       * `wh => wh.Id == id`, wh é cada vaga que está mapeada no contexto.
       * `i => i.Candidaturas` é uma propriedade da classe Vaga, que é `IList<Candidatura>`.

Na parte do código onde está `i => i.Candidaturas`, digo ao Entity que me retorne todas as candidaturas com a vaga de ID que estou buscando.

Ao retornar, trará da seguinte forma:
```javascript
{
    "empresa": "Teste",
    "titulo": "Vaga Teste",
    "descricao": "Criar os mais diferentes tipos de teste",
    "localizacao": "A",
    "nivel": 3,
    "candidaturas": [
        {
            "vagaID": 1,
            "pessoaID": 1,
            "score": 39,
            "pessoa": null,
            "dataCriacao": "2018-09-18T10:37:04.6222565-03:00"
        }
    ],
    "id": 1,
    "dataCriacao": "2018-09-18T10:37:04.5142504-03:00",
    "dataModificacao": null,
    "ativo": true
}
```


## Implementações:
* Domain-Driven Design (DDD)
* FluentValidation.AspNetCore
* Microsoft.EntityFrameworkCore.Design
* Microsoft.EntityFrameworkCore.Tools

## Criação
O projeto foi desenvolvido por Gabriel Vicente.
* [Linkedin](https://linkedin.com/in/gvms23)
* [Site Pessoal](https://gabrielvicente.ch)

## Licença

[![CC0](https://licensebuttons.net/p/zero/1.0/88x31.png)](https://creativecommons.org/publicdomain/zero/1.0/)