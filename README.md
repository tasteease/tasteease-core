*O antigo repositório foi migrado para este*

# Taste Ease S/A

- Para acessar recursos da Fase 1
- [Fase 1](/miscs/readme/fase-1.md)

## Miro DDD

- [Taste Ease Análise DDD](https://miro.com/app/board/uXjVMm2nBP0=/?share_link_id=573849043414)

## Kubernetes

![Imgur](./miscs/k8s-diagram.png)

## Database diagram

![Imgur](./miscs/database-diagram.png)

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
