using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ExpTracker
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        string _budgetFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), 
            ".fields.txt");
        public MainPage()
        {
            InitializeComponent();
            if (File.Exists(_budgetFile))
            {
                budgetEditor.Text = File.ReadAllText(_budgetFile);
            }
        }

        private async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            File.WriteAllText(_budgetFile, budgetEditor.Text);
            await Navigation.PushModalAsync(new ExpensesListPage { });
        }

        private void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            if (File.Exists(_budgetFile))
            {
                File.Delete(_budgetFile);
            }
            budgetEditor.Text = string.Empty;
        }

        private void MonthYear_DateSelected(object sender, DateChangedEventArgs e)
        {

        }

    }
}
