USE OnlineFoodOrderingDB;

ALTER TABLE tbUsers
ADD Phone NVARCHAR(20) NULL;

ALTER TABLE tbUsers
ADD Address NVARCHAR(255) NULL;