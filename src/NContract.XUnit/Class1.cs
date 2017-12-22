using System;
using System.Reflection;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace NContract.XUnit
{
    public class DisplayName : AfterTestFinished
    {
        public DisplayName(ITest test, string attributeName) : base(test, attributeName)
        {
            string line = "";
        }
    }
}
