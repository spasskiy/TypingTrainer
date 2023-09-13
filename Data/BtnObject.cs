using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace TypeTrainee.Data
{
    public class BtnObject
    {
        public Button Btn { get; set; }
        public string EngValue { get; set; }
        public string CapitalEngValue { get; set; }
        public string RusValue { get; set; }
        public string CapitalRusValue { get; set; }
        public SolidColorBrush OriginalButtonBrush { get; set; }        
        public string CurrentLetter { get; set; }

        public string Tag { get; set; }

        public BtnObject(Button btn, string engValue, string capitalEngVelue, string rusValue, string capitalRusValue)
        {
            Btn = btn;
            EngValue = engValue;
            CapitalEngValue = capitalEngVelue;
            RusValue = rusValue;
            CapitalRusValue = capitalRusValue;
            OriginalButtonBrush = (btn.Background as SolidColorBrush);            
            Tag = btn.Tag.ToString();
        }

        public void RedColor()
        {
            Btn.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("Red");
        }

        public void GreenColor()
        {
            Btn.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("Green");
        }

        public void ResetColor()
        {
            Btn.Background = OriginalButtonBrush;
        }
    }
}
