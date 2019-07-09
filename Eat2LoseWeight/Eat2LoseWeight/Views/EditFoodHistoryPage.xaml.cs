﻿using Eat2LoseWeight.Dialogs;
using Eat2LoseWeight.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Eat2LoseWeight.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditFoodHistoryPage : ContentPage
    {
        private EditFoodHistoryViewModel ViewModel { get; }

        public EditFoodHistoryPage()
        {
            InitializeComponent();
            ViewModel = new EditFoodHistoryViewModel(new ConfirmationDialog(this));
            BindingContext = ViewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await ViewModel.LoadAsync();
        }

        private void ListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e) => 
            ViewModel.SelectionChangedCommand.Execute(null);
    }
}