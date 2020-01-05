create procedure CreateSequence 
(
 @seqenceName varchar(128),
 @startValue bigint=1 ,
 @step int=1,
 @EndValue bigint =NULL
)
AS
begin
IF @seqenceName IS NULL BEGIN
    RAISERROR('The @seqenceName should not be null.',11,1)
    RETURN 0
   END 
   IF @startValue IS NULL BEGIN
    RAISERROR('The @startValue should not be null.',11,1)
    RETURN 0
   END
   IF @step IS NULL BEGIN 
    RAISERROR('The @step should not be null.',11,1)
    RETURN 0
   END 
   IF ExistsSequence(@seqenceName)=1 BEGIN
    RAISERROR('The name [%s] sequence already exists.',14,1,@seqenceName)  
    RETURN 0
   END
   
   INSERT INTO Sequences(SequenceName,StartValue,Step,EndValue)
   VALUES(@seqenceName,@startValue,@step,@EndValue)
   
   RETURN @@ROWCOUNT
end
go