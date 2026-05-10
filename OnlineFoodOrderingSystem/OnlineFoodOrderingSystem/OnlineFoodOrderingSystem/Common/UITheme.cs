using System.Drawing;
using System.Windows.Forms;

namespace OnlineFoodOrderingSystem.Common
{
    public static class UITheme
    {
        public static Color Sidebar = Color.FromArgb(32, 33, 36);
        public static Color Primary = Color.FromArgb(230, 57, 70);
        public static Color PrimaryDark = Color.FromArgb(190, 45, 55);
        public static Color Background = Color.FromArgb(248, 249, 250);
        public static Color Card = Color.White;
        public static Color TextDark = Color.FromArgb(40, 40, 40);
        public static Color TextLight = Color.White;
        public static Color Muted = Color.Gray;

        public static Font TitleFont = new Font("Segoe UI", 22, FontStyle.Bold);
        public static Font HeaderFont = new Font("Segoe UI", 16, FontStyle.Bold);
        public static Font NormalFont = new Font("Segoe UI", 10);
        public static Font ButtonFont = new Font("Segoe UI", 11, FontStyle.Bold);

        public static void StyleButton(Button btn)
        {
            btn.BackColor = Primary;
            btn.ForeColor = TextLight;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Font = ButtonFont;
            btn.Cursor = Cursors.Hand;

            btn.MouseEnter += (s, e) =>
            {
                btn.BackColor = PrimaryDark;
            };

            btn.MouseLeave += (s, e) =>
            {
                btn.BackColor = Primary;
            };
        }
    }
}