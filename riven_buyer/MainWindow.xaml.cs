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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace riven_buyer
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            AnimMainGrid();
        }
        private void AnimMainGrid()
        {
            // background color for grid needs to be defined before it can be animated
            // https://stackanswers.net/questions/invalidoperationexception-for-coloranimation-for-wpf-grid-background-inside-controltemplate
            MainGrid.Background = new SolidColorBrush(Colors.WhiteSmoke);
            ColorAnimation F = new ColorAnimation()
            {
                From = Color.FromRgb(28, 26, 38),
                To = Color.FromRgb(0, 0, 0),
                AutoReverse = true,
                RepeatBehavior = RepeatBehavior.Forever,
                Duration = new Duration(TimeSpan.FromSeconds(5))
            };
            Storyboard storyboard = new Storyboard();
            //storyboard.Duration = new Duration(new TimeSpan(0, 0, 1));
            storyboard.Children.Add(F);
            Storyboard.SetTarget(F, MainGrid);
            Storyboard.SetTargetProperty(F, new PropertyPath("Background.Color"));
            storyboard.Begin();
        }

        private void animButton(object sender)
        {
            // https://stackoverflow.com/questions/2131797/applying-animated-scaletransform-in-code-problem
            Button b = (Button)sender;
            Storyboard storyboard = new Storyboard();

            ScaleTransform scale = new ScaleTransform(1.0, 1.0);
            b.RenderTransformOrigin = new Point(0.5, 0.5);
            b.RenderTransform = scale;

            DoubleAnimation growAnimation = new DoubleAnimation();
            growAnimation.Duration = TimeSpan.FromMilliseconds(150);
            growAnimation.From = 1.05;
            growAnimation.To = 1;
            storyboard.Children.Add(growAnimation);

            Storyboard.SetTargetProperty(growAnimation, new PropertyPath("RenderTransform.ScaleX"));
            Storyboard.SetTarget(growAnimation, b);

            // now add Y

            DoubleAnimation growAnimation2 = new DoubleAnimation();
            growAnimation2.Duration = TimeSpan.FromMilliseconds(150);
            growAnimation2.From = 1.2;
            growAnimation2.To = 1;
            storyboard.Children.Add(growAnimation2);

            Storyboard.SetTargetProperty(growAnimation2, new PropertyPath("RenderTransform.ScaleY"));
            Storyboard.SetTarget(growAnimation2, b);

            /*ColorAnimation ca = new ColorAnimation()
            {
                From = Color.FromRgb(255, 0, 255),
                To = (Color)ColorConverter.ConvertFromString("#FFAC83D5"),
                AutoReverse = true,
                RepeatBehavior = new RepeatBehavior(111),
                Duration = new Duration(TimeSpan.FromSeconds(.15))
            };
            //b.BorderBrush.BeginAnimation(SolidColorBrush.ColorProperty, ca);

            storyboard.Children.Add(ca);
            Storyboard.SetTargetProperty(ca, new PropertyPath("(Border.BorderBrush).(SolidColorBrush.Color)"));
            Storyboard.SetTarget(ca, b);*/

            storyboard.Begin();
        }

        private bool CheckFieldEmpty(String str)
        {
            bool notEmpty = false;
            if (str.Length > 0)
            {
                notEmpty = true;
            }
            return notEmpty;
        }

        private bool CheckFieldMoney(String str)
        {
            bool isMoney = str.All(Char.IsDigit);
            return isMoney;
        }

        private bool CheckAllFields(String seller, String riven, String price)
        {
            bool ok = false;
            if (!CheckFieldEmpty(seller))
            {
                MessageBox.Show("Sellers name is empty");
            }
            else if (!CheckFieldEmpty(riven))
            {
                MessageBox.Show("Weapon name is empty");
            }
            else if (!CheckFieldEmpty(price))
            {
                MessageBox.Show("No offer entered");
            }
            else if (!CheckFieldMoney(price))
            {
                MessageBox.Show("Offer must be numerical");
            }
            else
            {
                ok = true;
            }
            return ok;
        }

        private void MouseEnterCode(object sender, MouseEventArgs e)
        {
            Button b = (Button)sender;
            //b.Foreground = new SolidColorBrush(Colors.White);
            ColorAnimation ca = new ColorAnimation()
            {
                From = (Color)ColorConverter.ConvertFromString("#FFAC83D5"),
                To = Color.FromRgb(255, 255, 255),
                AutoReverse = true,
                RepeatBehavior = RepeatBehavior.Forever,  // RepeatBehavior = new RepeatBehavior(3),
                Duration = new Duration(TimeSpan.FromSeconds(.45))
            };
            b.Foreground.BeginAnimation(SolidColorBrush.ColorProperty, ca);
        }
        private void MouseLeaveCode(object sender, MouseEventArgs e)
        {
            Button b = (Button)sender;
            Color col = (Color)ColorConverter.ConvertFromString("#FFAC83D5");
            b.Foreground = new SolidColorBrush(col);
        }

        private void btn_make_offer_Click(object sender, RoutedEventArgs e)
        {
            String name_seller = tbx_name_seller.Text.Trim();
            String name_riven = tbx_name_riven.Text.Trim();
            String price = tbx_price.Text.Trim();
            if (CheckAllFields(name_seller, name_riven, price))
            {
                StringBuilder sb = new StringBuilder("/w ");
                sb.Append(name_seller);
                sb.Append(" Hi, I can offer ");
                sb.Append(price);
                sb.Append(":platinum: for your [");
                sb.Append(name_riven);
                sb.Append("] riven if it's still for sale.");
                Clipboard.SetText(sb.ToString());
                animButton(sender);
            }
        }

        private void btn_buy_at_sellers_price_Click(object sender, RoutedEventArgs e)
        {
            String name_seller = tbx_name_seller.Text.Trim();
            String name_riven = tbx_name_riven.Text.Trim();
            String price = tbx_price.Text.Trim();
            if (CheckAllFields(name_seller, name_riven, price))
            {
                StringBuilder sb = new StringBuilder("/w ");
                sb.Append(name_seller);
                sb.Append(" Hi, have you still got your [");
                sb.Append(name_riven);
                sb.Append("] riven for ");
                sb.Append(price);
                sb.Append(":platinum:?");
                Clipboard.SetText(sb.ToString());
                animButton(sender);
            }
        }
    }
}
