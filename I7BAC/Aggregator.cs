using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using I7BAC.dto;

namespace I7BAC
{
    public static class Aggregator
    {
        public static void BroadCast(string patientId, string rekvisitionsNr, string dato, BilledeListe billeder, string sum)
        {
            OnMessageTransmitted?.Invoke(patientId, rekvisitionsNr, dato, billeder, sum);
        }

        public static Action<string, string, string, BilledeListe, string> OnMessageTransmitted;
    }
}
