create function ExistsSequence
(
 @seqenceName varchar(100) 
)
RETURNS int
AS
BEGIN
 DECLARE @count int
 SELECT @count=COUNT(SequenceName) FROM Sequences WHERE SequenceName=@seqenceName
 RETURN @count
END
go