using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace StudentCoreMvc.Controllers
{
    [Route("[Controller]")]
    public class AboutController
    {
        [Route("/")]
        public string AboutMe()
        {
            return "Hello AbountMe";
        }
        public string Company()
        {
            return " No Company";
        }
        
    }
}
