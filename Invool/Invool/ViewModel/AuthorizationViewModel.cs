using Invool.Data;
using Invool.Services;
using Invool.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Invool.ViewModel
{
    public class AuthorizationViewModel : ViewModelBase
    {
        public AuthorizationViewModel(LoginWindow authorizationWindow)
        {
            _ctx = new();
            _view = authorizationWindow;
            _userService = new(_ctx);
        }
        #region Context
        private ApplicationDbContext _ctx;
        #endregion
        private LoginWindow _view = null!;
        private string _login;
        private string _password;
        private UserService _userService;
        public string Login { get => _login; set => Set(ref _login, value, nameof(Login)); }
        public string Password { get => _password; set => Set(ref _password, value, nameof(Password)); }
        private bool PropertiesIsNull() => (string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(Login)) ? true : false;
        private bool UserIsExits()
        {
            var isExist = false;
            try
            {
                isExist = _userService.GetUsers().Any(c => c.Login == Login && c.Password == Password);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return isExist;
        }
        private void OpenMainWindow()
        {
            Password = _view.PasswordBox.Password;
            if (!PropertiesIsNull())
            {
                if (UserIsExits())
                {
                    var MainWindow = new MainWindow(_ctx);
                    var CurrentWindow = Application.Current.MainWindow;
                    MainWindow.Show();
                    Application.Current.MainWindow = MainWindow;
                    CurrentWindow.Close();
                }
                else
                {
                    MessageBox.Show("Учётная запись не найдена!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                    Login = null!;
                    Password = null!;
                    _view.PasswordBox.Password = null!;
                }
            }
            else
                MessageBox.Show("Все поля должны быть заполнены!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);


        }
        #region Commands
        public ICommand AuthorizationButton => new Command(authorization => OpenMainWindow());

        #endregion
    }
}
