using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GraphicKey
{
    class StartPointsDrawer
    {
        MainWindow window;
        List<Ellipse> borderCollection;
        List<Line> lineCollection;
        bool firstEdgeSelected = false;
        Ellipse previousEllipse;

        public StartPointsDrawer(MainWindow window)
        {
            this.window = window;
            borderCollection = new List<Ellipse>();
            lineCollection = new List<Line>();
        }

        public void DrawStartPoints()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Ellipse ellipse = new Ellipse { Width = 10, Height = 10, Fill = Brushes.Black };
                    ellipse.Name = $"ellipse{i}{j}";
                    ellipse.MouseDown += Ellipse_MouseDown;
                    ellipse.MouseEnter += Ellipse_MouseEnter;
                    Canvas.SetLeft(ellipse, 42+ 100*(j));
                    Canvas.SetTop(ellipse, 50 + 100*(i));
                    window.mainCanvas.Children.Add(ellipse);
                }
            }
        }

        private void Ellipse_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (firstEdgeSelected)
            {
                if (Mouse.LeftButton == MouseButtonState.Pressed)
                {
                    DrawBorder((Ellipse)sender);
                    //difference X between two  = 95, +5 is radius of ellipse
                    Line line = new Line { X1 = Canvas.GetLeft(previousEllipse) + 5, Y1 = Canvas.GetTop(previousEllipse) + 5, X2 = Canvas.GetLeft((Ellipse)sender) + 5, Y2 = Canvas.GetTop((Ellipse)sender) + 5, Stroke = Brushes.Black, StrokeThickness = 2 };
                    previousEllipse = (Ellipse)sender;
                    window.mainCanvas.Children.Add(line);
                    lineCollection.Add(line);
                }
            }
        }

        private void Ellipse_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Ellipse ellipse = (Ellipse)sender;
            if (ellipse != previousEllipse)
            {
                double leftPosition = Canvas.GetLeft(ellipse) + 20;
                double topPosition = Canvas.GetTop(ellipse) + 20;
                window.mainCanvas.Children.Remove(borderCollection.First(x => x.Name == $"border{leftPosition}{topPosition}"));
                borderCollection.Remove(borderCollection.First(x => x.Name == $"border{leftPosition}{topPosition}"));
            }
        }

        private void Ellipse_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            firstEdgeSelected = true;
            previousEllipse = (Ellipse)sender;
            DrawBorder((Ellipse)sender);
        }

        private void DrawBorder(Ellipse ellipse)
        {
            double leftPosition = Canvas.GetLeft(ellipse);
            double topPosition = Canvas.GetTop(ellipse);
            Ellipse borderEllipse = new Ellipse { Width = 50, Height = 50, Fill = Brushes.Transparent, Stroke = Brushes.Gray, StrokeThickness = 2 };
            borderEllipse.Name = $"border{leftPosition}{topPosition}";
            borderEllipse.MouseUp += Ellipse_MouseUp;
            Canvas.SetTop(borderEllipse, topPosition - 20);
            Canvas.SetLeft(borderEllipse, leftPosition - 20);
            window.mainCanvas.Children.Add(borderEllipse);
            borderCollection.Add(borderEllipse);
        }

        public void ClearAll()
        {
            foreach (Line l in lineCollection) window.mainCanvas.Children.Remove(l);
            foreach (Ellipse e in borderCollection) window.mainCanvas.Children.Remove(e);
            lineCollection.Clear();
            borderCollection.Clear();
        }
    }
}
