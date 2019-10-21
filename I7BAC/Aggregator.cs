using System;
using I7BAC.Model;

namespace I7BAC
{
    public static class Aggregator
    {
        public static void BroadCast(string patientId, string rekvisitionsNr, string dato, BilledeListe billeder)
        {
            OnMessageTransmitted?.Invoke(patientId, rekvisitionsNr, dato, billeder);
        }

        public static Action<string, string, string, BilledeListe> OnMessageTransmitted;


        public static void BroadCastPythonResult(string result)
        {
            OnPythonResultTransmitted?.Invoke(result);
        }
        public static Action<string> OnPythonResultTransmitted;
    }
}