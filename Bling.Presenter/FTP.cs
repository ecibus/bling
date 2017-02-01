using System;
using System.IO;
using System.Net;

namespace Bling.Presenter
{
    public class FTP
    {
        public void Upload(string filename)
        {
            //FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create("ftp://ftp.gemcorp.com/" + filename);
            //request.Method = WebRequestMethods.Ftp.UploadFile;
            //request.Credentials = new NetworkCredential(@"gem\docmagicftp", "g3mmortgag3!");

            //FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create("ftp://www.capmort123.com123/" + filename);
            FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create("ftp://www.mortcap.com/" + filename);
            request.Method = WebRequestMethods.Ftp.UploadFile;
            //request.Credentials = new NetworkCredential("meg", "meg");
            request.Credentials = new NetworkCredential("gem", "gem");

            request.UsePassive = true;
            request.UseBinary = true;
            request.KeepAlive = false;

            FileStream stream = File.OpenRead(@"\\transmit\dept$\SecMrkt\MCM\Uploads\" + filename);
            byte[] buffer = new byte[stream.Length];
            stream.Read(buffer, 0, buffer.Length);
            stream.Close();

            Stream reqStream = request.GetRequestStream();
            reqStream.Write(buffer, 0, buffer.Length);
            reqStream.Close();

        }
    }
}
