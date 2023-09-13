using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using TypeTrainee.Data;
using static System.Net.Mime.MediaTypeNames;



namespace TypeTrainee
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        TypeTraineeLogic typeTrainee;
        ButtonList buttonList;

        public MainWindow()
        {
            InitializeComponent();
            typeTrainee = new(
                selectedLevel, 
                speedRateDisplay, 
                accuracyDisplay, 
                leftTextField, 
                centerTextField, 
                rightTextField, 
                btnLanguage, 
                btnCapitalize, 
                InitButtonList(),
                wishibleAccuracy,
                whishableSpeedRate,
                newLetters,
                imgMute
                );
            
        }


        public ButtonList InitButtonList()
        {
            ButtonList buttonList = new ButtonList();
            buttonList.AddButton(btnOem3, "`", "~", "ё", "Ё");
            buttonList.AddButton(btnD1, "1", "!", "1", "!");
            buttonList.AddButton(btnD2, "2", "@", "2", "\"");
            buttonList.AddButton(btnD3, "3", "#", "3", "№");
            buttonList.AddButton(btnD4, "4", "$", "4", ";");
            buttonList.AddButton(btnD5, "5", "%", "5", "%");
            buttonList.AddButton(btnD6, "6", "^", "6", ":");
            buttonList.AddButton(btnD7, "7", "&", "7", "?");
            buttonList.AddButton(btnD8, "8", "*", "8", "*");
            buttonList.AddButton(btnD9, "9", "(", "9", "(");
            buttonList.AddButton(btnD0, "0", ")", "0", ")");
            buttonList.AddButton(btnOemMinus, "-", "_", "-", "_");
            buttonList.AddButton(btnOemPlus, "=", "+", "=", "+");
            buttonList.AddButton(btnOem5, "\\", "|", "\\", "/");
            buttonList.AddButton(btnQ, "q", "Q", "й", "Й");
            buttonList.AddButton(btnW, "w", "W", "ц", "Ц");
            buttonList.AddButton(btnE, "e", "E", "у", "У");
            buttonList.AddButton(btnR, "r", "R", "к", "К");
            buttonList.AddButton(btnT, "t", "T", "е", "Е");
            buttonList.AddButton(btnY, "y", "Y", "н", "Н");
            buttonList.AddButton(btnU, "u", "U", "г", "Г");
            buttonList.AddButton(btnI, "i", "I", "ш", "Ш");
            buttonList.AddButton(btnO, "o", "O", "щ", "Щ");
            buttonList.AddButton(btnP, "p", "P", "з", "З");
            buttonList.AddButton(btnOemOpenBrackets, "[", "{", "х", "Х");
            buttonList.AddButton(btnOem6, "]", "}", "ъ", "Ъ");
            buttonList.AddButton(btnCapital, "Caps Lock", "CAPS LOCK", "Caps Lock", "CAPS LOCK");
            buttonList.AddButton(btnA, "a", "A", "ф", "Ф");
            buttonList.AddButton(btnS, "s", "S", "ы", "Ы");
            buttonList.AddButton(btnD, "d", "D", "в", "В");
            buttonList.AddButton(btnF, "f", "F", "а", "А");
            buttonList.AddButton(btnG, "g", "G", "п", "П");
            buttonList.AddButton(btnH, "h", "H", "р", "Р");
            buttonList.AddButton(btnJ, "j", "J", "о", "О");
            buttonList.AddButton(btnK, "k", "K", "л", "Л");
            buttonList.AddButton(btnL, "l", "L", "д", "Д");
            buttonList.AddButton(btnOem1, ";", ":", "ж", "Ж");
            buttonList.AddButton(btnOemQuotes, "'", "\"", "э", "Э");
            buttonList.AddButton(btnZ, "z", "Z", "я", "Я");
            buttonList.AddButton(btnX, "x", "X", "ч", "Ч");
            buttonList.AddButton(btnC, "c", "C", "с", "С");
            buttonList.AddButton(btnV, "v", "V", "м", "М");
            buttonList.AddButton(btnB, "b", "B", "и", "И");
            buttonList.AddButton(btnN, "n", "N", "т", "Т");
            buttonList.AddButton(btnM, "m", "M", "ь", "Ь");
            buttonList.AddButton(btnOemComma, ",", "<", "б", "Б");
            buttonList.AddButton(btnOemPeriod, ".", ">", "ю", "Ю");
            buttonList.AddButton(btnOemQuestion, "/", "?", ".", ",");
            buttonList.AddButton(btnSpace, "Space", "SPACE", "Пробел", "ПРОБЕЛ");
            buttonList.AddButton(btnLeftShift, "Shift", "SHIFT", "Шифт", "Шифт");
            buttonList.AddButton(btnRightShift, "Shift", "SHIFT", "Шифт", "Шифт");          
            buttonList.AddButton(btnLeftCtrl, "Ctrl", "Ctrl", "Ctrl", "Ctrl");          
            buttonList.AddButton(btnRightCtrl, "Ctrl", "Ctrl", "Ctrl", "Ctrl");          
            buttonList.AddButton(btnSystem, "Alt", "Alt", "Alt", "Alt");          
            buttonList.AddButton(btnSystemR, "Alt", "Alt", "Alt", "Alt");          
            buttonList.AddButton(btnLWin, "Win", "Win", "Win", "Win");          
            buttonList.AddButton(btnRWin, "Win", "Win", "Win", "Win");          
            buttonList.AddButton(btnApps, "Fn", "Fn", "Fn", "Fn");          
            buttonList.AddButton(btnBack, "<=", "<=", "<=", "<=");         
            buttonList.AddButton(btnReturn, "Enter", "ENTER", "Enter", "ENTER");         
            buttonList.AddButton(btnTab, "Tab", "TAB", "Tab", "TAB");
            buttonList.AddButton(btnOemEmpty, "Empty", "Empty", "Empty", "Empty");

            
            return buttonList;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_ExitClick(object sender, RoutedEventArgs e)
        {
            Close();
        }        
        
        private void Plus_Button_Click(object sender, RoutedEventArgs e)
        {
            typeTrainee.PlusLevel();
        }

        private void Minus_Button_Click(object sender, RoutedEventArgs e)
        {
            typeTrainee.MinusLevel();
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            typeTrainee.ButtonDown(e.Key.ToString());
        }

        private void Window_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            typeTrainee.ButtonUp(e.Key.ToString());
        }

        private void Letter_Button_Pressed(object sender, RoutedEventArgs e)
        {
            typeTrainee.PressButton(sender as Button);
        }

        private void btnLanguage_Click(object sender, RoutedEventArgs e)
        {
            typeTrainee.LanguageBtnPressed();
        }

        private void btnCapitalize_Click(object sender, RoutedEventArgs e)
        {
            typeTrainee.CapitalizeBtnPressed();
        }

        private void accuracyDecreace_Click(object sender, RoutedEventArgs e)
        {
            typeTrainee.DecreaceAccuracy();
        }

        private void accuracyIncreace_Click(object sender, RoutedEventArgs e)
        {
            typeTrainee.IncreaceAccuracy();
        }

        private void decreaceSpeed_Click(object sender, RoutedEventArgs e)
        {
            typeTrainee.DecreaceWhishableSpeed();
        }

        private void increaceSpeed_Click(object sender, RoutedEventArgs e)
        {
            typeTrainee.IncreaceWhishableSpeed();
        }

        private void MuteButtonClick(object sender, RoutedEventArgs e)
        {
            typeTrainee.MuteClick();
        }

 
    }
}
