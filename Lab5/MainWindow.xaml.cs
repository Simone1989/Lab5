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
        string emailPattern = @"(\w|\D)+[@](\w|\D)+\.(\w|\D)+";
        const string update = "Update";
        const string createUser = "Create user";

        public MainWindow()
        {
            InitializeComponent();
        }

        // Function to check id name or email input is invalid:
        public bool CheckInput()
        {
            Match match = Regex.Match(TextBoxEmail.Text, emailPattern);

            if (TextBoxName.Text == null || TextBoxName.Text == string.Empty)
            {
                MessageBox.Show("Please enter name.");
                return false;
            }
            else if (TextBoxEmail.Text == null || TextBoxEmail.Text == string.Empty || !match.Success)
            {
                MessageBox.Show("Invalid email.");
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
            ButtonChangeUserInfo.IsEnabled = false;
        }

        //Function that checks if UserNameList already contains a certain object in both user and admin list
        public bool CheckForDuplicate()
        {
            if ((UserNameList(ListBoxUsers).Contains(TextBoxName.Text) && UserEmailList(ListBoxUsers).Contains(TextBoxEmail.Text)) || (UserNameList(ListBoxAdmins).Contains(TextBoxName.Text) && UserEmailList(ListBoxAdmins).Contains(TextBoxEmail.Text)))
            {
                MessageBox.Show("Username & Email already exists.\nTry again.");
                return false;
            }
            else if ((UserNameList(ListBoxUsers).Contains(TextBoxName.Text) && !UserEmailList(ListBoxUsers).Contains(TextBoxEmail.Text)) || (UserNameList(ListBoxAdmins).Contains(TextBoxName.Text) && !UserEmailList(ListBoxAdmins).Contains(TextBoxEmail.Text)))
            {
                MessageBox.Show("Username already exists.\nTry again.");
                return false;
            }
            else if ((!UserNameList(ListBoxUsers).Contains(TextBoxName.Text) && UserEmailList(ListBoxUsers).Contains(TextBoxEmail.Text)) || (!UserNameList(ListBoxAdmins).Contains(TextBoxName.Text) && UserEmailList(ListBoxAdmins).Contains(TextBoxEmail.Text)))
            {
                MessageBox.Show("Email already exists.\nTry again.");

                return false;
            }
            else
            {
                return true;
            }
        }

        //Name list created for Duplicate check
        private List<string> UserNameList(ListBox listbox)
        {
            List<string> nameList = new List<string>();
            for (int i = 0; i < listbox.Items.Count; i++)
            {
                nameList.Add(((User)listbox.Items.GetItemAt(i)).Name);
            }
            return nameList;
        }

        //Email list created for Duplicate check
        private List<string> UserEmailList(ListBox listbox)
        {
            List<string> emailList = new List<string>();
            for (int i = 0; i < listbox.Items.Count; i++)
            {
                emailList.Add(((User)listbox.Items.GetItemAt(i)).Email);
            }
            return emailList;
        }

        // Function to create a new user and add them to the user listbox
        public void NewUser()
        {
            if (ButtonCreateUser.Content.Equals(createUser) || ((User)ListBoxUsers.SelectedItem != null))
            {
                ListBoxUsers.Items.Add(new User(TextBoxName.Text, TextBoxEmail.Text));
            }

            else if(((User)ListBoxAdmins.SelectedItem != null))
            {
                ListBoxAdmins.Items.Add(new User(TextBoxName.Text, TextBoxEmail.Text));
            }
        }

        // Function to be called when the edit-button is pressed
        public void EditUserOrAdmin()
        {
            if (((User)ListBoxUsers.SelectedItem != null))
            {
                TextBoxName.Text = ((User)ListBoxUsers.SelectedItem).Name;
                TextBoxEmail.Text = ((User)ListBoxUsers.SelectedItem).Email;
                ButtonCreateUser.Content = update;

                if ((String)ButtonCreateUser.Content == update)
                {
                    ((User)ListBoxUsers.SelectedItem).Name = TextBoxName.Text;
                    ((User)ListBoxUsers.SelectedItem).Email = TextBoxEmail.Text;
                    ButtonsDisabled();
                }
            }
            else if (((User)ListBoxAdmins.SelectedItem != null))
            {
                TextBoxName.Text = ((User)ListBoxAdmins.SelectedItem).Name;
                TextBoxEmail.Text = ((User)ListBoxAdmins.SelectedItem).Email;
                ButtonCreateUser.Content = update;

                if ((String)ButtonCreateUser.Content == update)
                {
                    ((User)ListBoxAdmins.SelectedItem).Name = TextBoxName.Text;
                    ((User)ListBoxAdmins.SelectedItem).Email = TextBoxEmail.Text;
                    ButtonsDisabled();
                }
            }
        }

        private void ButtonCreateUser_Click(object sender, RoutedEventArgs e)
        {
            if (CheckInput())
            {
                //Här triggas Duplicate-funktionen. 
                if (CheckForDuplicate())
                {
                    NewUser();
                    TextBoxName.Clear();
                    TextBoxEmail.Clear();
                }
            }

            if ((String)ButtonCreateUser.Content == update)
            {
                if (((User)ListBoxUsers.SelectedItem != null))
                {
                    ListBoxUsers.Items.Remove(ListBoxUsers.SelectedItem);
                }
                else if (((User)ListBoxAdmins.SelectedItem != null))
                {
                    ListBoxAdmins.Items.Remove(ListBoxAdmins.SelectedItem);
                }
            }
            ButtonCreateUser.Content = createUser;
            ButtonsDisabled();
        }

        private void ListBoxUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ButtonDeleteUser.IsEnabled = true;
            ButtonChangeUserInfo.IsEnabled = true;
            ButtonMakeAdmin.IsEnabled = true;
            if ((User)ListBoxUsers.SelectedItem != null)
            {
                ListBoxAdmins.UnselectAll();
                LabelShowUserInfo.Content = "Username: " +
                    ((User)ListBoxUsers.SelectedItem).Name + "\nEmail Adress: " + ((User)ListBoxUsers.SelectedItem).Email;
            }
            else
            {
                LabelShowUserInfo.Content = string.Empty;
            }
        }

        private void ButtonDeleteUser_Click(object sender, RoutedEventArgs e)
        {
            ListBoxUsers.Items.Remove(ListBoxUsers.SelectedItem);
            ListBoxAdmins.Items.Remove(ListBoxAdmins.SelectedItem);
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
            ButtonDeleteUser.IsEnabled = true;
            ButtonChangeUserInfo.IsEnabled = true;
            if ((User)ListBoxAdmins.SelectedItem != null)
            {
                ListBoxUsers.UnselectAll();
                LabelShowUserInfo.Content = "Admin: " + ((User)ListBoxAdmins.SelectedItem).Name +
                    "\nEmail Adress: " + ((User)ListBoxAdmins.SelectedItem).Email;
            }
            else
            {
                LabelShowUserInfo.Content = string.Empty;
            }
        }

        private void ButtonDemoteAdmin_Click(object sender, RoutedEventArgs e)
        {
            ListBoxUsers.Items.Add(ListBoxAdmins.SelectedItem);
            ListBoxAdmins.Items.Remove(ListBoxAdmins.SelectedItem);
            ButtonsDisabled();
        }

        private void ButtonChangeUserInfo_Click(object sender, RoutedEventArgs e)
        {
             EditUserOrAdmin();
        }
    }
}