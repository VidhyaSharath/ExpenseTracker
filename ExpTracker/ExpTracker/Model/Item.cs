using System;
using System.Reflection;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace ExpTracker.Model
{
    
    public enum ItemCategory
    {
        BillsandUtilities,
        Entertainment,
        FoodandDining,
        Transport,
        HealthCare,
        Shopping,
        PersonalCare,
        Grocery
    }

    // Each item will have below fields that needs to be filled
    public class Item
    {
        public DateTime DateOfPurchase { get; set; }
       
        public Item(DateTime monthYear)
        {
            DateOfPurchase = DateTime.Now;
            this.MonthYear = monthYear;
        }

        public string FilePath { get; set; }
        public string ExpenseName { get; set; }
        public decimal ExpenseAmount { get; set; }
        public ItemCategory itemType { get; set; }
        public string SelectedType { get; set; }
        public DateTime MonthYear { get; set; }
        public string ImageFileName { get; set; }

        //  to populate the image once the user chooses the ExpenseCategory.
        public ImageSource itemTypeImage
        {
            get 
            {

                var Source = ImageSource.FromResource("ExpTracker.Assets." + itemType.ToString().ToLower() + ".png",
                    typeof(ExpTracker.Model.Item).GetTypeInfo().Assembly);

                return Source;
            }
        }

        //to display DateofPurchase on ExpensesListPage
        public string DateOfPurchaseString
        {
            get
            {
                return DateOfPurchase.ToString("MM-dd-yyyy");
            }
        }

        //to display TotalExpenses on ExpensesListPage. 
       /* public decimal TotalExpenses
        {
            get
            {
                decimal TotalExpenses = 0;
                var items = new List<ExpTracker.Model.Item>();
                foreach (var item in items)
                {
                    TotalExpenses += ExpenseAmount;
                }
                return TotalExpenses;
            }
          
        }*/
    }
}
