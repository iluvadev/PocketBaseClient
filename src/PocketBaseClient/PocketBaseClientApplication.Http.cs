// Project site: https://github.com/iluvadev/PocketBaseClient-csharp
//
// Issues: https://github.com/iluvadev/PocketBaseClient-csharp/issues
// License (MIT): https://github.com/iluvadev/PocketBaseClient-csharp/blob/main/LICENSE
//
// Copyright (c) 2022, iluvadev, and released under MIT License.
//
// pocketbase-csharp-sdk project: https://github.com/PRCV1/pocketbase-csharp-sdk 
// pocketbase project: https://github.com/pocketbase/pocketbase


using pocketbase_csharp_sdk;
using pocketbase_csharp_sdk.Models;
using pocketbase_csharp_sdk.Models.Files;
using PocketBaseClient.Orm;
using System.Collections;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace PocketBaseClient
{
    public partial class PocketBaseClientApplication
    {
        #region Sync support
        private HttpClient? _HttpClient = null;
        private HttpClient HttpClient => _HttpClient ??= new HttpClient();

        private void Send(string path, HttpMethod method, IDictionary<string, string>? headers = null, IDictionary<string, object?>? query = null, IDictionary<string, object>? body = null, IEnumerable<IFile>? files = null)
        {
            headers ??= new Dictionary<string, string>();
            query ??= new Dictionary<string, object?>();
            body ??= new Dictionary<string, object>();
            files ??= new List<IFile>();

            Uri url = Sdk.BuildUrl(path, query);

            using (HttpRequestMessage request = CreateRequest(url, method, headers: headers, query: query, body: body, files: files))
            {
                try
                {
                    var response = HttpClient.Send(request);
                    if ((int)response.StatusCode >= 400)
                        throw new ClientException(url.ToString(), statusCode: (int)response.StatusCode);
                }
                catch (Exception ex)
                {
                    if (ex is ClientException) throw;
                    throw new ClientException(url: url.ToString(), originalError: ex, isAbort: ex is HttpRequestException);
                }
            }
        }

        private T? Send<T>(string path, HttpMethod method, IDictionary<string, string>? headers = null, IDictionary<string, object?>? query = null, IDictionary<string, object>? body = null, IEnumerable<IFile>? files = null)
        {
            headers ??= new Dictionary<string, string>();
            query ??= new Dictionary<string, object?>();
            body ??= new Dictionary<string, object>();
            files ??= new List<IFile>();

            Uri url = Sdk.BuildUrl(path, query);

            using (HttpRequestMessage request = CreateRequest(url, method, headers: headers, query: query, body: body, files: files))
            {
                try
                {
                    var response = HttpClient.Send(request);
                    if ((int)response.StatusCode >= 400)
                        throw new ClientException(url.ToString(), statusCode: (int)response.StatusCode);

                    response.EnsureSuccessStatusCode();

                    using (var stream = response.Content.ReadAsStream())
                        return JsonSerializer.Deserialize<T>(stream);
                }
                catch (Exception ex)
                {
                    if (ex is ClientException) throw;
                    throw new ClientException(url: url.ToString(), originalError: ex, isAbort: ex is HttpRequestException);
                }
            }
        }

        private HttpRequestMessage CreateRequest(Uri url, HttpMethod method, IDictionary<string, string> headers, IDictionary<string, object?> query, IDictionary<string, object> body, IEnumerable<IFile> files)
        {
            HttpRequestMessage request;
            if (files.Count() > 0)
                request = BuildFileRequest(method, url, headers, body, files);
            else
                request = BuildJsonRequest(method, url, headers, body);

            if (!headers.ContainsKey("Authorization") && Sdk.AuthStore.IsValid)
                request.Headers.Add("Authorization", Sdk.AuthStore.Token);

            if (!headers.ContainsKey("Accept-Language"))
                request.Headers.Add("Accept-Language", "en-US");// pocketBase._language);

            return request;
        }
        private HttpRequestMessage BuildJsonRequest(HttpMethod method, Uri url, IDictionary<string, string>? headers = null, IDictionary<string, object>? body = null)
        {
            var request = new HttpRequestMessage(method, url);
            if (body is not null && body.Count > 0)
                request.Content = JsonContent.Create(body);

            if (headers is not null)
                foreach (var header in headers)
                    request.Headers.Add(header.Key, header.Value);

            return request;
        }
        private HttpRequestMessage BuildFileRequest(HttpMethod method, Uri url, IDictionary<string, string>? headers, IDictionary<string, object>? body, IEnumerable<IFile> files)
        {
            var request = new HttpRequestMessage(method, url);

            if (headers is not null)
                foreach (var header in headers)
                    request.Headers.Add(header.Key, header.Value);

            var form = new MultipartFormDataContent();
            foreach (var file in files)
            {
                var stream = file.GetStream();
                if (stream is null || string.IsNullOrWhiteSpace(file.FieldName) || string.IsNullOrWhiteSpace(file.FileName))
                    continue;

                var fileContent = new StreamContent(stream);
                var mimeType = GetMimeType(file);
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(mimeType);
                form.Add(fileContent, file.FieldName, file.FileName);
            }


            if (body is not null && body.Count > 0)
            {
                Dictionary<string, string> additionalBody = new Dictionary<string, string>();
                foreach (var item in body)
                {
                    if (item.Value is IList valueAsList && item.Value is not string)
                    {
                        for (int i = 0; i < valueAsList.Count; i++)
                        {
                            var listValue = valueAsList[i]?.ToString();
                            if (string.IsNullOrWhiteSpace(listValue))
                                continue;
                            additionalBody[$"{item.Key}{i}"] = listValue;
                        }
                    }
                    else
                    {
                        var value = item.Value?.ToString();
                        if (string.IsNullOrWhiteSpace(value))
                            continue;
                        additionalBody[item.Key] = value;
                    }
                }

                foreach (var item in additionalBody)
                {
                    var content = new StringContent(item.Value);
                    form.Add(content, item.Key);
                }
            }

            request.Content = form;
            return request;
        }
        private string GetMimeType(IFile file)
        {
            if (file is FilepathFile filePath)
            {
                var fileName = Path.GetFileName(filePath.FilePath);
                return MimeMapping.MimeUtility.GetMimeMapping(fileName);
            }
            return MimeMapping.MimeUtility.UnknownMimeType;
        }
        #endregion Sync support

        #region Get
        public async Task<T?> HttpGetAsync<T>(string url)
        {
            return await Sdk.SendAsync<T>(url, HttpMethod.Get);
        }
        public T? HttpGet<T>(string url)
        {
            return Send<T>(url, HttpMethod.Get);
        }

        public async Task<PagedCollectionModel<T>?> HttpGetListAsync<T>(string url, int? page = null, int? perPage = null, string? filter = null, string? sort = null)
        {
            var query = new Dictionary<string, object?>()
            {
                { "filter", filter },
                { "page", page },
                { "perPage", perPage },
                { "sort", sort },
                { "expand", null },
            };
            var pagedCollection = await Sdk.SendAsync<PagedCollectionModel<T>>(url, HttpMethod.Get, query: query);
            if (pagedCollection is null) throw new ClientException(url);
            return pagedCollection;
        }
        public PagedCollectionModel<T> HttpGetList<T>(string url, int? page = null, int? perPage = null, string? filter = null, string? sort = null)
        {
            var query = new Dictionary<string, object?>()
            {
                { "filter", filter },
                { "page", page },
                { "perPage", perPage },
                { "sort", sort },
                { "expand", null },
            };
            var pagedCollection = Send<PagedCollectionModel<T>>(url, HttpMethod.Get, query: query);
            if (pagedCollection is null) throw new ClientException(url);

            return pagedCollection;
        }
        #endregion Get

        #region Delete
        public async Task<bool> HttpDeleteAsync(string url)
        {
            try { await Sdk.SendAsync(url, HttpMethod.Delete); }
            catch { return false; }
            return true;
        }
        public bool HttpDelete(string url)
        {
            try { Send(url, HttpMethod.Delete); }
            catch { return false; }
            return true;
        }
        #endregion Delete

        #region Post
        public async Task<T?> HttpPostAsync<T>(string url, T element, IEnumerable<FieldFileBase>? files = null)
        {
            // Convert Serialized element to Dictionary<string, object>
            var body = JsonSerializer.Deserialize<Dictionary<string, object>>(JsonSerializer.Serialize(element));

            return await Sdk.SendAsync<T>(url, HttpMethod.Post, body: body, files: files);
        }
        public T? HttpPost<T>(string url, T element, IEnumerable<FieldFileBase>? files = null)
        {
            // Convert Serialized element to Dictionary<string, object>
            var body = JsonSerializer.Deserialize<Dictionary<string, object>>(JsonSerializer.Serialize(element));

            return Send<T>(url, HttpMethod.Post, body: body, files: files);
        }
        #endregion Post

        #region Patch
        public async Task<T?> HttpPatchAsync<T>(string url, T element, IEnumerable<FieldFileBase>? files = null)
        {
            // Convert Serialized element to Dictionary<string, object>
            var body = JsonSerializer.Deserialize<Dictionary<string, object>>(JsonSerializer.Serialize(element));

            return await Sdk.SendAsync<T>(url, HttpMethod.Patch, body: body, files: files);
        }
        public T? HttpPatch<T>(string url, T element, IEnumerable<FieldFileBase>? files = null)
        {
            // Convert Serialized element to Dictionary<string, object>
            var body = JsonSerializer.Deserialize<Dictionary<string, object>>(JsonSerializer.Serialize(element));

            return Send<T>(url, HttpMethod.Patch, body: body, files: files);
        }
        #endregion Patch

        #region GetStream
        public async Task<Stream> GetStreamAsync(string url, string? paramaters = null)
        {
            // TODO: REMOVE When: sdk project PR merged and published in NuGet
            Uri finalUrl = BuildUrl(url, paramaters);
            try
            {
                return await HttpClient.GetStreamAsync(finalUrl);
            }
            catch (Exception ex)
            {
                if (ex is ClientException) throw;
                else if (ex is HttpRequestException) throw new ClientException(url: finalUrl.ToString(), originalError: ex, isAbort: true);
                else throw new ClientException(url: finalUrl.ToString(), originalError: ex);
            }
        }
        private Uri BuildUrl(string path, string? parameters = null)
        {
            // TODO: REMOVE When: sdk project PR merged and published in NuGet
            var url = AppUrl + (AppUrl.EndsWith("/") ? "" : "/");

            if (!string.IsNullOrWhiteSpace(path))
                url += path.StartsWith("/") ? path.Substring(1) : path;

            if (!string.IsNullOrWhiteSpace(parameters))
                url += "?" + parameters;

            return new Uri(url, UriKind.RelativeOrAbsolute);
        }
        #endregion GetStream
    }
}
