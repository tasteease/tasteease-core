[![Deploy to Amazon EKS](https://github.com/tasteease/tasteease/actions/workflows/dotnet.yml/badge.svg)](https://github.com/tasteease/tasteease/actions/workflows/dotnet.yml)
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=tasteease_tasteease&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=tasteease_tasteease)
[![Maintainability Rating](https://sonarcloud.io/api/project_badges/measure?project=tasteease_tasteease&metric=sqale_rating)](https://sonarcloud.io/summary/new_code?id=tasteease_tasteease)

# Taste Ease S/A

Fases:
- [Fase 1](/miscs/fase-1/readme/fase-1.md)
- [Fase 2](/miscs/fase-2/readme/fase-2.md)
- [Fase 3](/miscs/fase-3/readme/fase-3.md)
- [Fase 4](/miscs/fase-4/readme/fase-4.md)

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

### Coreografia dos serviços

Os serviços de pedido (este) e o pagamentos (payment-service) estão com uma comunicação assíncrona quando o evento de pagamento ou recusa é recebido pelo serviço de pagamento. O mesmo envia uma mensagem via RabbitMq para o serviço de pedido, que atualiza a situação do pedido, notifica o cliente e a cozinha de que o pedido já pode ser começado a ser produzido.

A escolha pela arquitetura de coreografia permite que cada serviço trate os eventos recebidos, tanto nos casos de sucesso ou insucesso. Por exemplo, no caso do pagamento recebido, o serviço de pedido pode tratar os dois casos e também atuar no rollback da situação atual do pedido. Isto permite uma descentralização das decisões, permitindo o tratamento por cada serviço.

Pagamento recebido -> Fila de Pagamento -> Pedido atualizado para pago -> Fila para iniciar o pedido -> Pedido notificado para ser iniciado -> Pedido pronto -> Fila de Notificação -> Cliente recebe mensagem

### LGPD

Para observar o respeito as normas da Lei Geral de Proteção de Dados, um endpoint para que os usuários consigam apagar os seus dados foi implementado. Dentro do serviço de usuários, que armazena os dados, existe uma chamada para que estes mesmos dados possam ser excluídos da plataforma.

### Relatõrios
- [OWASP ZAP](/miscs/report.html)
- [LGPD](/miscs/RIPD.pdf)

## Docker

No diretório raiz do projeto, execute o comando:

```bash
 docker build -t tasteease/tasteease .
```

No diretório raiz do projeto, execute o comando:

```bash
 docker compose up -d
```

Authored by:

RM352294 - [Carlos Roberto Nascimento Junior](https://github.com/carona-jr)

RM351359 - [André Ribeiro](https://github.com/AndreRibeir0)

RM352094 - [José Ivan Ribeiro de Oliveira](https://github.com/estrng)
