CREATE TABLE [dbo].[Layout]
(
	[Id] int identity primary key,
	[Name] nvarchar(120) NOT NULL,
	[VenueId] int NOT NULL,
	[Description] nvarchar(120) NOT NULL,
)
