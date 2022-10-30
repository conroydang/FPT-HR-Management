use master
go
drop database StaffManager
go
create database StaffManager
go
use StaffManager
go
create table [Role]
(
    Id          int identity primary key,
    Title       nvarchar(100),
    Description nvarchar(100),
    IsActive    bit default 1
)
go
insert into [Role](Title,Description) values('Administrator','Admin')
insert into [Role](Title,Description) values('Training Staff','Training Staff')
insert into [Role](Title,Description) values('Trainer','Trainer')

go
create proc GetRoles
as
begin
    select * from [Role] where IsActive = 1
end
go
create proc GetRoleById @Id int
as
begin
    select * from Role where Id = @Id
end
go
--------------------------------------------------------------
create table Users
(
    Id       int identity primary key,
    Role     int not null
        constraint fk_role_user foreign key (Role) references Role (Id),
    Name     nvarchar(50),
    Email    nvarchar(50),
    Password nvarchar(50),
    Dob      date,
    Address  nvarchar(100),
    IsActive bit default 1
)
go
insert into Users(Name,Email,Password,Role) values('admin','admin@gmail.com','admim',1)
go
create proc GetUsers
as
begin
    select Users.Id, Role.Title, Name, Email, Password, Dob, Address
    from Users,
         Role
    where Users.Role = Role.Id
      and Users.IsActive = 1
end
go



go
create proc GetUserById @Id int
as
begin
    select Users.Id, Role.Title, Name, Email, Password, Dob, Address
    from Users,
         Role
    where Users.Role = Role.Id
      and Users.Id = @Id
      and Users.IsActive = 1
end
go
create proc [Login] @Email nvarchar(50), @Password nvarchar(50)
as
begin
select Users.Id, Role.Title, Name, Email, Password, Dob, Address
    from Users,
         Role
    where Users.Role = Role.Id
      and Users.IsActive = 1 and Email = @Email and Password = @Password
end
--------------------------------------------------------------------------
go
create table Trainee
(
    Id       int identity primary key,
    IDUser   int not null
        constraint fk_id_user foreign key (IDUser) references [Users] (Id),
    Note     nvarchar(max),
    IsActive bit default 1
)
go
create proc GetTrainees
as
begin
    select Trainee.Id, Users.Name, Trainee.Note
    from Trainee,
         Users
    where Trainee.IDUser = Users.Id
      and Trainee.IsActive = 1
end
go
create proc GetTraineeById @Id int
as
begin
    select Trainee.Id, Users.Name, Trainee.Note
    from Trainee,
         Users
    where Trainee.IsActive = 1
      and Trainee.IDUser = Users.Id
      and Trainee.Id = @Id
end
--------------------------------------------------------------------------------
go
create table CourseCategories
(
    Id       int identity primary key,
    Name     nvarchar(50),
    Content  nvarchar(max),
    IsActive bit default 1
)
go
create proc GetCategories
as
begin
    select * from CourseCategories where IsActive = 1
end
go
create proc GetCategoryById @Id int
as
begin
    select * from CourseCategories where Id = @Id
end
go

create table Course
(
    Id          int identity primary key,
    Name        nvarchar(50),
    Description nvarchar(max),
    Picture     nvarchar(max),
    LastUpdate  date,
    Category    int not null
        constraint fk_cate_course foreign key (Category) references CourseCategories (Id),
    IsActive    bit default 1
)
go
create proc GetCourses
as
begin
    select Course.Id,
           Course.Name[Course Name],
           Description,
           Picture,
           LastUpdate,
           CourseCategories.Name[Course Category]
    from CourseCategories,
         Course
    where Course.Category = CourseCategories.Id
      and Course.IsActive = 1
end
go
create proc GetCourseById @Id int
as
begin
    select Course.Id,
           Course.Name[Course Name],
           Description,
           Picture,
           LastUpdate,
           CourseCategories.Name[Course Category]
    from CourseCategories,
         Course
    where Course.Category = CourseCategories.Id
      and Course.IsActive = 1
      and Course.Id = @Id
end
go
create table Topic
(
    Id       int identity primary key,
    Name     nvarchar(50),
    Content  nvarchar(max),
    Field    nvarchar(max),
    Course   int not null
        constraint fk_course_topic foreign key (Course) references Course (Id),
    IsActive bit default 1
)
go
create proc GetTopicByCourse @IdCourse int
as
begin
    select Topic.Id, Topic.Name[Name Topic], Content, Field, Course.Name[Course Name]
    from Topic,
         Course
    where Topic.Course = @IdCourse
      and Topic.IsActive = 1
      and Topic.Course = Course.Id
end
go
create proc GetTopicById @Id int
as
begin
    select Topic.Id, Topic.Name[Name Topic], Content, Field, Course.Name[Course Name]
    from Topic,
         Course
    where Topic.Id = @Id
      and Topic.IsActive = 1
      and Topic.Course = Course.Id
end
-----------------------------------------------------------------------------------
go
create table Trainer
(
    Id       int identity primary key,
    IDUser   int not null unique
        constraint fk_trainer_user foreign key (IDUser) references Users (Id),
    IsActive bit default 1
)
go
create proc GetTraineres
as
begin
    select Trainer.Id, Users.Name
    from Trainer,
         [Users]
    where Trainer.IsActive = 1
      and Trainer.IDUser = Users.Id
end
go
create proc GetTrainerById @Id int
as
begin
    select Trainer.Id, Users.Name
    from Trainer,
         [Users]
    where Trainer.IsActive = 1
      and Trainer.IDUser = Users.Id
      and Trainer.Id = @Id
end
------------------------------------------------------------------------------------
go
create table TrainingStaff
(
    Id        int identity primary key,
    Name      nvarchar(100),
    IdTrainee int not null
        constraint fk_trainee foreign key (IdTrainee) references Trainee (Id),
    IdTrainer int not null
        constraint fk_trainer foreign key (IdTrainer) references Trainer (Id),
    IdCourse  int not null
        constraint fk_course foreign key (IdCourse) references Course (Id),
    IsActive  bit default 1
)
go
create proc GetTrainingStaffes
as
begin
    select TrainingStaff.Id,
           TrainingStaff.Name[Training Staff Name],
           Trainee.IDUser[ID Trainee],
           Trainer.IDUser[ID Trainer],
           Course.Name[Course Name]

    from TrainingStaff,
         Trainer,
         Trainee,
         Course
    where TrainingStaff.IdTrainee = Trainee.Id
      and TrainingStaff.IdTrainer = Trainer.Id
      and TrainingStaff.IsActive = 1
      and Course.Id = TrainingStaff.IdCourse
end
go
create proc GetTrainingStaff @Id int
as
begin
    select TrainingStaff.Id,
           TrainingStaff.Name[Training Staff Name],
           Trainee.IDUser[ID Trainee],
           Trainer.IDUser[ID Trainer],
           Course.Name[Course Name]
    from TrainingStaff,
         Trainer,
         Trainee,
         Course
    where TrainingStaff.IdTrainee = Trainee.Id
      and TrainingStaff.IdTrainer = Trainer.Id
      and TrainingStaff.IsActive = 1
      and Course.Id = TrainingStaff.IdCourse
      and TrainingStaff.Id = @Id
end
go
