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
using System.Windows.Shapes;

namespace BurgerPlus.Page
{
    /// <summary>
    /// Логика взаимодействия для WindowUser1.xaml
    /// </summary>
    public partial class WindowUser1 : Window
    {
        public WindowUser1()
        {

            InitializeComponent();
            AppFrame.FrameMain = MyFrame;
            AppFrame.FrameMain.Navigate(new MainWindow());

        }
    }
}
