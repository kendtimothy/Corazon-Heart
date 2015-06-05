using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace CorazonHeart
{
    /// <summary>
    /// Provides date formatting services.
    /// </summary>
    public class CDateFormat
    {
        public enum Format
        {
            DateTime,
            ShortDateTime,
            Date,
            ShortDate,
            Time24,
            Time,
            ShortTime,
            Smart
        }

        private const string DateTimeFormat = "dd MMMM yyyy hh:mm tt";
        private const string ShortDateTimeFormat = "dd-MMM-yy hh:mm tt";
        private const string _DateFormat = "dd MMMM, yyyy";
        private const string ShortDateFormat = "dd-MMM-yy";
        private const string TimeFormat24 = "HH:mm";
        private const string TimeFormat = "hh:mm tt";
        private const string ShortTimeFormat = "h:mm tt";

        public int GetAge(DateTime bday)
        {
            DateTime today = DateTime.Today;
            int age = today.Year - bday.Year;
            if (bday > today.AddYears(-age)) age--;

            return age;
        }

        public string GetDisplayedAge(DateTime bday)
        {
            int age = GetAge(bday);
            return age > 1 ? age + " years old" : age + " year old";
        }

        public string Convert(DateTime input, Format format)
        {
            string result = null;
            try
            {
                switch (format)
                {
                    case Format.DateTime:
                        result = input.ToString(DateTimeFormat);
                        break;
                    case Format.ShortDateTime:
                        result = input.ToString(ShortDateTimeFormat);
                        break;
                    case Format.Date:
                        result = input.ToString(_DateFormat);
                        break;
                    case Format.ShortDate:
                        result = input.ToString(ShortDateFormat);
                        break;
                    case Format.Time24:
                        result = input.ToString(TimeFormat24);
                        break;
                    case Format.Time:
                        result = input.ToString(TimeFormat);
                        break;
                    case Format.ShortTime:
                        result = input.ToString(ShortTimeFormat);
                        break;
                    case Format.Smart:
                        // let's do a bit calculation
                        break;
                }
            }
            catch (Exception)
            {
                result = null;
            }

            return result;
        }

        public string ConvertLocal(DateTime input, Format format)
        {
            //Portgaz p = Portgaz.Current;

            // let's convert the input datetime to local time
            long utcDifference = 0;
            //if (p.IsLoggedIn)
            //{
            //    utcDifference = p.User.UtcDifference;
            //}
            //else
            //{
            //    //edit, get from ip address
            //}
            // apply the computed datetime
            input = utcDifference >= 0 ? input + TimeSpan.FromTicks(utcDifference) : input - TimeSpan.FromTicks(utcDifference);

            string result = "";
            switch (format)
            {
                case Format.DateTime:
                    result = input.ToString(DateTimeFormat);
                    break;
                case Format.ShortDateTime:
                    result = input.ToString(ShortDateTimeFormat);
                    break;
                case Format.Date:
                    result = input.ToString(_DateFormat);
                    break;
                case Format.ShortDate:
                    result = input.ToString(ShortDateFormat);
                    break;
                case Format.Time:
                    result = input.ToString(TimeFormat);
                    break;
                case Format.Smart:
                    // let's do a bit calculation
                    break;
            }

            return result;
        }

        public DateTime? Convert(string input, Format format)
        {
            DateTime? result = new DateTime();

            try
            {
                switch (format)
                {
                    case Format.DateTime:
                        result = DateTime.ParseExact(input, DateTimeFormat, CultureInfo.InvariantCulture);
                        break;
                    case Format.ShortDateTime:
                        result = DateTime.ParseExact(input, ShortDateTimeFormat, CultureInfo.InvariantCulture);
                        break;
                    case Format.Date:
                        result = DateTime.ParseExact(input, _DateFormat, CultureInfo.InvariantCulture);
                        break;
                    case Format.ShortDate:
                        result = DateTime.ParseExact(input, ShortDateFormat, CultureInfo.InvariantCulture);
                        break;
                    case Format.Time24:
                        result = DateTime.ParseExact(input, TimeFormat24, CultureInfo.InvariantCulture);
                        break;
                    case Format.Time:
                        result = DateTime.ParseExact(input, TimeFormat, CultureInfo.InvariantCulture);
                        break;
                    case Format.ShortTime:
                        result = DateTime.ParseExact(input, ShortTimeFormat, CultureInfo.InvariantCulture);
                        break;
                    case Format.Smart:
                        // let's do a bit calculation
                        break;
                }
            }
            catch (Exception)
            {
                result = null;
            }

            return result;
        }

        /// <summary>
        /// Check if two date ranges clash.
        /// </summary>
        /// <param name="startDate1"></param>
        /// <param name="startDate2"></param>
        /// <param name="endDate1"></param>
        /// <param name="endDate2"></param>
        /// <param name="inclusiveStart"></param>
        /// <param name="inclusiveEnd"></param>
        /// <returns>Returns true if clash is found, false otherwise.</returns>
        public bool IsDateRangeClash(DateTime startDate1, DateTime startDate2, DateTime endDate1, DateTime endDate2, bool inclusiveStart = false, bool inclusiveEnd = false)
        {
            // init isSuccess
            bool isClash = false;

            if (inclusiveStart)
            {
                if (inclusiveEnd)
                {
                    // ALL INCLUSIVE
                    if ((startDate1 <= endDate2) && (startDate2 <= endDate1))
                        isClash = true;
                }
                else
                {
                    // ONLY INCLUSIVE START
                    if ((startDate1 <= endDate2) && (startDate2 < endDate1))
                        isClash = true;
                }
            }
            else
            {
                if (inclusiveEnd)
                {
                    // ONLY INCLUSIVE END
                    if ((startDate1 < endDate2) && (startDate2 <= endDate1))
                        isClash = true;
                }
                else
                {
                    // EXCLUSIVE (default case with inclusiveStart = inclusiveEnd = false)
                    if ((startDate1 < endDate2) && (startDate2 < endDate1))
                        isClash = true;
                }
            }

            return isClash;
        }

        public DateTime ConvertUtc(string input, Format format)
        {
            DateTime result = new DateTime();
            switch (format)
            {
                case Format.DateTime:
                    result = DateTime.ParseExact(input, DateTimeFormat, CultureInfo.InvariantCulture);
                    break;
                case Format.ShortDateTime:
                    result = DateTime.ParseExact(input, ShortDateTimeFormat, CultureInfo.InvariantCulture);
                    break;
                case Format.Date:
                    result = DateTime.ParseExact(input, _DateFormat, CultureInfo.InvariantCulture);
                    break;
                case Format.ShortDate:
                    result = DateTime.ParseExact(input, ShortDateFormat, CultureInfo.InvariantCulture);
                    break;
                case Format.Time:
                    result = DateTime.ParseExact(input, TimeFormat, CultureInfo.InvariantCulture);
                    break;
                case Format.ShortTime:
                    result = DateTime.ParseExact(input, ShortTimeFormat, CultureInfo.InvariantCulture);
                    break;
                case Format.Smart:
                    // let's do a bit calculation
                    break;
            }

            //Portgaz p = Portgaz.Current;

            // let's convert the input datetime to local time
            long utcDifference = 0;
            //if (p.IsLoggedIn)
            //{
            //    utcDifference = p.User.UtcDifference;
            //}
            //else
            //{
            //    //edit, get from ip address
            //}
            // apply the computed datetime (reverse process)
            result = utcDifference < 0 ? result + TimeSpan.FromTicks(utcDifference) : result - TimeSpan.FromTicks(utcDifference);

            return result;
        }

        public DateTime? ConvertPaypalDate(string input)
        {
            Nullable<DateTime> result = null;       // init
            try
            {
                // remove " PST" from the input
                input = input.Replace(" PST", "");

                // parse the string and we'll get the PST Time
                result = DateTime.ParseExact(input, "ss:mm:HH MMM dd, yyyy", CultureInfo.InvariantCulture);

                // convert PST to UTC
                result = ConvertPstToUtc(result.Value);
            }
            catch (Exception)
            {
                result = null;
            }

            return result;
        }

        public DateTime ConvertPstToUtc(DateTime input)
        {
            // Pacific Time (PST) time is 8 hours behind UTC
            return input - TimeSpan.FromHours(8);
        }
    }
}