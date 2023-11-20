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
    namespace flappy_bird_2
    {
        /// <summary>
        /// Interaction logic for MainWindow.xaml
        /// </summary>
        public partial class MainWindow : Window
        {
            DispatcherTimer GameTimer = new DispatcherTimer();
            double Points;
            int Gravity = 8;
            int speed = 0;
            bool Over;
            Rect supermanBox;

            public MainWindow()
            {
                InitializeComponent();
                GameTimer.Tick += MainEventTimer;
                GameTimer.Interval = TimeSpan.FromMilliseconds(20);
                StartGame();
            }
            //method event trong game
            private void MainEventTimer(object sender, EventArgs e)
            {
                //label cho điểm
                txtPoints.Content = "Points: " + Points;
                //khởi tạo nhân vật superman
                supermanBox = new Rect(Canvas.GetLeft(superman), Canvas.GetTop(superman), superman.Width - 10, superman.Height);
                //set vị trí superman ở giữa
                Canvas.SetTop(superman, Canvas.GetTop(superman) + Gravity);
                //nếu superman bay cao quá hoặc rớt thì die
                if (Canvas.GetTop(superman) < -100 || Canvas.GetTop(superman) > 458)
                {
                    StopGame();
                }
                //move pipe
                foreach (var x in Mygame.Children.OfType<Image>())
                {
                    if((string)x.Tag == "pipe1" ||  (string)x.Tag == "pipe2" || (string)x.Tag == "pipe3")
                    {
                        Canvas.SetLeft(x, Canvas.GetLeft(x) - 5);
                        if (Canvas.GetLeft(x) < -100) 
                        {
                            Canvas.SetLeft(x, 800);

                            Points += 0.25; 
                        }
                       //bị lỗi //Rect pipeHitbox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
                        //if (supermanBox.IntersectsWith(pipeHitbox))
                        //{
                        //    StopGame();
                        //}
                    }
                }
            }

            //method khi bay len
            private void Down(object sender, KeyEventArgs e)
            {
                if (e.Key == Key.Space)
                {
                    superman.RenderTransform = new RotateTransform(-30, superman.Width / 2, superman.Height / 2);
                    Gravity = -8;
                }

                if (e.Key == Key.R && Over == true)
                {
                    StartGame();
                }
            }
            //method bay
            private void Up(object sender, KeyEventArgs e)
            {
                //chuyển gốc độ khi nhân vật rơi xuống 
                superman.RenderTransform = new RotateTransform(5, superman.Width / 2, superman.Height / 2);
                Gravity = 8;
            }
            //bat dau vao tro choi
            private void StartGame()
            {

                Mygame.Focus();
                int temp = 300;

                Points = 0;
                Over = false;
                Canvas.SetTop(superman, 190);

                //chạy vòng lặp từng hình ảnh.... 
                foreach(var x in Mygame.Children.OfType<Image>())
                {
                    //set vị trí của các pipe
                    if((string)x.Tag == "pipe1")
                    {
                        Canvas.SetLeft(x, 500);
                    }
                    if ((string)x.Tag == "pipe2")
                    {
                        Canvas.SetLeft(x, 800);
                    }
                    if((string)x.Tag == "pipe3")
                    {
                        Canvas.SetLeft(x, 1100);
                    }
                    if ((string)x.Tag == "pipe4")
                    {
                        Canvas.SetLeft(x, 1800);
                    }
                   if ((string)x.Tag == "cloud2")
            {
                Canvas.SetLeft(x, 100 + temp);
                temp += 200; 
            }
            if ((string)x.Tag == "cloud3")
            {
                Canvas.SetLeft(x, 200 + temp);
                temp += 200; 
            }
            if ((string)x.Tag == "cloud4")
            {
                Canvas.SetLeft(x, 300 + temp);
                temp += 200; 
            }
                }
                GameTimer.Start();
            }
            //method kết thúc game  
            private void StopGame() 
            {
                GameTimer.Stop();
                Over = true;
                //////txtPoints.Content = "Your highest points: " + Points;
                txtPoints.Content += "Game over";
            }
        }
    }
