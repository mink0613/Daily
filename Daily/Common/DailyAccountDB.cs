﻿using Daily.Model;

namespace Daily.Common
{
    class DailyAccountDB : HttpWebRequest
    {
        private readonly string postUrl = "http://modoocoupon.woobi.co.kr/gnuboard4/daily/php/PostAccount.php";

        private readonly string deleteUrl = "http://modoocoupon.woobi.co.kr/gnuboard4/daily/php/DeleteAccount.php";

        private readonly string getWeeklyUrl = "http://modoocoupon.woobi.co.kr/gnuboard4/daily/php/GetWeeklyAccount.php";

        private readonly string getMonthlyUrl = "http://modoocoupon.woobi.co.kr/gnuboard4/daily/php/GetMonthlyTotalAccount.php";

        private readonly string getPeriodUrl = "http://modoocoupon.woobi.co.kr/gnuboard4/daily/php/GetPeriodTotalAccount.php";

        private readonly string getPeriodListUrl = "http://modoocoupon.woobi.co.kr/gnuboard4/daily/php/GetPeriodListAccount.php";

        public void DeleteDailyAccount(int id)
        {
            string fullUrl = deleteUrl + "?";
            fullUrl += "id=" + id.ToString();

            Post(fullUrl, "");
        }

        public void PostDailyAccount(ItemType type, string date, string name, int amount)
        {
            string fullUrl = postUrl + "?";
            fullUrl += "type=" + type.ToString() + "&date=" + date + "&name=" + name + "&amount=" + amount;

            Post(fullUrl, "");
        }

        public string GetWeeklyAccount(string startDate, string endDate)
        {
            string fullUrl = getWeeklyUrl + "?";
            fullUrl += "searchStartDate=" + startDate + "&searchEndDate=" + endDate;

            return Get(fullUrl);
        }

        public string GetMonthlyTotalAccount(int year, int month)
        {
            string fullUrl = getMonthlyUrl + "?";
            fullUrl += "searchYear=" + year + "&searchMonth=" + month;

            return Get(fullUrl);
        }

        public string GetPeriodTotalAccount(string startDate, string endDate)
        {
            string fullUrl = getPeriodUrl + "?";
            fullUrl += "searchStartDate=" + startDate + "&searchEndDate=" + endDate;

            return Get(fullUrl);
        }

        public string GetPeriodListAccount(string startDate, string endDate)
        {
            string fullUrl = getWeeklyUrl /* getPeriodListUrl */ + "?";
            fullUrl += "searchStartDate=" + startDate + "&searchEndDate=" + endDate;

            return Get(fullUrl);
        }
    }
}
