using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecisionSupportSystemsWPF.Model
{
    public class DataModel
    {
        private string columnName;
        private string data;

        public string ColumnName
        {
            get
            {
                return columnName;
            }
            set
            {
                columnName = value;
                NotifyPropertyChanged("ColumnName");
            }
        }

        public string Data
        {
            get
            {
                return data;
            }
            set
            {
                data = value;
                NotifyPropertyChanged("Data");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }


    }
}

