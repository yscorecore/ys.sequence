create procedure GetSequenceValue 
(
 @seqenceName varchar(128),
 @currentValue bigint output
)
AS
BEGIN
 DECLARE @flag int 
 UPDATE Sequences
 SET
  @flag=1,
  @currentValue= case when CurrentValue is null then StartValue
   			   when EndValue is null then CurrentValue + Step
   			   when Step > 0 AND currentValue + Step > EndValue then StartValue
   			   when Step < 0 AND currentValue + Step < EndValue then StartValue
   			   ELSE CurrentValue + Step
			   End,
  CurrentValue=@currentValue
 WHERE [Name]=@seqenceName 
 RETURN @flag
END
go