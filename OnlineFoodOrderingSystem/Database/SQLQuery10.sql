USE OnlineFoodOrderingDB;

-- Reset ALL users to password: admin123

UPDATE tbUsers
SET PasswordHash =
'240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9';

SELECT UserId, FullName, Email, Role
FROM tbUsers;