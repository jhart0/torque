create schema if not exists schema1;

create schema if not exists schema2;

create schema if not exists schema3;

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

create or replace function schema1.funcA()
returns table (id int)
language 'plpgsql'
as $BODY$
begin
    return query select 1;
end
$BODY$;

create or replace function schema2.funcA()
returns table (id int)
language 'plpgsql'
as $BODY$
begin
    return query select 1;
end
$BODY$;

create or replace function schema2.funcB()
returns table (id int)
language 'plpgsql'
as $BODY$
begin
    return query select 1;
end
$BODY$;