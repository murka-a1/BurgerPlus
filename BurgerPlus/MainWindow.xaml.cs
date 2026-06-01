using BurgerPlus.Class;
using BurgerPlus.Page;
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

namespace BurgerPlus
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int incorrectLoginAttempts = 0;
        private const int MaxLoginAttempts = 3;
        private string currentLogin = "";
        private int generatedCaptcha;
        private int incorrectAttempts = 0;
        private const int MaxIncorrectAttempts = 3;
        public MainWindow()
        {
            InitializeComponent();
            AppConnect.modelOdb = new BurgerPlusEntities();
            BtnVxod.Visibility = Visibility.Collapsed;
        }

        private void BtnVxod_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string login = TxbLogin.Text;
                string password = PsbPassword.Password;

                var user = AppConnect.modelOdb.Users.FirstOrDefault(x => x.Login == login);

                if (user == null)
                {
                    HandleFailedAttempt(login);
                    return;
                }

                if (user.Password != password)
                {
                    HandleFailedAttempt(login);
                    return;
                }

                ResetAttempts();

                switch (user.Id_Role)
                {
                    case 1:
                        MessageBox.Show("Здравствуйте, Администратор!",
                            "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                        WindowAdmin1 windowadmin = new WindowAdmin1();
                        windowadmin.Show();
                        this.Close();
                        break;
                    case 2:
                        MessageBox.Show("Здравствуйте, Пользователь!",
                            "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                        WindowUser1 windowusers = new WindowUser1();
                        windowusers.Show();
                        this.Close();
                        break;
                    default:
                        MessageBox.Show("Неизвестная роль пользователя!", "Ошибка",
                            MessageBoxButton.OK, MessageBoxImage.Error);
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message + "\nКритическая ошибка приложения!",
                    "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void HandleFailedAttempt(string login)
        {

            if (currentLogin != login)
            {
                ResetAttempts();
                currentLogin = login;
            }

            incorrectLoginAttempts++;
            int remainingAttempts = MaxLoginAttempts - incorrectLoginAttempts;

            ClearFields();
        }

        private void ResetAttempts()
        {
            incorrectLoginAttempts = 0;
        }

        private void ClearFields()
        {
            TxbLogin.Text = string.Empty;
            PsbPassword.Password = string.Empty;
            TxbLogin.Focus();
        }

        private void BtnCapcha_Click(object sender, RoutedEventArgs e)
        {
            if (incorrectAttempts >= MaxIncorrectAttempts)
            {
                MessageBox.Show("Вы заблокированы. Обратитесь к администратору.", "Блокировка",
                              MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(TxbCapchaGeneracia.Text))
            {
                MessageBox.Show("Сначала сгенерируйте капчу!", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!int.TryParse(TxbCapchaVvod.Text, out int userInput))
            {
                MessageBox.Show("Введите корректное число!", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (userInput == generatedCaptcha)
            {
                MessageBox.Show("Капча введена верно!", "Успех",
                              MessageBoxButton.OK, MessageBoxImage.Information);

                BtnVxod.Visibility = Visibility.Visible;

                incorrectAttempts = 0;
            }
            else
            {
                incorrectAttempts++;
                int remainingAttempts = MaxIncorrectAttempts - incorrectAttempts;

                if (incorrectAttempts >= MaxIncorrectAttempts)
                {
                    MessageBox.Show("Вы заблокированы. Обратитесь к администратору.", "Блокировка",
                                  MessageBoxButton.OK, MessageBoxImage.Warning);

                    TxbCapchaGeneracia.Text = string.Empty;
                    TxbCapchaVvod.Text = string.Empty;
                }
                else
                {
                    MessageBox.Show($"Неверная капча! Осталось попыток: {remainingAttempts}", "Ошибка",
                                  MessageBoxButton.OK, MessageBoxImage.Error);


                    Random random = new Random();
                    generatedCaptcha = random.Next(1, 101);
                    TxbCapchaGeneracia.Text = generatedCaptcha.ToString();
                    TxbCapchaVvod.Text = string.Empty;


                    BtnVxod.Visibility = Visibility.Collapsed;
                }
            }
        }
        private void BtnGeneraitCapch_Click(object sender, RoutedEventArgs e)
        {
            if (incorrectAttempts >= MaxIncorrectAttempts)
            {
                MessageBox.Show("Вы заблокированы. Обратитесь к администратору.", "Блокировка",
                              MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }


            Random random = new Random();
            generatedCaptcha = random.Next(1, 101);


            TxbCapchaGeneracia.Text = generatedCaptcha.ToString();


            TxbCapchaVvod.Text = string.Empty;


            BtnVxod.Visibility = Visibility.Collapsed;
        }
    }
}
