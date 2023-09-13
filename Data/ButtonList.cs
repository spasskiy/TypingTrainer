using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace TypeTrainee.Data
{
    public class ButtonList
    {
        List<BtnObject> buttons;
        
        public BtnObject FindButton(string Tag)
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                if (buttons[i].Tag == Tag)
                    return buttons[i];
            }
            
            return buttons[61];
        }

        public void ResetAllButtons()
        {
            for (int i = 0; i < buttons.Count; i++)
                buttons[i].ResetColor();
        }

        public BtnObject FindButtonByContetn(string content, string language)
        {
            if (content == " ")
            {
                return buttons[48];
            }


            if (language == "Рус")
            {
                if (content == ".")
                    return buttons[47];
                if (content == ",")
                    return buttons[47];
            }

            if (language == "Eng")
            {
                if (content == ".")                
                    return buttons[46];                
                if (content == "\"")
                    return buttons[37];
                if (content == ":")
                    return buttons[36];
                if (content == "?")
                    return buttons[47];
            }            

            for (int i = 0; i < buttons.Count; i++)
            {
                if (buttons[i].RusValue == content || buttons[i].CapitalRusValue == content || buttons[i].EngValue == content || buttons[i].CapitalEngValue == content)
                    return buttons[i];
            }
            return buttons[0];
        }
        public ButtonList()
        {
            buttons = new List<BtnObject>();
        }
        public void AddButton(Button btn, string engValue, string capitalEngVelue, string rusValue, string capitalRusValue)
        {
            BtnObject tmp = new(btn, engValue, capitalEngVelue, rusValue, capitalRusValue);
            buttons.Add(tmp);
        }

        public void EnglishLetters()
        {

            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].Btn.Content = buttons[i].EngValue;
                buttons[i].CurrentLetter = buttons[i].EngValue;
            }
        }

        public void RusLetters()
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].Btn.Content = buttons[i].RusValue;
                buttons[i].CurrentLetter = buttons[i].RusValue;
            }
        }
        public void CapitalEnglishLetters()
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].Btn.Content = buttons[i].CapitalEngValue;
                buttons[i].CurrentLetter = buttons[i].CapitalEngValue;
            }
        }

        public void CapitalRusLetters()
        {
            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].Btn.Content = buttons[i].CapitalRusValue;
                buttons[i].CurrentLetter = buttons[i].CapitalRusValue;
            }
        }
    }
}
