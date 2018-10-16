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
        public IntPtr WindowMain;

        public MainWindow()
        {
            InitializeComponent();

            lblWidthTotal.Content = WindowsAPI.Desktop.GetWidth();
            lblHeightTotal.Content = WindowsAPI.Desktop.GetHeight();
        }


        public Point RetornaValorXY()
        {
            Point getpos = PointToScreen(System.Windows.Input.Mouse.GetPosition(this));

            return getpos;
        }

        private void btnRec_Click(object sender, RoutedEventArgs e)
        {
          WindowBotMain.Opacity = 0.4;
          AddHandler(FrameworkElement.MouseDownEvent, new MouseButtonEventHandler(InsertListRec), true);
        }



        private void InsertListRec(object sender, RoutedEventArgs e)
        {
            lstRec.Items.Add(RetornaValorXY());
            //RetornaValorXY();
        }

        private void btbCheckWindow_Click(object sender, RoutedEventArgs e)
        {
            if (WindowsAPI.Window.DoesExist(txtWindow.Text))
            {
                WindowMain = WindowsAPI.Window.Get(txtWindow.Text);
                MessageBox.Show("Existe a janela: " + WindowsAPI.Window.GetTitle(WindowMain));
                lblTitleWindow.Content = WindowsAPI.Window.GetTitle(WindowMain);
                WindowsAPI.Window.SetTitle(WindowMain, "MARCELO's BOT - INJECT SUCCESS");

            }
            else
            {
                MessageBox.Show("Está janela não existe: "+ txtWindow.Text);
            }
            

        }


        //public static Point RetonarnaValorCursorAtual()
        //{

        //    System.Drawing.Point point = Control.MousePosition;
        //    return Point(point.X, point.Y);
        //}

    }
}
