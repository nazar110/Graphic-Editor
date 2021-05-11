using Code128_Generator;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
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
using Xceed.Wpf.AvalonDock.Layout;

namespace Raster_Graphics_Editor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        OpenFileDialog fileDialog = new OpenFileDialog();
        Bitmap changedImage = new Bitmap(1864, 1300);

        ChosenTool chosenTool = ChosenTool.Pencil;
        ChosenFigure chosenFigure = ChosenFigure.None;
        ChosenColor chosenColor = new ChosenColor();

        System.Windows.Point start = new System.Windows.Point();
        System.Windows.Point end = new System.Windows.Point();

        public ObservableCollection<object> Figures { get; set; } = new ObservableCollection<object>();

        public MainWindow()
        {
            // this.groupBoxForDrawing.Click += new System.EventHandler(this.groupBox1_Click);
            InitializeComponent();
        }
        #region AdditionalFunctions
        BitmapImage BitmapToImageSource(Bitmap bitmap)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                memory.Position = 0;
                BitmapImage bitmapimage = new BitmapImage();
                bitmapimage.BeginInit();
                bitmapimage.StreamSource = memory;
                bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapimage.EndInit();

                return bitmapimage;
            }
        }
        #endregion
        private void NewFile(object sender, RoutedEventArgs e)
        {
            // Bitmap img = changedImage;
            for (int i = 0; i < changedImage.Width; i++)
            {
                for (int j = 0; j < changedImage.Height; j++)
                {
                    changedImage.SetPixel(i, j, System.Drawing.Color.White);
                }
            }
            image1.Stretch = Stretch.None;
            image1.Source = BitmapToImageSource(changedImage);
            this.Title = "Graphic Editor - Unnamed";
        }
        private void OpenFile(object sender, RoutedEventArgs e)
        {

            fileDialog.Title = "Select a picture";
            fileDialog.Filter = "BMP Files (*.bmp)|*.bmp|JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";
            if (fileDialog.ShowDialog() == true)
            {
                MenuItem menuItem = (MenuItem)sender;
                if (menuItem != null)
                {
                    changedImage = new Bitmap($"{fileDialog.FileName}");
                    image1.Stretch = Stretch.None;
                    image1.Source = new BitmapImage(new Uri(fileDialog.FileName));
                    this.Title = $"Graphic Editor - {fileDialog.SafeFileName}";
                }
            }
        }

        private void ChooseTool(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = (MenuItem)sender;
            if (menuItem != null && (string)menuItem.Header == "Pencil")
            {
                chosenTool = ChosenTool.Pencil;
            }
            else if (menuItem != null && (string)menuItem.Header == "Eraser")
            {
                chosenTool = ChosenTool.Eraser;
            }
            else if (menuItem != null && (string)menuItem.Header == "Text")
            {
                chosenTool = ChosenTool.Text;
            }
            else if (menuItem != null && (string)menuItem.Header == "Brush")
            {
                chosenTool = ChosenTool.Brush;
            }
            else
            {
                chosenTool = ChosenTool.Pencil;
            }
        }
        private void ChooseFigure(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = (MenuItem)sender;
            if (menuItem != null && (string)menuItem.Header == "Line")
            {
                chosenFigure = ChosenFigure.Line;
            }
            else if (menuItem != null && (string)menuItem.Header == "Rectangle")
            {
                chosenFigure = ChosenFigure.Rectangle;
            }
            else if (menuItem != null && (string)menuItem.Header == "Ellipse")
            {
                chosenFigure = ChosenFigure.Ellipse;
            }
            else
            {
                chosenFigure = ChosenFigure.None;
            }
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ((Slider)sender).SelectionEnd = e.NewValue;
        }

        #region Figures
        private void GetStartPosition(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                start = e.GetPosition((IInputElement)image1);
            }
        }
        private void GetEndPosition(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Released)
            {
                end = e.GetPosition((IInputElement)image1);
                if (chosenFigure == ChosenFigure.Line)
                {
                    CreateALine(new System.Drawing.Point((int)start.X, (int)start.Y),
                        new System.Drawing.Point((int)end.X, (int)end.Y));
                }
            }
        }
        private void CreateALine(System.Drawing.Point start, System.Drawing.Point end)
        {
            System.Drawing.Pen pen = new System.Drawing.Pen(System.Drawing.Color.Black, 10);
            using (var graphics = Graphics.FromImage(changedImage))
            {
                graphics.DrawLine(pen, start, end);
            }
            //changedImage.Save("test.bmp");
            image1.Source = BitmapToImageSource(changedImage);

        }
        private void CreateRectangle(System.Drawing.Point start, System.Drawing.Point end)
        {
            System.Drawing.Pen pen = new System.Drawing.Pen(System.Drawing.Color.Black, 10);
            using (var graphics = Graphics.FromImage(changedImage))
            {
                graphics.DrawRectangle(pen, new System.Drawing.Rectangle(start, new System.Drawing.Size(end)));
            }
            //changedImage.Save("test.bmp");
            image1.Source = BitmapToImageSource(changedImage);

        }
        private void CreateEllipse(System.Drawing.Point start, System.Drawing.Point end)
        {
            System.Drawing.Pen pen = new System.Drawing.Pen(System.Drawing.Color.Black, 10);
            using (var graphics = Graphics.FromImage(changedImage))
            {
                graphics.DrawEllipse(pen, new System.Drawing.Rectangle(start, new System.Drawing.Size(end)));
            }
            //changedImage.Save("test.bmp");
            image1.Source = BitmapToImageSource(changedImage);

        }
        #endregion

        private void groupBoxForDrawing_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                start = e.GetPosition(this);
            }
        }

        private void groupBoxForDrawing_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Released)
            {
                end = e.GetPosition(this);
                if (chosenFigure == ChosenFigure.Line)
                {
                    CreateALine(new System.Drawing.Point((int)start.X, (int)start.Y),
                        new System.Drawing.Point((int)end.X, (int)end.Y));
                }
                if (chosenFigure == ChosenFigure.Rectangle)
                {
                    CreateRectangle(new System.Drawing.Point((int)start.X, (int)start.Y),
                        new System.Drawing.Point((int)end.X, (int)end.Y));
                }
                if (chosenFigure == ChosenFigure.Ellipse)
                {
                    CreateEllipse(new System.Drawing.Point((int)start.X, (int)start.Y),
                        new System.Drawing.Point((int)end.X, (int)end.Y));
                }
            }
        }
    }
}
