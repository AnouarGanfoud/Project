create proc SP_Login
@Email nvarchar(50),
@Password nvarchar(max),
@Isvalid bit out
as
begin
Set @Isvalid = (Select COUNT(Email) from Client where Email=@Email and [Password]=@Password)
end