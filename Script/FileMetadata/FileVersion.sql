-- Create table
create table FILEVERSION
(
  fileid       VARCHAR2(40),
  id           VARCHAR2(40) not null,
  fileinfor    CLOB,
  revision     NUMBER,
  remark       VARCHAR2(1000),
  modifiedtime DATE,
  modifier     VARCHAR2(40)
);
-- Add comments to the columns 
comment on column FILEVERSION.fileid
  is '资源ID';
comment on column FILEVERSION.id
  is '版本ID';
comment on column FILEVERSION.revision
  is '修订号';
comment on column FILEVERSION.remark
  is '修订备注';
comment on column FILEVERSION.modifiedtime
  is '修订日期';
comment on column FILEVERSION.modifier
  is '修订任务';
-- Create/Recreate primary, unique and foreign key constraints 
alter table FILEVERSION
  add constraint PK_FILEVERSION_FILEVERS primary key (ID);
