CREATE TABLE [dbo].[Event]
(
	[Id] int primary key identity,
	[Name] nvarchar(120) NOT NULL,
	[Description] nvarchar(max) NOT NULL,
	[DateTimeStart] datetime NOT NULL,
	[DateTimeFinish] datetime NOT NULL,
	[ImageUrl] nvarchar(MAX) NOT NULL,
	[LayoutId] int NOT NULL,
)
