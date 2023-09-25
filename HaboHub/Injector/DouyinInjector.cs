
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.Wpf;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace HaboHub.Injector
{
    public class DouyinInjector
    {
        
        private readonly WebView2 webView = new();
        private readonly WrapPanel myWrapPanel = new();
        private readonly Window window = new();
        public DouyinInjector()
        {
            _ = InitializeComponentAsync();
        }
        
        private async Task InitializeComponentAsync()
        {
            SetInvisible();
            myWrapPanel.Children.Add(webView);
            window.Content = myWrapPanel;
            window.Show();
            webView.WebMessageReceived += WebView_WebMessageReceived;
            await webView.EnsureCoreWebView2Async();
            webView.CoreWebView2.AddWebResourceRequestedFilter("https://lf-cdn-tos.bytescm.com/obj/static/webcast/douyin_live/7317.5c44ebf9.js", CoreWebView2WebResourceContext.All, CoreWebView2WebResourceRequestSourceKinds.All);
            webView.CoreWebView2.WebResourceRequested += CoreWebView2_WebResourceRequested;
        }

        private void SetInvisible()
        {
            window.Visibility = Visibility.Hidden;
            window.WindowStyle = WindowStyle.None;
            window.Height = 0;
            window.Width = 0;
        }
        public void Start(string url)
        {
            webView.Source=new Uri(url);
        }


        private void WebView_WebMessageReceived(object? sender, CoreWebView2WebMessageReceivedEventArgs e)
        {
            Debug.WriteLine(e.WebMessageAsJson);
        }

        private void CoreWebView2_WebResourceRequested(object? sender, CoreWebView2WebResourceRequestedEventArgs e)
        {
            Debug.Assert(e.Request.Uri == "https://lf-cdn-tos.bytescm.com/obj/static/webcast/douyin_live/7317.5c44ebf9.js");
            FileStream fs = File.Open(Environment.CurrentDirectory + "\\inject.js", FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            CoreWebView2WebResourceResponse response = webView.CoreWebView2.Environment.CreateWebResourceResponse(fs, 200, "OK", "Content-Type: application/javascript");
            e.Response = response;
            
        }


    }
}
