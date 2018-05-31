using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json; //Serializer
using AutoManSys.Model;
using System.Collections.Generic;
using System.Linq;

namespace AutoManSys.Modules
{
    public class Communication
    {
        //Using HttpClient class to send http request to OrderManSys. Later or I "should" build URL with paraments.
        //Like so: private static readonly HttpClient client = new HttpClient{BaseAddress=new Uri("")};
        public async Task<HttpResponseMessage> Send(string Controller,string Action,object toSerialize) //is object as parameter bad?
        {
            //Using HttpClient class to send http request.
            HttpClient client = new HttpClient();
            HttpResponseMessage response = new HttpResponseMessage();
            var JsonContent = new StringContent(JsonConvert.SerializeObject(toSerialize), Encoding.UTF8, "application/json");
            try
            {
                //The fixed ip for now is 192.168.0.100, will move to appsettings.json later.
                response = await client.PostAsync($"http://192.168.0.100:5000/{Controller}/{Action}",JsonContent);
                //Ensure the response is successful (No timeout/not found.)
                response.EnsureSuccessStatusCode();
            }
            catch (Exception e)
            {
                //Write Error to internal record
                Console.WriteLine("[Warn]:Connection to OrderManSys failed");
                Console.WriteLine($"[Debug]:Request failed when sending request to http://192.168.0.100:5000/{Controller}/{Action} status code:{response.StatusCode}");
                throw e;
            }
            //Return OK with status code from AutoManSys (Should be 200!)
            return response;
        }
    }
}