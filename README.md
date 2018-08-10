# JMSeg

It's a **fantastic project** to improve my C# & ASP.net knowledge =)

## Ambiente

Para subir a aplicação (e o banco de dados) basta executar o comando ```docker-compose up -d```

## API

**ATENÇÃO**

Para facilitar os testes dos endpoints, basta importar o arquivo ```jmseg.postman_collection.json``` no Postman.

#### Cadastro de novo usuário (não necessita de autenticação)

POST http://localhost:5000/api/v1/users/

```json
{
  "name": "Jackson",
  "password": "123",
  "email": "jackson.s.teixeira@gmail.com"
}
```

#### Login (não necessita de autenticação)

POST http://localhost:5000/api/v1/login

```json
{
  "email": "jackson.s.teixeira@gmail.com",
  "password": "123"
}
```

#### Lista de usuários

GET http://localhost:5000/api/v1/users/

#### Detalhes de um usuário

GET http://localhost:5000/api/v1/users/{id}

#### Atualização de um usuário

PUT http://localhost:5000/api/v1/users/

```json
{
  "id": 5,
  "name": "Jackson de Souza Teixeira",
  "password": "123",
  "email": "jackson.s.teixeira@gmail.com"
}
```

#### Remoção de um usuário

DELETE http://localhost:5000/api/v1/users/{id}

#### Troca de senha

POST http://localhost:5000/api/v1/users/reset_password

```json
{
  "Password": "123",
  "NewPassword": "999"
}
```