-- Create table
create table FILETAG
(
  id      VARCHAR2(40) not null,
  tagname VARCHAR2(40),
  fileid  VARCHAR2(40)
);
-- Add comments to the columns 
comment on column FILETAG.id
  is '版本ID';
comment on column FILETAG.fileid
  is 'TAG引用数';
-- Create/Recreate primary, unique and foreign key constraints 
alter table FILETAG
  add constraint PK_FILETAG primary key (ID);
