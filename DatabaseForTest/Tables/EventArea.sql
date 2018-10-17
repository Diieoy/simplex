CREATE TABLE [dbo].[EventArea]
(
	[Id] int identity primary key,
	[EventId] int NOT NULL,
	[LayoutId] int NOT NULL,
	[Description] nvarchar(200) NOT NULL,
	[CoordX] int NOT NULL,
	[CoordY] int NOT NULL,
	[Price] decimal NOT NULL, 
    CONSTRAINT [FK_Layout_EventArea] FOREIGN KEY ([LayoutId]) REFERENCES [Layout]([Id]), 
)
