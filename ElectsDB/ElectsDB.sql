USE [Electations]
GO
/****** Object:  Table [dbo].[Gender]    Script Date: 29.11.2021 21:11:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Gender](
	[IdGen] [int] IDENTITY(1,1) NOT NULL,
	[GenName] [nvarchar](15) NOT NULL,
 CONSTRAINT [PK_Gender] PRIMARY KEY CLUSTERED 
(
	[IdGen] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Candidate]    Script Date: 29.11.2021 21:11:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Candidate](
	[CandId] [int] IDENTITY(1,1) NOT NULL,
	[FName] [nvarchar](50) NOT NULL,
	[SName] [nvarchar](50) NOT NULL,
	[PName] [nvarchar](50) NULL,
	[Party] [int] NULL,
	[PassportNum] [int] NOT NULL,
	[PhotoPath] [varbinary](max) NULL,
	[Gender] [int] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Candidate] PRIMARY KEY CLUSTERED 
(
	[CandId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PartysList]    Script Date: 29.11.2021 21:11:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PartysList](
	[IdPart] [int] IDENTITY(1,1) NOT NULL,
	[NameParty] [nvarchar](55) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[PhotoPath] [varbinary](max) NULL,
 CONSTRAINT [PK_PartysList] PRIMARY KEY CLUSTERED 
(
	[IdPart] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PasData]    Script Date: 29.11.2021 21:11:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PasData](
	[IdPas] [int] IDENTITY(1,1) NOT NULL,
	[Serial] [nchar](4) NOT NULL,
	[Number] [nchar](10) NOT NULL,
	[Code] [nchar](6) NOT NULL,
	[GivedDate] [smalldatetime] NOT NULL,
	[BirthDate] [smalldatetime] NOT NULL,
	[GivedPlace] [nvarchar](150) NOT NULL,
	[BirthPlace] [nvarchar](150) NOT NULL,
 CONSTRAINT [PK_PasData] PRIMARY KEY CLUSTERED 
(
	[IdPas] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[CandidatsCurrentDisplay]    Script Date: 29.11.2021 21:11:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[CandidatsCurrentDisplay]
AS
SELECT        dbo.Candidate.CandId, dbo.Candidate.FName, dbo.Candidate.SName, dbo.Candidate.PName, dbo.Candidate.Party, dbo.Candidate.PassportNum, dbo.Candidate.PhotoPath, dbo.Candidate.Gender, dbo.Gender.GenName, 
                         dbo.Gender.IdGen, dbo.Candidate.IsDeleted, dbo.PartysList.IdPart, dbo.PartysList.NameParty, dbo.PasData.IdPas, dbo.PasData.Serial, dbo.PasData.Number, dbo.PasData.Code, dbo.PasData.GivedDate, 
                         dbo.PasData.GivedPlace, dbo.PasData.BirthDate, dbo.PasData.BirthPlace
FROM            dbo.Candidate INNER JOIN
                         dbo.Gender ON dbo.Candidate.Gender = dbo.Gender.IdGen INNER JOIN
                         dbo.PartysList ON dbo.Candidate.Party = dbo.PartysList.IdPart INNER JOIN
                         dbo.PasData ON dbo.Candidate.PassportNum = dbo.PasData.IdPas
GO
/****** Object:  Table [dbo].[Member]    Script Date: 29.11.2021 21:11:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Member](
	[ComID] [int] IDENTITY(1,1) NOT NULL,
	[FName] [nvarchar](50) NOT NULL,
	[SName] [nvarchar](50) NOT NULL,
	[PName] [nvarchar](50) NULL,
	[Gen] [int] NOT NULL,
	[PassportNum] [int] NOT NULL,
	[IdRole] [int] NOT NULL,
	[Password] [nvarchar](10) NOT NULL,
	[Login] [nvarchar](10) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[IsVoted] [bit] NOT NULL,
 CONSTRAINT [PK_Commisioner] PRIMARY KEY CLUSTERED 
(
	[ComID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[VotersCurrentDisplay]    Script Date: 29.11.2021 21:11:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[VotersCurrentDisplay]
AS
SELECT        dbo.Gender.GenName, dbo.Member.FName, dbo.Member.SName, dbo.Member.PName, dbo.Member.Gen, dbo.Member.ComID, dbo.Member.PassportNum, dbo.Member.Password, dbo.Member.Login, dbo.Member.IdRole, 
                         dbo.Gender.IdGen, dbo.Member.IsDeleted, dbo.Member.IsVoted, dbo.PasData.IdPas, dbo.PasData.Serial, dbo.PasData.Number, dbo.PasData.Code, dbo.PasData.GivedDate, dbo.PasData.BirthDate, dbo.PasData.GivedPlace, 
                         dbo.PasData.BirthPlace
FROM            dbo.Gender INNER JOIN
                         dbo.Member ON dbo.Gender.IdGen = dbo.Member.Gen INNER JOIN
                         dbo.PasData ON dbo.Member.PassportNum = dbo.PasData.IdPas
GO
/****** Object:  Table [dbo].[Bulletin]    Script Date: 29.11.2021 21:11:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bulletin](
	[BullId] [int] IDENTITY(1,1) NOT NULL,
	[VotetFor] [int] NOT NULL,
	[GivedDate] [datetime] NOT NULL,
	[SerialNum] [nvarchar](15) NOT NULL,
 CONSTRAINT [PK_Bulletin] PRIMARY KEY CLUSTERED 
(
	[BullId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 29.11.2021 21:11:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[RoleId] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](25) NOT NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Candidate] ADD  CONSTRAINT [DF_Candidate_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Member] ADD  CONSTRAINT [DF_Member_IsVoted]  DEFAULT ((0)) FOR [IsVoted]
GO
ALTER TABLE [dbo].[PartysList] ADD  CONSTRAINT [DF_PartysList_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Bulletin]  WITH CHECK ADD  CONSTRAINT [FK_Bulletin_Candidate] FOREIGN KEY([VotetFor])
REFERENCES [dbo].[Candidate] ([CandId])
GO
ALTER TABLE [dbo].[Bulletin] CHECK CONSTRAINT [FK_Bulletin_Candidate]
GO
ALTER TABLE [dbo].[Candidate]  WITH CHECK ADD  CONSTRAINT [FK_Candidate_Gender1] FOREIGN KEY([Gender])
REFERENCES [dbo].[Gender] ([IdGen])
GO
ALTER TABLE [dbo].[Candidate] CHECK CONSTRAINT [FK_Candidate_Gender1]
GO
ALTER TABLE [dbo].[Candidate]  WITH CHECK ADD  CONSTRAINT [FK_Candidate_PartysList1] FOREIGN KEY([Party])
REFERENCES [dbo].[PartysList] ([IdPart])
GO
ALTER TABLE [dbo].[Candidate] CHECK CONSTRAINT [FK_Candidate_PartysList1]
GO
ALTER TABLE [dbo].[Candidate]  WITH CHECK ADD  CONSTRAINT [FK_Candidate_PasData1] FOREIGN KEY([PassportNum])
REFERENCES [dbo].[PasData] ([IdPas])
GO
ALTER TABLE [dbo].[Candidate] CHECK CONSTRAINT [FK_Candidate_PasData1]
GO
ALTER TABLE [dbo].[Member]  WITH CHECK ADD  CONSTRAINT [FK_Commisioner_Gender] FOREIGN KEY([Gen])
REFERENCES [dbo].[Gender] ([IdGen])
GO
ALTER TABLE [dbo].[Member] CHECK CONSTRAINT [FK_Commisioner_Gender]
GO
ALTER TABLE [dbo].[Member]  WITH CHECK ADD  CONSTRAINT [FK_Member_PasData] FOREIGN KEY([PassportNum])
REFERENCES [dbo].[PasData] ([IdPas])
GO
ALTER TABLE [dbo].[Member] CHECK CONSTRAINT [FK_Member_PasData]
GO
ALTER TABLE [dbo].[Member]  WITH CHECK ADD  CONSTRAINT [FK_Member_Role1] FOREIGN KEY([IdRole])
REFERENCES [dbo].[Role] ([RoleId])
GO
ALTER TABLE [dbo].[Member] CHECK CONSTRAINT [FK_Member_Role1]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[42] 4[8] 2[32] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Candidate"
            Begin Extent = 
               Top = 13
               Left = 155
               Bottom = 266
               Right = 329
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Gender"
            Begin Extent = 
               Top = 90
               Left = 565
               Bottom = 221
               Right = 739
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "PartysList"
            Begin Extent = 
               Top = 6
               Left = 367
               Bottom = 102
               Right = 541
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "PasData"
            Begin Extent = 
               Top = 6
               Left = 777
               Bottom = 246
               Right = 951
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'CandidatsCurrentDisplay'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'CandidatsCurrentDisplay'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[41] 4[21] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Gender"
            Begin Extent = 
               Top = 50
               Left = 86
               Bottom = 153
               Right = 260
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Member"
            Begin Extent = 
               Top = 0
               Left = 417
               Bottom = 265
               Right = 591
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "PasData"
            Begin Extent = 
               Top = 10
               Left = 682
               Bottom = 247
               Right = 856
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'VotersCurrentDisplay'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'VotersCurrentDisplay'
GO
