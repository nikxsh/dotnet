using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Tools.Models;

namespace Tools
{
   //https://docs.microsoft.com/en-us/dotnet/csharp/tutorials/console-webapiclient
   public class RestClient
	{
      private readonly HttpClient client;

      public bool GetBranchesError { get; private set; }

      public RestClient()
      {
         client = new HttpClient();
      }

      public async Task<List<T>> GetJsonData<T>(string url)
      {
         var streamTask = client.GetStreamAsync(url);
         var result = await JsonSerializer.DeserializeAsync<List<T>>(await streamTask);
         return result;
      }
   }
}
