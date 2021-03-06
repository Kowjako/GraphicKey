﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GraphicKey
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        StartPointsDrawer drawer;

        public MainWindow()
        {
            InitializeComponent();
            drawer = new StartPointsDrawer(this);
            drawer.DrawStartPoints();
        }

        private void DockPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            drawer.ClearAll();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            drawer.SetKey();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            drawer.ValidateKey();
        }
    }
}
