create function ExistsSequence
(
 @seqenceName varchar(100) 
)
RETURNS int
AS
BEGIN
 DECLARE @count int
 SELECT @count=COUNT([Name]) FROM Sequences WHERE [Name]=@seqenceName
 RETURN @count
END
go