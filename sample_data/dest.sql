create schema if not exists schema1;

create schema if not exists schema2;


create table IF NOT EXISTS schema1.table1
(
    table1id int,
    table1propa varchar(50),
    primary key (table1id)
);

create or replace function schema2.funcA()
returns table (id int)
language 'plpgsql'
as $BODY$
begin
    return query select 2;
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