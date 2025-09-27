using System.Windows;
using System.Windows.Controls;

namespace ComputerShop
{
    public partial class Page1 : Page
    {
        public Page1()
        {
            InitializeComponent();

            // Tesztkapcsolat az oldal betöltésekor
            var db = new Connect();
            if (db.TestConnection(out string msg))
                LargeWindow.Text = msg;
            else
                LargeWindow.Text = "Hiba: " + msg;
        }

        // Bejelentkezés gomb
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var db = new Connect();

            string username = userNameTextBox.Text;
            string password = userPasswordTextBox.Password;

            bool success = db.Login(username, password, out string msg);

            LargeWindow.Text = msg;

            MessageBox.Show(msg, success ? "Siker" : "Hiba", MessageBoxButton.OK,
                            success ? MessageBoxImage.Information : MessageBoxImage.Error);
        }

        // Regisztráció gomb
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            // Navigálás a RegisterForm oldalra
            if (this.NavigationService != null)
            {
                this.NavigationService.Navigate(new RegisterForm());
            }
            else
            {
                MessageBox.Show("Hiba: Nem lehet navigálni a RegisterForm oldalra.");
            }
        }
    }
}
