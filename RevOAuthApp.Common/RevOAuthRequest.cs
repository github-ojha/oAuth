using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using RevOAuthApp.Common.Model;

namespace RevOAuthApp.Common
{
	public class RevOAuthRequest	
	{
		//private ILog _log;
		//public RevOAuthRequest(ILog log)
		//{
		//	_log = log;
		//}

		private bool error = false;
		public bool Error
		{
			get
			{
				return error;
			}
			set
			{
				error = value;
			}

		}
		public string GetRevOAuthRequestUrl(string revURL, string apiKey, string apiSecret, string redirectUri)
		{
			//_log.WriteDebug("Get Rev OAtuh Code Request Started...");

			var now = DateTime.UtcNow.ToString();
			string revCode = string.Empty;
			string revOAuthCodeRequest = revURL + "/oauth/authorization?apikey=" + apiKey + "&signature=" + Uri.EscapeDataString(GenerateSignature(apiKey + "::" + now, apiSecret)) + "&redirect_uri=" + redirectUri + "&verifier=" + apiKey + "::" + now + "&response_type=code";
			//_log.WriteDebug("REV URL:" + revOAuthCodeRequest);			
			//_log.WriteDebug("Get Rev OAuth Code Request End...");
			return revOAuthCodeRequest;
			
		}


		
		public string GetRevOAuthToken(string apiKey, string redirectUri, string authCode, string revUrl)
		{
			//_log.WriteDebug("Get Rev OAtuh Token Request Started...");
			string postData = JsonConvert.SerializeObject(new RevTokenRequest() { ApiKey = apiKey, RedirectUri = redirectUri, AuthCode = authCode, GrantType = "authorization_code" });			
			string revTokeurl = string.Concat(revUrl, "/oauth/token");
			string revToken = string.Empty;
			try
			{
				//.WriteDebug("URL :" + revTokeurl);
				HttpWebRequest httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(revTokeurl);
				httpWebRequest.Method = "POST";

				byte[] data = Encoding.ASCII.GetBytes(postData);

				httpWebRequest.ContentType = "application/json";
				httpWebRequest.ContentLength = data.Length;

				System.IO.Stream requestStream = httpWebRequest.GetRequestStream();
				requestStream.Write(data, 0, data.Length);
				requestStream.Close();

				HttpWebResponse myHttpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

				System.Net.HttpWebResponse hwresponse = (System.Net.HttpWebResponse)httpWebRequest.GetResponse();
				if (hwresponse.StatusCode == System.Net.HttpStatusCode.OK)
				{
					System.IO.Stream responseStream = hwresponse.GetResponseStream();
					System.IO.StreamReader myStreamReader = new System.IO.StreamReader(responseStream);
					error = false;
					revToken = myStreamReader.ReadToEnd();
					//_log.WriteDebug("REV Token :"+ revToken);
				}
				hwresponse.Close();

			}
			catch (Exception ex)
			{
				error = true;
				//_log.WriteDebug("Exception :" + ex.StackTrace);
				revToken = ex.Message;
			}

			//_log.WriteDebug("Get Rev OAtuh Token Request End...");

			return revToken;
		}

		private string GenerateSignature(string verifier, string apisecret)
		{
			var encoding = new ASCIIEncoding();
			var originalData = encoding.GetBytes(verifier);
			var keyBytes = encoding.GetBytes(apisecret);

			using (var hmacsha256 = new HMACSHA256(keyBytes))
			{
				try
				{
					var signedBytes = hmacsha256.ComputeHash(originalData);
					//_log.WriteDebug("Signature :"+ Convert.ToBase64String(signedBytes));
					return Convert.ToBase64String(signedBytes);
				}
				catch (CryptographicException)
				{
					throw new UnauthorizedAccessException();
				}
			}
		}
	}
}
