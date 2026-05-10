using OnlineFoodOrderingSystem.Common;
using OnlineFoodOrderingSystem.DataAccess;
using System.Drawing;
using System.Windows.Forms;

namespace OnlineFoodOrderingSystem.Forms
{
    public partial class MyOrdersForm : Form
    {
        private Repository repo = new Repository();

        private DataGridView grid;

        public MyOrdersForm()
        {
            InitializeUI();
            LoadOrders();
        }

        private void InitializeUI()
        {
            BackColor = Color.White;
            Dock = DockStyle.Fill;

            Label title = new Label();

            title.Text = "My Orders";

            title.Font = new Font(
                "Segoe UI",
                20,
                FontStyle.Bold);

            title.AutoSize = true;

            title.Location = new Point(30, 25);

            grid = new DataGridView();

            grid.Location = new Point(30, 85);

            grid.Size = new Size(950, 500);

            grid.BackgroundColor = Color.White;

            grid.BorderStyle = BorderStyle.FixedSingle;

            grid.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            grid.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            grid.ReadOnly = true;

            grid.AllowUserToAddRows = false;

            Controls.Add(title);
            Controls.Add(grid);
        }

        private void LoadOrders()
        {
            grid.DataSource =
                repo.GetOrders(AppSession.UserId);
        }
    }
}