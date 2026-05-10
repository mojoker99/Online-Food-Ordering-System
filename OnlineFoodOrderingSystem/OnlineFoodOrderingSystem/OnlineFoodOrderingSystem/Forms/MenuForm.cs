using OnlineFoodOrderingSystem.Common;
using OnlineFoodOrderingSystem.DataAccess;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace OnlineFoodOrderingSystem.Forms
{
    public partial class MenuForm : Form
    {
        private Repository repo = new Repository();

        private DataGridView grid;
        private TextBox txtSearch;
        private Button btnSearch;
        private Button btnAddCart;
        private Button btnCheckout;
        private NumericUpDown qty;

        public MenuForm()
        {
            InitializeUI();
            LoadMenu();
        }

        private void InitializeUI()
        {
            BackColor = Color.White;
            Dock = DockStyle.Fill;

            Label title = new Label();
            title.Text = "Browse Menu";
            title.Font = new Font("Segoe UI", 20, FontStyle.Bold);
            title.AutoSize = true;
            title.Location = new Point(30, 25);

            txtSearch = new TextBox();
            txtSearch.Location = new Point(30, 90);
            txtSearch.Width = 350;
            txtSearch.Font = new Font("Segoe UI", 11);

            btnSearch = CreateButton("Search");
            btnSearch.Location = new Point(400, 85);
            btnSearch.Click += BtnSearch_Click;

            grid = new DataGridView();
            grid.Location = new Point(30, 145);
            grid.Size = new Size(950, 380);
            grid.BackgroundColor = Color.White;
            grid.BorderStyle = BorderStyle.FixedSingle;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.MultiSelect = false;
            grid.ReadOnly = true;
            grid.AllowUserToAddRows = false;
            grid.RowHeadersWidth = 30;

            Label lblQty = new Label();
            lblQty.Text = "Quantity";
            lblQty.Font = new Font("Segoe UI", 10);
            lblQty.AutoSize = true;
            lblQty.Location = new Point(30, 550);

            qty = new NumericUpDown();
            qty.Location = new Point(30, 575);
            qty.Width = 100;
            qty.Minimum = 1;
            qty.Maximum = 20;
            qty.Value = 1;
            qty.Font = new Font("Segoe UI", 11);

            btnAddCart = CreateButton("Add to Cart");
            btnAddCart.Location = new Point(160, 565);
            btnAddCart.Click += BtnAddCart_Click;

            btnCheckout = CreateButton("Go to Cart");
            btnCheckout.Location = new Point(340, 565);
            btnCheckout.Click += BtnCheckout_Click;

            Controls.Add(title);
            Controls.Add(txtSearch);
            Controls.Add(btnSearch);
            Controls.Add(grid);
            Controls.Add(lblQty);
            Controls.Add(qty);
            Controls.Add(btnAddCart);
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

        private void LoadMenu()
        {
            grid.DataSource = repo.GetMenuItems();
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            grid.DataSource = repo.SearchMenu(txtSearch.Text.Trim());
        }

        private void BtnAddCart_Click(object sender, EventArgs e)
        {
            try
            {
                if (grid.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Please select a menu item first.");
                    return;
                }

                int menuItemId = Convert.ToInt32(
                    grid.SelectedRows[0].Cells["MenuItemId"].Value
                );

                int quantity = Convert.ToInt32(qty.Value);

                repo.AddToCart(
                    AppSession.UserId,
                    menuItemId,
                    quantity
                );

                MessageBox.Show(
                    "Item added to cart successfully.",
                    "Cart",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Add to cart error: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void BtnCheckout_Click(object sender, EventArgs e)
        {
            CartForm cart = new CartForm();
            cart.ShowDialog();
        }
    }
}