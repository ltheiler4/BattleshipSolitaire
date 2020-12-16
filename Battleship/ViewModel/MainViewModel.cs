using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.ViewModel
{
    using System.Windows.Controls;
    using System.Windows.Input;
    using Battleship.View;

    public class MainViewModel
    {
        private ICommand relayCommand;
        public ICommand RelayCommand
        {
            get { return this.relayCommand; }
            set { this.relayCommand = value; }
        }

        public MainViewModel()
        {
            this.RelayCommand = new RelayCommand(new Action<object>(this.OnClick));
        }

        public void OnClick(object obj)
        {
            var button = obj as Button;
            if (button.Name == "Generate")
            {
                Quiz quiz = new Quiz();
                quiz.Show();
            }
            else if (button.Name == "Config")
            {
                Config config = new Config();
                config.Show();
            }
        }
    }
}
