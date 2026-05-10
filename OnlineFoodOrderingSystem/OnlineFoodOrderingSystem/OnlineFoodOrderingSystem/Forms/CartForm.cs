using OnlineFoodOrderingSystem.Common;
using OnlineFoodOrderingSystem.DataAccess;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace OnlineFoodOrderingSystem.Forms
{
    public partial class CartForm : Form
    {
        private Repository repo = new Repository();

        private DataGridView grid;
        private Label lblTotal;
        private ComboBox cmbPayment;

        private Button btnRefresh;
        private Button btnRemove;
        private Button btnCheckout;

        public CartForm()
        {
            InitializeUI();
            LoadCart();
        }

        private void InitializeUI()
        {
            BackColor = Color.White;
            Dock = DockStyle.Fill;

            Label title = new Label();
            title.Text = "My Cart";
            title.Font = new Font("Segoe UI", 20, FontStyle.Bold);
            title.AutoSize = true;
            title.Location = new Point(30, 25);

            grid = new DataGridView();
            grid.Location = new Point(30, 85);
            grid.Size = new Size(900, 360);
            grid.BackgroundColor = Color.White;
            grid.BorderStyle = BorderStyle.FixedSingle;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.MultiSelect = false;
            grid.ReadOnly = true;
            grid.AllowUserToAddRows = false;
            grid.RowHeadersWidth = 30;

            lblTotal = new Label();
            lblTotal.Text = "Total: 0.00 EGP";
            lblTotal.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            lblTotal.AutoSize = true;
            lblTotal.Location = new Point(30, 470);

            Label lblPayment = new Label();
            lblPayment.Text = "Payment Method";
            lblPayment.Font = new Font("Segoe UI", 10);
            lblPayment.AutoSize = true;
            lblPayment.Location = new Point(30, 525);

            cmbPayment = new ComboBox();
            cmbPayment.Location = new Point(30, 550);
            cmbPayment.Width = 250;
            cmbPayment.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPayment.Font = new Font("Segoe UI", 11);
            cmbPayment.Items.Add("Cash on Delivery");
            cmbPayment.Items.Add("Online Payment");
            cmbPayment.SelectedIndex = 0;

            btnRefresh = CreateButton("Refresh");
            btnRefresh.Location = new Point(320, 545);
            btnRefresh.Click += BtnRefresh_Click;

            btnRemove = CreateButton("Remove Item");
            btnRemove.Location = new Point(480, 545);
            btnRemove.Click += BtnRemove_Click;

            btnCheckout = CreateButton("Place Order");
            btnCheckout.Location = new Point(660, 545);
            btnCheckout.Click += BtnCheckout_Click;

            Controls.Add(title);
            Controls.Add(grid);
            Controls.Add(lblTotal);
            Controls.Add(lblPayment);
            Controls.Add(cmbPayment);
            Controls.Add(btnRefresh);
            Controls.Add(btnRemove);
            Controls.Add(btnCheckout);
        }

        private Button CreateButton(string text)
        {
            Button btn = new Button();
            btn.Text = text;
            btn.Width = 150;
            btn.Height = 45;
            btn.BackColor = Color.FromArgb(230, 57, 70);
            btn.ForeColor = Color.White;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            return btn;
        }

        private void LoadCart()
        {
            try
            {
                DataTable dt = repo.GetCart(AppSession.UserId);
                grid.DataSource = dt;

                decimal total = 0;

                foreach (DataRow row in dt.Rows)
                {
                    total += Convert.ToDecimal(row["TotalPrice"]);
                }

                lblTotal.Text = "Total: " + total.ToString("0.00") + " EGP";
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Cart load error: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            LoadCart();
        }

        private void BtnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                if (grid.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Please select an item to remove.");
                    return;
                }

                int cartId = Convert.ToInt32(
                    grid.SelectedRows[0].Cells["CartId"].Value
                );

                repo.RemoveCartItem(cartId);

                MessageBox.Show("Item removed from cart.");

                LoadCart();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Remove error: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void BtnCheckout_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable cart = repo.GetCart(AppSession.UserId);

                if (cart.Rows.Count == 0)
                {
                    MessageBox.Show("Your cart is empty.");
                    return;
                }

                decimal total = 0;

                foreach (DataRow row in cart.Rows)
                {
                    total += Convert.ToDecimal(row["TotalPrice"]);
                }

                string paymentMethod = cmbPayment.SelectedItem.ToString();

                int orderId = repo.CreateOrder(
                    AppSession.UserId,
                    total,
                    paymentMethod
                );

                foreach (DataRow row in cart.Rows)
                {
                    repo.AddOrderDetail(
                        orderId,
                        Convert.ToInt32(row["MenuItemId"]),
                        Convert.ToInt32(row["Quantity"]),
                        Convert.ToDecimal(row["Price"])
                    );
                }

                repo.ClearCart(AppSession.UserId);

                MessageBox.Show(
                    "Order placed successfully.\nYour order number is: " + orderId,
                    "Order Confirmation",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                LoadCart();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Checkout error: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
    }
}