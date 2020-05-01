DECLARE @i int
DECLARE @Upper int
DECLARE @Lower int
DECLARE @Ran int

SET @i = 0
SET @Upper = 25
SET @Lower = 1



WHILE @i <= 12
BEGIN
SET @Ran = @Lower + CONVERT(INT, (@Upper-@Lower+1)*RAND())
INSERT INTO Histories (Quantity, LocationId, TimeStamp)
VALUES (@Ran , 11, (SELECT DATEADD(hour, @i, '04/24/2020 08:00:00')))
SET @i = @i + 1
SET @Ran = 0;
END