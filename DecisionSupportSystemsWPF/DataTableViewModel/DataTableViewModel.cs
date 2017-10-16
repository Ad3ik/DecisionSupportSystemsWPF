using DecisionSupportSystemsWPF.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;


namespace DecisionSupportSystemsWPF.DataTableViewMode
{
    public sealed class DataTableViewModel : INotifyPropertyChanged
    {
        //public event PropertyChangedEventHandler PropertyChanged;

        private static volatile DataTableViewModel instance;
        private static object syncRoot = new Object();
        private string filePath;
        public DataTable datatable = new DataTable();
        private DataView modifiedData;
        private DataTableViewModel() { }
        private DataRow row;
        private int rowindex;
        private List<string> allcolumnList;
        private List<string> numbercolumnList;
        private List<string> stringcolumnList;

        public static DataTableViewModel Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new DataTableViewModel();
                        }
                    }
                }

                return instance;
            }
        }

        public string FilePath
        {
            get
            {
                return filePath;
            }
            set
            {
                filePath = value;
                ModifiedData = Data;

            }
        }

        public DataView Data
        {
            //tutaj asdftesasdf
            get
            {

                bool firstline = false;
                string[] lines = System.IO.File.ReadAllLines(FilePath);
                string extension = FilePath.Split(new Char[] { '.' })[1];

                if (extension.Equals("xls") || extension.Equals("xlsx"))
                {
                    #region
                    //Excel.Application excelApp = new Excel.Application();
                    //Excel.Workbook workbook = excelApp.Workbooks.Open(FilePath);
                    //Excel.Worksheet worksheet = (Excel.Worksheet)workbook.Sheets[0];
                    //Excel.Range range = worksheet.UsedRange;

                    //for (int row = 2; row <= range.Rows.Count; row++)
                    //{
                    //    DataRow dr = datatable.NewRow();
                    //    for (int column = 1; column <= range.Columns.Count; column++)
                    //    {
                    //        dr[column - 1] = (range.Cells[row, column] as Excel.Range).Value2.ToString();
                    //    }
                    //    datatable.Rows.Add(dr);
                    //}
                    //workbook.Close(true, Missing.Value, Missing.Value);
                    //excelApp.Quit();
                    #endregion
                }

                else
                {
                    for (int i = 0; i < lines.Count(); i++)
                    {
                        string[] split;

                        if (!lines[i].StartsWith("#"))
                        {
                            if (firstline == false)
                            {
                                split = lines[i].Split(new Char[] { ' ', ',', '.', ';', '\t', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                                foreach (string s in split)
                                {
                                    datatable.Columns.Add(s);
                                }

                                firstline = true;
                            }
                            else
                            {
                                split = lines[i].Split(new Char[] { ' ', ',', ';', '\t', '\n' });
                                DataRow row = datatable.NewRow();
                                for (int x = 0; x < datatable.Columns.Count; x++)
                                {
                                    row[x] = split[x];
                                }
                                datatable.Rows.Add(row);
                            }
                        }
                    }
                }
                datatable.AcceptChanges();
                return datatable.DefaultView;
            }

        }

       
        public DataView ModifiedData
        {
            get
            {
                return modifiedData;
            }
            set
            {
                modifiedData = value;
                NotifyPropertyChanged("ModifiedData");
                GetAllColumnsName();
            }
        }

        
        public DataRow Row
        {
            get
            {
                return row;
            }
            set
            {
                row = value;
                NotifyPropertyChanged("Row");
            }
        }
        
        public int RowIndex
        {
            get
            {
                return rowindex;
            }
            set
            {
                rowindex = value;
                NotifyPropertyChanged("RowIndex");
            }
        }


       
        public List<string> AllColumnList
        {
            get
            {

                return allcolumnList;
            }
            set
            {
                allcolumnList = value;
                NotifyPropertyChanged("AllColumnList");
            }
        }

       
        public List<string> NumberColumnList
        {
            get
            {
                return numbercolumnList;
            }
            set
            {
                numbercolumnList = value;
                NotifyPropertyChanged("NumberColumnList");
            }
        }

       
        public List<string> StringColumnList
        {
            get
            {
                return stringcolumnList;
            }
            set
            {
                stringcolumnList = value;
                NotifyPropertyChanged("StringColumnList");
            }
        }

        public List<string> GetAllClass(int nrcolumn)
        {
            DataTable table = ModifiedData.ToTable();
            IList<string> classnames = new List<string>();
            foreach (DataRow row in table.Rows)
            {
                classnames.Add(row.ItemArray.ElementAt(nrcolumn).ToString());
            }
            return classnames.Distinct().ToList();
        }

        public void GetAllColumnsName()
        {
            List<string> numbercolumns = new List<string>();
            List<string> stringcolumns = new List<string>();
            List<string> allcolumns = new List<string>();
            DataTable table = ModifiedData.ToTable();

            for (int col = 0; col < table.Columns.Count; col++)
            {
                string cell = table.Rows[0].ItemArray.ElementAt(col).ToString();
                float x = 0;
                allcolumns.Add(table.Columns[col].ToString());
                if (float.TryParse(cell, NumberStyles.Number, CultureInfo.InvariantCulture, out x))
                {
                    numbercolumns.Add(table.Columns[col].ToString());
                }
                else
                {
                    stringcolumns.Add(table.Columns[col].ToString());
                }

            }

            AllColumnList = allcolumns;
            NumberColumnList = numbercolumns;
            StringColumnList = stringcolumns;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propertyName)

        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
