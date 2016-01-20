-- Create table
create table FILEMETADATA
(
  id               VARCHAR2(40) not null,
  resourcename     VARCHAR2(25),
  resourcesize     NUMBER,
  owner            VARCHAR2(25),
  extension        VARCHAR2(10),
  createdtime      DATE,
  lastmodifiedtime DATE,
  revision         NUMBER,
  mediaid          VARCHAR2(40),
  cataloguri       VARCHAR2(50),
  isdel            NUMBER,
  lastmodifier     VARCHAR2(25)
);
-- Add comments to the columns 
comment on column FILEMETADATA.id
  is '��ԴID';
comment on column FILEMETADATA.resourcename
  is '��Դ����';
comment on column FILEMETADATA.resourcesize
  is '��С';
comment on column FILEMETADATA.owner
  is '������';
comment on column FILEMETADATA.extension
  is '��׺��';
comment on column FILEMETADATA.createdtime
  is '����ʱ��';
comment on column FILEMETADATA.lastmodifiedtime
  is '����޸�ʱ��';
comment on column FILEMETADATA.revision
  is '��ǰ�汾��';
comment on column FILEMETADATA.mediaid
  is '����·��ID';
comment on column FILEMETADATA.cataloguri
  is '�߼�Ŀ¼';
comment on column FILEMETADATA.isdel
  is '�Ƿ�ɾ��';
comment on column FILEMETADATA.lastmodifier
  is '����޸���';
-- Create/Recreate primary, unique and foreign key constraints 
alter table FILEMETADATA
  add constraint PK_FILEMETADATA primary key (ID);
