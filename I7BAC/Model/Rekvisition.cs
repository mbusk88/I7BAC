using System;

namespace I7BAC.Model
{
    public class Rekvisition
    {
        public Guid? Rekvisitionsnr{ get; set; }
        public string Dato { get; set; }
        public BilledeListe Billeder { get; set; }
    }
}
