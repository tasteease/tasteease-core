_O antigo repositório foi migrado para este_

[![Deploy to Amazon EKS](https://github.com/tasteease/tasteease/actions/workflows/dotnet.yml/badge.svg)](https://github.com/tasteease/tasteease/actions/workflows/dotnet.yml)
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=tasteease_tasteease&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=tasteease_tasteease)
[![Maintainability Rating](https://sonarcloud.io/api/project_badges/measure?project=tasteease_tasteease&metric=sqale_rating)](https://sonarcloud.io/summary/new_code?id=tasteease_tasteease)

# Taste Ease S/A

Fases:
- [Fase 1](/miscs/fase-1/readme/fase-1.md)
- [Fase 2](/miscs/fase-2/readme/fase-2.md)

## Video de apresentação

[![Watch the video](/miscs/fase-1/ECRA.jpg)](https://youtu.be/LP8lCToo7as)

## Repositórios da infraestrutura

- [Repositório rds (database)](https://github.com/tasteease/tasteease-tf-db)
- [Repositório cognito com lambda (auth identity)](https://github.com/tasteease/tasteease-tf-cognito)
- [Repositório ecs (cluster)](https://github.com/tasteease/tasteease-tf-ecs)

### Mais

Os repositórios estão com a branch main (principal) travadas para alterações, que somente são aceitas com um pull request com uma aprovação de alguns dos membros da equipe. As pipelines e workflows foram criados em todos os repositórios:

- Este repositório possui um workflow para executar os `testes unitários`, além de usar o `SonarQube` para validação do código. Também está realizando o `build` da aplicação e fazendo o `push` da imagem no docker-hub.
- Os repositórios de infraestrutura (rds, cognito e ecs) estão configurados com dois workflows que executam o comando de `plan` e o `apply` no terraform cloud para serem aplicados na AWS.

Alteração video.

Authored by:

RM352294 - [Carlos Roberto Nascimento Junior](https://github.com/carona-jr)

RM351359 - [André Ribeiro](https://github.com/AndreRibeir0)

RM352094 - [José Ivan Ribeiro de Oliveira](https://github.com/estrng)
