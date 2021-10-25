using MonitoramentoTempoOcioso.Entities.User;
using MonitoramentoTempoOcioso.Forms;
using MonitoramentoTempoOcioso.Interfaces.Users;
using System;
using System.Configuration;
using System.Windows.Forms;

namespace MonitoramentoTempoOcioso
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            IUser currentUser = new LocalUser();

            var flagAuthorizationRequired = ConfigurationManager.AppSettings["AuthorizationRequired"];

            if (flagAuthorizationRequired != null && flagAuthorizationRequired.Equals("true")) {
                using (var loginForm = new LoginForm())
                {
                    var result = loginForm.ShowDialog();

                    if (result != DialogResult.OK)
                    {
                        return;
                    }

                    currentUser = loginForm.authenticatedUser;
                }
            }

            Application.Run(new TrayApplicationContext(currentUser));
        }
    }
}
