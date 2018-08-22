using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WindowsAPI;


using System.Windows.Input;


namespace TibiaBotMarcelo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        public MainWindow()
        {
            InitializeComponent();

            AddHandler(FrameworkElement.MouseDownEvent, new MouseButtonEventHandler(clickMouse), true);

            lblWidthTotal.Content = WindowsAPI.Desktop.GetWidth();
            lblHeightTotal.Content = WindowsAPI.Desktop.GetHeight();
        }

        public String RetornaValorXY()
        {
            Point getpos = PointToScreen(System.Windows.Input.Mouse.GetPosition(this));

            return getpos.ToString();
        }
        private void clickMouse(object sender, RoutedEventArgs e)
        {
           
                //MessageBox.Show("LoL");
                lblPosClick.Content = RetornaValorXY();
            
           
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {

            
        }

        private void btbCheckWindow_Click(object sender, RoutedEventArgs e)
        {
            if (WindowsAPI.Window.DoesExist(txtWindow.Text))
            {
                MessageBox.Show("Existe a janela");
            }
            else
            {
                MessageBox.Show("Não existe está janela");
            }
            

        }


        //public static Point RetonarnaValorCursorAtual()
        //{

        //    System.Drawing.Point point = Control.MousePosition;
        //    return Point(point.X, point.Y);
        //}

    }
}
