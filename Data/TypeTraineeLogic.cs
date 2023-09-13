using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.TextFormatting;

namespace TypeTrainee.Data
{
    internal class TypeTraineeLogic
    {
        
        string CurrentString { get; set; }
        List<string> TemplateWordsList { get; set; }
        int CurrentStringPosition { get; set; }

        string language;
        MediaPlayer Player { get; set; }
        int MistakeCounter { get; set; }
        int SpeedRate { get; set; }
        Stopwatch Stopwatch { get; set; }

        int CurrentLevel { get; set; }

        TextBlock CurrentLevelDisplay { get; set; }
        TextBlock AccuracyDisplay { get; set; }
        TextBlock WhishableAccuracy { get; set; }
        TextBlock SpeedRateDisplay { get; set; }
        TextBlock WhishableSpeed { get; set; }

        TextBlock NewLetters { get; set; }

        Label LeftTextField { get; set; }
        Label CenterTextField { get; set; }
        Label RightTextField { get; set; }

        Button BtnLanguage { get; set; }
        Button BtnCapitalize { get; set; }
        ButtonList BtList { get; set; }
        Boolean IsCapitalize { get; set; }
        bool IsCorrect = true;
        bool shiftFlag = false;

        Image Mute { get; set; }
        bool isMute = false;
        

        //HACK: Слишком громоздкий конструктор. Да и само решение передавать столько параметров в конструктор выглядит не очень хорошей идеей.
        /*
         * Вероятно проблема в неправильном разделении логики. Ошибка в архитектуре программы
         * Наверное это всё нужно делать как-то по другому. Оставляя на этом уровне только абстракцию.
         */
        public 
            TypeTraineeLogic(
            TextBlock CurrentLevelDisplay,
            TextBlock SpeedRateDisplay,
            TextBlock AccuracyDisplay,
            Label LeftTextField,
            Label CenterTextField,
            Label RightTextField,
            Button BtnLanguage,
            Button BtnCapitalize,
            ButtonList btList,
            TextBlock whishableAccuracy,
            TextBlock whishableSpeed,
            TextBlock newLetters,
            Image mute
            )
        {
            
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Player = new();
            Stopwatch = new();
            MistakeCounter = 0;
            SpeedRate = 0;
            CurrentStringPosition = 0;
            TemplateWordsList = new();
            this.CurrentLevelDisplay = CurrentLevelDisplay;
            this.SpeedRateDisplay = SpeedRateDisplay;
            this.AccuracyDisplay = AccuracyDisplay;
            this.LeftTextField = LeftTextField;
            this.CenterTextField = CenterTextField;
            this.RightTextField = RightTextField;

            
            SwitchList(@"DataFiles\Rus.txt");
            

            ResetToFirstLevel();

            this.BtnLanguage = BtnLanguage;
            this.BtnCapitalize = BtnCapitalize;
            language = "Рус";
            IsCapitalize = false;
            BtList = btList;
            
            BtList.FindButtonByContetn(CenterTextField.Content.ToString(), language).GreenColor();
            WhishableAccuracy = whishableAccuracy;
            WhishableSpeed = whishableSpeed;
            NewLetters = newLetters;
            Mute = mute;
            
        }



        public void IncreaceWhishableSpeed()
        {
            int tmp = int.Parse(WhishableSpeed.Text);
            if (tmp < 2000)
                tmp += 10;
            WhishableSpeed.Text = tmp.ToString();
        }

        public void DecreaceWhishableSpeed()
        {
            int tmp = int.Parse(WhishableSpeed.Text);
            if (tmp > 10)
                tmp -= 10;
            WhishableSpeed.Text = tmp.ToString();
        }

        public void SwitchList(string path)
        {
            TemplateWordsList.Clear();
            using (StreamReader fs = new StreamReader(path, Encoding.GetEncoding(1251)))
            {
                while (true)
                {
                    CurrentString = fs.ReadLine();
                    if (CurrentString == null)
                    {
                        CurrentString = TemplateWordsList[0];
                        break;
                    }
                    if (CurrentString != "")
                        TemplateWordsList.Add(CurrentString);
                }
                fs.Close();
            }
        }

        public void IncreaceAccuracy()
        {
            int tmp = int.Parse(WhishableAccuracy.Text);
            if (tmp < 100)
            {
                tmp++;
            }

            WhishableAccuracy.Text = tmp.ToString();

        }
        public void DecreaceAccuracy()
        {
            int tmp = int.Parse(WhishableAccuracy.Text);
            if (tmp > 1)
            {
                tmp--;
            }

            WhishableAccuracy.Text = tmp.ToString();
        }

        public void ResetToFirstLevel()
        {
            CurrentLevel = 1;
            CurrentString = TemplateWordsList[0];
            LeftTextField.Content = "";
            CenterTextField.Content = CurrentString[0];
            RightTextField.Content = CurrentString.Substring(1, CurrentString.Length - 1);
            SetCurrentString(1);
            CurrentLevelDisplay.Text = CurrentLevel.ToString();
        }
        public void SwitchLanguageToEnglish()
        {            
            SwitchList(@"DataFiles\Eng.txt");
            ResetToFirstLevel();
        }

        public void SwitchLanguageToRussian()
        {
            SwitchList(@"DataFiles\Rus.txt");
            ResetToFirstLevel();
        }
        public void LanguageBtnPressed()
        {
            BtList.FindButtonByContetn(CenterTextField.Content.ToString(), language).ResetColor();
            if (language == "Рус")
            {
                language = "Eng";
                if (IsCapitalize)
                {
                   
                    BtnCapitalize.Content = "Z";
                    BtList.CapitalEnglishLetters();
                    SwitchLanguageToEnglish();
                }
                else
                {
                    
                    BtnCapitalize.Content = "z";
                    BtList.EnglishLetters();
                    SwitchLanguageToEnglish();
                }
            }
            else
            {
                language = "Рус";
                if (IsCapitalize)
                {
                    BtnCapitalize.Content = "Ё";
                    BtList.CapitalRusLetters();
                    SwitchLanguageToRussian();
                }
                else
                {
                    BtnCapitalize.Content = "ё";
                    BtList.RusLetters();
                    SwitchLanguageToRussian();
                }
            }
            BtnLanguage.Content = language;
            BtList.FindButtonByContetn(CenterTextField.Content.ToString(), language).GreenColor();
            NewLetters.Text = TemplateWordsList[int.Parse(CurrentLevelDisplay.Text) - 1].Split()[1].ToUpper();
        }

        public void CapitalizeBtnPressed()
        {
            if (language == "Рус")
            {
                if (IsCapitalize)
                {
                    BtnCapitalize.Content = "ё";
                    BtList.RusLetters();
                }
                else
                {
                    BtnCapitalize.Content = "Ё";
                    BtList.CapitalRusLetters();
                }
            }
            else
            {
                if (IsCapitalize)
                {
                    BtnCapitalize.Content = "w";
                    BtList.EnglishLetters();
                }
                else
                {
                    BtnCapitalize.Content = "W";
                    BtList.CapitalEnglishLetters();
                }
            }
            IsCapitalize = !IsCapitalize;
        }


        public void ButtonDown(string btn)
        {



            if ((btn == "LeftShift") || (btn == "RightShift"))
            {
                if (shiftFlag == true)
                    return;
                shiftFlag = true;
                CapitalizeBtnPressed();
                return;
            }
            if (btn == "Capital")
            {
                CapitalizeBtnPressed();
                return;
            }


            BtnObject currentButton = BtList.FindButton(btn);
            
            if (currentButton.EngValue != "Empty")
            {
                PressButton(currentButton.Btn);
                if (!IsCorrect)
                {
                    currentButton.RedColor();
                    IsCorrect = true;
                }
                else
                    currentButton.ResetColor();
            }

        }

        public void ButtonUp(string btn)
        {
            if (btn == "LeftShift" || btn == "RightShift")
            {
                CapitalizeBtnPressed();
                shiftFlag = false;
                return;
            }

            BtnObject currentButton = BtList.FindButton(btn);
            currentButton.ResetColor();
            BtList.FindButtonByContetn(CenterTextField.Content.ToString(), language).GreenColor();
        }

        public void PressButton(Button btn)
        {
            if (!Stopwatch.IsRunning)
                Stopwatch.Start();

            string tmp = (btn).Content.ToString();
            char symbol;

            if (tmp == "Space" || tmp == "SPACE" || tmp == "Пробел" || tmp == "ПРОБЕЛ")
            {
                symbol = ' ';
            }
            else if (tmp.Length > 1)
            {
                symbol = '\0';
            }
            else
            {
                symbol = char.Parse(tmp);
            }


            if (Check_isSymbolIsCorrect(symbol))
            {
                if (!isMute)
                {
                    Player.Open(new Uri(@"DataFiles\myagkoe-spokoynoe-najatie-klavishi.mp3", UriKind.Relative));
                    Player.Play();
                }
                MoveToNextSymbol();
            }
            else
            {
                if (!isMute)
                {
                    Player.Open(new Uri(@"DataFiles\wrong_key.mp3", UriKind.Relative));
                    Player.Play();
                }
                MistakeCounter++;
                AccuracySetter();
                IsCorrect = false;
            }
            SpeedRate = 60 * CurrentStringPosition / (int)(Stopwatch.ElapsedMilliseconds / 1000 + 1);
            SpeedRateDisplay.Text = SpeedRate.ToString();
        }


        public void MinusLevel()
        {
            BtList.ResetAllButtons();
            BtList.FindButtonByContetn(CenterTextField.Content.ToString(), language).ResetColor();
            if (CurrentLevelDisplay.Text != "1")
            {
                CurrentLevelDisplay.Text = (int.Parse(CurrentLevelDisplay.Text) - 1).ToString();
                SetCurrentString(int.Parse(CurrentLevelDisplay.Text));
            }
            AccuracyDisplay.Text = "100%";
            Stopwatch.Stop();
            Stopwatch.Reset();
            BtList.FindButtonByContetn(CenterTextField.Content.ToString(), language).GreenColor();
            NewLetters.Text = TemplateWordsList[CurrentLevel - 1].Split()[1].ToUpper();
        }

        public void PlusLevel()
        {

            NextLevel(true);
        }

        public void NextLevel(bool isSkip = false)
        {
            BtList.ResetAllButtons();

            if ((int.Parse(WhishableAccuracy.Text) <= int.Parse(AccuracyDisplay.Text.Replace("%", "")) && int.Parse(WhishableSpeed.Text) <= int.Parse(SpeedRateDisplay.Text)) || isSkip)
            {
                if (CurrentLevelDisplay.Text != TemplateWordsList.Count.ToString())
                {
                    int level = int.Parse(CurrentLevelDisplay.Text);
                    CurrentLevelDisplay.Text = (++level).ToString();
                    SetCurrentString(level);
                    NewLetters.Text = TemplateWordsList[level - 1].Split()[1].ToUpper();
                    if (!isSkip)
                        MessageBox.Show("Уровень пройден");
                }
                else
                {
                    MessageBox.Show("Поздравляем. Вы прошли все уровни!");
                }
            }
            else
            {
                SetCurrentString(int.Parse(CurrentLevelDisplay.Text));
                MessageBox.Show("Попробуйте ещё раз. Слишком медленно или много ошибок.");
            }

            MistakeCounter = 0;
            SpeedRate = 0;
            Stopwatch.Stop();
            Stopwatch.Reset();
            BtList.FindButtonByContetn(CenterTextField.Content.ToString(), language).GreenColor();
        }

        private void SetCurrentString(int index)
        {
           
                RandomStringGenerator randomStringGenerator = new();
                CurrentString = randomStringGenerator.StringGenerator(TemplateWordsList[index - 1]);
           

            CurrentStringPosition = 0;
            LeftTextField.Content = "";
            CenterTextField.Content = CurrentString[0];
            RightTextField.Content = CurrentString.Substring(1, CurrentString.Length - 1);
        }

        private void AccuracySetter()
        {
            double result = 100 * (double)CurrentStringPosition / (CurrentStringPosition + MistakeCounter);
            AccuracyDisplay.Text = (int)result + "%";
        }

        private bool Check_isSymbolIsCorrect(char input)
        {
            if (input == CurrentString[CurrentStringPosition])
                return true;
            return false;
        }

        public void MoveToNextSymbol()
        {
            if (CurrentStringPosition < CurrentString.Length - 1)
            {
                CurrentStringPosition++;
                LeftTextField.Content = CurrentString.Substring(0, CurrentStringPosition);
                CenterTextField.Content = CurrentString[CurrentStringPosition];
                RightTextField.Content = CurrentString.Substring(CurrentStringPosition + 1);
                AccuracySetter();
                BtList.FindButtonByContetn(CenterTextField.Content.ToString(), language).GreenColor();

            }
            else
            {
                NextLevel();
            }
        }

        public void MuteClick()
        {

            if (isMute)
            {
                Mute.Source = BitmapFrame.Create(new Uri(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"DataFiles\muteOf.png")));
            }
            else
            {

                Mute.Source = BitmapFrame.Create(new Uri(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"DataFiles\muteOn.jpg")));
            }
            isMute = !isMute;
        }

    }
}
