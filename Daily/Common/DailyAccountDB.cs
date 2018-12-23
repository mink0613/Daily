using Daily.Model;

namespace Daily.Common
{
    class DailyAccountDB : HttpWebRequest
    {
        private readonly string postUrl = "http://modoocoupon.woobi.co.kr/gnuboard4/daily/php/PostAccount.php";

        private readonly string deleteUrl = "http://modoocoupon.woobi.co.kr/gnuboard4/daily/php/DeleteAccount.php";

        private readonly string getWeeklyUrl = "http://modoocoupon.woobi.co.kr/gnuboard4/daily/php/GetWeeklyAccount.php";

        private readonly string getMonthlyUrl = "http://modoocoupon.woobi.co.kr/gnuboard4/daily/php/GetMonthlyAccount.php";

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

        public string GetMonthlyAccount(int month)
        {
            string fullUrl = getMonthlyUrl + "?";
            fullUrl += "searchMonth=" + month;

            return Get(fullUrl);
        }
    }
}
