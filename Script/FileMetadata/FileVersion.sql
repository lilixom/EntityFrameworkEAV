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
  is '��ԴID';
comment on column FILEVERSION.id
  is '�汾ID';
comment on column FILEVERSION.revision
  is '�޶���';
comment on column FILEVERSION.remark
  is '�޶���ע';
comment on column FILEVERSION.modifiedtime
  is '�޶�����';
comment on column FILEVERSION.modifier
  is '�޶�����';
-- Create/Recreate primary, unique and foreign key constraints 
alter table FILEVERSION
  add constraint PK_FILEVERSION_FILEVERS primary key (ID);
