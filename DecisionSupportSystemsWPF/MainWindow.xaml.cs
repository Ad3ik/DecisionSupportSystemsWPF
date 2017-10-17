using DecisionSupportSystemsWPF.DataTableViewMode;
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
using System.Data;

namespace DecisionSupportSystemsWPF
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public DataTableViewModel vm = DataTableViewModel.Instance;

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = vm;
        }

        private void ChooseFile_Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".txt";
            dlg.Filter = "Pliki txt (*.txt)|*.txt|Pliki programu Excel (*.xls) (*.xlsx)|*.xls;*.xlsx";

            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();


            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                filePathTextbox.Text = dlg.FileName;
                vm.FilePath = dlg.FileName;

            }
        }

        private void ShowTable_Click(object sender, RoutedEventArgs e)
        {
                if (filePathTextbox.Text != null)
                {
                    fileGrid.Visibility = Visibility.Hidden;
                    tableGrid.Visibility = Visibility.Visible;
                    scrollview.Visibility = Visibility.Visible;
                    this.Height = 600;
                    this.Width = 800;
                }
                else
                {
                    MessageBox.Show("Wybierz plik");
                }
            
        }

        private void SelectedRow_Click(object sender, SelectionChangedEventArgs e)
        {
            DataRowView row = (DataRowView)tableDataGrid.SelectedItem;
            vm.Row = row.Row;
            vm.RowIndex = tableDataGrid.SelectedIndex;
        }

        private void Chart2d_Click(object sender, RoutedEventArgs e)
        {

            ChartWindow window = new ChartWindow();
            window.ShowDialog();

        }
    }

}
