using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BestFor.Sturgeon
{
    public class TeamsModel
    {
        public List<TeamModel> Teams { get; set; }

        public TeamsModel()
        {
            Teams = new List<TeamModel>();
        }
    }
}
