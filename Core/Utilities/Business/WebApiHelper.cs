using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;

namespace Core.Utilities.Business
{
    public class WebApiHelper
    {
        public static async Task<T> GetMethod<T>(string uri, Dictionary<string, string> headers = null)
        {
            var client = new RestClient(uri);
            var request = new RestRequest(Method.GET) { RequestFormat = DataFormat.Json };

            var result = GetResult<T>( client, request, null, headers);

            return await result;
        }
   
        private static async Task<T> GetResult<T>(RestClient client, RestRequest request, object obj = null, Dictionary<string, string> headers = null)
        {
            if (headers != null) //header varsa requeste headerları ekle
            {
                foreach (var header in headers)
                {
                    request.AddHeader(header.Key, header.Value);
                }
            }

            if (obj != null) //post,put,delete gibi işlemler için servise gönderilecek nesne varsa requeste ekle
            {
                //request.AddJsonBody(obj);
                request.AddObject(obj);
            }
            //client üzerinden requesti servise yolla ve
            var response = client.Execute(request);

            return JsonConvert.DeserializeObject<T>(response.Content, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

        }
    }
}
