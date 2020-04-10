create PROCEDURE GetOrCreateSequenceValue(
 IN seqence_name VARCHAR(128),
 OUT current_value BIGINT,
 IN start_value BIGINT ,
 IN end_value bigint ,
 IN step_value INT )
BEGIN
   DECLARE row_id CHAR(36);
   SELECT case when ISNULL(CurrentValue) then StartValue
   			   when ISNULL(EndValue) then CurrentValue + Step
   			   when Step > 0 AND currentValue + Step > EndValue then StartValue
   			   when Step < 0 AND currentValue + Step < EndValue then StartValue
   			   ELSE CurrentValue + Step
		  END , id
   INTO current_value, row_id
   FROM `Sequences`
   WHERE `Name` = seqence_Name ;
   if not isNULL(row_id) then
   	UPDATE `Sequences`
   	SET CurrentValue = current_value
   	WHERE id = row_id;
   ELSE
   	SET current_value = start_value;
   	INSERT INTO `Sequences`(Id,Name,StartValue,EndValue,Step,CurrentValue) 
		VALUES (uuid(), seqence_name, start_value, end_value, step_value, current_value);
   end if;
END