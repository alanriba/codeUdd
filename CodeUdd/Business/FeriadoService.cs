using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CodeUdd.Contract;
using CodeUdd.Dto;
using CodeUdd.Exceptions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace PruebaUdd.Business
{
    public class FeriadoService : IFeriadoService
    {
        private readonly HttpClient _httpClient;
        public static string SettingName = "UrlFeriado";
        public string Url { get; set; }

        public FeriadoService()
        {
            _httpClient = new HttpClient();
            Url = Url ?? "https://apis.digital.gob.cl/fl/feriados/";
        }

        public async Task<bool> VerificarFeriado(DateTime fecha)
        {
            var response = await _httpClient.GetAsync(this.Url + $"{fecha.Year}/{fecha.Month}/{fecha.Day}");

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                JObject jObject = JObject.Parse(result);
                JToken token = jObject.Descendants()
                                      .FirstOrDefault(t => t.Path == "error");
               if(token.Path.Equals("error"))
                {
                    return false;
                }
            }
            return true;
        }
    }
}