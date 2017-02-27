using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace NContract.Core.RestApi
{
    public class RestApiClientConfigurationBuilder
    {
        private readonly RestApiClientConfiguration _restApiClientConfiguration;

        public RestApiClientConfigurationBuilder()
        {
            _restApiClientConfiguration = new RestApiClientConfiguration
            {
                ResponseContentType = RestApiClientDefaultConfiguration.ContentType,
                Encoding = RestApiClientDefaultConfiguration.Encoding
            };
        }

        public RestApiClientConfigurationBuilder WithBaseUri(string baseUri)
        {
            _restApiClientConfiguration.BaseUri = baseUri;
            return this;
        }

        public RestApiClientConfigurationBuilder WithRequestUri(string requestUri)
        {
            _restApiClientConfiguration.RequestUri = requestUri;
            return this;
        }

        public RestApiClientConfigurationBuilder WithModel(object model = null)
        {
            _restApiClientConfiguration.Model = model;
            PrepareContent(model);
            return this;
        }

        public RestApiClientConfigurationBuilder WithEncoding(Encoding encoding)
        {
            _restApiClientConfiguration.Encoding = encoding;
            return this;
        }

        public RestApiClientConfigurationBuilder WithEmptyModel()
        {
            _restApiClientConfiguration.Content = new StringContent(string.Empty, _restApiClientConfiguration.Encoding, _restApiClientConfiguration.ContentType);
            _restApiClientConfiguration.Content.Headers.Clear();
            return this;
        }

        public RestApiClientConfigurationBuilder WithHeaders(IEnumerable<KeyValuePair<string, string>> headers)
        {
            _restApiClientConfiguration.Headers = headers.ToList();
            return this;
        }

        public RestApiClientConfigurationBuilder WithHttpMethod(HttpMethod method)
        {
            _restApiClientConfiguration.HttpMethod = method;
            return this;
        }

        public RestApiClientConfigurationBuilder WithContentType(string contentType)
        {
            _restApiClientConfiguration.ContentType = contentType;
            if (_restApiClientConfiguration.Content?.Headers != null)
            {
                if (_restApiClientConfiguration.Content.Headers.Contains("Content-Type"))
                    _restApiClientConfiguration.Content.Headers.Remove("Content-Type");
                _restApiClientConfiguration.Content.Headers.Add("Content-Type", _restApiClientConfiguration.ContentType);
            }
            return this;
        }

        public RestApiClientConfigurationBuilder WithResponseContentType(ResponseContentType responseContentType)
        {
            _restApiClientConfiguration.ResponseContentType = responseContentType;
            return this;
        }


        private void PrepareContent(object model)
        {
            var serializedRequest = JsonConvert.SerializeObject(model);

            _restApiClientConfiguration.Content = new StringContent(serializedRequest, _restApiClientConfiguration.Encoding, _restApiClientConfiguration.ContentType);

            if (_restApiClientConfiguration.Headers != null)
                foreach (var header in _restApiClientConfiguration.Headers)
                    _restApiClientConfiguration.Content.Headers.Add(header.Key, header.Value);
        }

        public RestApiClientConfiguration Build()
        {
            if (_restApiClientConfiguration.HttpMethod == null)
            {
                throw new Exception("HttpMethod is not set.");
            }

            return _restApiClientConfiguration;
        }
    }
}