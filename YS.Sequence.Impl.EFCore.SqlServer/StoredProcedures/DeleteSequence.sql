CREATE procedure DeleteSequence
(
 @seqenceName varchar(128)
)
as
begin
 delete Sequences  where SequenceName=@seqenceName
 return @@rowcount
end
go