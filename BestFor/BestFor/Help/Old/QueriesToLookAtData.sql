use bestfor
go

-- select * from sys.tables where name like 'asp%'
select * from AspNetRoleClaims
select * from AspNetRoles
select * from AspNetUserClaims
select * from AspNetUserLogins
select * from AspNetUserRoles
select * from AspNetUsers
-- admin user ...
insert AspNetUsers(id, AccessFailedCount, ConcurrencyStamp, Email, EmailConfirmed, LockoutEnabled, LockoutEnd, 
NormalizedEmail, NormalizedUserName, PasswordHash, PhoneNumber, 
PhoneNumberConfirmed, SecurityStamp, TwoFactorEnabled, UserName)
values(NEWID(), 0, NEWID(), 'admin1@bestfor.com', 0, 1, NULL,
'ADMIN1@BESTFOR.COM', 'ADMIN1@BESTFOR.COM', 'AQAAAAEAACcQAAAAEFR27QT3IEEovW6z8sRIv3ZPGV1S4y/jWYyBIYlXZhnx321GSYn4WmIAPnGPMbf57Q==', NULL,
0, NEWID(), 0, 'admin1@bestfor.com') -- 1_Bestfor
go


