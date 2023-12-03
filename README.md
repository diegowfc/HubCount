# HubCount

Utilizei o banco de dados SQLServer para salvar os dados da aplicação. Como desenhei o banco pensando em uma aplicação real, algumas chaves foram criadas como identity, portanto, caso for fazer o insert manual, pode desativá-la, como no exemplo:

SET IDENTITY_INSERT DBOnionSA.dbo.Produtos ON

INSERT INTO DBOnionSA.dbo.Produtos (Id, Nome, Descricao, PrecoUnitario) VALUES (3, 'Televisão', 'Sistema de reprodução audiovisual', 5000.00)

SET IDENTITY_INSERT DBOnionSA.dbo.Produtos OFF

Como citado no e-mail, gostaria de ter tido mais tempo para validar o código com testes e talvez escreve-lo melhor.

Obrigado!
