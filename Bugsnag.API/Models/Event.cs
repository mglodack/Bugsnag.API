using Jil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bugsnag.API.Models
{
    public class Event
    {
        [JilDirective("id")]
        public string Id { get; set; }

        [JilDirective("error_id")]
        public string ErrorId { get; set; }

        [JilDirective("meta_data")]
        public string MetaData { get; set; }
    }
}
