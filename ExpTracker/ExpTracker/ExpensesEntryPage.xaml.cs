using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ExpTracker
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExpensesEntryPage : ContentPage
    {
       

        public ExpensesEntryPage()
        {
          //  Title = "Expenses Entry";
            InitializeComponent();
            
        }
   
        private async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var item = (ExpTracker.Model.Item)BindingContext;

            if (!string.IsNullOrWhiteSpace(item.ExpenseName))

            {
                // Append MonthYear to folder name
                var folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), 
                    item.MonthYear.Date.Year.ToString() + item.MonthYear.Date.Month.ToString());

                Directory.CreateDirectory(folderPath);

                var filename = string.IsNullOrWhiteSpace(item.FilePath) ? Path.Combine(folderPath, 
                                  $"{Path.GetRandomFileName()}.field.txt") : item.FilePath;

                string data = item.ExpenseName + "\t" + item.ExpenseAmount.ToString() + "\t" + 
                            item.DateOfPurchase.ToString() + "\t" + item.SelectedType;

                File.WriteAllText(filename, data);
            }
            
            await Navigation.PopModalAsync();
        }


        private async void OnCancelButtonClicked(object sender, EventArgs e)
        {
            var item = (ExpTracker.Model.Item)BindingContext;

            var folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                                item.MonthYear.Date.Year.ToString() + item.MonthYear.Date.Month.ToString());

            Directory.CreateDirectory(folderPath);

            var filename = string.IsNullOrWhiteSpace(item.FilePath) ? Path.Combine(folderPath,
                            $"{Path.GetRandomFileName()}.field.txt") : item.FilePath;

            if (File.Exists(filename))
            {
                File.Delete(filename);
            }
            
            await Navigation.PopModalAsync();
        }

    }
}