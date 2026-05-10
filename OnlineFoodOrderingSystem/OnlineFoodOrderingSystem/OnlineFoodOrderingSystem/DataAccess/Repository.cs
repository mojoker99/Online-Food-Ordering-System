using OnlineFoodOrderingSystem.Common;
using System;
using System.Data;
using System.Data.SqlClient;

namespace OnlineFoodOrderingSystem.DataAccess
{
    public class Repository
    {
        public bool Login(string email, string password)
        {
            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand(
                @"SELECT UserId, FullName, Role, PasswordHash
                  FROM tbUsers
                  WHERE Email = @Email", con))
            {
                cmd.Parameters.AddWithValue("@Email", email);
                con.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (!reader.Read())
                        return false;

                    string storedHash = reader["PasswordHash"].ToString();
                    string enteredHash = PasswordHasher.Hash(password);

                    if (storedHash != enteredHash)
                        return false;

                    AppSession.UserId = Convert.ToInt32(reader["UserId"]);
                    AppSession.FullName = reader["FullName"].ToString();
                    AppSession.Role = reader["Role"].ToString();
                    AppSession.Email = email;

                    return true;
                }
            }
        }

        public void Register(string fullName, string email, string phone, string address, string password)
        {
            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand(
                @"INSERT INTO tbUsers
                  (FullName, Email, Phone, Address, PasswordHash, Role)
                  VALUES
                  (@FullName, @Email, @Phone, @Address, @PasswordHash, 'Customer')", con))
            {
                cmd.Parameters.AddWithValue("@FullName", fullName);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Phone", phone);
                cmd.Parameters.AddWithValue("@Address", address);
                cmd.Parameters.AddWithValue("@PasswordHash", PasswordHasher.Hash(password));

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public DataTable GetMenuItems()
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = Database.GetConnection())
            using (SqlDataAdapter da = new SqlDataAdapter(
                "SELECT * FROM tbMenuItems WHERE IsAvailable = 1",
                con))
            {
                da.Fill(dt);
            }

            return dt;
        }

        public DataTable SearchMenu(string keyword)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand(
                @"SELECT *
                  FROM tbMenuItems
                  WHERE IsAvailable = 1
                  AND (ItemName LIKE @Keyword OR Category LIKE @Keyword OR Description LIKE @Keyword)", con))
            {
                cmd.Parameters.AddWithValue("@Keyword", "%" + keyword + "%");

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
            }

            return dt;
        }

        public void SaveMenuItem(int id, string itemName, string category, string description, decimal price, bool available)
        {
            using (SqlConnection con = Database.GetConnection())
            {
                SqlCommand cmd;

                if (id == 0)
                {
                    cmd = new SqlCommand(
                        @"INSERT INTO tbMenuItems
                          (ItemName, Category, Description, Price, IsAvailable)
                          VALUES
                          (@ItemName, @Category, @Description, @Price, @IsAvailable)", con);
                }
                else
                {
                    cmd = new SqlCommand(
                        @"UPDATE tbMenuItems
                          SET ItemName=@ItemName,
                              Category=@Category,
                              Description=@Description,
                              Price=@Price,
                              IsAvailable=@IsAvailable
                          WHERE MenuItemId=@MenuItemId", con);

                    cmd.Parameters.AddWithValue("@MenuItemId", id);
                }

                cmd.Parameters.AddWithValue("@ItemName", itemName);
                cmd.Parameters.AddWithValue("@Category", category);
                cmd.Parameters.AddWithValue("@Description", description);
                cmd.Parameters.AddWithValue("@Price", price);
                cmd.Parameters.AddWithValue("@IsAvailable", available);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteMenuItem(int id)
        {
            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand(
                "DELETE FROM tbMenuItems WHERE MenuItemId=@Id", con))
            {
                cmd.Parameters.AddWithValue("@Id", id);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void AddToCart(int userId, int menuItemId, int quantity)
        {
            using (SqlConnection con = Database.GetConnection())
            {
                con.Open();

                string checkQuery = @"
                    SELECT CartId, Quantity
                    FROM tbCart
                    WHERE UserId=@UserId AND MenuItemId=@MenuItemId";

                using (SqlCommand checkCmd = new SqlCommand(checkQuery, con))
                {
                    checkCmd.Parameters.AddWithValue("@UserId", userId);
                    checkCmd.Parameters.AddWithValue("@MenuItemId", menuItemId);

                    using (SqlDataReader reader = checkCmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int cartId = Convert.ToInt32(reader["CartId"]);
                            int oldQuantity = Convert.ToInt32(reader["Quantity"]);
                            reader.Close();

                            using (SqlCommand updateCmd = new SqlCommand(
                                @"UPDATE tbCart
                                  SET Quantity=@Quantity
                                  WHERE CartId=@CartId", con))
                            {
                                updateCmd.Parameters.AddWithValue("@Quantity", oldQuantity + quantity);
                                updateCmd.Parameters.AddWithValue("@CartId", cartId);
                                updateCmd.ExecuteNonQuery();
                            }

                            return;
                        }
                    }
                }

                using (SqlCommand insertCmd = new SqlCommand(
                    @"INSERT INTO tbCart
                      (UserId, MenuItemId, Quantity)
                      VALUES
                      (@UserId, @MenuItemId, @Quantity)", con))
                {
                    insertCmd.Parameters.AddWithValue("@UserId", userId);
                    insertCmd.Parameters.AddWithValue("@MenuItemId", menuItemId);
                    insertCmd.Parameters.AddWithValue("@Quantity", quantity);
                    insertCmd.ExecuteNonQuery();
                }
            }
        }

        public DataTable GetCart(int userId)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand(
                @"SELECT
                    c.CartId,
                    c.MenuItemId,
                    m.ItemName,
                    m.Category,
                    m.Price,
                    c.Quantity,
                    (m.Price * c.Quantity) AS TotalPrice
                  FROM tbCart c
                  INNER JOIN tbMenuItems m ON c.MenuItemId = m.MenuItemId
                  WHERE c.UserId = @UserId", con))
            {
                cmd.Parameters.AddWithValue("@UserId", userId);

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
            }

            return dt;
        }

        public void RemoveCartItem(int cartId)
        {
            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand(
                "DELETE FROM tbCart WHERE CartId=@CartId", con))
            {
                cmd.Parameters.AddWithValue("@CartId", cartId);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void ClearCart(int userId)
        {
            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand(
                "DELETE FROM tbCart WHERE UserId=@UserId", con))
            {
                cmd.Parameters.AddWithValue("@UserId", userId);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public int CreateOrder(int userId, decimal totalAmount, string paymentMethod)
        {
            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand(
                @"INSERT INTO tbOrders
                  (UserId, OrderDate, TotalAmount, PaymentMethod, OrderStatus)
                  OUTPUT INSERTED.OrderId
                  VALUES
                  (@UserId, GETDATE(), @TotalAmount, @PaymentMethod, 'Pending')", con))
            {
                cmd.Parameters.AddWithValue("@UserId", userId);
                cmd.Parameters.AddWithValue("@TotalAmount", totalAmount);
                cmd.Parameters.AddWithValue("@PaymentMethod", paymentMethod);

                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public void AddOrderDetail(int orderId, int menuItemId, int quantity, decimal price)
        {
            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand(
                @"INSERT INTO tbOrderDetails
                  (OrderId, MenuItemId, Quantity, UnitPrice)
                  VALUES
                  (@OrderId, @MenuItemId, @Quantity, @UnitPrice)", con))
            {
                cmd.Parameters.AddWithValue("@OrderId", orderId);
                cmd.Parameters.AddWithValue("@MenuItemId", menuItemId);
                cmd.Parameters.AddWithValue("@Quantity", quantity);
                cmd.Parameters.AddWithValue("@UnitPrice", price);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public DataTable GetOrders(int userId)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand(
                @"SELECT OrderId, OrderDate, TotalAmount, PaymentMethod, OrderStatus
                  FROM tbOrders
                  WHERE UserId=@UserId
                  ORDER BY OrderDate DESC", con))
            {
                cmd.Parameters.AddWithValue("@UserId", userId);

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
            }

            return dt;
        }

        public DataTable GetAllOrders()
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = Database.GetConnection())
            using (SqlDataAdapter da = new SqlDataAdapter(
                @"SELECT
                    o.OrderId,
                    u.FullName,
                    o.OrderDate,
                    o.TotalAmount,
                    o.PaymentMethod,
                    o.OrderStatus
                  FROM tbOrders o
                  INNER JOIN tbUsers u ON o.UserId = u.UserId
                  ORDER BY o.OrderDate DESC", con))
            {
                da.Fill(dt);
            }

            return dt;
        }

        public void UpdateOrderStatus(int orderId, string status)
        {
            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand(
                @"UPDATE tbOrders
                  SET OrderStatus=@Status
                  WHERE OrderId=@OrderId", con))
            {
                cmd.Parameters.AddWithValue("@Status", status);
                cmd.Parameters.AddWithValue("@OrderId", orderId);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public int GetTotalUsers()
        {
            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM tbUsers", con))
            {
                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public int GetTotalMenuItems()
        {
            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM tbMenuItems", con))
            {
                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public int GetTotalOrders()
        {
            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM tbOrders", con))
            {
                con.Open();
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public decimal GetTotalRevenue()
        {
            using (SqlConnection con = Database.GetConnection())
            using (SqlCommand cmd = new SqlCommand("SELECT ISNULL(SUM(TotalAmount),0) FROM tbOrders", con))
            {
                con.Open();
                return Convert.ToDecimal(cmd.ExecuteScalar());
            }
        }
    }
}