using Calculator_BLL.Helpers;
using Calculator_BLL.Model;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace Calculator_BLL.API
{
    public class ExternalApi
    {
        private HttpClient httpClient;
        public ExternalApi(HttpClient http)
        {
            httpClient = http;
        }
        public List<Ship> GetAllShips()
        {

            var res = FindPageOfShip(StringHelper.getStarships);
            var ship = new List<Ship>(res.results);
            while (!string.IsNullOrEmpty(res.next))
            {
                res = FindPageOfShip(res.next);

                ship.AddRange(res.results);
            }
            return ship;

        }

        private ShipResponse FindPageOfShip(string urlpage)
        {
            if (httpClient.BaseAddress == null)
                httpClient.BaseAddress = new Uri(StringHelper.urlbase);

            var res = httpClient.GetAsync(urlpage.Replace(StringHelper.urlbase, string.Empty)).Result;
            if (res.IsSuccessStatusCode)
            {
                //var prp = res.Content.ReadAsStringAsync().Result;
                return res.Content.ReadAsAsync<ShipResponse>().Result;
            }
            return new ShipResponse();
        }

    }
}
