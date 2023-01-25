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
        public event PropertyChangedEventHandler? PropertyChanged;
        private ObservableCollection<Devise> devises;

        public ObservableCollection<Devise> Devises
        {
            get { return devises; }
            set { devises = value; OnPropertyChanged("Devises"); }
        }

        private Devise? selectedDevise;

        public Devise? SelectedDevise
        {
            get { return selectedDevise; }
            set { selectedDevise = value; OnPropertyChanged("SelectedDevise"); }
        }


        private int? montant;

        public int? Montant
        {
            get { return montant; }
            set { montant = value; OnPropertyChanged("Montant"); }
        }
        private double? resultat;

        public double? Resultat
        {
            get { return resultat; }
            set { resultat = value; OnPropertyChanged("Resultat"); }
        }


        public ConvertisseurEuroPage()
        {
            this.InitializeComponent();
            this.DataContext = this;
            GetDataOnLoadAsync();
        }

        private async void GetDataOnLoadAsync()
        {
            WSService service = new WSService("https://localhost:44340/api/");
            List<Devise> result = await service.GetDevisesAsync("Devises");
            if (result is null)
                ShowAsync("L'api n'est actuellement pas disponible.");
            else
                Devises = new ObservableCollection<Devise>(result);
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
            if (ComboDevises.SelectedIndex == -1)
                ShowAsync("Vous devez séléctionner une devise.");
            else if (BindMontant.Text == "")
                ShowAsync("Vous devez entrer un montant");

            Resultat = int.Parse(BindMontant.Text)* Devises[ComboDevises.SelectedIndex].Taux;
        }

        private void ShowAsync(string err)
        {
            ContentDialog message = new ContentDialog
            {
                Title = "Error",
                Content = err,
                CloseButtonText = "OK",
            };
        }
    }
}
