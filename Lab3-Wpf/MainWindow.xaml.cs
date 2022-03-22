using System;
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

using System.Windows.Threading;


namespace Lab3_Wpf
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        Random rnd;
        DispatcherTimer timer;
        int wHeight, wWidth;
        int LevelInterval = 1000;
        public MainWindow()
        {
            InitializeComponent();
            rnd = new Random();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(LevelInterval);
            timer.Tick += Timer_Tick;
            timer.Start();

            wHeight = (int)this.Height;
            wWidth = (int)this.Width;

            CreateRectangles(5);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            CreateRectangles(1);
        }

        private void CreateRectangles(int n)
        {
            int width = 250;
            int height = 50;
            
            for (int i = 0; i < n; i++)
            {
                Rectangle rect = new Rectangle();
                if (rnd.Next(2) == 0)
                {
                    rect.Height = width;
                    rect.Width = height;
                }
                else
                {
                    rect.Height = height;
                    rect.Width = width;
                }
                

                rect.HorizontalAlignment = HorizontalAlignment.Left;
                rect.VerticalAlignment = VerticalAlignment.Top;
                int x = rnd.Next((int)(wWidth - rect.Width - 50));
                int y = rnd.Next((int)(wHeight - rect.Height - 100));
                rect.Margin = new Thickness(x, y, 0, 0);
                rect.Stroke = new SolidColorBrush(Colors.Black);
                byte r = (byte)rnd.Next(256);
                byte g = (byte)rnd.Next(256);
                byte b = (byte)rnd.Next(256);
                rect.Fill = new SolidColorBrush(Color.FromRgb(r, g, b));

                rect.MouseDown += Rect_MouseDown;
                grid.Children.Add(rect);

            }

        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //MessageBox.Show("Changed!!");
            wWidth = (int)this.ActualWidth;
            wHeight = (int)this.ActualHeight;
        }

        private void Rect_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Rectangle rect = (Rectangle)sender;

            int i = grid.Children.IndexOf(rect);
            Rect ri = new Rect(rect.Margin.Left, rect.Margin.Top, rect.Width, rect.Height);

            for(int j = i + 1; j < grid.Children.Count; j++)
            {
                Rectangle R = (Rectangle)grid.Children[j];
                Rect rj = new Rect(R.Margin.Left, R.Margin.Top, R.Width, R.Height);

                if (rj.IntersectsWith(ri))
                {
                    return;
                }

            }


            grid.Children.Remove(rect);
            if(grid.Children.Count == 0)
            {
                timer.Stop();
                MessageBox.Show("Victory!");
                LevelInterval -= 100;
                timer.Interval = TimeSpan.FromMilliseconds(LevelInterval);
                CreateRectangles(5);
                timer.Start();

            }
        }
    }
}
