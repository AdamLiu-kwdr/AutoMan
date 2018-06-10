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
        
        HttpClient client = new HttpClient();
        HttpResponseMessage response = new HttpResponseMessage();
        private readonly string _orderManSysAddress;
        public Communication(string OrderManSysAddress)
        {
            _orderManSysAddress = OrderManSysAddress;
        }

        //Using HttpClient class to send http request to OrderManSys. Later or I "should" build URL with paraments.
        //Like so: private static readonly HttpClient client = new HttpClient{BaseAddress=new Uri("")};
        public async Task<HttpResponseMessage> PostAsync(string Controller, string Action, object toSerialize) //is object as parameter bad?
        {
            //Using HttpClient class to send http request.
            var JsonContent = new StringContent(JsonConvert.SerializeObject(toSerialize), Encoding.UTF8, "application/json");
            try
            {
                Console.WriteLine($"[Debug]:Requesting {_orderManSysAddress}{Controller}/{Action}.");
                response = await client.PostAsync($"{_orderManSysAddress}{Controller}/{Action}", JsonContent);
                //Ensure the response is successful (No timeout/not found.)
                response.EnsureSuccessStatusCode();
            }
            catch (Exception e)
            {
                //Write Error to internal record
                Console.WriteLine("[Warn]:Connection to OrderManSys failed");
                Console.WriteLine($"[Debug]:Request failed when sending request to {_orderManSysAddress}{Controller}/{Action} status code:{response.StatusCode}");
                throw e;
            }
            //Return OK with status code from AutoManSys (Should be 200!)
            return response;
        }

        //This method send GET with parameters in URL
        public async Task<HttpResponseMessage> SendAsync(string Controller, string Action, string parameters) //is object as parameter bad?
        {
            //Using HttpClient class to send http request.
            try
            {
                response = await client.GetAsync($"{_orderManSysAddress}{Controller}/{Action}{parameters}");
                //Ensure the response is successful (No timeout/not found.)
                response.EnsureSuccessStatusCode();
            }
            catch (Exception e)
            {
                //Write Error to internal record
                Console.WriteLine("[Warn]:Connection to OrderManSys failed");
                Console.WriteLine($"[Debug]:Request failed when sending request to {_orderManSysAddress}{Controller}/{Action}{parameters} status code:{response.StatusCode}");
                throw e;
            }
            //Return OK with status code from AutoManSys (Should be 200!)
            return response;
        }
    }
}