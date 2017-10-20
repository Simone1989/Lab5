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
        public MainWindow()
        {
            InitializeComponent();
        }

        // Function to check id name or email input is invalid:
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

        // Function to re-disable buttons after use
        public void ButtonsDisabled()
        {
            ButtonDeleteUser.IsEnabled = false;
            ButtonMakeAdmin.IsEnabled = false;
            ButtonDemoteAdmin.IsEnabled = false;
        }


        // Function to create a new user and add them to the user listbox
        public void NewUser()
        {
            ListBoxUsers.Items.Add(new User(TextBoxName.Text, TextBoxEmail.Text, false));
        }
        private void ButtonCreateUser_Click(object sender, RoutedEventArgs e)
        {
            if(CheckInput())
            {
                NewUser();
            }
        }

        private void ListBoxUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ButtonDeleteUser.IsEnabled = true;
            ButtonChangeUserInfo.IsEnabled = true;
            ButtonMakeAdmin.IsEnabled = true;
            LabelShowUserInfo.Content = ((User)ListBoxUsers.SelectedItem).Name;
            if()
            {

            }
        }

        private void ButtonDeleteUser_Click(object sender, RoutedEventArgs e)
        {
            ListBoxUsers.Items.Remove(ListBoxUsers.SelectedItem);
            LabelShowUserInfo.Content = null;
            ButtonsDisabled();
        }

        private void ButtonMakeAdmin_Click(object sender, RoutedEventArgs e)
        {
            ListBoxAdmins.Items.Add(ListBoxUsers.SelectedItem);
            ListBoxUsers.Items.Remove(ListBoxUsers.SelectedItem);
            ButtonsDisabled();
        }

        private void ListBoxAdmins_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ButtonDemoteAdmin.IsEnabled = true;
        }

        private void ButtonDemoteAdmin_Click(object sender, RoutedEventArgs e)
        {
            ListBoxUsers.Items.Add(ListBoxAdmins.SelectedItem);
            ListBoxAdmins.Items.Remove(ListBoxAdmins.SelectedItem);
            ButtonsDisabled();
        }
    }
}