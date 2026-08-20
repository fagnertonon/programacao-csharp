-- ============================================================================
--  O banco que recebe as respostas do simulado e da prova
--  UC12 - Tecnico em Informatica - Senac
--
--  Rodar UMA VEZ na VPS, como root ou como um usuario administrativo.
--  Depois, preencher os quatro valores em comum/Conexao.cs e compilar.
-- ============================================================================

CREATE DATABASE IF NOT EXISTS provauc12
  CHARACTER SET utf8mb4
  COLLATE utf8mb4_unicode_ci;

USE provauc12;

-- ----------------------------------------------------------------------------
--  A tabela. Uma LINHA POR ENVIO, e nao uma linha por aluno.
--
--  Nao existe UPDATE em lugar nenhum: se o aluno enviar tres vezes, ficam tres
--  linhas, e voce ve a evolucao dele. A ultima e a que vale, e as anteriores
--  contam a historia.
-- ----------------------------------------------------------------------------

CREATE TABLE IF NOT EXISTS resposta (
  Id         INT AUTO_INCREMENT PRIMARY KEY,
  aluno      VARCHAR(120)  NOT NULL,
  maquina    VARCHAR(120)  NOT NULL,
  questao    VARCHAR(40)   NOT NULL,   -- o id do topico: contar, media, ...
  numero     INT           NOT NULL,   -- 1 a 10 no simulado, 1 a 5 na prova
  metodo     VARCHAR(60)   NOT NULL,   -- o nome do metodo
  passaram   INT           NOT NULL,   -- testes que passaram
  total      INT           NOT NULL,   -- testes daquela questao
  resolvido  TINYINT       NOT NULL,   -- 1 quando passaram = total
  codigo     MEDIUMTEXT    NOT NULL,   -- o Desafios.cs inteiro, como enviado
  quando     DATETIME      NOT NULL DEFAULT CURRENT_TIMESTAMP,

  INDEX ix_aluno (aluno),
  INDEX ix_quando (quando)
);

-- ============================================================================
--  OS DOIS USUARIOS - e aqui esta a parte que importa
-- ============================================================================

-- ----------------------------------------------------------------------------
--  1. O usuario do ALUNO: SOMENTE INSERT, e somente nesta tabela.
--
--  A senha dele vai dentro do executavel que trinta pessoas recebem. Nao ha
--  como esconder: qualquer um abre o arquivo num editor e le. A defesa nao e
--  esconder a senha - e o banco recusar tudo que nao seja inserir.
--
--  Com este GRANT, o pior que alguem consegue fazer com a senha e inserir
--  linha de mentira, que fica rastreavel pela maquina e pela hora. Ler a prova
--  do colega, alterar a propria nota ou apagar a tabela: nao da.
--
--  TROQUE A SENHA ABAIXO antes de rodar.
-- ----------------------------------------------------------------------------

CREATE USER IF NOT EXISTS 'aluno_uc12'@'%'
  IDENTIFIED BY 'TROQUE-ESTA-SENHA';

GRANT INSERT ON provauc12.resposta TO 'aluno_uc12'@'%';

-- ----------------------------------------------------------------------------
--  2. O SEU usuario, para ler os resultados. Este NAO vai para o aplicativo.
-- ----------------------------------------------------------------------------

CREATE USER IF NOT EXISTS 'professor_uc12'@'%'
  IDENTIFIED BY 'OUTRA-SENHA-DIFERENTE';

GRANT SELECT, DELETE ON provauc12.resposta TO 'professor_uc12'@'%';

FLUSH PRIVILEGES;

-- ============================================================================
--  CONFERIR QUE O GRANT FUNCIONOU  -  faca isto ANTES da prova
--
--  Conecte com 'aluno_uc12' e rode:
--
--      SELECT * FROM resposta;
--
--  Tem que dar erro:
--      ERROR 1142 (42000): SELECT command denied to user 'aluno_uc12'
--
--  Se ele conseguir ler, o GRANT nao pegou e a prova esta exposta.
-- ============================================================================


-- ============================================================================
--  CONSULTAS PARA A CORRECAO
-- ============================================================================

-- Quem enviou, quantas questoes resolveu, e quando foi o ultimo envio.
-- Considera so o ULTIMO envio de cada aluno em cada questao.
--
-- SELECT r.aluno,
--        SUM(r.resolvido) AS resolvidas,
--        COUNT(*)         AS questoes,
--        MAX(r.quando)    AS ultimo_envio
--   FROM resposta r
--   JOIN (SELECT aluno, questao, MAX(Id) AS Id
--           FROM resposta GROUP BY aluno, questao) u
--     ON u.Id = r.Id
--  GROUP BY r.aluno
--  ORDER BY resolvidas DESC, ultimo_envio;

-- O codigo de um aluno, no ultimo envio dele.
--
-- SELECT questao, numero, metodo, passaram, total, quando, codigo
--   FROM resposta
--  WHERE aluno = 'NOME DO ALUNO'
--  ORDER BY Id DESC;

-- Quem NAO enviou nada. Compare com a sua lista de chamada.
--
-- SELECT DISTINCT aluno FROM resposta ORDER BY aluno;

-- Questao por questao: quantos resolveram. Mostra o que a turma nao aprendeu.
--
-- SELECT numero, metodo,
--        SUM(resolvido) AS resolveram,
--        COUNT(DISTINCT aluno) AS tentaram
--   FROM resposta
--  GROUP BY numero, metodo
--  ORDER BY numero;
