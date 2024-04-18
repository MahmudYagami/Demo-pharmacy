CREATE TABLE USERSP
(
id INT PRIMARY KEY IDENTITY (1,1),
username VARCHAR(MAX) NULL,
password VARCHAR(MAX) NULL,
role VARCHAR(MAX) NULL,
status VARCHAR(MAX) NULL,
date_register DATE NULL
)
INSERT INTO USERSP (username, password,role,status,date_register) VALUES('admin','123','ADMIN','ON','01-02-2024')
SELECT * FROM USERSP

CREATE TABLE catagories
(
   id INT PRIMARY KEY IDENTITY(1,1),
   category VARCHAR(MAX) NULL,
   status VARCHAR(MAX) NULL,
   date_insert DATE NULL
)