using System;

namespace SoxLib
{
    public abstract class SoxTime
    {
        public abstract string ToCommandLineArg();
    }

    public class SoxTimeSamples : SoxTime
    {
        public int Value { get; set; }

        public override string ToCommandLineArg()
        {
            return Value + "s";
        }
    }
}
