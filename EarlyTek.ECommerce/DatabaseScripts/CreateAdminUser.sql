USE [EarlyTekECommerce]


GO

DECLARE @userId nvarchar(128) = CAST(NEWID() AS NVARCHAR(128));
DECLARE @roleId nvarchar(128) = CAST(NEWID() AS NVARCHAR(128));

INSERT INTO [dbo].[AspNetRoles]
           ([Id]
           ,[Name])
     VALUES (@roleId,  'Administrators')

INSERT INTO [dbo].[AspNetUsers]
           ([Id]
           ,[UserName]
           ,[PasswordHash]
           ,[SecurityStamp]
           ,[Discriminator])
     VALUES
           (@userId
           ,'demo'				--User Name = demo
           ,'ANLZv14WF8m+q0qeHKVja9vk48UjE1TwGmlnDmrfyo+AP5waTyAFB6Tm0SkYJERQ6w==' --Password = xxxxxx
           ,'496bfc7a-a223-40eb-8d7f-522272d7f29c'
           ,'ApplicationUser')

INSERT INTO [dbo].[AspNetUserRoles]
           ([UserId]
           ,[RoleId])
     VALUES
           (@userId
           ,@roleId)

GO

