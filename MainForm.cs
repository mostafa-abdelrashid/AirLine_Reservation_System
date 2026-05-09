using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using AirLine_Reservation_System.Forms;

namespace AirLine_Reservation_System
{
    public class MainForm : Form
    {
        private Panel sidePanel;
        private Panel contentPanel;
        private Label lblTitle;

        // Color Palette
        private readonly Color SideBarColor = Color.FromArgb(15, 23, 42);
        private readonly Color AccentColor  = Color.FromArgb(56, 189, 248);
        private readonly Color HoverColor   = Color.FromArgb(30, 58, 100);
        private readonly Color TextColor    = Color.FromArgb(226, 232, 240);
        private readonly Color BgColor      = Color.FromArgb(30, 41, 59);

        public MainForm()
        {
            InitializeUI();
        }

        private void InitializeUI()
        {
            this.Text = "✈ Airline Reservation System";
            this.Size = new Size(1000, 650);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = BgColor;
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.MaximizeBox = true;
            this.WindowState = FormWindowState.Maximized;
            this.Font = new Font("Segoe UI", 9.5f);

            // --- Side Panel ---
            sidePanel = new Panel()
            {
                Width = 220,
                Dock = DockStyle.Left,
                BackColor = SideBarColor
            };

            // Logo area
            Panel logoPanel = new Panel() { Size = new Size(220, 80), Location = new Point(0, 0), BackColor = Color.FromArgb(10, 15, 30) };
            Label lblLogo = new Label()
            {
                Text = "✈  AirReserve",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = AccentColor,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill
            };
            logoPanel.Controls.Add(lblLogo);
            sidePanel.Controls.Add(logoPanel);

            // Nav Buttons
            string[] navItems = { "🏠  Dashboard", "👤  Passengers", "✈  Flights", "🏢  Airports", "🛫  Airlines", "🛩  Aircraft", "📋  Bookings", "🎫  Tickets", "💳  Payments" };
            int navY = 90;
            foreach (var item in navItems)
            {
                Button btn = new Button()
                {
                    Text = item,
                    Size = new Size(220, 48),
                    Location = new Point(0, navY),
                    FlatStyle = FlatStyle.Flat,
                    BackColor = SideBarColor,
                    ForeColor = TextColor,
                    Font = new Font("Segoe UI", 10f),
                    TextAlign = ContentAlignment.MiddleLeft,
                    Padding = new Padding(20, 0, 0, 0),
                    Cursor = Cursors.Hand,
                    Tag = item
                };
                btn.FlatAppearance.BorderSize = 0;
                btn.FlatAppearance.MouseOverBackColor = HoverColor;
                btn.Click += NavButton_Click;
                sidePanel.Controls.Add(btn);
                navY += 50;
            }
            this.Controls.Add(sidePanel);

            // --- Content Panel ---
            contentPanel = new Panel()
            {
                Dock = DockStyle.Fill,
                BackColor = BgColor
            };

            lblTitle = new Label()
            {
                Text = "Dashboard",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = AccentColor,
                Location = new Point(20, 20),
                AutoSize = true
            };
            contentPanel.Controls.Add(lblTitle);

            this.Controls.Add(contentPanel);
            contentPanel.BringToFront();
            
            RefreshStats();
        }

        private void RefreshStats()
        {
            // Remove old stat cards
            for (int i = contentPanel.Controls.Count - 1; i >= 0; i--)
            {
                if (contentPanel.Controls[i] is Panel)
                {
                    contentPanel.Controls.RemoveAt(i);
                }
            }

            // Stat cards
            AddStatCard("Total Passengers", "SELECT COUNT(*) FROM Passenger", 0);
            AddStatCard("Total Flights", "SELECT COUNT(*) FROM Flight", 1);
            AddStatCard("Total Bookings", "SELECT COUNT(*) FROM Booking", 2);
            AddStatCard("Total Tickets", "SELECT COUNT(*) FROM Ticket", 3);
        }

        private void AddStatCard(string title, string query, int index)
        {
            DBHelper db = new DBHelper();
            string count = "N/A";
            try { count = db.ExecuteQuery(query).Rows[0][0].ToString(); } catch { }

            Panel card = new Panel()
            {
                Size = new Size(160, 90),
                Location = new Point(20 + index * 180, 80),
                BackColor = Color.FromArgb(15, 23, 42),
                Cursor = Cursors.Default
            };
            card.Paint += (s, e) =>
            {
                using (var pen = new Pen(AccentColor, 1))
                    e.Graphics.DrawRectangle(pen, 0, 0, card.Width - 1, card.Height - 1);
            };
            Label lCount = new Label() { Text = count, Font = new Font("Segoe UI", 22, FontStyle.Bold), ForeColor = AccentColor, Location = new Point(10, 10), AutoSize = true };
            Label lTitle = new Label() { Text = title, Font = new Font("Segoe UI", 8f), ForeColor = TextColor, Location = new Point(10, 55), AutoSize = true };
            card.Controls.Add(lCount);
            card.Controls.Add(lTitle);
            contentPanel.Controls.Add(card);
        }

        private void NavButton_Click(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            string tag = btn.Tag.ToString();
            if (tag.Contains("Passengers")) OpenForm(new PassengerForm());
            else if (tag.Contains("Flights"))   OpenForm(new FlightForm());
            else if (tag.Contains("Airports"))  OpenForm(new AirportForm());
            else if (tag.Contains("Airlines"))  OpenForm(new AirlineForm());
            else if (tag.Contains("Aircraft"))  OpenForm(new AircraftForm());
            else if (tag.Contains("Bookings"))  OpenForm(new BookingForm());
            else if (tag.Contains("Tickets"))   OpenForm(new TicketForm());
            else if (tag.Contains("Payments"))  OpenForm(new PaymentForm());
        }

        private void OpenForm(Form f)
        {
            f.StartPosition = FormStartPosition.CenterScreen;
            f.ShowDialog();
            RefreshStats();
        }
    }
}
