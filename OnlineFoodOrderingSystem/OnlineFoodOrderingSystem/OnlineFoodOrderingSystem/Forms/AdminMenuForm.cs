using OnlineFoodOrderingSystem.DataAccess;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace OnlineFoodOrderingSystem.Forms
{
    public partial class AdminMenuForm : Form
    {
        private Repository repo = new Repository();

        private TextBox txtName;
        private TextBox txtCategory;
        private TextBox txtDescription;
        private TextBox txtPrice;

        private CheckBox chkAvailable;

        private Button btnSave;
        private Button btnDelete;
        private Button btnClear;

        private DataGridView grid;

        private int selectedId = 0;

        public AdminMenuForm()
        {
            InitializeUI();
            LoadData();
        }

        private void InitializeUI()
        {
            BackColor = Color.White;
            Dock = DockStyle.Fill;

            Panel leftPanel = new Panel();
            leftPanel.Dock = DockStyle.Left;
            leftPanel.Width = 520;
            leftPanel.Padding = new Padding(30, 20, 20, 20);
            leftPanel.BackColor = Color.White;

            Panel rightPanel = new Panel();
            rightPanel.Dock = DockStyle.Fill;
            rightPanel.Padding = new Padding(20, 85, 30, 60);
            rightPanel.BackColor = Color.White;

            Label title = new Label();
            title.Text = "Manage Menu Items";
            title.Font = new Font("Segoe UI", 20, FontStyle.Bold);
            title.AutoSize = true;
            title.Location = new Point(30, 20);

            Label lblName = CreateLabel("Item Name", 30, 95);
            txtName = CreateTextBox(30, 120);

            Label lblCategory = CreateLabel("Category", 30, 175);
            txtCategory = CreateTextBox(30, 200);

            Label lblDescription = CreateLabel("Description", 30, 255);
            txtDescription = CreateTextBox(30, 280);

            Label lblPrice = CreateLabel("Price", 30, 335);
            txtPrice = CreateTextBox(30, 360);

            chkAvailable = new CheckBox();
            chkAvailable.Text = "Available";
            chkAvailable.Location = new Point(30, 415);
            chkAvailable.AutoSize = true;
            chkAvailable.Checked = true;

            btnSave = CreateButton("Save / Update");
            btnSave.Location = new Point(30, 465);
            btnSave.Click += BtnSave_Click;

            btnDelete = CreateButton("Delete");
            btnDelete.Location = new Point(180, 465);
            btnDelete.Click += BtnDelete_Click;

            btnClear = CreateButton("Clear");
            btnClear.Location = new Point(330, 465);
            btnClear.Click += BtnClear_Click;

            leftPanel.Controls.Add(title);
            leftPanel.Controls.Add(lblName);
            leftPanel.Controls.Add(txtName);
            leftPanel.Controls.Add(lblCategory);
            leftPanel.Controls.Add(txtCategory);
            leftPanel.Controls.Add(lblDescription);
            leftPanel.Controls.Add(txtDescription);
            leftPanel.Controls.Add(lblPrice);
            leftPanel.Controls.Add(txtPrice);
            leftPanel.Controls.Add(chkAvailable);
            leftPanel.Controls.Add(btnSave);
            leftPanel.Controls.Add(btnDelete);
            leftPanel.Controls.Add(btnClear);

            grid = new DataGridView();
            grid.Dock = DockStyle.Fill;
            grid.BackgroundColor = Color.White;
            grid.BorderStyle = BorderStyle.FixedSingle;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.MultiSelect = false;
            grid.ReadOnly = true;
            grid.AllowUserToAddRows = false;
            grid.RowHeadersWidth = 30;
            grid.ScrollBars = ScrollBars.Both;
            grid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            grid.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            grid.CellClick += Grid_CellClick;

            rightPanel.Controls.Add(grid);

            Controls.Add(rightPanel);
            Controls.Add(leftPanel);
        }

        private Label CreateLabel(string text, int x, int y)
        {
            return new Label
            {
                Text = text,
                Location = new Point(x, y),
                AutoSize = true,
                Font = new Font("Segoe UI", 10)
            };
        }

        private TextBox CreateTextBox(int x, int y)
        {
            return new TextBox
            {
                Location = new Point(x, y),
                Width = 430,
                Font = new Font("Segoe UI", 11)
            };
        }

        private Button CreateButton(string text)
        {
            Button btn = new Button();

            btn.Text = text;
            btn.Width = 130;
            btn.Height = 45;
            btn.BackColor = Color.FromArgb(230, 57, 70);
            btn.ForeColor = Color.White;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            return btn;
        }

        private void LoadData()
        {
            grid.DataSource = repo.GetMenuItems();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            decimal price;

            if (!decimal.TryParse(txtPrice.Text.Trim(), out price))
            {
                MessageBox.Show("Invalid price.");
                return;
            }

            repo.SaveMenuItem(
                selectedId,
                txtName.Text.Trim(),
                txtCategory.Text.Trim(),
                txtDescription.Text.Trim(),
                price,
                chkAvailable.Checked
            );

            MessageBox.Show("Item saved successfully.");
            LoadData();
            ClearFields();
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (selectedId == 0)
            {
                MessageBox.Show("Please select an item first.");
                return;
            }

            repo.DeleteMenuItem(selectedId);

            MessageBox.Show("Item deleted successfully.");
            LoadData();
            ClearFields();
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void Grid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            DataGridViewRow row = grid.Rows[e.RowIndex];

            selectedId = Convert.ToInt32(row.Cells["MenuItemId"].Value);
            txtName.Text = row.Cells["ItemName"].Value.ToString();
            txtCategory.Text = row.Cells["Category"].Value.ToString();
            txtDescription.Text = row.Cells["Description"].Value.ToString();
            txtPrice.Text = row.Cells["Price"].Value.ToString();
            chkAvailable.Checked = Convert.ToBoolean(row.Cells["IsAvailable"].Value);
        }

        private void ClearFields()
        {
            selectedId = 0;
            txtName.Clear();
            txtCategory.Clear();
            txtDescription.Clear();
            txtPrice.Clear();
            chkAvailable.Checked = true;
            grid.ClearSelection();
        }
    }
}