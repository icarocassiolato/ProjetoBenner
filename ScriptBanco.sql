-- DROP SCHEMA dbo;

CREATE SCHEMA dbo;
-- Benner.dbo.Erro definition

-- Drop table

-- DROP TABLE Benner.dbo.Erro;

CREATE TABLE Benner.dbo.Erro (
	Id int IDENTITY(1,1) NOT NULL,
	Descricao varchar(MAX) COLLATE Latin1_General_CI_AS NOT NULL,
	DataHora datetime DEFAULT getdate() NOT NULL,
	CONSTRAINT PK__Erro__3214EC0796D3BAB8 PRIMARY KEY (Id)
);


-- Benner.dbo.Usuario definition

-- Drop table

-- DROP TABLE Benner.dbo.Usuario;

CREATE TABLE Benner.dbo.Usuario (
	Id int IDENTITY(1,1) NOT NULL,
	Nome varchar(50) COLLATE Latin1_General_CI_AS NOT NULL,
	Senha varchar(300) COLLATE Latin1_General_CI_AS NOT NULL,
	CONSTRAINT PK__Usuario__3214EC070ADF6DA9 PRIMARY KEY (Id)
);


-- Benner.dbo.ProgramaPersonalizado definition

-- Drop table

-- DROP TABLE Benner.dbo.ProgramaPersonalizado;

CREATE TABLE Benner.dbo.ProgramaPersonalizado (
	Id int IDENTITY(1,1) NOT NULL,
	Tempo int DEFAULT 30 NOT NULL,
	Potencia int DEFAULT 10 NOT NULL,
	SimboloProgresso char(1) COLLATE Latin1_General_CI_AS NOT NULL,
	Instrucoes varchar(MAX) COLLATE Latin1_General_CI_AS NULL,
	Nome varchar(100) COLLATE Latin1_General_CI_AS NULL,
	IdUsuario int NULL,
	CONSTRAINT PK__Programa__3214EC0744CA6F48 PRIMARY KEY (Id),
	CONSTRAINT ProgramaPersonalizado_Usuario_FK FOREIGN KEY (IdUsuario) REFERENCES Benner.dbo.Usuario(Id)
);