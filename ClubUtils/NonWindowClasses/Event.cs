using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubUtils
{
    class Event
    {
        public string name;
        public DateTime time;
        public string club;
        public bool recurs;
        public DateTime endTime;

        public Event(string n, DateTime startTime, DateTime stopTime, string clubName, bool recurring)
        {
            name = n;
            time = startTime;
            club = clubName;
            recurs = recurring;
            endTime = stopTime;
        }
    }
}
