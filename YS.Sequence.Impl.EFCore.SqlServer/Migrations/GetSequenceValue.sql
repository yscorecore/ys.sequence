create procedure GetSequenceValue 
(
 @seqenceName varchar(128),
 @currentValue bigint output
)
AS
BEGIN
 DECLARE @tmp bigint,@flag int 
 UPDATE Sequences
 SET
  @flag=1,
  @currentValue= ISNULL (CurrentValue,StartValue),
  @tmp=@currentValue+STEP,
  CurrentValue=CASE WHEN EndValue IS NULL THEN @tmp  
      WHEN  @tmp>EndValue THEN @tmp%EndValue+StartValue-1 
      ELSE @tmp END
 WHERE [Name]=@seqenceName 
 RETURN @flag
END
go