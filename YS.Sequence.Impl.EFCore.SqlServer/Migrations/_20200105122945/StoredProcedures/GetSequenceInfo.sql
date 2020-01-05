create procedure GetSequenceInfo 
(
 @seqenceName varchar(128) 
)
AS
BEGIN
 SELECT * FROM Sequences WHERE [Name]=@seqenceName
 RETURN @@ROWCOUNT
END
go