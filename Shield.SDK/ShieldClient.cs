using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Shield.SDK.Models;
using Shield.SDK.Enums;
using Shield.SDK.Errors;

namespace Shield.SDK
{
    public class ShieldClient
    {
        private readonly string _baseURL;
        private readonly string _apiKey;
        private HttpClient _httpClient;

        public ShieldClient(ShieldOptions options)
        {
            _baseURL = options.BaseURL ?? "https://shield.openfort.xyz";
            _apiKey = options.ApiKey;
            _httpClient = new HttpClient();
        }

        public async Task<Share> GetSecret(ShieldAuthOptions auth)
        {
            try
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"{_baseURL}/shares");
                var headers = GetAuthHeaders(auth);
                foreach (var header in headers)
                {
                    request.Headers.TryAddWithoutValidation(header.Key, header.Value);
                }
                var response = await _httpClient.SendAsync(request);

                if (!response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                        throw new NoSecretFoundError("No secret found for the given auth options");

                    var errorResponse = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Unexpected response: {response.StatusCode}: {errorResponse}");
                }

                var data = JsonSerializer.Deserialize<Share>(await response.Content.ReadAsStringAsync());
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception($"Unexpected error: {ex.Message}", ex);
            }
        }

        public async Task DeleteSecret(ShieldAuthOptions auth)
        {
            try
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, $"{_baseURL}/shares");
                var headers = GetAuthHeaders(auth);
                foreach (var header in headers)
                {
                    request.Headers.TryAddWithoutValidation(header.Key, header.Value);
                }
                var response = await _httpClient.SendAsync(request);


                if (!response.IsSuccessStatusCode)
                {
                    var errorResponse = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Unexpected response: {response.StatusCode}: {errorResponse}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Unexpected error: {ex.Message}", ex);
            }
        }

        private async Task CreateSecret(string path, Share share, ShieldAuthOptions auth)
        {
            try
            {
                var content = JsonSerializer.Serialize(new
                {
                    share.Secret,
                    share.Entropy,
                    share.EncryptionParameters?.Salt,
                    share.EncryptionParameters?.Iterations,
                    share.EncryptionParameters?.Length,
                    share.EncryptionParameters?.Digest,
                    auth.EncryptionPart
                });
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, $"{_baseURL}/{path}")
                {
                    Content = new StringContent(content, Encoding.UTF8, "application/json")
                };
                var headers = GetAuthHeaders(auth);
                foreach (var header in headers)
                {
                    request.Headers.TryAddWithoutValidation(header.Key, header.Value);
                }

                var response = await _httpClient.SendAsync(request);

                if (!response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
                        throw new SecretAlreadyExistsError("Secret already exists for the given auth options");

                    var errorResponse = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Unexpected response: {response.StatusCode}: {errorResponse}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Unexpected error: {ex.Message}", ex);
            }
        }

        public async Task PreRegister(Share share, ShieldAuthOptions auth)
        {
            await CreateSecret("admin/preregister", share, auth);
        }

        public async Task StoreSecret(Share share, ShieldAuthOptions auth)
        {
            await CreateSecret("shares", share, auth);
        }

        private Dictionary<string, string> GetAuthHeaders(ShieldAuthOptions options)
        {
            var headers = new Dictionary<string, string>
            {
                ["x-api-key"] = _apiKey,
                ["Access-Control-Allow-Origin"] = _baseURL,
                ["x-auth-provider"] = options.AuthProvider.ToString()
            };

            if (options.ExternalUserId != null)
                headers["x-user-id"] = options.ExternalUserId;
            if (options.ApiKey != null)
                headers["x-api-key"] = options.ApiKey;
            if (options.ApiSecret != null)
                headers["x-api-secret"] = options.ApiSecret;
            if (options.EncryptionPart != null)
                headers["x-encryption-part"] = options.EncryptionPart;

            if (IsOpenfortAuthOptions(options))
                headers["Authorization"] = $"Bearer {((OpenfortAuthOptions)options).OpenfortOAuthToken}";
            if (IsCustomAuthOptions(options))
                headers["Authorization"] = $"Bearer {((CustomAuthOptions)options).CustomToken}";

            return headers;
        }

        private bool IsOpenfortAuthOptions(ShieldAuthOptions options)
        {
            return options is OpenfortAuthOptions;
        }

        private bool IsCustomAuthOptions(ShieldAuthOptions options)
        {
            return options is CustomAuthOptions;
        }
    }
}
