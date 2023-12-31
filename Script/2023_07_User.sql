

CREATE DATABASE [LucasNotes.UserDb]

use [LucasNotes.UserDb]

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
GO

"SELECT " +
                      "[Id] AS UserId," +
                      "[Name] AS UserName," +
                      "[Pwd] AS Password," +
                      "[Email]," +
                      "[Gender] " +
                      "FROM [dbo].[User] " +
                      "WHERE [Name] = @Name AND [Pwd] = @Pwd";

