CREATE procedure GetOrCreateSequenceValue 
(
  @seqenceName varchar(128),
  @currentValue bigint output,
  @startValue bigint = 1,
  @endValue bigint = null,
  @step int =1
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
 
 IF @flag IS NULL--no record
 BEGIN
   Set @currentValue = @startValue
   INSERT INTO Sequences([Id],[Name],StartValue,EndValue,Step,CurrentValue) 
   VALUES(newid(), @seqenceName,@startValue,@endValue,@step,@currentValue) --²åÈëÐòÁÐ
 END

 RETURN @flag 
END