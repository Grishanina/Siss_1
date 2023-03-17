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

namespace Siss
{
    /// <summary>
    /// Логика взаимодействия для Auto.xaml
    /// </summary>
    public partial class Auto : Page
    {
        public Auto()
        {
            InitializeComponent();
        }

        // Проверка данных для входа
        private void go_Click(object sender, RoutedEventArgs e)
        {
            Users UsersObject = ClassBase.BD.Users.FirstOrDefault(z => z.Login == login.Text && z.Pass == pass.Text);
            if (UsersObject == null)
            {
                MessageBox.Show("Не верно введены логин или пароль!");
            }
            else
            {
                switch (UsersObject.ID_role)
                {
                    case 1:  // Клиент                    
                        ClassFrame.Mfrm.Navigate(new PageProduct());
                        MessageBox.Show("Здравствуйте, Клиент!");
                        break;
                    case 2:  // Администратор                    
                        ClassFrame.Mfrm.Navigate(new PageProduct());
                        MessageBox.Show("Здравствуйте, Администратор!");
                        break;
                    case 3:  // Менеджер                     
                        ClassFrame.Mfrm.Navigate(new PageProduct());
                        MessageBox.Show("Здравствуйте, Менеджер!");
                        //ClassFrame.Mfrm.Navigate(new PageProduct(UsersObject));
                        break;
                    default:
                        break;
                }
            }
        }

        // Вход ка НЕАВТОРИЗОВАННЫЙ поьзователь
        private void goto_Click(object sender, RoutedEventArgs e)
        {
            ClassFrame.Mfrm.Navigate(new PageProduct());
        }
    }
}
