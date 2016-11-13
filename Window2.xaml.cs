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
using System.Windows.Shapes;
//using System.Object;
namespace AOIGameSnake
{
    /// <summary>
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {

        private static string imageSet;
        private static string answerSet;
        //private static string answerCk;
        public Window2()
        {
            InitializeComponent();
            //Question question1 = new Question("If A and B are both a logical '1', what is the output?", 1);

            //textBox.Text = question1.returnQuestion();

            //button1.Content = "'1'";
            //button2.Content = "'0'";

            //Application.Current.Properties["Score"] = 0;


            //scoreBox.Text = "Score: " + Application.Current.Properties["Score"];

            //int randObject = 

            
            //to generate random question
            Random rand = new Random();
            int randObject = rand.Next(5);

            if (randObject == 0)
            {
                Question question1 = new Question("If A and B are both a logical '1', what is the output?", "C:\\Users\\S\\Pictures\\symbol-of-and-gate.png", "'1'");
                //question1.Image_Loaded();
                imageSet = question1.returnImage();
                textBox.Text = question1.returnQuestion();
                answerSet = question1.returnAnswer();
                //textBox.Text = answerSet;
            }

            else if (randObject == 1)
            {
                Question question2 = new Question("If A is a logical '1' and B is a logical '0', what is the output?", "C:\\Users\\S\\Pictures\\2000px-Or-gate-en.svg.png", "'1'");
                //question1.Image_Loaded();
                imageSet = question2.returnImage();
                textBox.Text = question2.returnQuestion();
                answerSet = question2.returnAnswer();
            }

            else if (randObject == 2)
            {
                Question question3 = new Question("If A1 and B1 are both '0', what is Y1?", "C:\\Users\\S\\Pictures\\NORGate.png", "'1'");
                //question1.Image_Loaded();
                imageSet = question3.returnImage();
                textBox.Text = question3.returnQuestion();
                answerSet = question3.returnAnswer();
            }

            else if (randObject == 3)
            {
                Question question4 = new Question("If A is a 1 and B and C are a '0', What is the output?", "C:\\Users\\S\\Pictures\\Circuit4.png", "'0'");
                //question1.Image_Loaded();
                imageSet = question4.returnImage();
                textBox.Text = question4.returnQuestion();
                answerSet = question4.returnAnswer();
            }

            else if (randObject ==4)
            {
                Question question5 = new Question("Which logic gate does this truth table represent?", "C:\\Users\\S\\Pictures\\2-Input-NAND-Gate-Truth-Table.jpg", "NAND");
                //question1.Image_Loaded();
                imageSet = question5.returnImage();
                textBox.Text = question5.returnQuestion();
                answerSet = question5.returnAnswer();
            }
        }

        
        /*private void button1_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Correct Answer!", "Correct!", MessageBoxButton.OK);

            Application.Current.Properties["Score"] = 1;
            scoreBox.Text = "Score: " + Application.Current.Properties["Score"];

            //Question question2 = new Question("If A is a logical '1' and B is a logical '0', what is the output?", 2, "C:\\Users\\S\\Pictures\\2000px-Or-gate-en.svg.png");

            //textBox.Text = question2.returnQuestion();

           /* BitmapImage c = new BitmapImage();
            c.BeginInit();
            c.UriSource = new Uri(question2.returnImage());
            c.EndInit();
            var image = sender as Image;
            image.Source = c;*/

           // SetImage("C:\\Users\\S\\Pictures\\2000px-Or-gate-en.svg.png");

            /*Window win2 = new Window();
            win2.Show();
            this.Close();*/

            //string score = Application.Current.Properties["Score"];

            /*Window2 win2 = new Window2();
            win2.Show();
            this.Hide();


        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Incorrect Answer!", "Incorrect!", MessageBoxButton.OK, MessageBoxImage.Warning);


            //button2.Visibility = 0;
            
        }*/

        public void Image_Loaded(object sender, RoutedEventArgs e)
        {
            BitmapImage b = new BitmapImage();
            //Question question1 = new Question("C:\\Users\\S\\Pictures\\symbol-of-and-gate.png");
            b.BeginInit();
            b.UriSource = new Uri(imageSet);
            b.EndInit();
            var image = sender as Image;
            image.Source = b;

            /*Image img = new Image();
            BitmapImage bitmapImage = new BitmapImage();
            Uri uri = new Uri(imageSet);
            bitmapImage.UriSource = uri;
            img.Source = bitmapImage;*/
        }

        private void SetImage(string path)
        {
            image.BeginInit();
            image.Source = new BitmapImage(new Uri(path, UriKind.RelativeOrAbsolute));
            image.EndInit();
        }

        private void Submitbtn_Click(object sender, RoutedEventArgs e)
        {
            //System.Windows.Application.Current.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal,(System.Threading.ThreadStart)delegate { answerCk = AnswerBox.Text; });
            //textBox.Text = answerCk;
            string answerCk = UserInput.ToString();
            //textBox.Text = answerCk;
            if (answerCk == "System.Windows.Controls.TextBox: " + answerSet)
            {
                MessageBox.Show("Correct Answer!", "Correct!", MessageBoxButton.OK);
                Window1 win1 = new Window1();
                win1.Show();
                this.Close();
            }

            else
            {
                MessageBox.Show("Wrong Answer, Try Again!", "Incorrect!", MessageBoxButton.OK);
            }
            //textBox.Text = AnswerBox.Text;
        }
    }




    class Question
    {
        private string question;
        //private string aoiType;
        //private int questionNum;
        private string image;
        //private int lives;
        private string answer;

        public Question(string q, string i, string a)
        {
            this.question = q;
            //this.aoiType = aoi;
            //this.questionNum = num;
            this.image = i;
            this.answer = a;
            //lives = l;
        }

        public Question()
        {
            this.question = "";
            this.image = "";
            this.answer = "";
            //lives = 0;
        }

        public Question(string i)
        {
            this.image = i;
        }

        public string returnQuestion()
        {
            return question;
        }

        /*public int returnQuestionNum()
        {
            return questionNum;
        }*/


        public string returnImage()
        {
            return image;
        }

        public string returnAnswer()
        {
            return answer;
        }

    }
}

