CREATE PROCEDURE [dbo].[GetAllEventsByName]
                @Name NVARCHAR(MAX)
                AS
                SELECT * FROM Event WHERE Event.Name=@Name