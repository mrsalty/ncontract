using System;

namespace NContract
{
    internal class Runner
    {
        public DateTime RunTimeUtc { get; }
        
        public RunningStatus RunningStatus { get; private set; }

        public Runner()
        {
            RunTimeUtc = DateTime.UtcNow;
            RunningStatus = RunningStatus.Started;
        }
    }

    
}