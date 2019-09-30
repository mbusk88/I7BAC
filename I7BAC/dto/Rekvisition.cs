using System;
using System.Collections.Generic;

namespace I7BAC.dto
{
    public class Rekvisition
    {
        public Guid Rekvisitionsnr{ get; set; }
        public DateTime Dato { get; set; }
        public BilledeListe Billeder { get; set; }
    }
}
