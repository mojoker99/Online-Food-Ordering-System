using OnlineFoodOrderingSystem.Common;
using OnlineFoodOrderingSystem.DataAccess;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace OnlineFoodOrderingSystem.Forms
{
    public partial class AdminMainForm : Form
    {
        private Repository repo = new Repository();

        private Panel sidebar;
        private Panel contentPanel;

        public AdminMainForm()
        {
            InitializeUI();
        }

        private void InitializeUI()
        {
            Text = "Online Food Ordering - Admin";
            WindowState = FormWindowState.Maximized;
            BackColor = UITheme.Background;

            sidebar = new Panel();
            sidebar.Dock = DockStyle.Left;
            sidebar.Width = 260;
            sidebar.BackColor = UITheme.Sidebar;

            Label logo = new Label();
            logo.Text = "👨‍🍳";
            logo.Font = new Font("Segoe UI Emoji", 38, FontStyle.Bold);
            logo.AutoSize = true;
            logo.Location = new Point(90, 25);

            Label title = new Label();
            title.Text = "Admin Panel";
            title.ForeColor = Color.White;
            title.Font = new Font("Segoe UI", 18, FontStyle.Bold);
            title.AutoSize = true;
            title.Location = new Point(45, 95);

            Label welcome = new Label();
            welcome.Text = "Welcome, " + AppSession.FullName;
            welcome.ForeColor = Color.LightGray;
            welcome.Font = new Font("Segoe UI", 10);
            welcome.AutoSize = true;
            welcome.Location = new Point(35, 135);

            Button btnDashboard = CreateButton("📊  Dashboard", 190);
            btnDashboard.Click += (s, e) => ShowDashboard();

            Button btnMenu = CreateButton("🍽  Manage Menu", 260);
            btnMenu.Click += (s, e) => OpenChildForm(new AdminMenuForm());

            Button btnOrders = CreateButton("📦  Manage Orders", 330);
            btnOrders.Click += (s, e) => OpenChildForm(new AdminOrdersForm());

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
            sidebar.Controls.Add(btnDashboard);
            sidebar.Controls.Add(btnMenu);
            sidebar.Controls.Add(btnOrders);
            sidebar.Controls.Add(btnLogout);

            contentPanel = new Panel();
            contentPanel.Dock = DockStyle.Fill;
            contentPanel.BackColor = UITheme.Background;
            contentPanel.Padding = new Padding(10);

            Controls.Add(contentPanel);
            Controls.Add(sidebar);

            ShowDashboard();
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

        private void ShowDashboard()
        {
            contentPanel.Controls.Clear();

            Label title = new Label();
            title.Text = "Dashboard";
            title.Font = UITheme.TitleFont;
            title.ForeColor = UITheme.TextDark;
            title.AutoSize = true;
            title.Location = new Point(40, 35);

            Label subtitle = new Label();
            subtitle.Text = "Restaurant system overview";
            subtitle.Font = UITheme.NormalFont;
            subtitle.ForeColor = UITheme.Muted;
            subtitle.AutoSize = true;
            subtitle.Location = new Point(43, 80);

            Panel usersCard = CreateCard("👥", "Total Users", repo.GetTotalUsers().ToString(), 40, 140);
            Panel menuCard = CreateCard("🍽", "Menu Items", repo.GetTotalMenuItems().ToString(), 300, 140);
            Panel ordersCard = CreateCard("📦", "Total Orders", repo.GetTotalOrders().ToString(), 560, 140);
            Panel revenueCard = CreateCard("💰", "Revenue", repo.GetTotalRevenue().ToString("0.00") + " EGP", 820, 140);

            contentPanel.Controls.Add(title);
            contentPanel.Controls.Add(subtitle);
            contentPanel.Controls.Add(usersCard);
            contentPanel.Controls.Add(menuCard);
            contentPanel.Controls.Add(ordersCard);
            contentPanel.Controls.Add(revenueCard);
        }

        private Panel CreateCard(string icon, string titleText, string valueText, int x, int y)
        {
            Panel card = new Panel();

            card.Location = new Point(x, y);
            card.Size = new Size(220, 150);
            card.BackColor = UITheme.Card;
            card.BorderStyle = BorderStyle.FixedSingle;

            Label iconLabel = new Label();
            iconLabel.Text = icon;
            iconLabel.Font = new Font("Segoe UI Emoji", 24, FontStyle.Bold);
            iconLabel.AutoSize = true;
            iconLabel.Location = new Point(18, 15);

            Label title = new Label();
            title.Text = titleText;
            title.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            title.ForeColor = UITheme.TextDark;
            title.AutoSize = true;
            title.Location = new Point(20, 60);

            Label value = new Label();
            value.Text = valueText;
            value.Font = new Font("Segoe UI", 20, FontStyle.Bold);
            value.ForeColor = UITheme.Primary;
            value.AutoSize = true;
            value.Location = new Point(20, 95);

            card.Controls.Add(iconLabel);
            card.Controls.Add(title);
            card.Controls.Add(value);

            return card;
        }
    }
}