using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
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

namespace 生成验证码
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
          
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ShengCheng();
            txtYz.Focus();  
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {             
            if (txtYz.Text == cap.verifyCodeText)
            {
                MessageBox.Show("验证通过");
                DeleteItself();
                this.Close();
            }
            else
            {
                MessageBox.Show("验证失败");
                txtYz.Clear();
                ShengCheng();       
            }
        }
        Captcha cap;
        private void ShengCheng()
        {           
            cap = new Captcha();
            cap.AddLowerLetter = true;
            cap.AddUpperLetter = true;
            cap.BackgroundColor = System.Drawing.Color.Aquamarine;
            Bitmap b = cap.Output();

            MemoryStream ms = new MemoryStream();
            b.Save(ms, ImageFormat.Bmp);
            BitmapImage bit = new BitmapImage();
            bit.BeginInit();
            bit.StreamSource = ms;
            bit.EndInit();
            img.Source = bit;   
        }
        private static void DeleteItself()
        {
            string batDelFile = System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + "\\DeleteItself.bat";
            using (StreamWriter vStreamWriter = new StreamWriter(batDelFile, false, Encoding.Default))
            {
                vStreamWriter.Write(string.Format(
                    ":del\r\n" +
                    " del \"{0}\"\r\n" +
                    "if exist \"{0}\" goto del\r\n" +
                    "del %0\r\n", System.Windows.Forms.Application.ExecutablePath));
            }

            //************ 执行批处理
            ProcessStartInfo psi = new ProcessStartInfo();
            psi.FileName = batDelFile;
            psi.CreateNoWindow = true;
            psi.WindowStyle = ProcessWindowStyle.Hidden;
            Process.Start(psi);
            //************ 结束退出
        }

    }
}
