CREATE DATABASE Initiumsoft_Test;

USE Initiumsoft_Test;

CREATE TABLE [dbo].[Queue]
     ( 
        QueueId					INT	IDENTITY(1,1)			NOT NULL  , 
		Name					VARCHAR(55)					NOT NULL  ,
		Duration				TIME						NOT NULL  ,
		Creation				DATETIME					NOT NULL  	DEFAULT (GETDATE()) ,
		Status					TINYINT						NOT NULL	DEFAULT 1,
			CONSTRAINT PK_Queue PRIMARY KEY CLUSTERED (QueueId ASC) ON [PRIMARY],
     )		
GO

CREATE TABLE [dbo].[Client]
     ( 
        ClientId				INT							NOT NULL  , 
		QueueId					INT							NOT NULL  , 
		Name					VARCHAR(55)					NOT NULL  ,
		Creation				DATETIME					NOT NULL  	DEFAULT (GETDATE()) ,
		Status					TINYINT						NOT NULL	DEFAULT 0,
			CONSTRAINT PK_Client PRIMARY KEY CLUSTERED (ClientId ASC) ON [PRIMARY],
			FOREIGN KEY (QueueId) REFERENCES [dbo].[Queue](QueueId)
     )		
GO