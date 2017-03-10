namespace NContract.FluentRestApi
{
    public class RestApiClientFactory
    {
        public RestApiClient Create(RestApiClientConfiguration restApiClientConfiguration)
        {
            return new RestApiClient(restApiClientConfiguration);
        }
    }
}