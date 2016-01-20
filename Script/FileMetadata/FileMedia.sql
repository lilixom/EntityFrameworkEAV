-- Create table
create table FILEMEDIA
(
  id           VARCHAR2(40) not null,
  relativepath VARCHAR2(200),
  fullpath     VARCHAR2(200),
  checksum     VARCHAR2(128)
);
-- Add comments to the columns 
comment on column FILEMEDIA.id
  is '版本ID';
comment on column FILEMEDIA.relativepath
  is '相对路径';
comment on column FILEMEDIA.fullpath
  is '绝对路径';
-- Create/Recreate primary, unique and foreign key constraints 
alter table FILEMEDIA
  add constraint PK_FILEMEDIA primary key (ID);
