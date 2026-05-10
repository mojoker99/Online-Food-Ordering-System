using OnlineFoodOrderingSystem.Common;
using OnlineFoodOrderingSystem.DataAccess;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace OnlineFoodOrderingSystem.Forms
{
    public class LoginForm : Form
    {
        private Repository repo = new Repository();

        private TextBox txtEmail;
        private TextBox txtPassword;

        private Button btnLogin;
        private Button btnRegister;

        private CheckBox chkShowPassword;

        public LoginForm()
        {
            InitializeUI();
        }

        private void InitializeUI()
        {
            Text = "Online Food Ordering - Login";

            Size = new Size(920, 540);

            StartPosition = FormStartPosition.CenterScreen;

            FormBorderStyle = FormBorderStyle.FixedSingle;

            MaximizeBox = false;

            BackColor = Color.WhiteSmoke;

            // =========================================
            // LEFT SIDE
            // =========================================

            Panel leftPanel = new Panel();

            leftPanel.Dock = DockStyle.Left;

            leftPanel.Width = 380;

            leftPanel.BackColor = Color.FromArgb(25, 25, 35);

            Label logo = new Label();

            logo.Text = "🍔";

            logo.Font =
                new Font(
                    "Segoe UI Emoji",
                    72,
                    FontStyle.Bold);

            logo.AutoSize = false;

            logo.Size = new Size(380, 100);

            logo.TextAlign =
                ContentAlignment.MiddleCenter;

            logo.Location = new Point(0, 70);

            logo.ForeColor = Color.White;

            Label brand = new Label();

            brand.Text = "Food Ordering";

            brand.Font =
                new Font(
                    "Segoe UI",
                    28,
                    FontStyle.Bold);

            brand.ForeColor = Color.White;

            brand.AutoSize = false;

            brand.Size = new Size(380, 50);

            brand.TextAlign =
                ContentAlignment.MiddleCenter;

            brand.Location = new Point(0, 180);

            Label slogan = new Label();

            slogan.Text =
                "Fresh meals, fast orders,\nsmart restaurant management.";

            slogan.Font =
                new Font(
                    "Segoe UI",
                    12);

            slogan.ForeColor = Color.Gainsboro;

            slogan.AutoSize = false;

            slogan.Size = new Size(320, 60);

            slogan.TextAlign =
                ContentAlignment.MiddleCenter;

            slogan.Location = new Point(30, 245);

            Panel line = new Panel();

            line.BackColor =
                Color.FromArgb(230, 57, 70);

            line.Size = new Size(220, 3);

            line.Location = new Point(80, 335);

            Label footer = new Label();

            footer.Text =
                "Food Truck";

            footer.Font =
                new Font(
                    "Segoe UI",
                    10,
                    FontStyle.Bold);

            footer.ForeColor = Color.Silver;

            footer.AutoSize = false;

            footer.Size = new Size(380, 30);

            footer.TextAlign =
                ContentAlignment.MiddleCenter;

            footer.Location = new Point(0, 455);

            leftPanel.Controls.Add(logo);

            leftPanel.Controls.Add(brand);

            leftPanel.Controls.Add(slogan);

            leftPanel.Controls.Add(line);

            leftPanel.Controls.Add(footer);

            // =========================================
            // LOGIN CARD
            // =========================================

            Panel card = new Panel();

            card.BackColor = Color.White;

            card.Size = new Size(430, 410);

            card.Location = new Point(435, 60);

            card.BorderStyle = BorderStyle.FixedSingle;

            Label title = new Label();

            title.Text = "Welcome Back";

            title.Font =
                new Font(
                    "Segoe UI",
                    24,
                    FontStyle.Bold);

            title.ForeColor =
                Color.FromArgb(35, 35, 35);

            title.AutoSize = true;

            title.Location = new Point(55, 35);

            Label subtitle = new Label();

            subtitle.Text =
                "Login to continue to your account";

            subtitle.Font =
                new Font(
                    "Segoe UI",
                    10);

            subtitle.ForeColor = Color.Gray;

            subtitle.AutoSize = true;

            subtitle.Location = new Point(60, 85);

            // =========================================
            // EMAIL
            // =========================================

            Label lblEmail = new Label();

            lblEmail.Text = "Email Address";

            lblEmail.Font =
                new Font(
                    "Segoe UI",
                    10,
                    FontStyle.Bold);

            lblEmail.AutoSize = true;

            lblEmail.Location = new Point(60, 135);

            txtEmail = new TextBox();

            txtEmail.Width = 300;

            txtEmail.Height = 35;

            txtEmail.Location = new Point(60, 160);

            txtEmail.Font =
                new Font(
                    "Segoe UI",
                    11);

            // =========================================
            // PASSWORD
            // =========================================

            Label lblPassword = new Label();

            lblPassword.Text = "Password";

            lblPassword.Font =
                new Font(
                    "Segoe UI",
                    10,
                    FontStyle.Bold);

            lblPassword.AutoSize = true;

            lblPassword.Location = new Point(60, 215);

            txtPassword = new TextBox();

            txtPassword.Width = 300;

            txtPassword.Height = 35;

            txtPassword.Location = new Point(60, 240);

            txtPassword.Font =
                new Font(
                    "Segoe UI",
                    11);

            txtPassword.UseSystemPasswordChar = true;

            chkShowPassword = new CheckBox();

            chkShowPassword.Text = "Show password";

            chkShowPassword.Font =
                new Font(
                    "Segoe UI",
                    9);

            chkShowPassword.AutoSize = true;

            chkShowPassword.Location =
                new Point(60, 280);

            chkShowPassword.CheckedChanged +=
                ChkShowPassword_CheckedChanged;

            // =========================================
            // LOGIN BUTTON
            // =========================================

            btnLogin = new Button();

            btnLogin.Text = "Login";

            btnLogin.Width = 300;

            btnLogin.Height = 45;

            btnLogin.Location =
                new Point(60, 320);

            btnLogin.BackColor =
                Color.FromArgb(230, 57, 70);

            btnLogin.ForeColor = Color.White;

            btnLogin.FlatStyle = FlatStyle.Flat;

            btnLogin.FlatAppearance.BorderSize = 0;

            btnLogin.Font =
                new Font(
                    "Segoe UI",
                    11,
                    FontStyle.Bold);

            btnLogin.Cursor = Cursors.Hand;

            btnLogin.Click += BtnLogin_Click;

            // =========================================
            // REGISTER BUTTON
            // =========================================

            btnRegister = new Button();

            btnRegister.Text =
                "Create New Account";

            btnRegister.Width = 300;

            btnRegister.Height = 42;

            btnRegister.Location =
                new Point(60, 375);

            btnRegister.BackColor = Color.White;

            btnRegister.ForeColor =
                Color.FromArgb(230, 57, 70);

            btnRegister.FlatStyle = FlatStyle.Flat;

            btnRegister.FlatAppearance.BorderColor =
                Color.FromArgb(230, 57, 70);

            btnRegister.FlatAppearance.BorderSize = 1;

            btnRegister.Font =
                new Font(
                    "Segoe UI",
                    10,
                    FontStyle.Bold);

            btnRegister.Cursor = Cursors.Hand;

            btnRegister.Click += BtnRegister_Click;

            // =========================================
            // ADD CONTROLS
            // =========================================

            card.Controls.Add(title);

            card.Controls.Add(subtitle);

            card.Controls.Add(lblEmail);

            card.Controls.Add(txtEmail);

            card.Controls.Add(lblPassword);

            card.Controls.Add(txtPassword);

            card.Controls.Add(chkShowPassword);

            card.Controls.Add(btnLogin);

            card.Controls.Add(btnRegister);

            Controls.Add(card);

            Controls.Add(leftPanel);
        }

        private void ChkShowPassword_CheckedChanged(
            object sender,
            EventArgs e)
        {
            txtPassword.UseSystemPasswordChar =
                !chkShowPassword.Checked;
        }

        private void BtnLogin_Click(
            object sender,
            EventArgs e)
        {
            try
            {
                bool success =
                    repo.Login(
                        txtEmail.Text.Trim(),
                        txtPassword.Text);

                if (!success)
                {
                    MessageBox.Show(
                        "Invalid email or password",
                        "Login Failed",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return;
                }

                Hide();

                if (AppSession.Role == "Admin")
                {
                    new AdminMainForm().Show();
                }
                else
                {
                    new MainForm().Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Login error: " + ex.Message);
            }
        }

        private void BtnRegister_Click(
            object sender,
            EventArgs e)
        {
            Hide();

            new RegisterForm().Show();
        }
    }
}