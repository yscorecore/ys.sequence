create procedure ResetSequence 
(
 @seqenceName varchar(128)
)
AS
BEGIN
 UPDATE Sequences
 SET CurrentValue = NULL 
 WHERE SequenceName=@seqenceName
 RETURN @@ROWCOUNT
END
go