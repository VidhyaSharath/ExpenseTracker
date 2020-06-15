using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using ExpTracker.Model;
using Item = ExpTracker.Model.Item;

namespace ExpTracker
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExpensesListPage : ContentPage
      
    {
        readonly DateTime monthYear;

        // public decimal ExpenseAmount { get; private set; }
        // private readonly string monthYearT;

        public ExpensesListPage()
        {
          //  Title = "Expenses List";
            InitializeComponent();

          //  this.monthYear = monthYear;
          //   monthYearT = $"Expense List for {monthYear.Month}-{monthYear.Year}";
        }

        protected override void OnAppearing()
        {
            //base.OnAppearing();

            var items = new List<ExpTracker.Model.Item>();

            var folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), 
                monthYear.Date.Year.ToString() + monthYear.Date.Month.ToString()); 

            if (Directory.Exists(folderPath))
            {
                var files = Directory.EnumerateFiles(folderPath, "*.field.txt");
                foreach (var filename in files)
                {
                    var data = File.ReadAllText(filename);

                    var values = data.Split(new string[] { "\t" }, StringSplitOptions.None);

                    try
                        
                    {
                        items.Add(new ExpTracker.Model.Item(monthYear)
                        {
                            FilePath = filename,
                            ExpenseName = values[0],
                            DateOfPurchase = DateTime.Parse(values[2]),
                            ExpenseAmount = Decimal.Parse(values[1]),
                            itemType = (ItemCategory)Enum.Parse(typeof(ItemCategory), values[3]),
                            SelectedType = values[3],
                            ImageFileName = ((ItemCategory)Enum.Parse(typeof(ItemCategory), values[3])).ToString().ToLower() + ".png"
                        });
                    }
                    catch (Exception)
                    {
                        File.Delete(filename);
                    }
                }
            }

            listView.ItemsSource = items.OrderBy(d => d.DateOfPurchase).ToList();

         // string budgetFileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), monthYear.Date.Year.ToString() + monthYear.Date.Month.ToString() + ".fields.txt");

            /*Decimal expense = 0;
            foreach (Item item in items)
            {
                expense += item.ExpenseAmount;
            } */

         //Expenses.Text = "Total Expenses: $" + expense.ToString();

          // Budget.Text = "Total Budget for Month: $" + File.ReadAllText(budgetFileName);
        }
  
        private async void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                await Navigation.PushModalAsync(new ExpensesEntryPage()
                {
                    BindingContext = e.SelectedItem as ExpTracker.Model.Item
                });
            }
        }

        private async void OnAddingNewExpense(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new ExpensesEntryPage()
            {
                BindingContext = new ExpTracker.Model.Item(monthYear)
            });
        }

        /*public decimal TotalExpenses
        {
            get
            {
                decimal TotalExpenses = 0;
                var items = new List<ExpTracker.Model.Item>();
                string _budgetFile = Path.Combine
                    (Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), ".fields.txt");
                foreach (var item in items)
                {
                    TotalExpenses += ExpenseAmount;
                }
                return TotalExpenses;
            }
        }*/
    }
}