using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GraphicKey
{
    class StartPointsDrawer
    {
        MainWindow window;
        List<Ellipse> borderCollection;

        public StartPointsDrawer(MainWindow window)
        {
            this.window = window;
            borderCollection = new List<Ellipse>();
        }

        public void DrawStartPoints()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Ellipse ellipse = new Ellipse { Width = 10, Height = 10, Fill = Brushes.Black };
                    ellipse.MouseDown += Ellipse_MouseDown;
                    ellipse.Name = $"ellipse{i}{j}";
                    Grid.SetRow(ellipse, i);
                    Grid.SetColumn(ellipse, j);
                    window.mainGrid.Children.Add(ellipse);
                }
            }
        }

        private void Ellipse_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Ellipse ellipse = (Ellipse)sender;
            int rowPosition = Convert.ToInt32(ellipse.Name.Substring(6, 1));
            int columnPosition = Convert.ToInt32(ellipse.Name.Substring(7, 1));
            window.mainGrid.Children.Remove(borderCollection.First(x => x.Name == $"border{rowPosition}{columnPosition}"));
            borderCollection.Remove(borderCollection.First(x => x.Name == $"border{rowPosition}{columnPosition}"));
        }

        private void Ellipse_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Ellipse ellipse = (Ellipse)sender;
            int rowPosition = Convert.ToInt32(ellipse.Name.Substring(7, 1));
            int columnPosition = Convert.ToInt32(ellipse.Name.Substring(8, 1));
            Ellipse borderEllipse = new Ellipse { Width = 50, Height = 50, Fill = Brushes.Transparent,  Stroke = Brushes.Gray, StrokeThickness = 2 };
            borderEllipse.Name = $"border{rowPosition}{columnPosition}";
            borderEllipse.MouseUp += Ellipse_MouseUp;
            Grid.SetRow(borderEllipse, rowPosition);
            Grid.SetColumn(borderEllipse, columnPosition);
            window.mainGrid.Children.Add(borderEllipse);
            borderCollection.Add(borderEllipse);
        }
    }
}
