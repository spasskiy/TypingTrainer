using System;
using System.Collections.Generic;
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
using static System.Net.Mime.MediaTypeNames;

namespace TypeTrainee
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string currentString;
        List<string> templateWordsList;
        int currentStringPosition;

        async void initCurrentString()
        {
            templateWordsList = new();
            using (StreamReader fs = new StreamReader(@"TextUnits\chunks.txt", Encoding.GetEncoding(1251)))
            {
                while (true)
                {                    
                    currentString = fs.ReadLine();
                    if (currentString == null) 
                    {
                        currentString = templateWordsList[0];
                        break;
                    }
                    templateWordsList.Add(currentString);
                }                
            }
        }
        public MainWindow()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            InitializeComponent();

            initCurrentString();

            currentStringPosition = 0;

            leftTextField.Content = "";
            centerTextField.Content = currentString[0];
            rightTextField.Content = currentString.Substring(1, currentString.Length - 1);

        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_ExitClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Set_Current_Tmplate_String(int index)
        {
            currentString = templateWordsList[index - 1];
            currentStringPosition = 0;
            leftTextField.Content = "";
            centerTextField.Content = currentString[0];
            rightTextField.Content = currentString.Substring(1, currentString.Length - 1);
        }
        private void Plus_Button_Click(object sender, RoutedEventArgs e)
        {
            if(selectedLevel.Text != templateWordsList.Count.ToString()) 
            { 
                selectedLevel.Text = (int.Parse(selectedLevel.Text) + 1).ToString();
                Set_Current_Tmplate_String(int.Parse(selectedLevel.Text));
            }
        }

        private void Minus_Button_Click(object sender, RoutedEventArgs e)
        {
            if (selectedLevel.Text != "1")
            { 
                selectedLevel.Text = (int.Parse(selectedLevel.Text) - 1).ToString();
                Set_Current_Tmplate_String(int.Parse(selectedLevel.Text));
            }
        }
    }
}
