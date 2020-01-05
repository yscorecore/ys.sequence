CREATE procedure DeleteSequence
(
 @seqenceName varchar(128)
)
as
begin
 delete Sequences  where [Name]=@seqenceName
 return @@rowcount
end
go