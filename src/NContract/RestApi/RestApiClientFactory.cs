namespace NContract.RestApi
{
    public class RestApiClientFactory
    {
        private readonly ContractTest _contractTest;

        public RestApiClientFactory(ContractTest test)
        {
            _contractTest = test;
        }

        public RestApiClient Create(RestApiClientConfiguration restApiClientConfiguration)
        {
            return new RestApiClient(restApiClientConfiguration, _contractTest, new HttpClientFactory());
        }
    }
}