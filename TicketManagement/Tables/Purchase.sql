CREATE TABLE [dbo].[Purchase]
(
	[Id] INT identity PRIMARY KEY, 
    [UserId] NVARCHAR(200) NOT NULL, 
    [EventSeatId] INT NOT NULL
	
)
