CREATE DATABASE Initiumsoft_Test;

USE Initiumsoft_Test;

CREATE TABLE [dbo].[Queue]
     ( 
        QueueId					INT	IDENTITY(1,1)			NOT NULL  , 
		Name					VARCHAR(55)					NOT NULL  ,
		Duration				TINYINT						NOT NULL  ,
		Creation				DATETIME					NOT NULL  	DEFAULT (GETDATE()) ,
		Status					TINYINT						NOT NULL	DEFAULT 0,
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

CREATE TABLE [dbo].[Attended]
     ( 
        AttendedId				INT	IDENTITY(1,1)			NOT NULL  , 
		ClientId				INT							NOT NULL  , 
		QueueId					INT							NOT NULL  , 
		Creation				DATETIME					NOT NULL  	DEFAULT (GETDATE()) ,
		Status					TINYINT						NOT NULL	DEFAULT 0,
			CONSTRAINT PK_Attended PRIMARY KEY CLUSTERED (AttendedId ASC) ON [PRIMARY],
			FOREIGN KEY (ClientId) REFERENCES [dbo].[Client](ClientId),
			FOREIGN KEY (QueueId) REFERENCES [dbo].[Queue](QueueId)
     )		
GO

GO

INSERT INTO [dbo].[Queue]
           ([Name]
           ,[Duration]
           ,[Creation]
           ,[Status])
     VALUES
           ('Cola 1'
           ,2
           ,GETDATE()
           ,1),
		   ('Cola 2'
           ,3
           ,GETDATE()
           ,1)
GO