create procedure ResetSequence 
(
 @seqenceName varchar(128)
)
AS
BEGIN
 UPDATE Sequences
 SET CurrentValue = NULL 
 WHERE [Name]=@seqenceName
 RETURN @@ROWCOUNT
END
go