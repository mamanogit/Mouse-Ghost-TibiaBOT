using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WindowsAPI;
using NHotkey.Wpf;


using System.Windows.Input;
using System.Threading;
using NHotkey;

namespace TibiaBotMarcelo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        public IntPtr WindowMain;
        public List<System.Drawing.Point> ListOfPoints = new List<System.Drawing.Point>();

        public MainWindow()
        {
            InitializeComponent();

            //register hotkeys global
            HotkeyManager.Current.AddOrReplace("Increment", Key.F2, ModifierKeys.Alt, StopLoop);

            lblWidthTotal.Content = WindowsAPI.Desktop.GetWidth();
            lblHeightTotal.Content = WindowsAPI.Desktop.GetHeight();
        }


        public System.Drawing.Point RetornaValorXY()
        {
            System.Windows.Point getpos = PointToScreen(System.Windows.Input.Mouse.GetPosition(this));
            System.Drawing.Point getDrawPoint = new System.Drawing.Point(Convert.ToInt32(getpos.X), Convert.ToInt32(getpos.Y));
            return getDrawPoint;
        }

        private void btnRec_Click(object sender, RoutedEventArgs e)
        {
            Recording();
        }

        private void Recording()
        {
            if (!btnRec.Content.ToString().Equals("Recording..."))
            {
                WindowBotMain.Opacity = 0.4;
                btnRec.Content = "Recording...";
                btnPush.IsEnabled = false;
                AddHandler(FrameworkElement.MouseDownEvent, new MouseButtonEventHandler(InsertListRec), true);
            }
            else
            {
                WindowBotMain.Opacity = 1;
                btnRec.Content = "REC";
                btnPush.IsEnabled = true;
                RemoveHandler(FrameworkElement.MouseDownEvent, new MouseButtonEventHandler(InsertListRec));
            }
        }
        

        private void ReturnStateInit()
        {
            ListOfPoints.Clear();
            lstRec.Items.Clear();
            foreach (var item in ListOfPoints)
            {
                lstRec.Items.Remove(item);
            }
            ;
        }

        private void UnsertListRec(object sender, RoutedEventArgs e)
        {
            //subscribe handler mouse with nothing
        }

        private void InsertListRec(object sender, RoutedEventArgs e)
        {
            var point = RetornaValorXY();
            ListOfPoints.Add(point);
            lstRec.Items.Clear();
            foreach (var item in ListOfPoints)
            {
                lstRec.Items.Add(item);
            }
            ;
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

        private void btnPush_Click(object sender, RoutedEventArgs e)
        {
            StartPushing();
        }

        private void StartPushing()
        {
            WindowsAPI.Window.SetFocused(WindowMain);
            
            btnPush.IsEnabled = false;
            AddHandler(FrameworkElement.MouseDownEvent, new MouseButtonEventHandler(UnsertListRec), true);
            if (lstRec.Items.Count > 0)
            {
                for (int i = 0; i < ListOfPoints.Count; i++)
                {
                    if (i +1 == ListOfPoints.Count)
                    {
                        if (chkPushLoop.IsChecked == true)
                        {
                            i = 0;
                        }
                        else { break; }
                       
                    }
                    try
                    {
                        Task.WaitAll(MyLeftDrag(ListOfPoints[i], ListOfPoints[i + 1]));
                       //WindowsAPI.Mouse.LeftDrag(WindowMain, ListOfPoints[i], ListOfPoints[i + 1], 50, 500);
                        
                    }
                    catch (Exception)
                    {
                        //FODA-SE
                    }
                    
                }
               
            }
            btnPush.IsEnabled = true;
        }

        private async Task MyLeftDrag(System.Drawing.Point p1, System.Drawing.Point p2)
        {
            if (p1 == p2) return;
            //WindowsAPI.Mouse.Move(p1.X, p1.Y);

            WindowsAPI.Mouse.LeftDown(p1.X, p1.Y);
            Thread.Sleep(200);
            //WindowsAPI.Mouse.Move(p2.X, p2.Y);
            WindowsAPI.Mouse.LeftUp(p2.X, p2.Y);
        }

        private void Stop(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.F5)
            {
                System.Windows.Application.Current.Shutdown();
            }
            else if (e.Key == Key.F1)
            {
                StartPushing();
            }
            else if (e.Key == Key.F2)
            {
                Recording();
            }
        }

        private void StopLoop(object sender, HotkeyEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void btnClearRecord_Click(object sender, RoutedEventArgs e)
        {
            ReturnStateInit();
        }


        //public static Point RetonarnaValorCursorAtual()
        //{

        //    System.Drawing.Point point = Control.MousePosition;
        //    return Point(point.X, point.Y);
        //}

    }
}
