using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zoopark.DTO
{
    public class Sotrudnikk
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Otchestvo { get; set; }
        public string Scheadule { get; set; }
        public int Salary { get; set; }
        public int Idposition { get; set; }

        public position Position { get; set; }
    }
}
