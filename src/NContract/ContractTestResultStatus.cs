using NUnit.Framework;

namespace NContract
{
    public enum ContractTestResultStatus
    {
        Failed,
        Inconcludent,
        Passed,
        Skipped
    }

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
                default:
                    contractTestResultStatus = ContractTestResultStatus.Inconcludent;
                    break;
            }
            return contractTestResultStatus;
        }
    }
}