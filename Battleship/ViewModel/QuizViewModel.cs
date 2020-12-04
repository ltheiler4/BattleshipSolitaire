using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using Battleship.View;
using System.Diagnostics;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using Battleship.Model;
using Battleship.Model.Ships;

namespace Battleship.ViewModel
{
    using System.Reflection;
    using Battleship.Properties;
    using Orientation = System.Windows.Controls.Orientation;

    public class QuizViewModel : NotifyPropertyChangedBaseViewModel
    {
        private ICommand relayCommand;
        private List<Ship> ships;
        private Grid grid;
        private List<string> countedNumbersOfShipsColumn;
        private Grid columnNumberGrid;
        private Grid rowNumberGrid;
        public List<Field> AllFields = new List<Field>();


        public ICommand RelayCommand
        {
            get { return this.relayCommand; }
            set { this.relayCommand = value; }
        }

        public List<Ship> Ships
        {
            get { return ships; }
            set
            {
                this.ships = value;
                this.OnPropertyChanged();
            }
        }

        public Grid Grid
        {
            get { return grid; }
            set
            {
                grid = value;
                this.OnPropertyChanged();
            }
        }

        public Grid ColumnNumberGrid
        {
            get { return columnNumberGrid; }
            set
            {
                columnNumberGrid = value;
                this.OnPropertyChanged();
            }
        }
        public Grid RowNumberGrid
        {
            get { return rowNumberGrid; }
            set
            {
                rowNumberGrid = value;
                this.OnPropertyChanged();
            }
        }


        public QuizViewModel()
        {
            this.RelayCommand = new RelayCommand(new Action<object>(this.ShowMessage));
            CreatePlayGround(7, 7);
            this.GenerateShips();
            this.PlaceShips();
            this.GenerateNumbersOfShips();

        }

        public void ShowMessage(object obj)
        {
            var clickedButton = obj as Button;
        }

        public void GenerateNumbersOfShips()
        {
            //Schöner machen
            for (int rowCounter = 0; rowCounter < this.grid.RowDefinitions.Count; rowCounter++)
            {
                var row = this.AllFields.Count(r => r.RowCoordinate == rowCounter && r.IsShip == true);
                Label numberOfShips = new Label();
                numberOfShips.Content = row.ToString();
                numberOfShips.HorizontalContentAlignment = HorizontalAlignment.Center;
                numberOfShips.VerticalAlignment = VerticalAlignment.Center;
                numberOfShips.FontSize = 15;
                numberOfShips.SetValue(Grid.RowProperty, rowCounter);
                this.rowNumberGrid.Children.Add(numberOfShips);
            }
            for (int columnCounter = 0; columnCounter < this.grid.ColumnDefinitions.Count; columnCounter++)
            {
                var column = this.AllFields.Count(c => c.ColumnCoordinate == columnCounter && c.IsShip == true);
                Label numberOfShips = new Label();
                numberOfShips.Content = column.ToString();
                numberOfShips.HorizontalContentAlignment = HorizontalAlignment.Center;
                numberOfShips.VerticalAlignment = VerticalAlignment.Center;
                numberOfShips.FontSize = 15;
                numberOfShips.SetValue(Grid.ColumnProperty, columnCounter);
                this.columnNumberGrid.Children.Add(numberOfShips);
            }
        }

        public void PlaceShips()
        {
            //viel redundant probieren zu optimieren
            Random random = new Random();
            int randomRow;
            int randomColumn;
            var isShipPlaced = false;

            foreach (var ship in Ships)
            {
                var orientationArray = Enum.GetValues(typeof(Alignment));
                var orientation = (Alignment)orientationArray.GetValue(random.Next(orientationArray.Length));
                if (orientation == Alignment.Horizontal)
                {
                    do
                    {
                        isShipPlaced = false;
                        var breite = this.grid.ColumnDefinitions.Count;
                        var maxStartPosition = breite - ship.Width;
                        randomColumn = random.Next(maxStartPosition + 1);
                        randomRow = random.Next(this.grid.RowDefinitions.Count);
                        if (this.IsFieldFree(randomRow, randomColumn, orientation, ship))
                        {
                            this.SetWaterHorizontalShips(randomRow, randomColumn, ship);
                            for (int i = 0; i < ship.Width; i++)
                            {
                                var Button = this.grid.Children.OfType<Button>().Where(r => Grid.GetRow(r) == randomRow).First(c => Grid.GetColumn(c) == randomColumn);
                                Button.Background = Brushes.DarkGray;
                                Field field = new Field(randomRow, randomColumn, true);
                                this.AllFields.Add(field);
                                randomColumn++;
                            }
                            isShipPlaced = true;
                        }
                    } while (!isShipPlaced);
                }
                else
                {
                    if (orientation == Alignment.Vertical)
                    {
                        do
                        {
                            isShipPlaced = false;
                            var hoehe = this.grid.RowDefinitions.Count;
                            var maxStartPosition = hoehe - ship.Width;
                            randomRow = random.Next(maxStartPosition + 1);
                            randomColumn = random.Next(this.grid.ColumnDefinitions.Count);
                            if (this.IsFieldFree(randomRow, randomColumn, orientation, ship))
                            {
                                this.SetWaterVerticalShips(randomRow, randomColumn, ship);
                                for (int i = 0; i < ship.Width; i++)
                                {
                                    var Button = this.grid.Children.OfType<Button>().Where(r => Grid.GetRow(r) == randomRow).First(c => Grid.GetColumn(c) == randomColumn);
                                    Button.Background = Brushes.DarkGray;
                                    Field field = new Field(randomRow, randomColumn, true);
                                    this.AllFields.Add(field);
                                    randomRow++;
                                }
                                isShipPlaced = true;
                            }
                        } while (!isShipPlaced);
                    }

                }
            }

        }

        

        public void SetWaterHorizontalShips(int randomRow, int randomColumn, Ship ship)
        {
            Field waterLeft = new Field(randomRow, randomColumn - 1, false);
            Field waterRight = new Field(randomRow, randomColumn + ship.Width, false);
            this.AllFields.Add(waterLeft);
            this.AllFields.Add(waterRight);

            for (int i = 0; i < ship.Width + 2; i++)
            {
                Field waterAbove = new Field(randomRow -1 , randomColumn - 1, false);
                Field waterUnder = new Field(randomRow + 1 , randomColumn - 1, false);
                this.AllFields.Add(waterAbove);
                this.AllFields.Add(waterUnder);
                randomColumn++;
            }
        }
        public void SetWaterVerticalShips(int randomRow, int randomColumn, Ship ship)
        {
            Field waterAbove = new Field(randomRow -1, randomColumn, false);
            Field waterUnder = new Field(randomRow + ship.Width, randomColumn, false);
            this.AllFields.Add(waterAbove);
            this.AllFields.Add(waterUnder);

            for (int i = 0; i < ship.Width + 2; i++)
            {
                Field waterLeft = new Field(randomRow - 1, randomColumn - 1, false);
                Field waterRight = new Field(randomRow - 1, randomColumn +1, false);
                this.AllFields.Add(waterLeft);
                this.AllFields.Add(waterRight);
                randomRow++;
            }
        }

        private bool IsFieldFree(int randomRow, int randomColumn, Alignment orientation, Ship ship)
        {
            //Vlt möglich zu verbessern
            if (orientation == Alignment.Horizontal)
            {
                foreach (var field in this.AllFields.Where(r => r.RowCoordinate == randomRow))
                {
                    for (int i = randomColumn; i < randomColumn + ship.Width; i++)
                    {
                        if (field.ColumnCoordinate == i)
                        {
                            return false;
                        }
                    }
                }

                var s = this.AllFields;
            }
            else
            {
                if (orientation == Alignment.Vertical)
                {
                    foreach (var field in this.AllFields.Where(r => r.ColumnCoordinate == randomColumn))
                    {
                        for (int i = randomRow; i < randomRow + ship.Width; i++)
                        {
                            if (field.RowCoordinate == i)
                            {
                                return false;
                            }
                        }
                    }
                }
            }

            return true;
        }


        public void GenerateShips()
        {
            Ships = new List<Ship>()
            {
                new Carrier(),
                new Cruiser(),
                new Destroyer(),
                new Submarine(),
            };
        }

        public void CreatePlayGround(int anzRows, int anzColumns)
        {
            Button btn;
            this.grid = new Grid();
            this.columnNumberGrid = new Grid();
            this.rowNumberGrid = new Grid();

            for (int i = 0; i < anzColumns; i++)
            {
                this.grid.ColumnDefinitions.Add(new ColumnDefinition());
                this.columnNumberGrid.ColumnDefinitions.Add(new ColumnDefinition());

            }
            for (int i = 0; i < anzRows; i++)
            {
                this.grid.RowDefinitions.Add(new RowDefinition());
                this.rowNumberGrid.RowDefinitions.Add(new RowDefinition());

            }

            for (int row = 0; row < this.grid.RowDefinitions.Count; row++)
            {
                for (int column = 0; column < this.grid.ColumnDefinitions.Count; column++)
                {
                    btn = new Button { Width = 50, Height = 50, Background = Brushes.White };
                    btn.SetValue(Grid.ColumnProperty, column);
                    btn.SetValue(Grid.RowProperty, row);
                    btn.CommandParameter = btn;
                    btn.SetBinding(Button.CommandProperty, "RelayCommand");
                    this.grid.Children.Add(btn);


                }
            }
        }
    }
}
