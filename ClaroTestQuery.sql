create database ClaroTestDb
go

use ClaroTestDb
go

create table ClassRooms(
	Id int not null identity(1,1),
	[Name] varchar(30) not null,
	Code varchar(10) not null,
	[Description] varchar(100) not null,
	TeachersQuantity int not null,
	StudentsQuantity int not null,
	constraint Pk_ClassRooms primary key (Id),
	constraint Idx_ClassRooms_Code unique (Code)
)
go

create table Teachers(
	Id int not null identity(1,1),
	FullName varchar(30) not null,
	Phone varchar(20) not null,
	Email varchar(50),
	constraint Pk_Teachers primary key (Id),
	constraint Idx_Teachers_Phone unique (Phone),
	constraint Idx_Teachers_Email unique (Email)
)
go

create table Students(
	Id int not null identity(1,1),
	FullName varchar(30) not null,
	Phone varchar(20) not null,
	Email varchar(50),
	Enrollment varchar(10),
	constraint Pk_Students primary key (Id),
	constraint Idx_Students_Phone unique (Phone),
	constraint Idx_Students_Email unique (Email),
	constraint Idx_Students_Enrollment unique (Enrollment)
)
go

create table DaysOfWeek(
	Id int not null identity(1,1),
	[Day] varchar(10) not null,

	constraint Pk_DaysOfWeek primary key(Id),
	constraint Idx_DaysOfWeek_Daye unique ([Day])
)
go

create table ClassRoomAssignments(
	Id int not null identity(1,1),
	TeacherId int not null,
	StudentId int not null,
	ClassRoomId int not null,
	DayOfWeekId int not null,
	StartHour datetime not null,
	EndHour datetime not null,

	constraint Pk_ClassRoomAssignments primary key(Id, TeacherId, StudentId),
	constraint Fk_ClassRoomAssignments_Teachers foreign key (TeacherId) references Teachers(Id),
	constraint Fk_ClassRoomAssignments_Students foreign key (StudentId) references Students(Id),
	constraint Fk_ClassRoomAssignments_ClassRoom foreign key (ClassRoomId) references ClassRooms(Id),
	constraint Fk_ClassRoomAssignments_DaysOfWeek foreign key (DayOfWeekId) references DaysOfWeek(Id)
)
go


insert into DaysOfWeek values('Lunes')
insert into DaysOfWeek values('Martes')
insert into DaysOfWeek values('Miércoles')
insert into DaysOfWeek values('Jueves')
insert into DaysOfWeek values('Viernes')
insert into DaysOfWeek values('Sábado')
insert into DaysOfWeek values('Domingo')
go
