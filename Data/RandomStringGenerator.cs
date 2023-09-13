using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeTrainee.Data
{
    public class RandomStringGenerator
    {
        public string StringGenerator(string inputString)
        {
            string[] subs = inputString.Split(' ');
            string result = "";
            if (subs.Length > 2)
                throw new Exception("Ошибка формата строки шаблона.");            
            else
            {
                for (int i = 0; i < 12; i++)
                {
                    result += WordGenerator(subs[0], subs[1]) + " ";
                }
                return result.Trim();
            }
            return "";
        }
        string WordGenerator(string main, string symbol)
        {

            string result = "";
            Random rnd = new Random();

            for (int i = 0; i < 3; i++)
            {
                if (rnd.Next() % 2 == 0)
                    result += main[rnd.Next() % main.Length];
                result += symbol[rnd.Next() % symbol.Length];
                if (rnd.Next() % 2 == 0)
                    result += main[rnd.Next() % main.Length];
            }
            return result;
        }
    }
}
