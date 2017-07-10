using Jil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bugsnag.API.Models
{
    public class Error
    {
        [JilDirective("id")]
        public string Id { get; set; }

        [JilDirective("project_id")]
        public string ProjectId { get; set; }

        [JilDirective("error_class")]
        public string ErrorClass { get; set; }

        [JilDirective("message")]
        public string Message { get; set; }

        [JilDirective("context")]
        public string Context { get; set; }

        [JilDirective("first_seen")]
        public DateTimeOffset FirstSeen { get; set; }

        [JilDirective("last_seen")]
        public DateTimeOffset LastSeen { get; set; }
    }
}
