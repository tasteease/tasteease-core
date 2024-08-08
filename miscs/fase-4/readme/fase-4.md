[![Deploy to Amazon EKS](https://github.com/tasteease/tasteease/actions/workflows/dotnet.yml/badge.svg)](https://github.com/tasteease/tasteease/actions/workflows/dotnet.yml)
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=tasteease_tasteease&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=tasteease_tasteease)
[![Maintainability Rating](https://sonarcloud.io/api/project_badges/measure?project=tasteease_tasteease&metric=sqale_rating)](https://sonarcloud.io/summary/new_code?id=tasteease_tasteease)

# Taste Ease S/A

Fases:
- [Fase 1](/miscs/fase-1/readme/fase-1.md)
- [Fase 2](/miscs/fase-2/readme/fase-2.md)
- [Fase 3](/miscs/fase-3/readme/fase-3.md)

## Video de apresentação

[![Watch the video](/miscs/fase-1/ECRA.jpg)](https://youtu.be/YqYHhsRq4WE)

## Repositórios dos serviços

- [Repositório core](https://github.com/tasteease/tasteease-core)
- [Repositório user-service](https://github.com/tasteease/tasteease-user-service)
- [Repositório payment-service](https://github.com/tasteease/tasteease-payment-service)

## Repositórios da infraestrutura

- [Repositório rds (database)](https://github.com/tasteease/tasteease-tf-db)
- [Repositório cognito com lambda (auth identity)](https://github.com/tasteease/tasteease-tf-cognito)
- [Repositório ecs (cluster)](https://github.com/tasteease/tasteease-tf-ecs)

### Mais

Os repositórios estão com a branch main (principal) travadas para alterações, que somente são aceitas com um pull request com uma aprovação de um dos membros da equipe. As pipelines e workflows foram criados em todos os repositórios:

- Todos os repositórios possuem workflows para executar os `testes unitários`, além de usarem o `SonarQube` para validação do código. Também estão realizando o `build` das aplicações e fazendo o `push` das imagens no docker-hub.
- Os repositórios de infraestrutura (rds, cognito e ecs) estão configurados com dois workflows que executam o comando de `plan` e o `apply` no terraform cloud para serem aplicados na AWS.

### Cobertura de Testes do Core

![Cobertura de testes](/test-coverage-image.jpg)

[Relatório de cobertura dos testes](/test-coverage.json)

Authored by:

RM352294 - [Carlos Roberto Nascimento Junior](https://github.com/carona-jr)

RM351359 - [André Ribeiro](https://github.com/AndreRibeir0)

RM352094 - [José Ivan Ribeiro de Oliveira](https://github.com/estrng)
