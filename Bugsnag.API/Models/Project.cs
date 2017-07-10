using Jil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bugsnag.API.Models
{
    public class Project
    {
        [JilDirective("id")]
        public string Id { get; set; }

        [JilDirective("name")]
        public string Name { get; set; }
    }
}
