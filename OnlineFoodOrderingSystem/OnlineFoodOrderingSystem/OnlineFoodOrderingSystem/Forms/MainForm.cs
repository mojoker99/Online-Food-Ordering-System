using OnlineFoodOrderingSystem.Common;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace OnlineFoodOrderingSystem.Forms
{
    public class MainForm : Form
    {
        private Panel sidebar;
        private Panel contentPanel;

        public MainForm()
        {
            InitializeUI();
        }

        private void InitializeUI()
        {
            Text = "Online Food Ordering System";
            WindowState = FormWindowState.Maximized;
            BackColor = UITheme.Background;

            sidebar = new Panel();
            sidebar.BackColor = UITheme.Sidebar;
            sidebar.Dock = DockStyle.Left;
            sidebar.Width = 260;

            Label logo = new Label();
            logo.Text = "🍔";
            logo.Font = new Font("Segoe UI Emoji", 38, FontStyle.Bold);
            logo.AutoSize = true;
            logo.Location = new Point(95, 25);

            Label title = new Label();
            title.Text = "Food Ordering";
            title.ForeColor = Color.White;
            title.Font = new Font("Segoe UI", 18, FontStyle.Bold);
            title.AutoSize = true;
            title.Location = new Point(35, 95);

            Label welcome = new Label();
            welcome.Text = "Welcome, " + AppSession.FullName;
            welcome.ForeColor = Color.LightGray;
            welcome.Font = new Font("Segoe UI", 10);
            welcome.AutoSize = true;
            welcome.Location = new Point(35, 135);

            Button btnMenu = CreateButton("🍽  Browse Menu", 190);
            btnMenu.Click += (s, e) => OpenChildForm(new MenuForm());

            Button btnCart = CreateButton("🛒  My Cart", 260);
            btnCart.Click += (s, e) => OpenChildForm(new CartForm());

            Button btnOrders = CreateButton("📦  My Orders", 330);
            btnOrders.Click += (s, e) => OpenChildForm(new MyOrdersForm());

            Button btnLogout = CreateButton("🚪  Logout", 400);
            btnLogout.Click += (s, e) =>
            {
                DialogResult result = MessageBox.Show(
                    "Are you sure you want to logout?",
                    "Logout",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    AppSession.Clear();
                    Hide();
                    new LoginForm().Show();
                }
            };

            sidebar.Controls.Add(logo);
            sidebar.Controls.Add(title);
            sidebar.Controls.Add(welcome);
            sidebar.Controls.Add(btnMenu);
            sidebar.Controls.Add(btnCart);
            sidebar.Controls.Add(btnOrders);
            sidebar.Controls.Add(btnLogout);

            contentPanel = new Panel();
            contentPanel.Dock = DockStyle.Fill;
            contentPanel.BackColor = UITheme.Background;
            contentPanel.Padding = new Padding(10);

            Controls.Add(contentPanel);
            Controls.Add(sidebar);

            OpenChildForm(new MenuForm());
        }

        private Button CreateButton(string text, int top)
        {
            Button btn = new Button();

            btn.Text = text;
            btn.Width = 210;
            btn.Height = 48;
            btn.Left = 25;
            btn.Top = top;
            btn.TextAlign = ContentAlignment.MiddleLeft;
            btn.Padding = new Padding(15, 0, 0, 0);

            UITheme.StyleButton(btn);

            return btn;
        }

        private void OpenChildForm(Form childForm)
        {
            contentPanel.Controls.Clear();

            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;

            contentPanel.Controls.Add(childForm);
            childForm.BringToFront();
            childForm.Show();
        }
    }
}