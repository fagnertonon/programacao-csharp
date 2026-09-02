-- =====================================================================
--  PORTARIA - sistema de acesso
--  Script de criacao do banco - UC12
--
--  COMO RODAR:
--    1. Abra o MySQL Workbench e conecte no localhost.
--    2. Abra este arquivo (File > Open SQL Script).
--    3. Clique no RAIO AMARELO (Execute All), nao no raio com o cursor.
--    4. Clique no icone de atualizar do painel SCHEMAS, na esquerda.
--       O banco portariadb tem que aparecer na lista.
--
--  ATENCAO: a primeira linha APAGA o banco se ele ja existir. Rode este
--  script UMA VEZ. Rodar de novo depois de ter cadastrado usuarios
--  apaga os usuarios.
--
--  Este banco se chama portariadb. O conectadb das aulas anteriores
--  continua na sua maquina, intacto - sao dois bancos diferentes.
-- =====================================================================

DROP DATABASE IF EXISTS portariadb;

-- utf8mb4 e o que faz acento aparecer certo.
-- Sem isso, "Joao" vira "Joo" na lista.
CREATE DATABASE portariadb
    DEFAULT CHARACTER SET utf8mb4
    DEFAULT COLLATE utf8mb4_general_ci;

USE portariadb;

-- ---------------------------------------------------------------------
--  Tabela unica do sistema.
--
--  UNIQUE (Login): o banco recusa dois logins iguais. A conferencia do
--  LoginExiste() no C# e a mensagem bonita; esta linha e a garantia.
--
--  DataCadastro com DEFAULT CURRENT_TIMESTAMP: o banco carimba a data
--  sozinho. E por isso que o INSERT do CriarConta tem tres parametros,
--  e nao quatro.
-- ---------------------------------------------------------------------
CREATE TABLE Usuario (
    Id           INT          NOT NULL AUTO_INCREMENT,
    Nome         VARCHAR(60)  NOT NULL,
    Login        VARCHAR(30)  NOT NULL,
    Senha        VARCHAR(50)  NOT NULL,
    DataCadastro DATETIME     NOT NULL DEFAULT CURRENT_TIMESTAMP,

    PRIMARY KEY (Id),
    CONSTRAINT UQ_Usuario_Login UNIQUE (Login)
) ENGINE = InnoDB
  DEFAULT CHARSET = utf8mb4;

-- ---------------------------------------------------------------------
--  Tres contas de exemplo, para o sistema nao nascer vazio.
--
--  A senha fica em texto puro, igual aos projetos anteriores da turma.
--  Nao faca isso num sistema de verdade: aqui e assim porque o assunto
--  da unidade e integracao com banco, nao seguranca.
--
--  Repare que TODOS os nomes tem duas palavras. Isso importa - guarde
--  a observacao para a caca ao defeito da segunda noite.
-- ---------------------------------------------------------------------
INSERT INTO Usuario (Nome, Login, Senha) VALUES
    ('Ana Souza',     'ana',   '1234'),
    ('Bruno Lima',    'bruno', '1234'),
    ('Administrador Geral', 'admin', 'admin');

-- ---------------------------------------------------------------------
--  Conferencia. A segunda consulta usa o MESMO ORDER BY do
--  ListarTodos(0), mas NAO traz a coluna Senha - ela nao aparece
--  em tela nenhuma do sistema.
--
--  Nao copie esta consulta para o TODO 10: la sao CINCO colunas,
--  e sem a Senha o MontarUsuario nao acha r["Senha"].
-- ---------------------------------------------------------------------
DESCRIBE Usuario;

SELECT Id, Nome, Login, DataCadastro
  FROM Usuario
 ORDER BY Nome;
