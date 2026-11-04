using UnityEngine;

namespace SERES.Utils
{
    public class DateConversion : MonoBehaviour
    {
        private static int _minute = 60;
        private static int _hour = 3600;
        private static int _day = 86400;
        private static int _january = 2678400;
        private static int _february = 2419200;
        private static int _march = 2678400;
        private static int _april = 2592000;
        private static int _may = 2678400;
        private static int _june = 2592000;
        private static int _july = 2678400;
        private static int _august = 2678400;
        private static int _september = 2592000;
        private static int _october = 2678400;
        private static int _november = 2592000;
        private static int _december = 2678400;
        private static int _year = 31536000;
        public static string FloatToDate(float timer)
        {
            int seconds = (int)timer;

            // Calculate the year
            int year = seconds / _year;
            seconds = seconds - (year * _year);
            
            int month = SecondsToMonth(seconds);
            seconds = seconds - MonthToAmountOfSeconds(month);

            int day = seconds / _day;
            seconds = seconds - (day * _day);

            int hour = seconds / _hour;
            seconds = seconds - (hour * _hour);

            int minute = seconds / _minute;
            seconds = seconds - (minute * _minute);

            return IntToDateFormat(year, 4) + "/" + IntToDateFormat(month, 2) + "/" + IntToDateFormat(day + 1, 2) + " " + IntToDateFormat(hour, 2) + ":" + IntToDateFormat(minute, 2) + ":" + IntToDateFormat(seconds, 2);
        }

        public static string IntToDateFormat(int number, int amount)
        {
            string numberString = number.ToString();
            if (numberString.Length < amount)
            {
                int zerosToAdd = amount - numberString.Length;
                for (int i = 0; i < zerosToAdd; i++)
                {
                    numberString = "0" + numberString;
                }
                return numberString;
            }
            else if (numberString.Length > amount)
            {
                return numberString.Substring(numberString.Length - amount);
            }
            return number.ToString();
        }

        public static int SecondsToMonth(int seconds)
        {
            if (seconds < _january)
                return 1;
            else if (seconds < _january + _february)
                return 2;
            else if (seconds < _january + _february + _march)
                return 3;
            else if (seconds < _january + _february + _march + _april)
                return 4;
            else if (seconds < _january + _february + _march + _april + _may)
                return 5;
            else if (seconds < _january + _february + _march + _april + _may + _june)
                return 6;
            else if (seconds < _january + _february + _march + _april + _may + _june + _july)
                return 7;
            else if (seconds < _january + _february + _march + _april + _may + _june + _july + _august)
                return 8;
            else if (seconds < _january + _february + _march + _april + _may + _june + _july + _august + _september)
                return 9;
            else if (seconds < _january + _february + _march + _april + _may + _june + _july + _august + _september + _october)
                return 10;
            else if (seconds < _january + _february + _march + _april + _may + _june + _july + _august + _september + _october + _november)
                return 11;
            return 12;
        }

        public static int MonthToAmountOfSeconds(int month)
        {
            switch (month)
            {
                case 1:
                    return 0;
                case 2:
                    return _january;
                case 3:
                    return _january + _february;
                case 4:
                    return _january + _february + _march;
                case 5:
                    return _january + _february + _march + _april;
                case 6:
                    return _january + _february + _march + _april + _may;
                case 7:
                    return _january + _february + _march + _april + _may + _june;
                case 8:
                    return _january + _february + _march + _april + _may + _june + _july;
                case 9:
                    return _january + _february + _march + _april + _may + _june + _july + _august;
                case 10:
                    return _january + _february + _march + _april + _may + _june + _july + _august + _september;
                case 11:
                    return _january + _february + _march + _april + _may + _june + _july + _august + _september + _october;
                case 12:
                    return _january + _february + _march + _april + _may + _june + _july + _august + _september + _october + _november;
            }
            return 0;
        }
    }
}