
CREATE DATABASE [LucasNotes.UserDb]
GO

USE [LucasNotes.UserDb];
GO

IF OBJECT_ID(N'[dbo].[User]', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Pwd] [nvarchar](500) NOT NULL,
	[Email] [nvarchar](200) NULL,
	[Gender] [int] NOT NULL,
	CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
	) ON [PRIMARY]
END
GO

INSERT INTO [dbo].[User](Name, Pwd, Email, Gender) VALUES('admin', '$2b$10$CgQQr5DpMZMp199JT5FnIepwW9cgEs4HwS1WJdvQdM6wFmL/EAns2', '271298011@qq.com', 1)
GO
