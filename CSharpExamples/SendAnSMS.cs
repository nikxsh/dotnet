using System.IO;
using System.Net;
using System.Text;

namespace DotNetDemos.CSharpExamples
{
    class SendAnSMS
    {

        public SendAnSMS()
        { }

        public void SendSMS()
        {
            string value = PostData("http://site21.way2sms.com/Login1.action", "username=9028269355&password=Poseidon");
            string token = ParseToken(value);
            value = PostData("http://site23.way2sms.com/smstoss.action;jsessionid=" + token , "ssaction=ss&Token=" + token + "&mobile=9028269355&message=Test From C#");
        }

        public string PostData(string address, string data)
        {
            string returnURL = string.Empty;

            HttpWebRequest request = WebRequest.Create(address) as HttpWebRequest;
            request.AllowAutoRedirect = false;
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            byte[] postData = Encoding.UTF8.GetBytes(data);
            request.ContentLength = postData.Length;

            Stream requestStream = request.GetRequestStream();
            requestStream.Write(postData, 0, postData.Length);

            WebResponse response = request.GetResponse();
            returnURL = response.Headers["Location"];

            return returnURL;
        }

        public string ParseToken(string URL)
        {
            string[] values = URL.Split('?');
            return values[1].Split('=')[1];
        }

    }
}
