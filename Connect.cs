using MySql.Data.MySqlClient;
using System;

namespace ComputerShop
{
    internal class Connect
    {
        private string _host = "127.0.0.1";
        private string _database = "computershop";
        private string _user = "root";
        private string _password = "";
        private string ConnectionString;

        public Connect()
        {
            ConnectionString = $"SERVER={_host};DATABASE={_database};USERID={_user};PASSWORD={_password};SslMode=None";
        }

        // Tesztkapcsolat
        public bool TestConnection(out string message)
        {
            try
            {
                using (var conn = new MySqlConnection(ConnectionString))
                {
                    conn.Open();
                    message = "Sikeres csatlakozás az adatbázishoz";
                    return true;
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return false;
            }
        }

        // Bejelentkezés ellenőrzése
        public bool Login(string username, string password, out string message)
        {
            try
            {
                using (var conn = new MySqlConnection(ConnectionString))
                {
                    conn.Open();
                    string sql = "SELECT * FROM computershop WHERE UserName = @username AND Password = @userpassword;";
                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@userpassword", password);

                        using (var dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                message = "Sikeres bejelentkezés";
                                return true;
                            }
                            else
                            {
                                message = "Hibás felhasználónév vagy jelszó";
                                return false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return false;
            }
        }
    }
}
