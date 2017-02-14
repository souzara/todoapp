USE [TodoAppDb]
GO
/****** Object:  Table [dbo].[Todo]    Script Date: 13/02/2017 22:38:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Todo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Text] [varchar](2000) NOT NULL,
	[IsCompleted] [bit] NOT NULL,
 CONSTRAINT [Pk_Todo] PRIMARY KEY CLUSTERED  
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TodoLog]    Script Date: 13/02/2017 22:38:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TodoLog](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [varchar](1000) NOT NULL,
	[CreationTime] [datetime] NOT NULL,
	[TodoId] [int] NOT NULL,
 CONSTRAINT [Pk_TodoLog] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Todo] ON 

INSERT [dbo].[Todo] ([Id], [Text], [IsCompleted]) VALUES (3, N'Gravar vídeo sobre unidade de testes outro dia, porque ficou extensooo', 0)
INSERT [dbo].[Todo] ([Id], [Text], [IsCompleted]) VALUES (4, N'Gravar vídeo sobre unidade de testes outro dia, porque ficou extensooo', 0)
SET IDENTITY_INSERT [dbo].[Todo] OFF
SET IDENTITY_INSERT [dbo].[TodoLog] ON 

INSERT [dbo].[TodoLog] ([Id], [Description], [CreationTime], [TodoId]) VALUES (2, N'Inclusão de nova todo', CAST(N'2017-02-11T00:49:34.213' AS DateTime), 3)
INSERT [dbo].[TodoLog] ([Id], [Description], [CreationTime], [TodoId]) VALUES (3, N'Inclusão de nova todo', CAST(N'2017-02-11T00:52:03.553' AS DateTime), 4)
SET IDENTITY_INSERT [dbo].[TodoLog] OFF
ALTER TABLE [dbo].[TodoLog]  WITH CHECK ADD  CONSTRAINT [Fk_TodoLog_Todo] FOREIGN KEY([TodoId])
REFERENCES [dbo].[Todo] ([Id])
GO
ALTER TABLE [dbo].[TodoLog] CHECK CONSTRAINT [Fk_TodoLog_Todo]
GO
