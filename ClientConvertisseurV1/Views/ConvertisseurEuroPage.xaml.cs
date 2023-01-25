// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using ClientConvertisseurV1.Models;
using ClientConvertisseurV1.Services;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ClientConvertisseurV1.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ConvertisseurEuroPage : Page, INotifyPropertyChanged
    {

        private double euro;
        public Devise DeviseSelected { get; set; }

        public double Euro
        {
            get { return euro; }
            set { euro = value; OnPropertyChanged("Euro"); }
        }

        private double result;
        public double Result
        {
            get => result; set { result = value; OnPropertyChanged("Result"); }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        private ObservableCollection<Devise> lesDevises;

        public ObservableCollection<Devise> LesDevises
        {
            get { return lesDevises; }
            set { lesDevises = value; OnPropertyChanged("LesDevises");}
        }


        public ConvertisseurEuroPage()
        {
            this.InitializeComponent();
            this.DataContext = this;
            GetDataOnLoadAsync();

        }

        private async void GetDataOnLoadAsync()
        {
            WSService service = new WSService("https://localhost:44338/api/");
            List<Devise> result = await service.GetDevisesAsync("Devises");
            LesDevises = new ObservableCollection<Devise>(result);

        }
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        private void ConvertirButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Result = int.Parse(BindMontant.Text) * LesDevises[ComboDevises.SelectedIndex].Taux;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Erreur Wola");
            }
        }
    }
}
