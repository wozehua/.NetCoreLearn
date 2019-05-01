using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace StudentCoreMvc.ViewComponents
{
    public class InternetStatus:ViewComponent
    {

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var httpClient = new HttpClient();
            var response =await httpClient.GetAsync("https://www.baidu.com");
            return View(response.StatusCode == System.Net.HttpStatusCode.OK);
        }
    }
}
