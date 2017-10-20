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
            CheckInput();
            NewUser();
        }

        // If Name or Email input is invalid: 
        public void CheckInput()
        {
            if (TextBoxName.Text == null || TextBoxName.Text == "")
            {
                MessageBox.Show("Please enter name.");
            }
            else if(TextBoxEmail.Text == null || TextBoxEmail.Text == "")
            {
                MessageBox.Show("Ivalid email.");
            }
        }
        public void NewUser()
        {
            User new2 = new User(TextBoxName.Text, TextBoxEmail.Text, false);
            ListBoxUsers.Items.Add(new2.Name);
        }
    }
}
