_O antigo repositório foi migrado para este_

[![Deploy to Amazon EKS](https://github.com/tasteease/tasteease/actions/workflows/dotnet.yml/badge.svg)](https://github.com/tasteease/tasteease/actions/workflows/dotnet.yml)
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=tasteease_tasteease&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=tasteease_tasteease)
[![Maintainability Rating](https://sonarcloud.io/api/project_badges/measure?project=tasteease_tasteease&metric=sqale_rating)](https://sonarcloud.io/summary/new_code?id=tasteease_tasteease)

# Taste Ease S/A

- Para acessar recursos da Fase 1.
- [Fase 1](../../fase-1/readme/fase-1.md)

## Video de apresentação

[![Watch the video](../../fase-1/ECRA.jpg)](https://youtu.be/YqYHhsRq4WE)

## Miro DDD

- [Taste Ease Análise DDD](https://miro.com/app/board/uXjVMm2nBP0=/?share_link_id=573849043414)

## Kubernetes

![Imgur](.../../fase-1/k8s-diagram.png)

## Database diagram

![Imgur](.../../fase-1/database-diagram.png)

### Passos para execução da infraestrutura com Kubernetes

- Assumindo que o docker desktop/kubernets ou minikube já está instalado e configurado, execute os seguintes comandos:
  - Certifique-se de que você está dendo da pasta "infra/" do projeto

```bash
# Crie o namespace da aplicação
kubectl create namespace tasteease

# Crie os volumes de persistência para o banco de dados
kubectl apply -f volumes/db-storage.yaml
kubectl get pv,pvc -n tasteease

# Criar o config map
kubectl apply -f config/cfg-tasteease-api.yaml
kubectl get configmap -n tasteease

# Criar os pods do banco de dados
kubectl apply -f deployments/deployment-db.yaml

# Criar os pods da API com o Deployment
kubectl apply -f deployments/deployment-api.yaml
kubectl apply -f services/services.yaml

# Criar metrics server and hpa
kubectl apply -f hpas/metrics.yaml
kubectl apply -f hpas/hpa-tasteease-api.yaml
```

Authored by:

RM352294 - [Carlos Roberto Nascimento Junior](https://github.com/carona-jr)

RM351359 - [André Ribeiro](https://github.com/AndreRibeir0)

RM352094 - [José Ivan Ribeiro de Oliveira](https://github.com/estrng)
