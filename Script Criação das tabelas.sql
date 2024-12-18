﻿-- SCRIPT SQL PARA CRIAÇÃO DA TABELA DE CLIENTE
CREATE TABLE CLIENTE(
	ID					UNIQUEIDENTIFIER	PRIMARY KEY,
	NOME				VARCHAR(150)		NOT NULL,
	EMAIL				VARCHAR(50)			NOT NULL,
	CPF					VARCHAR(11)			NOT NULL,
	DATACRIACAO			DATETIME			NOT NULL,
	DATAULTIMAALTERACAO	DATETIME			NOT NULL,
	ATIVO				BIT					NOT NULL);

