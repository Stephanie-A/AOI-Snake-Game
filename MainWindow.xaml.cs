using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Timers;
using System.Drawing;
using System.Windows.Threading;
using System.Windows.Navigation;
using System.Linq;
using System.Threading.Tasks;

//Only issue is multiple warning boxes pop up
namespace AOIGameSnake
{
    
    

    public partial class Window1 : Window
    {
       // public int numLives = Convert.ToInt32(Application.Current.Properties["NumLeft"]);
        //public static int ToInt32(object value);
        public int score = Convert.ToInt32(Application.Current.Properties["Score"]);
        public Window1()
        {
            InitializeComponent();
            
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timer_Tick);

            /* Here user can change the speed of the snake. 
             * Possible speeds are FAST, MODERATE, SLOW and DAMNSLOW */
            timer.Interval = MODERATE;
            timer.Start();

            //Application.Current.Properties["Score"] = 0;

            this.KeyDown += new KeyEventHandler(OnButtonKeyDown);
            paintSnake(startingPoint);
            currentPosition = startingPoint;

            // Instantiate Food Objects
            for (int n = 0; n < 10; n++)
            {
                paintBonus(n);
            }
        }

        // This list describes the Bonus Red pieces of Food on the Canvas
        private List<Point> bonusPoints = new List<Point>();

        // This list describes the body of the snake on the Canvas
        private List<Point> snakePoints = new List<Point>();


        


        private Brush snakeColor = Brushes.Green;
        private enum SIZE
        {
            THIN = 4,
            NORMAL = 6,
            THICK = 8
        };
        private enum MOVINGDIRECTION
        {
            UPWARDS = 8,
            DOWNWARDS = 2,
            TOLEFT = 4,
            TORIGHT = 6
        };

        private TimeSpan FAST = new TimeSpan(1);
        private TimeSpan MODERATE = new TimeSpan(10000);
        private TimeSpan SLOW = new TimeSpan(50000);
        private TimeSpan DAMNSLOW = new TimeSpan(500000);



        private Point startingPoint = new Point(100, 100);
        private Point currentPosition = new Point();

        // Movement direction initialisation
        private int direction = 0;

        /* Placeholder for the previous movement direction
         * The snake needs this to avoid its own body.  */
        private int previousDirection = 0;

        /* Here user can change the size of the snake. 
         * Possible sizes are THIN, NORMAL and THICK */
        private int headSize = (int)SIZE.THICK;



        private int length = 100;
        //private int score = Application.Current.Properties["Score"];
        private Random rnd = new Random();

        private void paintSnake(Point currentposition)
        {

            /* This method is used to paint a frame of the snake´s body
             * each time it is called. */

                Ellipse newEllipse = new Ellipse();
                newEllipse.Fill = snakeColor;
                newEllipse.Width = headSize;
                newEllipse.Height = headSize;

                Canvas.SetTop(newEllipse, currentposition.Y);
                Canvas.SetLeft(newEllipse, currentposition.X);

                int count = paintCanvas.Children.Count;
                paintCanvas.Children.Add(newEllipse);
                snakePoints.Add(currentposition);


                // Restrict the tail of the snake
                if (count > length)
                {
                    paintCanvas.Children.RemoveAt(count - length + 9);
                    snakePoints.RemoveAt(count - length);
                }
            
        }


        private void paintBonus(int index)
        {
            
                Point bonusPoint = new Point(rnd.Next(5, 620), rnd.Next(5, 380));



                Ellipse newEllipse = new Ellipse();
                newEllipse.Fill = Brushes.Red;
                newEllipse.Width = headSize;
                newEllipse.Height = headSize;

                Canvas.SetTop(newEllipse, bonusPoint.Y);
                Canvas.SetLeft(newEllipse, bonusPoint.X);
                paintCanvas.Children.Insert(index, newEllipse);
                bonusPoints.Insert(index, bonusPoint);
            

        }

        public static bool IsWindowOpen<Window2>(string name = "") where Window2 : Window
        {
            return string.IsNullOrEmpty(name) ? Application.Current.Windows.OfType<Window2>().Any()
                : Application.Current.Windows.OfType<Window2>().Any(w => w.Name.Equals(name));
        } 


        private void timer_Tick(object sender, EventArgs e)
        {
            // Expand the body of the snake to the direction of movement


                switch (direction)
                {
                    case (int)MOVINGDIRECTION.DOWNWARDS:
                        currentPosition.Y += 1;
                        paintSnake(currentPosition);
                        break;
                    case (int)MOVINGDIRECTION.UPWARDS:
                        currentPosition.Y -= 1;
                        paintSnake(currentPosition);
                        break;
                    case (int)MOVINGDIRECTION.TOLEFT:
                        currentPosition.X -= 1;
                        paintSnake(currentPosition);
                        break;
                    case (int)MOVINGDIRECTION.TORIGHT:
                        currentPosition.X += 1;
                        paintSnake(currentPosition);
                        break;
                }
            

            // Restrict to boundaries of the Canvas
            if ((currentPosition.X < 5) || (currentPosition.X > 620) ||
                (currentPosition.Y < 5) || (currentPosition.Y > 380))
            {
                GameOver();
                this.Close();
                //Application.Current.Windows[1].Close();
                //Application.Current.Windows[2].Close();
               // this.Close();
            }

            

            // Hitting a bonus Point causes the lengthen-Snake Effect
            int n = 0; 
            foreach (Point point in bonusPoints)
            {

                if ((Math.Abs(point.X - currentPosition.X) < headSize) &&
                    (Math.Abs(point.Y - currentPosition.Y) < headSize))
                {
                    length += 10;
                    score += 10;

                    // In the case of food consumption, erase the food object
                    // from the list of bonuses as well as from the canvas

                   /* Window2 win2 = new Window2();
                    win2.Show();*/
                    //this.Hide();



                        bonusPoints.RemoveAt(n);
                        paintCanvas.Children.RemoveAt(n);
                        paintBonus(n);
                        break;
                    
                }
                n++;
            }

            // Restrict hits to body of Snake


            for (int q = 0; q < (snakePoints.Count - headSize * 2); q++)
            {

                    Point point = new Point(snakePoints[q].X, snakePoints[q].Y);
                    if ((Math.Abs(point.X - currentPosition.X) < (headSize)) &&
                         (Math.Abs(point.Y - currentPosition.Y) < (headSize)))
                    {
                        GameOver();
                       // break;
                    }
                

            }




        }



        private void OnButtonKeyDown(object sender, KeyEventArgs e)
        {


            
                switch (e.Key)
                {

                    case Key.Down:
                        if (previousDirection != (int)MOVINGDIRECTION.UPWARDS)
                            direction = (int)MOVINGDIRECTION.DOWNWARDS;
                        break;
                    case Key.Up:
                        if (previousDirection != (int)MOVINGDIRECTION.DOWNWARDS)
                            direction = (int)MOVINGDIRECTION.UPWARDS;
                        break;
                    case Key.Left:
                        if (previousDirection != (int)MOVINGDIRECTION.TORIGHT)
                            direction = (int)MOVINGDIRECTION.TOLEFT;
                        break;
                    case Key.Right:
                        if (previousDirection != (int)MOVINGDIRECTION.TOLEFT)
                            direction = (int)MOVINGDIRECTION.TORIGHT;
                        break;

                }
                previousDirection = direction;
            

        }



        private void GameOver()
        {
            //numLives = 5 - Convert.ToInt32(Application.Current.Properties["NumLeft"]);
            //numLives -= 1;
            
                MessageBoxResult result = MessageBox.Show("Save Yourself?", "Try Again?", MessageBoxButton.YesNo, MessageBoxImage.Question);

                Application.Current.Properties["Score"] = score;
                //Application.Current.Properties["NumLeft"] = numLives;

                if (result== MessageBoxResult.Yes) 
                {
                    
                        Window2 win2 = new Window2();
                        win2.Show();
                        this.Close();
                        System.Windows.Threading.Dispatcher.Run();
                    
                        
                }

                else //if (result == MessageBoxResult.No)
                {
                    MessageBox.Show("Your score was " + score, "Game Over!", MessageBoxButton.OK);
                    //Application.Current.Shutdown();
                    this.Close();
                }

         
                //break;

            

            
        }

        /*class Question
        {
            private string question;
            //private string aoiType;
            //private int questionNum;
            private string image;
            // private int answer;

            //public Question(string q = "", /*string aoi, int num/*, int a,*/ /*string i = "")*/
            /*{
                this.question = q;
                //this.aoiType = aoi;
                //this.questionNum = num;
                this.image = i;
                //this.answer = a;
            }

            public Question()
            {
                question = "";
                image = "";
            }

            public Question(string i)
            {
                this.image = i;
            }

            public string returnQuestion()
            {
                return question;
            }

           /* public int returnQuestionNum()
            {
                return questionNum;
            }


            public string returnImage()
            {
                return image;
            }
        }*/
    }
}

