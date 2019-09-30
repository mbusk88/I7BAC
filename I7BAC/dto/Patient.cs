using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace I7BAC.dto
{
    public class Patient
    {
        public string Navn { get; set; }
        public string CPR { get; set; }
        public RekvisitionListe RekvisitionListe { get; set; }
    }


}
