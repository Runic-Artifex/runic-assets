using System;
using System.Reflection;
using System.Text;
using RunicAssets;
using RunicAssets.CsWebUi;

var source = new EmbeddedAssetSource(
    Assembly.GetExecutingAssembly(),
    [new("index.html", "RunicAssets.CsWebUiAotSmoke.index.html", IsEntryPoint: true)]);
global::CsWebUi.WebUiFileHandlerResult result = source.ToWebUiFileHandler()("/");
string response = Encoding.UTF8.GetString(result.Response.Span);
return result.IsHandled
    && response.StartsWith("HTTP/1.1 200 OK\r\n", StringComparison.Ordinal)
    && response.Contains("ETag: \"sha256-", StringComparison.Ordinal)
    && response.EndsWith("\r\n\r\n<!doctype html><title>NativeAOT adapter smoke</title>\n", StringComparison.Ordinal)
    ? 0
    : 1;
