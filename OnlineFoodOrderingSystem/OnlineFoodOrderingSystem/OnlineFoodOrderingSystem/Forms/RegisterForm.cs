using OnlineFoodOrderingSystem.Common;
using OnlineFoodOrderingSystem.DataAccess;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace OnlineFoodOrderingSystem.Forms
{
    public partial class RegisterForm : Form
    {
        private Repository repo = new Repository();

        private TextBox txtFullName;
        private TextBox txtEmail;
        private TextBox txtPhone;
        private TextBox txtAddress;
        private TextBox txtPassword;

        private Button btnRegister;
        private Button btnBack;

        public RegisterForm()
        {
            InitializeUI();
        }

        private void InitializeUI()
        {
            Text = "Customer Registration";
            Size = new Size(520, 620);
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = Color.WhiteSmoke;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;

            Label title = new Label();
            title.Text = "Create Account";
            title.Font = new Font("Segoe UI", 24, FontStyle.Bold);
            title.AutoSize = true;
            title.Location = new Point(130, 30);

            Label subtitle = new Label();
            subtitle.Text = "Register and start ordering food";
            subtitle.Font = new Font("Segoe UI", 10);
            subtitle.ForeColor = Color.Gray;
            subtitle.AutoSize = true;
            subtitle.Location = new Point(150, 80);

            AddLabel("Full Name", 70, 125);
            txtFullName = CreateTextBox(70, 150);

            AddLabel("Email", 70, 200);
            txtEmail = CreateTextBox(70, 225);

            AddLabel("Phone", 70, 275);
            txtPhone = CreateTextBox(70, 300);

            AddLabel("Address", 70, 350);
            txtAddress = CreateTextBox(70, 375);

            AddLabel("Password", 70, 425);
            txtPassword = CreateTextBox(70, 450);
            txtPassword.UseSystemPasswordChar = true;

            btnRegister = new Button();
            btnRegister.Text = "Register";
            btnRegister.Location = new Point(70, 505);
            btnRegister.Width = 170;
            btnRegister.Height = 45;
            btnRegister.BackColor = Color.FromArgb(230, 57, 70);
            btnRegister.ForeColor = Color.White;
            btnRegister.FlatStyle = FlatStyle.Flat;
            btnRegister.FlatAppearance.BorderSize = 0;
            btnRegister.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            btnRegister.Click += BtnRegister_Click;

            btnBack = new Button();
            btnBack.Text = "Back to Login";
            btnBack.Location = new Point(260, 505);
            btnBack.Width = 170;
            btnBack.Height = 45;
            btnBack.BackColor = Color.White;
            btnBack.ForeColor = Color.FromArgb(230, 57, 70);
            btnBack.FlatStyle = FlatStyle.Flat;
            btnBack.FlatAppearance.BorderColor = Color.FromArgb(230, 57, 70);
            btnBack.FlatAppearance.BorderSize = 1;
            btnBack.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            btnBack.Click += BtnBack_Click;

            Controls.Add(title);
            Controls.Add(subtitle);
            Controls.Add(txtFullName);
            Controls.Add(txtEmail);
            Controls.Add(txtPhone);
            Controls.Add(txtAddress);
            Controls.Add(txtPassword);
            Controls.Add(btnRegister);
            Controls.Add(btnBack);
        }

        private void AddLabel(string text, int x, int y)
        {
            Label lbl = new Label();
            lbl.Text = text;
            lbl.Location = new Point(x, y);
            lbl.AutoSize = true;
            lbl.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            Controls.Add(lbl);
        }

        private TextBox CreateTextBox(int x, int y)
        {
            TextBox txt = new TextBox();
            txt.Location = new Point(x, y);
            txt.Width = 360;
            txt.Font = new Font("Segoe UI", 11);
            return txt;
        }

        private void BtnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                string fullName = txtFullName.Text.Trim();
                string email = txtEmail.Text.Trim();
                string phone = txtPhone.Text.Trim();
                string address = txtAddress.Text.Trim();
                string password = txtPassword.Text;

                if (fullName == "" || email == "" || phone == "" || address == "" || password == "")
                {
                    MessageBox.Show("Please fill all fields.");
                    return;
                }

                if (!email.Contains("@") || !email.Contains("."))
                {
                    MessageBox.Show("Please enter a valid email.");
                    return;
                }

                if (password.Length < 4)
                {
                    MessageBox.Show("Password must be at least 4 characters.");
                    return;
                }

                repo.Register(fullName, email, phone, address, password);

                bool success = repo.Login(email, password);

                if (success)
                {
                    MessageBox.Show(
                        "Account created successfully. Welcome " + AppSession.FullName + "!",
                        "Success",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    Hide();
                    new MainForm().Show();
                }
                else
                {
                    MessageBox.Show("Account created, but login failed. Please login manually.");
                    Hide();
                    new LoginForm().Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Registration error: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            Hide();
            new LoginForm().Show();
        }
    }
}