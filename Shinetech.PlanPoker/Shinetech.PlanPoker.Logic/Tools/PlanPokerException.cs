using System;

namespace Shinetech.PlanPoker.Logic.Tools
{
    public class PlanPokerException : Exception
    {
        public string Key { get; set; }
        public PlanPokerException(string key)
        {
            Key = key;
        }
    }
}