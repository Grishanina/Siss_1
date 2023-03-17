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

namespace Siss
{
    /// <summary>
    /// Логика взаимодействия для PageProduct.xaml
    /// </summary>
    public partial class PageProduct : Page
    {

        
        public PageProduct()
        {
            InitializeComponent();

            listProduct.ItemsSource = ClassBase.BD.Product.ToList();

            tbCount.Text = "По данным запросам найдено количество записей: " + ClassBase.BD.Product.ToList().Count;
        }

        // Процент скидки на товар
        private void PricePT_Loaded(object sender, RoutedEventArgs e)
        {
            TextBlock tb = (TextBlock)sender;
            int index = Convert.ToInt32(tb.Uid);

            List<Product> TP = ClassBase.BD.Product.Where(x => x.ID_Product == index).ToList();

            double pr = 0;

            foreach (Product prd in TP)
            {
                pr += Convert.ToDouble(prd.ProductDiscountAmount);
            }

            tb.Text = "" + pr.ToString("F1") + " %";
        }

        void Filter()  // метод для одновременной фильтрации, поиска и сортировки
        {
            List<Product> productList = ClassBase.BD.Product.ToList();

            // поиск совпадений по названию продукта
            if (!string.IsNullOrWhiteSpace(poisk.Text))  // если строка не пустая и если она не состоит из пробелов
            {
                productList = productList.Where(x => x.ProductName.ToLower().Contains(poisk.Text.ToLower())).ToList();
            }

            // фильтрация
            switch (filter.SelectedIndex)
            {
                case 1:
                    {
                        productList = productList.Where(x => x.ProductDiscountAmount >= 0 && x.ProductDiscountAmount < 10).ToList();
                    }
                    break;
                case 2:
                    {
                        productList = productList.Where(x => x.ProductDiscountAmount >= 10 && x.ProductDiscountAmount < 15).ToList();
                    }
                    break;
                case 3:
                    {
                        productList = productList.Where(x => x.ProductDiscountAmount >= 15 && x.ProductDiscountAmount < 20).ToList();
                    }
                    break;
                case 4:
                    {
                        productList = productList.Where(x => x.ProductDiscountAmount >= 20 && x.ProductDiscountAmount < 30).ToList();
                    }
                    break;
            }

            // сортировка
            switch (sort.SelectedIndex)
            {
                case 1:
                    {
                        productList.Sort((x, y) => x.ProductCost.CompareTo(y.ProductCost));
                    }
                    break;
                case 2:
                    {
                        productList.Sort((x, y) => x.ProductCost.CompareTo(y.ProductCost));
                        productList.Reverse();
                    }
                    break;
            }

            listProduct.ItemsSource = productList;
            if (productList.Count == 0)
            {
                MessageBox.Show("нет записей");
            }
            tbCount.Text = "По данным запросам найдено количество записей: " + productList.Count;
        }

        private void filter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        private void sort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        private void poisk_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filter();
        }

        private void cost_Loaded(object sender, RoutedEventArgs e)
        {
            TextBlock tb = (TextBlock)sender;
            int index = Convert.ToInt32(tb.Uid);

            List<Product> TP = ClassBase.BD.Product.Where(x => x.ID_Product == index).ToList();

            double pr = 0;

            foreach (Product prd in TP)
            {
                if(prd.ProductDiscountAmount!=0)
                {
                   pr += Convert.ToDouble(prd.ProductCost - ((prd.ProductDiscountAmount * prd.ProductCost) / 100));
                   tb.Text = "Цена " + pr.ToString() + " руб.";
                }
                else
                {
                    tb.Text = "";
                }
                
            }   
        }

        private void cost_dis_Loaded(object sender, RoutedEventArgs e)
        {
            TextBlock tb = (TextBlock)sender;
            int index = Convert.ToInt32(tb.Uid);

            List<Product> TP = ClassBase.BD.Product.Where(x => x.ID_Product == index).ToList();

            double pr = 0;

            foreach (Product prd in TP)
            {
                if(prd.ProductDiscountAmount != 0)
                {
                    pr += Convert.ToDouble(prd.ProductCost);
                    tb.Text = "Цена " + pr.ToString() + " руб.";
                    tb.TextDecorations = TextDecorations.Strikethrough;
                }
                else
                {
                    pr += Convert.ToDouble(prd.ProductCost);
                    tb.Text = "Цена " + pr.ToString() + " руб.";
                    
                }
                
            }
            

            
        }
    }
}
