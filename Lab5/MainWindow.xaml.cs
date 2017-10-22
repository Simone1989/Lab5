﻿using System;
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
        int numberOfAdmins = 0;
        int numberOfUsers = 0;


        public MainWindow()
        {
            InitializeComponent();
        }

        // Function to check id name or email input is invalid:
        public bool CheckInput()
        {
            Match match = Regex.Match(TextBoxEmail.Text, emailPattern);

            if (TextBoxName.Text == null || TextBoxName.Text == "")
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
        }

        //Funktion som kollar om användarnamn/email redan finns
        //OBS ANVÄNDS EJ
        public void CheckIndex()
        {
            if (ListBoxUsers.Items.Contains((User)ListBoxUsers.SelectedItem))
            {
                MessageBox.Show("bajs");
            }
        }

        // Function to create a new user and add them to the user listbox
        public void NewUser()
        {
            ListBoxUsers.Items.Add(new User(TextBoxName.Text, TextBoxEmail.Text));
            numberOfUsers++;
        }
        private void ButtonCreateUser_Click(object sender, RoutedEventArgs e)
        {
            if(CheckInput())
            {
                NewUser();
                TextBoxName.Clear();
                TextBoxEmail.Clear();
            }
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
            numberOfUsers--;
            ButtonsDisabled();
        }

        private void ButtonMakeAdmin_Click(object sender, RoutedEventArgs e)
        {
            ListBoxAdmins.Items.Add(ListBoxUsers.SelectedItem);
            ListBoxUsers.Items.Remove(ListBoxUsers.SelectedItem);
            numberOfAdmins++;
            ButtonsDisabled();
        }

        private void ListBoxAdmins_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ButtonDemoteAdmin.IsEnabled = true;
            ButtonDeleteUser.IsEnabled = true;
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
            TextBoxName.Text = ((User)ListBoxUsers.SelectedItem).Name;
            TextBoxEmail.Text = ((User)ListBoxUsers.SelectedItem).Email;
            ButtonCreateUser.Content = "Update";
        }
    }
}