create schema if not exists schema1;

create table IF NOT EXISTS schema1.table1
(
    table1id int,
    table1propa varchar(50),
    primary key (table1id)
);

create schema if not exists schema2;

create table IF NOT EXISTS schema2.table1
(
    table2id int,
    table2propa varchar(50),
    primary key (table2id)
);
