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
 
 IF @flag IS NULL--no record
 BEGIN
   INSERT INTO Sequences([Id],[Name],StartValue,EndValue,Step) VALUES(newid(), @seqenceName,@startValue,@endValue,@step) --²åÈëÐòÁÐ
   UPDATE Sequences
   SET
    @flag=1,
    @currentValue= ISNULL (CurrentValue,StartValue),
    @tmp=@currentValue+STEP,
    CurrentValue=CASE WHEN EndValue IS NULL THEN @tmp  
        WHEN  @tmp>EndValue THEN @tmp%EndValue+StartValue-1 
        ELSE @tmp END
   WHERE [Name]=@seqenceName    
   
 END

 RETURN @flag 
END