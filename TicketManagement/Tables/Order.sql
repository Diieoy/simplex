CREATE TABLE [dbo].[Order]
(
	[Id] INT identity PRIMARY KEY, 
    [UserId] NVARCHAR(200) NOT NULL,
	[SeatId] int NOT NULL,
	[DateTimeOrder] datetime NOT NULL
)
