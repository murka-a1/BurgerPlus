using BurgerPlus.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BurgerPlus.Page
{
    /// <summary>
    /// Логика взаимодействия для PageUsersAdd.xaml
    /// </summary>
    public partial class PageUsersAdd
    {
        private Users _currentUsers = new Users();
        public PageUsersAdd(Users selectedUsers)
        {
            InitializeComponent();
            ComboRole.ItemsSource = BurgerPlusEntities.GetContext().Roles.ToList();

            if (selectedUsers != null)
                _currentUsers = selectedUsers;

            DataContext = _currentUsers;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_currentUsers.Login))
                errors.AppendLine("Укажите логин!");
            if (string.IsNullOrWhiteSpace(_currentUsers.Password))
                errors.AppendLine("Укажите пароль!");
            if (string.IsNullOrWhiteSpace(_currentUsers.LastName))
                errors.AppendLine("Укажите фамилию!");
            if (string.IsNullOrWhiteSpace(_currentUsers.FirstName))
                errors.AppendLine("Укажите имя!");
            if (string.IsNullOrWhiteSpace(_currentUsers.Patronymic))
                errors.AppendLine("Укажите пароль!");
            if (_currentUsers.Roles == null)
                errors.AppendLine("Выберите роль!");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            if (_currentUsers.Id_Role == 0)
                BurgerPlusEntities.GetContext().Users.Add(_currentUsers);
            try
            {
                BurgerPlusEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена");
                AppFrame.FrameMain.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }

        private void BtnGoBack_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.FrameMain.GoBack();
        }
    }
}