create database siguria;

create table users(
name varchar(50) not null primary key,
hashpassword varchar(200) not null,
saltt varchar(200) not null );

select * from users;
