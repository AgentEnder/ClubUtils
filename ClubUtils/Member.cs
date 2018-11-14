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
        public ranks rank;
        public enum ranks { USER, SECRETARY, TREASURER, VICE_PRESIDENT, PRESIDENT, ADVISOR};

        public Member(string n, string e, string c, string r)
        {
            fullName = n;
            email = e;
            clubName = c;
            switch (r)
            {
                case "User":
                    rank = ranks.USER;
                    break;
                case "Secretary":
                    rank = ranks.SECRETARY;
                    break;
                case "Treasurer":
                    rank = ranks.TREASURER;
                    break;
                case "VicePresident":
                    rank = ranks.VICE_PRESIDENT;
                    break;
                case "President":
                    rank = ranks.PRESIDENT;
                    break;
                case "FacultyAdvisor":
                    rank = ranks.ADVISOR;
                    break;
                default:
                    rank = ranks.USER;
                    break;
            }
        }

    }
}
