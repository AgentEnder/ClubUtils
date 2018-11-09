using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubUtils
{
    public class Member
    {
        public string fullName;
        public string email;
        public string clubName;
        public string rank;

        public Member(string n, string e, string c, string r)
        {
            fullName = n;
            email = e;
            clubName = c;
            rank = r;
        }

    }
}
