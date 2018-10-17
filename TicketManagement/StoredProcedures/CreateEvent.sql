CREATE PROCEDURE [dbo].[CreateEvent]
                @Name NVARCHAR(120), @Description NVARCHAR(MAX), @DateTimeStart datetime, @DateTimeFinish datetime, @ImageUrl NVARCHAR(MAX), @LayoutId INT
                AS
                --вставляем данные в Event
                INSERT INTO Event VALUES(@Name, @Description, @DateTimeStart, @DateTimeFinish, @ImageUrl, @LayoutId)

                --получаем id последнего эл-та Event
                DECLARE @EventId INT
                DECLARE cur CURSOR FOR
                SELECT MAX(Id) FROM Event
                OPEN cur
                FETCH NEXT FROM cur INTO @EventId
                CLOSE cur

                --выборка из Area и инсерт в EventArea
                DECLARE @AreaIdForEventId INT, @DescriptionForEventArea NVARCHAR(200), @CoordX INT, @CoordY INT, @EventAreaId INT
                DECLARE cur2 CURSOR FOR
                SELECT Area.Id, Area.Description, Area.CoordX, Area.CoordY FROM Layout INNER JOIN Area ON Layout.Id=Area.LayoutId WHERE Layout.Id=@LayoutId
                OPEN cur2
                FETCH NEXT FROM cur2 INTO @AreaIdForEventId, @DescriptionForEventArea, @CoordX, @CoordY
                WHILE @@FETCH_STATUS = 0
                BEGIN
	                INSERT INTO EventArea VALUES(@EventId, @LayoutId, @DescriptionForEventArea, @CoordX, @CoordY, 0)

		                --получаем EventArea.Id, куда только что вставили данные
		                DECLARE curs CURSOR FOR
		                SELECT MAX(Id) FROM EventArea
		                OPEN curs
		                FETCH NEXT FROM curs INTO @EventAreaId
		                CLOSE curs
		                DEALLOCATE curs

		                --выборка из Seat и инсерт в EventSeat
		                DECLARE @Row INT, @Number INT
		                DECLARE curso CURSOR FOR
		                SELECT Seat.Row, Seat.Number FROM Layout INNER JOIN Area ON Layout.Id=Area.LayoutId INNER JOIN Seat ON Area.Id=Seat.AreaId WHERE Layout.Id=@LayoutId AND Area.Id=@AreaIdForEventId
		                OPEN curso
		                FETCH NEXT FROM curso INTO @Row, @Number
		                WHILE @@FETCH_STATUS = 0
		                BEGIN
		                INSERT INTO EventSeat VALUES(@EventAreaId, @Row, @Number, 0)
			                FETCH NEXT FROM curso INTO @Row, @Number
		                END
		                CLOSE curso
		                DEALLOCATE curso

	                FETCH NEXT FROM cur2 INTO @AreaIdForEventId, @DescriptionForEventArea, @CoordX, @CoordY
                END
                CLOSE cur2
