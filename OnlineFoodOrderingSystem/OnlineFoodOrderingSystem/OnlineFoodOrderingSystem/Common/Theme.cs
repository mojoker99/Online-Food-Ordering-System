using System.Drawing;
using System.Windows.Forms;

namespace OnlineFoodOrderingSystem.Common
{
    public static class Theme
    {
        public static readonly Color Primary = Color.FromArgb(220, 53, 69);
        public static readonly Color Dark = Color.FromArgb(33, 37, 41);
        public static readonly Color Light = Color.FromArgb(248, 249, 250);

        public static void StyleButton(Button btn)
        {
            btn.BackColor = Primary;
            btn.ForeColor = Color.White;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btn.Height = 38;
        }

        public static void StyleGrid(DataGridView grid)
        {
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.MultiSelect = false;
            grid.ReadOnly = true;
            grid.AllowUserToAddRows = false;
            grid.RowHeadersVisible = false;
            grid.BackgroundColor = Color.White;
        }
    }
}
