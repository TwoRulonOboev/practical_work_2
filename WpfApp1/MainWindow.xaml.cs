using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            await Task.Run(() => AppendText());
        }

        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            await Task.Run(() => ShowCharacterCount());
        }



        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            string url = uri.Text;
            try
            {
                await Task.Run(() => LoadPage(url));
            }
            catch (Exception ex)
            {
                text.Dispatcher.Invoke(() => text.Text = "Произошла ошибка: " + ex.Message);
            }
        }

        private void LoadPage(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = client.GetAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                    string responseBody = response.Content.ReadAsStringAsync().Result;
                    text.Dispatcher.Invoke(() => text.Text = responseBody);
                }
                else
                {
                    text.Dispatcher.Invoke(() => text.Text = "Ошибка при загрузке страницы");
                }
            }
        }

        private void AppendText()
        {
            text.Dispatcher.Invoke(() => text.SelectedText += "<<>>");
        }

        private void ShowCharacterCount()
        {
            string selectedText2 = text.Dispatcher.Invoke(() => text.SelectedText);
            int length2 = selectedText2.Length;
            string selectedText = text.Dispatcher.Invoke(() => text.SelectedText);
            int length = selectedText.Replace(" ", "").Length;
            string message = " Количество символов (без пробелов): " + length.ToString() + "\n Количество символов (с пробелами): " + length2.ToString();
            MessageBox.Show(message);
        }
    }
}