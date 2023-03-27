using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zoopark.DTO
{
    public class Animal
    {
        public int ID { get; set; }
        public int IdFeed { get; set; }
        public int IdSotrudnik { get; set; }
        public int IdFamily { get; set; }
        public string Anname { get; set; }

        public Food Food { get; set; }
        public Family Family { get; set; }
        public Sotrudnikk Sotrudnikk { get; set; }
    }
}
