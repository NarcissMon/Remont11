using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows;
using MySql.Data.MySqlClient;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Remont11
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int MyRowCount;
        private readonly string dataConnect = "server=localhost;user=root;database=DNS;port=3306;password=18092003";

        public MainWindow()
        {
            InitializeComponent();
        }
        private void LoadTable(string tableName) // метод для отображения всех таблиц
        {
            try
            {
                string sql = $"SELECT * FROM dns.{tableName}";
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(sql, dataConnect))
                {
                    DataSet ds = new DataSet("DNS");
                    adapter.Fill(ds);
                    DG1.DataContext = ds.Tables[0];
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Button_Click1(object sender, RoutedEventArgs e) // данные об изготовителе
        {
            LoadTable("dannieobizgotovitele");
        }

        private void Button_Click(object sender, RoutedEventArgs e) // филиал
        {
            LoadTable("filial");
        }
  
        private void Button_Click_1(object sender, RoutedEventArgs e) // фирма изготовитель
        {
            LoadTable("firmaizgotovitel");
        }

        private void Button_Click_2(object sender, RoutedEventArgs e) // изготовитель товара страна
        {
            LoadTable("izgotoviteltovarastrana");
        }

        private void Button_Click_3(object sender, RoutedEventArgs e) // категория товара
        {
            LoadTable("kategoriyatovara");
        }

        private void Button_Click_4(object sender, RoutedEventArgs e) // название филиала
        {
            LoadTable("nazvaniefiliala");
        }

        private void Button_Click_5(object sender, RoutedEventArgs e) // покупатель
        {
            LoadTable("pokupatel");
        }

        private void Button_Click_6(object sender, RoutedEventArgs e) // ремонт
        {
            LoadTable("remont");
        }

        private void Button_Click_7(object sender, RoutedEventArgs e) // товар
        {
            LoadTable("tovar");
        }

        private void Button_Click_8(object sender, RoutedEventArgs e) // улица покупателя
        {
            LoadTable("ulitsapokupatelya");
        }

        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            //Создание экземпляра класса
            izgotoviteltovarastrana WNS = new izgotoviteltovarastrana();
            //Отображение созданного окна
            WNS.ShowDialog();
            String sql = "select * from izgotoviteltovarastrana";
            MySqlDataAdapter adapter = new MySqlDataAdapter(sql, dataConnect);
            //Создание пустого объекта ds 
            DataSet ds = new DataSet("DNS");
            //Заполнение объекта ds результатом запроса из строки
            adapter.Fill(ds);
            MyRowCount  = ds.Tables[0].Rows.Count;
            //Создание новой пустой строки в единственной таблице объекта дс типа датасет с тойже схемой, как таблица
            var MyNewRow = ds.Tables[0].NewRow();
            //Заполнение пустых ячеек новой строки значениями, вычисление номера след строи
                                         //Заполнение названия страны
            MyNewRow[1] = Transfer.ValueString; //set

            var proverka = MyNewRow[1].ToString();
            if (proverka.Length !=0)
            {
                ds.Tables[0].Rows.Add(MyNewRow);
            }
            
            DG1.DataContext = ds.Tables[0];

            MySqlCommandBuilder My = new MySqlCommandBuilder(adapter);
            //
            adapter.InsertCommand = My.GetInsertCommand();

            if (Transfer.ValueString != null)
            {
                adapter.Update(ds.Tables[0]);
                Transfer.ValueString = null;

                // Очищаем таблицу перед заполнением
                ds.Tables[0].Clear();

                adapter.Fill(ds);
                DG1.DataContext = ds.Tables[0];
            }
        }

        private void Button_Click_10(object sender, RoutedEventArgs e)
        {
            nazvaniefiliala WNS = new nazvaniefiliala();
            WNS.ShowDialog();
            String sql = "select * from nazvaniefiliala";
            MySqlDataAdapter adapter = new MySqlDataAdapter(sql, dataConnect);
            DataSet ds = new DataSet("DNS");
            adapter.Fill(ds);
            MyRowCount = ds.Tables[0].Rows.Count;
            var MyNewRow = ds.Tables[0].NewRow();
            MyNewRow[0] = MyRowCount + 1;
            MyNewRow[1] = Transfer.ValueString;                                                   
            ds.Tables[0].Rows.Add(MyNewRow);
            DG1.DataContext = ds.Tables[0];

            
            MySqlCommandBuilder My = new MySqlCommandBuilder(adapter);
            
            adapter.InsertCommand = My.GetInsertCommand();

            if (Transfer.ValueString != null)
            {

                adapter.Update(ds.Tables[0]);
                Transfer.ValueString = null;

            }
        }

        private void Button_Click_11(object sender, RoutedEventArgs e) //удаление из страны изготовителя
        {
            try
            {
                int index;
                String sql = "select * from izgotoviteltovarastrana";
                MySqlDataAdapter adapter = new MySqlDataAdapter(sql, dataConnect);
                DataSet ds = new DataSet("DNS");
                adapter.Fill(ds);
                index = DG1.SelectedIndex;
                ds.Tables[0].Rows[index].Delete();
                MySqlCommandBuilder My = new MySqlCommandBuilder(adapter);
                adapter.DeleteCommand = My.GetDeleteCommand();
                adapter.Update(ds.Tables[0]);
                ds.Tables[0].AcceptChanges();
                DG1.DataContext = ds.Tables[0];
            }
            catch (System.IndexOutOfRangeException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void TableComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string tableName = (string)TableComboBox.SelectedItem;
            LoadTable(tableName);
        }

        private void Button_Click_12(object sender, RoutedEventArgs e)
        {
            //Создание экземпляра класса
            izgotoviteltovarastrana WNS = new izgotoviteltovarastrana();
            //Отображение созданного окна
            WNS.ShowDialog();
            String sql = "select * from "+ TableComboBox.SelectedItem.ToString();
            
            MySqlDataAdapter adapter = new MySqlDataAdapter(sql, dataConnect);
            //Создание пустого объекта ds 
            DataSet ds = new DataSet("DNS");
            //Заполнение объекта ds результатом запроса из строки
            adapter.Fill(ds);
            MyRowCount = ds.Tables[0].Rows.Count;
            //Создание новой пустой строки в единственной таблице объекта дс типа датасет с тойже схемой, как таблица
            var MyNewRow = ds.Tables[0].NewRow();
            //Заполнение пустых ячеек новой строки значениями, вычисление номера след строи
            //Заполнение названия страны
            MyNewRow[1] = Transfer.ValueString; //set

            var proverka = MyNewRow[1].ToString();
            if (proverka.Length != 0)
            {
                ds.Tables[0].Rows.Add(MyNewRow);
            }

            DG1.DataContext = ds.Tables[0];

            MySqlCommandBuilder My = new MySqlCommandBuilder(adapter);
            //
            adapter.InsertCommand = My.GetInsertCommand();

            if (Transfer.ValueString != null)
            {
                adapter.Update(ds.Tables[0]);
                Transfer.ValueString = null;

                // Очищаем таблицу перед заполнением
                ds.Tables[0].Clear();

                adapter.Fill(ds);
                DG1.DataContext = ds.Tables[0];
            }
        }
    }
    }


