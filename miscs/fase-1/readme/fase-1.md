# Taste Ease S/A

[Voltar](/README.md)

## Miro DDD

- [Taste Ease Análise DDD](https://miro.com/app/board/uXjVMm2nBP0=/?share_link_id=573849043414)

## Para construir a image

No diretório raiz do projeto, execute o comando:

```bash
 docker build -t tasteease/tasteease .
```

## Para subir infra do projeto

No diretório raiz do projeto, execute o comando:

```bash
 docker compose up -d
```

## Acesso PGAdmin

- <http://localhost:82>
- Password: @12345

  ![Imgur](https://i.imgur.com/OKXKlVE.jpg)

  ![Imgur](https://i.imgur.com/cYMmDpv.jpg)

- Adicionar o servidor utilizando o host com o nome do container:
  - tasteease-postgresql
  - usuario: postgres
  - senha: postgres

## Para utilização da collection no insomnia

- Faça a importação do arquivo:
  `taste-ease_Insomnia_2023-10-29.json` ou `taste-ease_Insomnia_2023-10-29.yaml`
  no insomnia

## SEED

Após acessar o PgAdmin, execute o script `seed.sql` para popular o banco de dados.
No local de execução de scripts do PgAdmin, copie e cole o conteudo do arquivo `seed.sql` e execute.

## Kubernetes

```bash

# Inicia o cluster
minikube start

#pods
kubectl apply -f pods/pod-tasteease-api.yaml
kubectl apply -f pods/pod-tasteease-db.yaml

#services
kubectl apply -f services/svc-tasteease-api.yaml
kubectl apply -f services/svc-tasteease-pgadmin.yaml
kubectl apply -f services/svc-tasteease-postgres.yaml

```

## Contribuição

Authored by:

RM352294 - [Carlos Roberto Nascimento Junior](https://github.com/carona-jr)

RM351359 - [André Ribeiro](https://github.com/AndreRibeir0)

RM352094 - [José Ivan Ribeiro de Oliveira](https://github.com/estrng)
