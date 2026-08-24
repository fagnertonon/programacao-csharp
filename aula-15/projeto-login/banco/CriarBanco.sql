-- =====================================================================
-- Conecta - a tabela de contas
-- UC12 - Tecnico em Informatica - projeto Conecta
--
-- Como executar:
--   MySQL Workbench > File > Open SQL Script > escolha este arquivo >
--   clique no raio AMARELO (Execute All), nao no raio com o cursor.
--
-- Este script pode ser rodado QUANTAS VEZES QUISER. Ele nao apaga nada:
-- tudo aqui e IF NOT EXISTS ou INSERT IGNORE. Rodou duas vezes por
-- engano? Nao aconteceu nada.
--
-- (O script da Atividade 1, em codigo/atividade-01/CriarBanco.sql, comeca
--  com DROP DATABASE e cria tambem a tabela Postagem. Este aqui e o
--  recorte da noite: so a tabela de contas, e sem apagar.)
-- =====================================================================

CREATE DATABASE IF NOT EXISTS conectadb
    CHARACTER SET utf8mb4
    COLLATE utf8mb4_general_ci;

USE conectadb;

-- ---------------------------------------------------------------------
-- Tabela Usuario
--
-- UNIQUE (Login): o banco recusa dois logins iguais. O LoginExiste() que
-- voce escreve no C# e a mensagem bonita; esta linha e a garantia. E e
-- ela tambem que faz o INSERT IGNORE la embaixo poder rodar duas vezes
-- sem duplicar conta - sem o UNIQUE, o banco nao teria como saber que a
-- conta ja estava la.
--
-- CHECK: tamanho minimo depois de tirar os espacos das pontas.
--
-- A tabela Postagem nao entra hoje: ela e da Atividade 2.
-- ---------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS Usuario (
    Id    INT AUTO_INCREMENT PRIMARY KEY,
    Nome  VARCHAR(50) NOT NULL,
    Login VARCHAR(50) NOT NULL,
    Senha VARCHAR(50) NOT NULL,

    CONSTRAINT UQ_Usuario_Login UNIQUE (Login),
    CONSTRAINT CK_Usuario_Nome  CHECK (CHAR_LENGTH(TRIM(Nome))  >= 3),
    CONSTRAINT CK_Usuario_Login CHECK (CHAR_LENGTH(TRIM(Login)) >= 3)
) ENGINE = InnoDB;

-- ---------------------------------------------------------------------
-- Tres contas de exemplo
--
-- Servem para voce ter com que entrar antes de cadastrar qualquer coisa.
-- IGNORE: se a conta ja existir, o MySQL passa por cima em silencio.
-- ---------------------------------------------------------------------
INSERT IGNORE INTO Usuario (Nome, Login, Senha) VALUES
    ('Administrador', 'admin', 'admin'),
    ('Ana Souza',     'ana',   '1234'),
    ('Bruno Lima',    'bruno', '1234');

-- ---------------------------------------------------------------------
-- Conferencia
--
-- Tem que aparecer TRES linhas. Se voce rodar o script de novo,
-- continuam tres.
--
-- Repare que a coluna Senha nao esta aqui: senha nao se mostra no
-- projetor. Que ela esteja em texto puro dentro da tabela e assunto de
-- outra noite - hoje o assunto e o banco guardar.
-- ---------------------------------------------------------------------
SELECT Id, Nome, Login FROM Usuario ORDER BY Id;
