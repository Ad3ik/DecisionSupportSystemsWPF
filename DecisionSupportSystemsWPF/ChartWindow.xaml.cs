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
using System.Windows.Shapes;
using DecisionSupportSystemsWPF.DataTableViewMode;
using DecisionSupportSystemsWPF.Model;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Globalization;

namespace DecisionSupportSystemsWPF
{
    /// <summary>
    /// Logika interakcji dla klasy ChartWindow.xaml
    /// </summary>
    public partial class ChartWindow : Window
    {
        public DataTableViewModel vm = DataTableViewModel.Instance;
        //public List<KeyValuePair<double, double>> data = new List<KeyValuePair<double,double>>();
        public List<double> firstdata = new List<double>();
        public List<double> lastdata = new List<double>();
        public string firstcolumn;
        public string secondcolumn;
        public string classcolumn;

        public ChartWindow()
        {
            InitializeComponent();
        }

        private void showChart_Click(object sender, RoutedEventArgs e)
        {
            DataTable table = vm.ModifiedData.ToTable();
            IList<DataChart> data = new List<DataChart>();
            IList<string> classnames = new List<string>();

            int firstcolumnnumber = 0;
            int secondcolumnnumber = 1;
            int classcolumnnumber = 4;

            string Pgm = @"C:\Program Files\gnuplot\bin\gnuplot.exe";
            Process extPro = new Process();
            extPro.StartInfo.FileName = Pgm;
            extPro.StartInfo.UseShellExecute = false;
            extPro.StartInfo.RedirectStandardInput = true;
            extPro.Start();

            StreamWriter gnupStWr = extPro.StandardInput;
            gnupStWr.WriteLine("set xlabel \"x\" font \"Times, 12\"");
            gnupStWr.WriteLine("\n splot '-' title 'test' w  lines ls 1 \n");

            for (int r = 0; r < table.Rows.Count; r++)
            {
                DataRow row = table.Rows[r];
                double firstcolumndouble = double.Parse(row.ItemArray.ElementAt(firstcolumnnumber).ToString(), NumberStyles.Number, CultureInfo.InvariantCulture);
                double secondcolumndouble = double.Parse(row.ItemArray.ElementAt(secondcolumnnumber).ToString(), NumberStyles.Number, CultureInfo.InvariantCulture);

                 String tmp = firstcolumndouble.ToString() + " " + secondcolumndouble.ToString()+ " \n";
                    tmp.Replace(",", ".");
                Debug.Print(tmp);
                gnupStWr.WriteLine(tmp);
                 //gnupStWr.WriteLine("e\n");
            
             
               
                //  gnupStWr.WriteLine(firstcolumndouble);
                //  gnupStWr.WriteLine(secondcolumndouble);
       

                //DataChart datarow = new DataChart()
                //{
                //    rowdata = new KeyValuePair<double, double>(firstcolumndouble, secondcolumndouble),
                //    classcolumn = row.ItemArray.ElementAt(classcolumnnumber).ToString()
                //};

                //data.Add(datarow);
            }

            //for (int i = 1; i < 7; i++)
            //{
            //    String tmp = dat[i].ToString() + " " + dat2[i].ToString() + " " + dat3[i].ToString() + " \n";
            //    Debug.Print(tmp);
            //    gnupStWr.WriteLine(tmp);
            //    Debug.Print(i.ToString());
            //}


             
            gnupStWr.WriteLine("e\n");
            gnupStWr.Flush();


            // chooseGrid.Visibility = Visibility.Hidden;
            // chartGrid.Visibility = Visibility.Visible;
        }
    }
}
