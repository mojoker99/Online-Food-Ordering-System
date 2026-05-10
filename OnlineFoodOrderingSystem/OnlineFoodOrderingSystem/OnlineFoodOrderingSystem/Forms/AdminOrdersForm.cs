using OnlineFoodOrderingSystem.DataAccess;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace OnlineFoodOrderingSystem.Forms
{
    public partial class AdminOrdersForm : Form
    {
        private Repository repo = new Repository();

        private DataGridView grid;
        private ComboBox cmbStatus;
        private Button btnUpdate;
        private Label lblTitle;

        public AdminOrdersForm()
        {
            InitializeUI();
            LoadOrders();
        }

        private void InitializeUI()
        {
            BackColor = Color.White;
            Dock = DockStyle.Fill;

            lblTitle = new Label();
            lblTitle.Text = "Manage Orders";
            lblTitle.Font = new Font("Segoe UI", 22, FontStyle.Bold);
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(35, 25);

            grid = new DataGridView();

            grid.Location = new Point(35, 85);
            grid.Size = new Size(1100, 450);

            grid.BackgroundColor = Color.White;
            grid.BorderStyle = BorderStyle.FixedSingle;

            grid.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            grid.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            grid.MultiSelect = false;
            grid.ReadOnly = true;
            grid.AllowUserToAddRows = false;

            grid.RowHeadersWidth = 30;

            grid.ColumnHeadersDefaultCellStyle.Font =
                new Font("Segoe UI", 10, FontStyle.Bold);

            grid.DefaultCellStyle.Font =
                new Font("Segoe UI", 10);

            Label lblStatus = new Label();

            lblStatus.Text = "Update Order Status";

            lblStatus.Font = new Font("Segoe UI", 11);

            lblStatus.AutoSize = true;

            lblStatus.Location = new Point(35, 570);

            cmbStatus = new ComboBox();

            cmbStatus.Location = new Point(35, 600);

            cmbStatus.Width = 260;

            cmbStatus.Font = new Font("Segoe UI", 11);

            cmbStatus.DropDownStyle =
                ComboBoxStyle.DropDownList;

            cmbStatus.Items.Add("Pending");
            cmbStatus.Items.Add("Preparing");
            cmbStatus.Items.Add("Out for Delivery");
            cmbStatus.Items.Add("Delivered");

            cmbStatus.SelectedIndex = 0;

            btnUpdate = new Button();

            btnUpdate.Text = "Update Status";

            btnUpdate.Width = 180;

            btnUpdate.Height = 45;

            btnUpdate.Location = new Point(330, 595);

            btnUpdate.BackColor =
                Color.FromArgb(230, 57, 70);

            btnUpdate.ForeColor = Color.White;

            btnUpdate.FlatStyle = FlatStyle.Flat;

            btnUpdate.FlatAppearance.BorderSize = 0;

            btnUpdate.Font = new Font(
                "Segoe UI",
                10,
                FontStyle.Bold);

            btnUpdate.Click += BtnUpdate_Click;

            Controls.Add(lblTitle);
            Controls.Add(grid);
            Controls.Add(lblStatus);
            Controls.Add(cmbStatus);
            Controls.Add(btnUpdate);
        }

        private void LoadOrders()
        {
            grid.DataSource = repo.GetAllOrders();

            ColorOrderStatuses();
        }

        private void ColorOrderStatuses()
        {
            foreach (DataGridViewRow row in grid.Rows)
            {
                if (row.Cells["OrderStatus"].Value == null)
                    continue;

                string status =
                    row.Cells["OrderStatus"]
                    .Value.ToString();

                if (status == "Pending")
                {
                    row.DefaultCellStyle.BackColor =
                        Color.FromArgb(255, 243, 205);
                }
                else if (status == "Preparing")
                {
                    row.DefaultCellStyle.BackColor =
                        Color.FromArgb(207, 226, 255);
                }
                else if (status == "Out for Delivery")
                {
                    row.DefaultCellStyle.BackColor =
                        Color.FromArgb(226, 217, 243);
                }
                else if (status == "Delivered")
                {
                    row.DefaultCellStyle.BackColor =
                        Color.FromArgb(209, 231, 221);
                }
            }
        }

        private void BtnUpdate_Click(
            object sender,
            EventArgs e)
        {
            try
            {
                if (grid.SelectedRows.Count == 0)
                {
                    MessageBox.Show(
                        "Please select an order first.");

                    return;
                }

                int orderId = Convert.ToInt32(
                    grid.SelectedRows[0]
                    .Cells["OrderId"].Value);

                string status =
                    cmbStatus.SelectedItem.ToString();

                repo.UpdateOrderStatus(
                    orderId,
                    status);

                MessageBox.Show(
                    "Order status updated successfully.",
                    "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                LoadOrders();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Update error: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}