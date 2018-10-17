CREATE PROCEDURE [dbo].[CheckEventSeatByLayoutId]
                @LayoutId INT
                AS
                SELECT Count(*) FROM Seat INNER JOIN Area ON Seat.AreaId=Area.Id INNER JOIN Layout ON Area.LayoutId=Layout.Id WHERE Layout.Id=@LayoutId