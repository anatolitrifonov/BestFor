use bestfor
go


if exists(select * from sys.tables where name = 'sturgeonteams')
begin
	drop table sturgeonteams
	print 'dropped table'
end
go

create table sturgeonteams
(
	id int not null identity(1,1) primary key,
	name nvarchar(200) not null,
	secret_string nvarchar(200) not null
)
go

insert sturgeonteams(name, secret_string) values (N'crazy team 1', N'password');
insert sturgeonteams(name, secret_string) values (N'crazy team 2', N'password');
insert sturgeonteams(name, secret_string) values (N'crazy team 3', N'password');
insert sturgeonteams(name, secret_string) values (N'crazy team 4', N'password');
insert sturgeonteams(name, secret_string) values (N'crazy team 5', N'password');
insert sturgeonteams(name, secret_string) values (N'crazy team 6', N'password');
insert sturgeonteams(name, secret_string) values (N'crazy team 7', N'password');
go
