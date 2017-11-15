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
   Insert into Bestellung values(@Date, @Time, @Nbre_P, @Items, @FirstName, @LastName, @Adresse,@Phone,@Email)  
End