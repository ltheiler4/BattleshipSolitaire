using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using Battleship.View;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Battleship.Model;
using Battleship.Model.Ships;
using Microsoft.Win32;
using System.Reflection;
using Battleship.Properties;
using Config = Battleship.Model.Config;

namespace Battleship.ViewModel
{
    public class QuizViewModel : NotifyPropertyChangedBaseViewModel
    {
        private ICommand relayCommand;
        private List<Ship> ships;
        private Grid grid;
        private Grid columnNumberGrid;
        private Grid rowNumberGrid;
        public List<Field> AllFields = new List<Field>();
        public List<Field> AllShips = new List<Field>();
        public List<Field> ListOfAllShips = new List<Field>();
        public Brush color;


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
            CheckConfig();
            CreatePlayGround(Config.RowValue, Config.ColumnValue);
            this.GenerateShips();
            this.PlaceShips();
            this.GenerateNumbersOfShips();
            this.AllShips = this.AllFields.FindAll(r => r.IsShip == true);
            this.ListOfAllShips = this.AllFields.FindAll(r => r.IsShip == true);

        }

        public void ShowMessage(object obj)
        {
            var clickedButton = obj as Button;
            if (clickedButton.Name == "export")
            {
                SaveFileDialog mySaveFileDialog = new SaveFileDialog();
                mySaveFileDialog.FileName = "Quiz.png";
                mySaveFileDialog.Filter = "Image files (*.jpg, *.png) | *.jpg; *.png";
                mySaveFileDialog.ShowDialog();
                ExportQuiz(Window.GetWindow(Grid), mySaveFileDialog.FileName);
            }
            else if (clickedButton.Name == "hilfe")
            {
                HelpShip();
            }
            else if (clickedButton.Name == "GridButton")
            {
                if (clickedButton.Background == Brushes.DarkGray)
                {
                    clickedButton.Background = color;
                }
                else if (clickedButton.Background == Brushes.LightSkyBlue)
                {
                    clickedButton.Background = Brushes.DarkGray;
                }
                else
                {
                    clickedButton.Background = Brushes.LightSkyBlue;
                }

            }
            else if (clickedButton.Name == "correct")
            {
                CorrectQuiz();
            }

        }

        public void CheckConfig()
        {
            if (Config.ColumnValue == 0)
            {
                Config.ColumnValue = 6;
            }
            if (Config.RowValue == 0)
            {
                Config.RowValue = 6;
            }
            if (Config.EinerShip == 0)
            {
                Config.EinerShip = 1;
            }
            if (Config.ZweierShip == 0)
            {
                Config.ZweierShip = 1;
            }
            if (Config.DreierShip == 0)
            {
                Config.DreierShip = 1;
            }
            if (Config.ViererShip == 0)
            {
                Config.ViererShip = 1;
            }
        }
        public void CorrectQuiz()
        {
            bool isCorrect = true;
            for (int row = 0; row < this.Grid.RowDefinitions.Count; row++)
            {
                for (int column = 0; column < this.Grid.ColumnDefinitions.Count; column++)
                {
                    var Button = this.Grid.Children.OfType<Button>().Where(r => Grid.GetRow(r) == row).First(c => Grid.GetColumn(c) == column);
                    var ShipPart = this.ListOfAllShips.Count(r => r.RowCoordinate == row && r.ColumnCoordinate == column);
                    if (Button.Background == Brushes.DarkGray)
                    {
                        if (ShipPart == 0)
                        {
                            isCorrect = false;
                        }
                    }
                    else if (Button.Background == this.color)
                    {
                        if (ShipPart != 0)
                        {
                            isCorrect = false;
                        }
                    }
                    else if (Button.Background == Brushes.LightSkyBlue)
                    {
                        if (ShipPart != 0)
                        {
                            isCorrect = false;
                        }
                    }
                }
            }

            if (isCorrect == true)
            {
                MessageBox.Show("Richtig");
            }
            else
            {
                MessageBox.Show("Falsch");
            }
        }

        public void HelpShip()
        {
            if (this.AllShips.Count != 0)
            {
                Random rnd = new Random();
                var randomIndex = rnd.Next(this.AllShips.Count);
                var item = this.AllShips[randomIndex];
                var Button = this.Grid.Children.OfType<Button>().Where(r => Grid.GetRow(r) == item.RowCoordinate).First(c => Grid.GetColumn(c) == item.ColumnCoordinate);
                Button.Background = Brushes.DarkGray;
                this.AllShips.RemoveAt(randomIndex);
            }
            else
            {
                MessageBox.Show("Es wurden bereits alle Schiffe aufgedeckt.");
            }
        }

        public static void ExportQuiz(Visual target, string fileName)
        {
            if (target == null || string.IsNullOrEmpty(fileName))
            {
                return;
            }
            Rect bounds = VisualTreeHelper.GetDescendantBounds(target);
            RenderTargetBitmap renderTarget = new RenderTargetBitmap((Int32)bounds.Width, (Int32)bounds.Height, 96, 96, PixelFormats.Pbgra32);
            DrawingVisual visual = new DrawingVisual();
            using (DrawingContext context = visual.RenderOpen())
            {
                VisualBrush visualBrush = new VisualBrush(target);
                context.DrawRectangle(visualBrush, null, new Rect(new System.Windows.Point(), bounds.Size));
            }

            renderTarget.Render(visual);
            PngBitmapEncoder bitmapEncoder = new PngBitmapEncoder();
            bitmapEncoder.Frames.Add(BitmapFrame.Create(renderTarget));
            using (Stream stm = File.Create(fileName))
            {
                bitmapEncoder.Save(stm);
            }
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
            for (int columnCounter = 0; columnCounter < this.Grid.ColumnDefinitions.Count; columnCounter++)
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
                        var breite = this.Grid.ColumnDefinitions.Count;
                        var maxStartPosition = breite - ship.Width;
                        randomColumn = random.Next(maxStartPosition + 1);
                        randomRow = random.Next(this.Grid.RowDefinitions.Count);
                        if (this.IsFieldFree(randomRow, randomColumn, orientation, ship))
                        {
                            this.SetWaterHorizontalShips(randomRow, randomColumn, ship);
                            for (int i = 0; i < ship.Width; i++)
                            {
                                //var Button = this.Grid.Children.OfType<Button>().Where(r => Grid.GetRow(r) == randomRow).First(c => Grid.GetColumn(c) == randomColumn);
                                //Button.Background = Brushes.DarkGray;
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
                            var hoehe = this.Grid.RowDefinitions.Count;
                            var maxStartPosition = hoehe - ship.Width;
                            randomRow = random.Next(maxStartPosition + 1);
                            randomColumn = random.Next(this.Grid.ColumnDefinitions.Count);
                            if (this.IsFieldFree(randomRow, randomColumn, orientation, ship))
                            {
                                this.SetWaterVerticalShips(randomRow, randomColumn, ship);
                                for (int i = 0; i < ship.Width; i++)
                                {
                                    //var Button = this.Grid.Children.OfType<Button>().Where(r => Grid.GetRow(r) == randomRow).First(c => Grid.GetColumn(c) == randomColumn);
                                    //Button.Background = Brushes.DarkGray;
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
                Field waterAbove = new Field(randomRow - 1, randomColumn - 1, false);
                Field waterUnder = new Field(randomRow + 1, randomColumn - 1, false);
                this.AllFields.Add(waterAbove);
                this.AllFields.Add(waterUnder);
                randomColumn++;
            }
        }
        public void SetWaterVerticalShips(int randomRow, int randomColumn, Ship ship)
        {
            Field waterAbove = new Field(randomRow - 1, randomColumn, false);
            Field waterUnder = new Field(randomRow + ship.Width, randomColumn, false);
            this.AllFields.Add(waterAbove);
            this.AllFields.Add(waterUnder);

            for (int i = 0; i < ship.Width + 2; i++)
            {
                Field waterLeft = new Field(randomRow - 1, randomColumn - 1, false);
                Field waterRight = new Field(randomRow - 1, randomColumn + 1, false);
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
            Ships = new List<Ship>();
            for (int i = 0; i < Config.ViererShip; i++)
            {
                Ships.Add(new Carrier());
            }

            for (int i = 0; i < Config.DreierShip; i++)
            {
                Ships.Add(new Cruiser());
            }

            for (int i = 0; i < Config.ZweierShip; i++)
            {
                Ships.Add(new Destroyer());
            }

            for (int i = 0; i < Config.EinerShip; i++)
            {
                Ships.Add(new Submarine() );
            }

        }

        public void CreatePlayGround(int anzRows, int anzColumns)
        {
            Button btn;
            this.Grid = new Grid();
            this.ColumnNumberGrid = new Grid();
            this.RowNumberGrid = new Grid();

            for (int i = 0; i < anzColumns; i++)
            {
                this.Grid.ColumnDefinitions.Add(new ColumnDefinition());
                this.ColumnNumberGrid.ColumnDefinitions.Add(new ColumnDefinition());

            }
            for (int i = 0; i < anzRows; i++)
            {
                this.Grid.RowDefinitions.Add(new RowDefinition());
                this.RowNumberGrid.RowDefinitions.Add(new RowDefinition());

            }

            for (int row = 0; row < this.Grid.RowDefinitions.Count; row++)
            {
                for (int column = 0; column < this.Grid.ColumnDefinitions.Count; column++)
                {
                    btn = new Button { Width = 50, Height = 50, Background = Brushes.White };
                    btn.SetValue(Grid.ColumnProperty, column);
                    btn.SetValue(Grid.RowProperty, row);
                    btn.CommandParameter = btn;
                    btn.Name = "GridButton";
                    btn.SetBinding(Button.CommandProperty, "RelayCommand");
                    this.Grid.Children.Add(btn);
                    this.color = btn.Background;
                }
            }
        }
    }
}
