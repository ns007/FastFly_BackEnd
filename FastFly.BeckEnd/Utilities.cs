﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FastFly.BeckEnd
{
    public class Utilities
    {
        public static string HeadOfDepartmentMail = @"ממתין עבורך טופס בקשה ליציאה לכנס מאחד מן המרצים במחלקה שלך,
אנא כנס למערכת FastFly על מנת לצפות בנתונים";

        public static string signersMail = @"ממתין עבורך טופס בקשה ליציאה לכנס מאחד מן המרצים בשנקר,
אנא כנס למערכת FastFly על מנת לצפות בנתונים";
        public static DateTime stringToDateTime(string date)
        {
            DateTime tempDateTime = new DateTime(Convert.ToInt32(date.Substring(0, 4)), Convert.ToInt32(date.Substring(4, 2)), Convert.ToInt32(date.Substring(6, 2)));
            return tempDateTime;
        }

        public static TimeSpan stringToTimeSpan(string fromHour)
        {
            TimeSpan temp = new TimeSpan(Convert.ToInt32(fromHour.Substring(0, 2)), Convert.ToInt32(fromHour.Substring(2, 2)), 0);
            return temp;
        }
    }
}