using NUnit.Framework.Interfaces;

namespace NContract.NUnit
{
    public static class TestStatusExtensions
    {
        public static ContractTestResultStatus ToContractTestStatus(this TestStatus testStatus)
        {
            ContractTestResultStatus contractTestResultStatus;
            switch (testStatus)
            {
                case TestStatus.Failed:
                    contractTestResultStatus = ContractTestResultStatus.Failed;
                    break;
                case TestStatus.Inconclusive:
                    contractTestResultStatus = ContractTestResultStatus.Inconcludent;
                    break;
                case TestStatus.Passed:
                    contractTestResultStatus = ContractTestResultStatus.Passed;
                    break;
                case TestStatus.Skipped:
                    contractTestResultStatus = ContractTestResultStatus.Skipped;
                    break;
                case TestStatus.Warning:
                    contractTestResultStatus = ContractTestResultStatus.Warning;
                    break;
                default:
                    contractTestResultStatus = ContractTestResultStatus.Inconcludent;
                    break;
            }
            return contractTestResultStatus;
        }
    }
}