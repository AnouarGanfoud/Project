--Create procedure [dbo].[AddNewClients]  
--(  
   
--   @FirstName nvarchar (50),  
--   @LastName nvarchar (50),  
--   @Email nvarchar (50),
--   @Phone int,
--   @Password nvarchar (50)  
--)  
--as  
--begin  
--   Insert into Client values(@FirstName,@LastName,@Email,@Phone,@Password)  
--End

--Create Procedure [dbo].[GetClientsInfo]  
--as  
--begin  
--   select * from Client
--End

--Create procedure [dbo].[UpdateClientsInfo]  
--(  
--   @CltId int,
--   @FirstName nvarchar (50),  
--   @LastName nvarchar (50),  
--   @Email nvarchar (50),
--   @Phone int,
--   @Password nvarchar (50)  
--)  
--as  
--begin  
--   Update Client   
--   set FirstName=@FirstName,  
-- LastName=@LastName,
-- Email=@Email,
-- Phone=@Phone,
-- Password=@Password
--End

--Create procedure [dbo].[DeleteClient]  
--(  
--   @CltId int  
--)  
--as   
--begin  
--   Delete from Client where Id=@CltId  
--End
CREATE procedure [dbo].[AddNewBestellung]  
(  
   
   @Date Date,
   @Time Time(7),
   @Nbre_P int,
   @Items nvarchar(50),
   @FirstName nvarchar (50),  
   @LastName nvarchar (50), 
   @Adresse nvarchar(50),
   @Phone nvarchar (50),
   @Email nvarchar (50)
  
  
)  
as  
begin  
   Insert into Bestellung values(@Date, @Time, @Nbre_P, @Items,@FirstName,@LastName,@Adresse,@Phone,@Email)  
End