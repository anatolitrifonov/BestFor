use bestfor
go


if exists(select * from sys.tables where name = 'sturgeonscores')
begin
	drop table sturgeonscores
	print 'dropped table sturgeonscores'
end
go

if exists(select * from sys.tables where name = 'sturgeonteams')
begin
	drop table sturgeonteams
	print 'dropped table sturgeonteams'
end
go

create table sturgeonteams
(
	id int not null identity(1,1) primary key,
	name nvarchar(200) not null unique,
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

if exists(select * from sys.tables where name = 'sturgeonscores')
begin
	drop table sturgeonscores
	print 'dropped table sturgeonscores'
end
go

create table sturgeonscores
(
	id int not null identity(1,1) primary key,
	team_id int not null foreign key references sturgeonteams(id),
	slot int not null,
	score int not null
)
go

insert sturgeonscores(team_id, slot, score) values(1, 1, 3);
insert sturgeonscores(team_id, slot, score) values(1, 2, 3);
insert sturgeonscores(team_id, slot, score) values(1, 3, 3);
insert sturgeonscores(team_id, slot, score) values(1, 4, 3);
insert sturgeonscores(team_id, slot, score) values(1, 5, 3);
insert sturgeonscores(team_id, slot, score) values(1, 6, 3);
insert sturgeonscores(team_id, slot, score) values(1, 7, 3);
insert sturgeonscores(team_id, slot, score) values(1, 8, 3);
insert sturgeonscores(team_id, slot, score) values(1, 9, 3);
insert sturgeonscores(team_id, slot, score) values(2, 1, 3);
insert sturgeonscores(team_id, slot, score) values(2, 2, 5);
insert sturgeonscores(team_id, slot, score) values(2, 3, 7);
insert sturgeonscores(team_id, slot, score) values(2, 4, 5);
insert sturgeonscores(team_id, slot, score) values(2, 5, 3);
insert sturgeonscores(team_id, slot, score) values(2, 6, 7);
insert sturgeonscores(team_id, slot, score) values(2, 7, 3);
insert sturgeonscores(team_id, slot, score) values(2, 8, 7);
insert sturgeonscores(team_id, slot, score) values(2, 9, 3);
go

