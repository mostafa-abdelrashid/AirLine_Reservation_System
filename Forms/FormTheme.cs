using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace AirLine_Reservation_System.Forms
{
    /// <summary>
    /// Shared helper to apply a consistent dark theme to any Form.
    /// </summary>
    public static class FormTheme
    {
        public static readonly Color BgColor     = Color.FromArgb(30, 41, 59);
        public static readonly Color PanelColor  = Color.FromArgb(15, 23, 42);
        public static readonly Color AccentColor = Color.FromArgb(56, 189, 248);
        public static readonly Color TextColor   = Color.FromArgb(226, 232, 240);
        public static readonly Color InputBg     = Color.FromArgb(51, 65, 85);
        public static readonly Color BtnAdd      = Color.FromArgb(34, 197, 94);
        public static readonly Color BtnUpdate   = Color.FromArgb(234, 179, 8);
        public static readonly Color BtnDelete   = Color.FromArgb(239, 68, 68);
        public static readonly Color BtnClear    = Color.FromArgb(100, 116, 139);

        public static void Apply(Form form, string title)
        {
            form.BackColor = BgColor;
            form.ForeColor = TextColor;
            form.Font = new Font("Segoe UI", 9.5f);
            form.Text = title;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.FormBorderStyle = FormBorderStyle.FixedSingle;
            form.MaximizeBox = false;
        }

        public static Label MakeLabel(string text, int x, int y, int w = 110)
            => new Label() { Text = text, Location = new Point(x, y), Size = new Size(w, 22), ForeColor = TextColor, TextAlign = ContentAlignment.MiddleRight };

        public static TextBox MakeTextBox(int x, int y, int w = 180, bool readOnly = false)
            => new TextBox() { Location = new Point(x, y), Size = new Size(w, 26), BackColor = InputBg, ForeColor = TextColor, BorderStyle = BorderStyle.FixedSingle, ReadOnly = readOnly };

        public static DateTimePicker MakeDatePicker(int x, int y, int w = 180)
            => new DateTimePicker() { Location = new Point(x, y), Size = new Size(w, 26), Format = DateTimePickerFormat.Short, CalendarForeColor = TextColor };

        public static ComboBox MakeComboBox(int x, int y, int w = 180)
        {
            var cb = new ComboBox() { Location = new Point(x, y), Size = new Size(w, 26), BackColor = InputBg, ForeColor = TextColor, FlatStyle = FlatStyle.Flat, DropDownStyle = ComboBoxStyle.DropDownList };
            return cb;
        }

        public static Button MakeButton(string text, int x, int y, Color bg)
        {
            var btn = new Button()
            {
                Text = text,
                Location = new Point(x, y),
                Size = new Size(90, 32),
                BackColor = bg,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9f, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btn.FlatAppearance.BorderSize = 0;
            return btn;
        }

        public static DataGridView MakeGrid()
        {
            var dgv = new DataGridView()
            {
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AllowUserToAddRows = false,
                BackgroundColor = Color.FromArgb(15, 23, 42),
                GridColor = Color.FromArgb(51, 65, 85),
                BorderStyle = BorderStyle.None,
                ColumnHeadersHeight = 36,
                RowTemplate = { Height = 30 },
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                RowHeadersVisible = false
            };
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(15, 23, 42);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(56, 189, 248);
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            dgv.DefaultCellStyle.BackColor = Color.FromArgb(30, 41, 59);
            dgv.DefaultCellStyle.ForeColor = Color.FromArgb(226, 232, 240);
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(56, 189, 248);
            dgv.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(38, 52, 74);
            dgv.EnableHeadersVisualStyles = false;
            return dgv;
        }

        public static Panel MakeHeaderPanel(string title, string subtitle, int width)
        {
            var panel = new Panel() { BackColor = Color.FromArgb(15, 23, 42), Dock = DockStyle.Top, Height = 70 };
            var lbl = new Label() { Text = title, Font = new Font("Segoe UI", 16, FontStyle.Bold), ForeColor = Color.FromArgb(56, 189, 248), Location = new Point(20, 10), AutoSize = true };
            var sub = new Label() { Text = subtitle, Font = new Font("Segoe UI", 9f), ForeColor = Color.FromArgb(148, 163, 184), Location = new Point(22, 42), AutoSize = true };
            panel.Controls.Add(lbl);
            panel.Controls.Add(sub);
            return panel;
        }
    }
}
