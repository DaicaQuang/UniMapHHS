DECLARE @i int
DECLARE @Upper int
DECLARE @Lower int
DECLARE @Ran int

SET @i = 8
SET @Upper = 85
SET @Lower = 1


WHILE @i <= 20
BEGIN
SET @Ran = @Lower + CONVERT(INT, (@Upper-@Lower+1)*RAND())
INSERT INTO Histories (Quantity, LocationId)
VALUES (@Ran , 2)
SET @i = @i + 1
SET @Ran = 0;
END

