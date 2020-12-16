using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Battleship.Model;

namespace Battleship.ViewModel
{
    public class ConfigViewModel : NotifyPropertyChangedBaseViewModel
    {
        private ICommand relayCommand;
        public ICommand RelayCommand
        {
            get { return this.relayCommand; }
            set { this.relayCommand = value; }
        }

        private int rowValue;
        public int RowValue
        {
            get { return this.rowValue; }
            set
            {
                this.rowValue = value;
                OnPropertyChanged();
            }
        }

        private int columnValue;
        public int ColumnValue
        {
            get { return this.columnValue; }
            set
            {
                this.columnValue = value;
                OnPropertyChanged();
            }
        }

        private int einerShip;
        public int EinerShip 
        {
            get { return this.einerShip; }
            set
            {
                this.einerShip = value;
                OnPropertyChanged();
            }
        }

        private int zweierShip;
        public int ZweierShip
        {
            get { return this.zweierShip; }
            set
            {
                this.zweierShip = value;
                OnPropertyChanged();
            }
        }
        private int dreierShip;
        public int DreierShip
        {
            get { return this.dreierShip; }
            set
            {
                this.dreierShip = value;
                OnPropertyChanged();
            }
        }

        private int viererShip;
        public int ViererShip
        {
            get { return this.viererShip; }
            set
            {
                this.viererShip = value;
                OnPropertyChanged();
            }
        }


        public ConfigViewModel()
        {
            this.RelayCommand = new RelayCommand(new Action<object>(this.OnClick));
        }

        public void OnClick(object obj)
        {
            var button = obj as Button;
            Config.ColumnValue = ColumnValue;
            Config.RowValue = RowValue;
            Config.EinerShip = EinerShip;
            Config.ZweierShip = ZweierShip;
            Config.DreierShip = DreierShip;
            Config.ViererShip = ViererShip;
            var a = Window.GetWindow(button);
            a.Close();
        }
    }
}
