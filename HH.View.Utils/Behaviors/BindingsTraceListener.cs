using System;
using System.Diagnostics;

namespace HH.View.Utils.Behaviors
{
    [DebuggerNonUserCode]
    public sealed class BindingsTraceListener : TraceListener
    {
        public override void Write(string message)
        {
            Console.Write(message);
        }

        public override void WriteLine(string message)
        {
            Console.WriteLine(message);
        }
    }
}
