create schema if not exists schema1;

create table IF NOT EXISTS schema1.table1
(
    table1id int,
    table1propa varchar(50),
    primary key (table1id)
);