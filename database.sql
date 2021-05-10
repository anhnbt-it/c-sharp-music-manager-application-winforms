USE [master]

IF EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = N'MusicManager')
	DROP DATABASE [MusicManager]
GO

CREATE DATABASE [MusicManager]
GO

use MusicManager

SET NOCOUNT ON
-- Restricts volume of output to errors and
-- messages that use the PRINT function

PRINT 'CREATING TABLE Albums'
CREATE TABLE [Albums] (
	[AlbumID] [int] IDENTITY (1, 1) NOT NULL ,
	[AlbumName] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[CreatedDate] [smalldatetime] NOT NULL ,
	CONSTRAINT [PK_Albums] PRIMARY KEY  CLUSTERED 
	(
		[AlbumID]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO




PRINT 'CREATING TABLE Artists'
CREATE TABLE [Artists] (
	[ArtistID] [int] IDENTITY (1, 1) NOT NULL ,
	[ArtistName] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	CONSTRAINT [PK_Artists] PRIMARY KEY  CLUSTERED 
	(
		[ArtistID]
	)  ON [PRIMARY] 
) ON [PRIMARY]
GO




PRINT 'CREATING TABLE Songs'
CREATE TABLE [Songs] (
	[SongID] [int] IDENTITY (1, 1) NOT NULL ,
	[SongName] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[ArtistID] [int] NOT NULL ,
	CONSTRAINT [PK_Songs] PRIMARY KEY  CLUSTERED 
	(
		[SongID]
	)  ON [PRIMARY] ,
	CONSTRAINT [FK_Songs_Artists] FOREIGN KEY 
	(
		[ArtistID]
	) REFERENCES [Artists] (
		[ArtistID]
	)
) ON [PRIMARY]
GO




PRINT 'CREATING TABLE AlbumSong'
CREATE TABLE [AlbumSong] (
	[AlbumID] [int] NOT NULL ,
	[SongID] [int] NOT NULL ,
	CONSTRAINT [PK_AlbumSong] PRIMARY KEY  CLUSTERED 
	(
		[AlbumID],
		[SongID]
	)  ON [PRIMARY] ,
	CONSTRAINT [FK_AlbumSong_Albums] FOREIGN KEY 
	(
		[AlbumID]
	) REFERENCES [Albums] (
		[AlbumID]
	),
	CONSTRAINT [FK_AlbumSong_Songs] FOREIGN KEY 
	(
		[SongID]
	) REFERENCES [Songs] (
		[SongID]
	)
) ON [PRIMARY]
GO

SET NOCOUNT ON
-- Restricts volume of output to errors and
-- messages that use the PRINT function


PRINT 'INSERTING DATA INTO TABLE Albums'
ALTER TABLE [Albums] NOCHECK CONSTRAINT ALL
SET IDENTITY_INSERT [Albums] ON
INSERT INTO [Albums] ([AlbumID],[AlbumName],[CreatedDate]) VALUES (1,'aaaaa','2004/08/03 09:24:00 PM')
SET IDENTITY_INSERT [Albums] OFF
ALTER TABLE [Albums] CHECK CONSTRAINT ALL

PRINT 'INSERTING DATA INTO TABLE Artists'
ALTER TABLE [Artists] NOCHECK CONSTRAINT ALL
SET IDENTITY_INSERT [Artists] ON
INSERT INTO [Artists] ([ArtistID],[ArtistName]) VALUES (1,'Bee Gees')
INSERT INTO [Artists] ([ArtistID],[ArtistName]) VALUES (2,'Beatles')
INSERT INTO [Artists] ([ArtistID],[ArtistName]) VALUES (3,'Eagle')
INSERT INTO [Artists] ([ArtistID],[ArtistName]) VALUES (4,'Abba')
INSERT INTO [Artists] ([ArtistID],[ArtistName]) VALUES (5,'Micheal Jackson')
INSERT INTO [Artists] ([ArtistID],[ArtistName]) VALUES (6,'The Corrs')
INSERT INTO [Artists] ([ArtistID],[ArtistName]) VALUES (7,'Gun ''N Roses')
INSERT INTO [Artists] ([ArtistID],[ArtistName]) VALUES (8,'Whitney Houston')
SET IDENTITY_INSERT [Artists] OFF
ALTER TABLE [Artists] CHECK CONSTRAINT ALL

PRINT 'INSERTING DATA INTO TABLE Songs'
ALTER TABLE [Songs] NOCHECK CONSTRAINT ALL
SET IDENTITY_INSERT [Songs] ON
INSERT INTO [Songs] ([SongID],[SongName],[ArtistID]) VALUES (1,'Hotel California',3)
INSERT INTO [Songs] ([SongID],[SongName],[ArtistID]) VALUES (2,'How Deep Is Your Love?',1)
INSERT INTO [Songs] ([SongID],[SongName],[ArtistID]) VALUES (3,'Tragedy',1)
INSERT INTO [Songs] ([SongID],[SongName],[ArtistID]) VALUES (4,'Happy New Year',4)
INSERT INTO [Songs] ([SongID],[SongName],[ArtistID]) VALUES (5,'Thriller',5)
INSERT INTO [Songs] ([SongID],[SongName],[ArtistID]) VALUES (7,'Hey Jude',2)
INSERT INTO [Songs] ([SongID],[SongName],[ArtistID]) VALUES (9,'Let It Be',2)
INSERT INTO [Songs] ([SongID],[SongName],[ArtistID]) VALUES (11,'Patience',7)
INSERT INTO [Songs] ([SongID],[SongName],[ArtistID]) VALUES (12,'Don''t Cry',7)
INSERT INTO [Songs] ([SongID],[SongName],[ArtistID]) VALUES (14,'Run to You',8)
INSERT INTO [Songs] ([SongID],[SongName],[ArtistID]) VALUES (15,'Queen of the Night',8)
INSERT INTO [Songs] ([SongID],[SongName],[ArtistID]) VALUES (16,'The Greatest Love of All',8)
SET IDENTITY_INSERT [Songs] OFF
ALTER TABLE [Songs] CHECK CONSTRAINT ALL

PRINT 'INSERTING DATA INTO TABLE AlbumSong'
ALTER TABLE [AlbumSong] NOCHECK CONSTRAINT ALL
INSERT INTO [AlbumSong] ([AlbumID],[SongID]) VALUES (1,1)
INSERT INTO [AlbumSong] ([AlbumID],[SongID]) VALUES (1,2)
INSERT INTO [AlbumSong] ([AlbumID],[SongID]) VALUES (1,3)
ALTER TABLE [AlbumSong] CHECK CONSTRAINT ALL
