USE OnlineFoodOrderingDB;

-- 1) Remove wrong primary keys if they exist
-- This keeps the tables able to store many users, many cart items, many orders.

-- 2) Make sure email is the only unique user field
-- Multiple users are allowed, but each email must be different.

-- 3) Check current data
SELECT * FROM tbUsers;
SELECT * FROM tbOrders;
SELECT * FROM tbOrderDetails;
SELECT * FROM tbCart;