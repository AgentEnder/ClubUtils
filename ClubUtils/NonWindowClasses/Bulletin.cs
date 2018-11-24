using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubUtils
{
    class Bulletin
    {
        public string heading;
        public DateTime time;
        public string body;
        public string clubName;

        public Bulletin(string head, DateTime timestamp, string body_s, string club)
        {
            heading = head;
            time = timestamp;
            body = body_s;
            clubName = club;
        }
    }
}
