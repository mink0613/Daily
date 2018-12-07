using Daily.Model;

namespace Daily.Common
{
    class DailyAccountDB : HttpWebRequest
    {
        private readonly string postUrl = "http://modoocoupon.woobi.co.kr/gnuboard4/daily/php/PostAccount.php";

        private readonly string getWeeklyUrl = "http://modoocoupon.woobi.co.kr/gnuboard4/daily/php/GetWeeklyAccount.php";

        public void PostDailyAccount(ItemType type, string date, string name, int amount)
        {
            string fullUrl = postUrl + "?";
            fullUrl += "type=" + type.ToString() + "&date=" + date + "&name=" + name + "&amount=" + amount;

            Request(fullUrl, "POST", "");
        }
    }
}
