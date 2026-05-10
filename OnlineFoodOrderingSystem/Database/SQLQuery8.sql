USE OnlineFoodOrderingDB;

UPDATE tbUsers
SET 
    Email = 'admin@food.com',
    PasswordHash = '240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9',
    Role = 'Admin'
WHERE FullName = 'System Admin';

SELECT * FROM tbUsers;