
CREATE DATABASE [Estudo]

USE [Estudo]
GO

/****** Object:  Table [dbo].[Salas]    Script Date: 14/05/2019 07:45:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Salas](
	[SalaId] [int] IDENTITY(1,1) NOT NULL,
	[SalaTitulo] [varchar](100) NULL,
	[SalaDescricao] [varchar](200) NULL,
PRIMARY KEY CLUSTERED 
(
	[SalaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[AgendamentoSala]    Script Date: 14/05/2019 07:45:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AgendamentoSala](
	[AgendamentoId] [int] IDENTITY(1,1) NOT NULL,
	[SalaId] [int] NULL,
	[AgendamentoInicial] [datetime] NULL,
	[AgendamentoFinal] [datetime] NULL,
	[AgendamentoStatus] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[AgendamentoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 14/05/2019 07:45:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuario](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [nvarchar](100) NOT NULL,
	[Login] [nvarchar](50) NOT NULL,
	[Senha] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_dbo.Usuario] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[AgendamentoSala]  WITH CHECK ADD  CONSTRAINT [fk_agendamentosala] FOREIGN KEY([SalaId])
REFERENCES [dbo].[Salas] ([SalaId])
GO
ALTER TABLE [dbo].[AgendamentoSala] CHECK CONSTRAINT [fk_agendamentosala]
GO
/****** Object:  StoredProcedure [dbo].[AddAgendamentoSala]    Script Date: 14/05/2019 07:45:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[AddAgendamentoSala]
(
@SalaId int,
@AgendamentoInicial datetime,
@AgendamentoFinal datetime,
@AgendamentoStatus bit
)as
begin
	insert into AgendamentoSala (SalaId, AgendamentoInicial, AgendamentoFinal, AgendamentoStatus)
	 values (@SalaId, @AgendamentoInicial, @AgendamentoFinal, @AgendamentoStatus )
end
GO
/****** Object:  StoredProcedure [dbo].[AddSala]    Script Date: 14/05/2019 07:45:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].[AddSala]
(
@SalaTitulo varchar(100),
@SalaDescricao varchar(100)
)as
begin
	insert into Salas (SalaTitulo, SalaDescricao) values (@SalaTitulo, @SalaDescricao)
end
GO
/****** Object:  StoredProcedure [dbo].[AddUsuario]    Script Date: 14/05/2019 07:45:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddUsuario]
(
	@Nome varchar(100),
	@Login varchar(100),
	@Senha varchar(100)
)AS
	BEGIN
		INSERT INTO Usuario(Nome, Login, Senha) VALUES (@Nome, @Login, @Senha)
	END
GO
/****** Object:  StoredProcedure [dbo].[DeleteAgendamentoSala]    Script Date: 14/05/2019 07:45:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[DeleteAgendamentoSala] (@AgendamentoId int, @SalaId int)
as
begin
	delete from AgendamentoSala where SalaId = @SalaId and AgendamentoId = @AgendamentoId
end

GO
/****** Object:  StoredProcedure [dbo].[DeleteSalas]    Script Date: 14/05/2019 07:45:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[DeleteSalas](
@SalaId Int
)as
begin
DELETE FROM AgendamentoSala WHERE SalaId = @SalaId
DELETE FROM Salas WHERE SalaId = @SalaId

end
GO
/****** Object:  StoredProcedure [dbo].[GetAgendamentoSala]    Script Date: 14/05/2019 07:45:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[GetAgendamentoSala]
as
begin
	Select AgendamentoId, SalaId, AgendamentoInicial, AgendamentoFinal, AgendamentoStatus from AgendamentoSala
end
GO
/****** Object:  StoredProcedure [dbo].[GetSalas]    Script Date: 14/05/2019 07:45:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetSalas]
as
begin
	select * from Salas
end
GO
/****** Object:  StoredProcedure [dbo].[GetSalasBySalaId]    Script Date: 14/05/2019 07:45:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[GetSalasBySalaId](@SalaId int)
as
begin
	select * from Salas where SalaId = @SalaId
end
GO
/****** Object:  StoredProcedure [dbo].[GetUsuarioByLogin]    Script Date: 14/05/2019 07:45:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[GetUsuarioByLogin]
(
@login varchar(100),
@senha varchar(100)
)as 
begin
	select 1 from Usuario where Login=@login and Senha=@senha
end
GO
/****** Object:  StoredProcedure [dbo].[VerificaAgendamentoSala]    Script Date: 14/05/2019 07:45:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[VerificaAgendamentoSala]
(
@SalaId int,
@DataInicial datetime,
@DataFinal datetime
)as
begin	
	select 1 from AgendamentoSala where SalaId = @SalaId
	and (AgendamentoInicial between @DataInicial and @DataFinal)
	or (AgendamentoFinal  between @DataInicial and @DataFinal)	
end

GO
