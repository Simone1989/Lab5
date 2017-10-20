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
using System.Text.RegularExpressions;

namespace Lab5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //List<User> UserList = new List<User>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonCreateUser_Click(object sender, RoutedEventArgs e)
        {
            if(CheckInput())
            {
                NewUser();
            }
        }

        // If Name or Email input is invalid:
        public bool CheckInput()
        {
            if (TextBoxName.Text == null || TextBoxName.Text == "")
            {
                MessageBox.Show("Please enter name.");
                return false;
            }
            else if (TextBoxEmail.Text == null || TextBoxEmail.Text == "")
            {
                MessageBox.Show("Ivalid email.");
                return false;
            }
            else
            {
                return true;
            }
        }

        public void NewUser()
        {
            //Currently displays object name. 
            ListBoxUsers.Items.Add(new User(TextBoxName.Text, TextBoxEmail.Text, false));
        }

        private void ListBoxUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ButtonDeleteUser.IsEnabled = true;
            ButtonChangeUserInfo.IsEnabled = true;
            ButtonMakeAdmin.IsEnabled = true;
            LabelShowUserInfo.Content = ListBoxUsers.SelectedItem.ToString();
        }

        private void ButtonDeleteUser_Click(object sender, RoutedEventArgs e)
        {
            ListBoxUsers.Items.Remove(ListBoxUsers.SelectedItem);
            LabelShowUserInfo.Content = null;
        }
    }
}