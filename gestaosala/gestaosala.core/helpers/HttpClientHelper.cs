using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace gestaosala.core.helpers
{
    public class HttpClientHelper
    {
        private HttpClient _client;
        private StringContent _content;
        private Encoding _encoding;
        private string _mediaType;
        private string _endpoint;

        #region Constructors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="client"></param>
        public HttpClientHelper(HttpClient client)
        {
            _client = client;
            _encoding = Encoding.UTF8;
            _mediaType = "application/json";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="client"></param>
        /// <param name="encoding"></param>
        public HttpClientHelper(HttpClient client, Encoding encoding)
        {
            _client = client;
            _encoding = encoding;
            _mediaType = "application/json";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="client"></param>
        /// <param name="encoding"></param>
        /// <param name="mediaType"></param>
        public HttpClientHelper(HttpClient client, Encoding encoding, string mediaType)
        {
            _client = client;
            _encoding = encoding;
            _mediaType = mediaType;
        }
        #endregion

        /// <summary>
        /// Serializa o objeto em um JSON
        /// </summary>
        /// <param name="o">Objeto a ser serializado</param>
        /// <returns>Objeto serializado no tipo string</returns>
        public string Serialize(object o)
            => JsonConvert.SerializeObject(o);

        /// <summary>
        /// Converte o conteúdo a ser enviado
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public HttpClientHelper WithContentSerialized(object o)
        {
            _content = new StringContent(Serialize(o), _encoding, _mediaType);
            return this;
        }

        /// <summary>
        /// Converte o conteúdo a ser enviado
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public HttpClientHelper WithContent(object o)
        {
            _content = new StringContent(o.ToString(), _encoding, _mediaType);
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="endpoint"></param>
        /// <returns></returns>
        public HttpClientHelper SetEndpoint(string endpoint)
        {
            _endpoint = endpoint;
            return this;
        }

        /// <summary>
        /// Adiciona cabeçalhos à requisição
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public HttpClientHelper AddHeader(string name, string value)
        {
            _client.DefaultRequestHeaders.Add(name, value);
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<HttpResponseMessage> GetAsync()
            => await _client.GetAsync(_endpoint);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<HttpResponseMessage> PostAsync()
            => await _client.PostAsync(_endpoint, _content);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<HttpResponseMessage> PutAsync()
            => await _client.PutAsync(_endpoint, _content);

        public async Task<HttpResponseMessage> PatchAsync()
        {
            var method = new HttpMethod("PATCH");
            var request = new HttpRequestMessage(method, _endpoint)
            {
                Content = _content
            };
            return await _client.SendAsync(request);
        }
        //=> await _client.(_endpoint, _content);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<HttpResponseMessage> DeleteAsync()
            => await _client.DeleteAsync(_endpoint);
    }
}
