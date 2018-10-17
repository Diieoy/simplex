CREATE PROCEDURE [dbo].[GetEventsByLayoutId]
                @LayoutId INT
                AS
                SELECT * FROM Event WHERE Event.LayoutId=@LayoutId
