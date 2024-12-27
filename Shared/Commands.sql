mysql -u admin -p -h tradingplatformdb.cl8aqoogwsx7.eu-north-1.rds.amazonaws.com

---------------------------------
SET FOREIGN_KEY_CHECKS = 0;

DELETE FROM Users;
DELETE FROM Orders;

SET FOREIGN_KEY_CHECKS = 1;
---------------------------------

DESCRIBE Users;

---------------------------------
--SQL Command to Move the Column

ALTER TABLE Users;= 
MODIFY COLUMN Email varchar(255) NOT NULL AFTER PasswordHash;

---------------------------------
--SQL Command to change column name 
ALTER TABLE Users
CHANGE COLUMN PasswordHash Password varchar(255) NOT NULL;
