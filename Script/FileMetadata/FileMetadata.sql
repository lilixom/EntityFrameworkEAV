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
  is '资源ID';
comment on column FILEMETADATA.resourcename
  is '资源名称';
comment on column FILEMETADATA.resourcesize
  is '大小';
comment on column FILEMETADATA.owner
  is '归属人';
comment on column FILEMETADATA.extension
  is '后缀名';
comment on column FILEMETADATA.createdtime
  is '创建时间';
comment on column FILEMETADATA.lastmodifiedtime
  is '最后修改时间';
comment on column FILEMETADATA.revision
  is '当前版本号';
comment on column FILEMETADATA.mediaid
  is '物理路径ID';
comment on column FILEMETADATA.cataloguri
  is '逻辑目录';
comment on column FILEMETADATA.isdel
  is '是否删除';
comment on column FILEMETADATA.lastmodifier
  is '最后修改人';
-- Create/Recreate primary, unique and foreign key constraints 
alter table FILEMETADATA
  add constraint PK_FILEMETADATA primary key (ID);
