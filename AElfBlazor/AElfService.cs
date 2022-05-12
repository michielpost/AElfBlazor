using AElfBlazor.Models;
using Microsoft.JSInterop;
using System;
using System.Dynamic;
using System.Threading.Tasks;

namespace AElfBlazor
{
    // This class provides JavaScript functionality for MetaMask wrapped
    // in a .NET class for easy consumption. The associated JavaScript module is
    // loaded on demand when first needed.
    //
    // This class can be registered as scoped DI service and then injected into Blazor
    // components for use.

    public class AElfService : IAsyncDisposable
    {
        private readonly Lazy<Task<IJSObjectReference>> moduleTask;
        private bool jsLoaded;

        //public static event Func<Task>? ConnectEvent;
        //public static event Func<Task>? DisconnectEvent;

        public AElfService(IJSRuntime jsRuntime)
        {
            moduleTask = new(() => LoadScripts(jsRuntime).AsTask());
        }

        public ValueTask<IJSObjectReference> LoadScripts(IJSRuntime jsRuntime)
        {
            //await jsRuntime.InvokeAsync<IJSObjectReference>("import", "https://unpkg.com/aelf-sdk@3.2.40/dist/aelf.umd.js");
            return jsRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/AElfBlazor/aelfJsInterop.js");
        }

        public async ValueTask<bool> HasNightElfAsync()
        {
            var module = await moduleTask.Value;

            var result = await module.InvokeAsync<bool>("HasNightElf");
            Console.WriteLine($"Installed: {result}");
            return result;
        }

        public async ValueTask InitializeNightElfAsync(string appName, string nodeUrl = "https://explorer-test.aelf.io/chain")
        {
            var module = await moduleTask.Value;

            if (!jsLoaded)
            {
                await module.InvokeVoidAsync("loadJs", "https://unpkg.com/aelf-sdk@3.2.40/dist/aelf.umd.js");
                jsLoaded = true;
            }

            await module.InvokeVoidAsync("Initialize", nodeUrl, appName);
        }

        public async ValueTask<string> ExecuteSmartContractAsync(string address, string functionName, ExpandoObject payload)
        {
            var module = await moduleTask.Value;
            return await module.InvokeAsync<string>("ExecuteSmartContract", address, functionName, payload);
        }

        public async ValueTask<T> ReadSmartContractAsync<T>(string address, string functionName, ExpandoObject payload)
        {
            var module = await moduleTask.Value;
            return await module.InvokeAsync<T>("ReadSmartContract", address, functionName, payload);
        }

        public async ValueTask<bool> IsConnectedAsync()
        {
            var module = await moduleTask.Value;
            var result = await module.InvokeAsync<bool>("IsConnected");
            Console.WriteLine($"Connected: {result}");
            return result;

        }
        public async ValueTask<string?> LoginAsync()
        {
            var module = await moduleTask.Value;
            var result = await module.InvokeAsync<string?>("Login");

            Console.WriteLine($"Login: {result}");
            return string.IsNullOrEmpty(result) ? null : result;
        }
        public async ValueTask<string?> GetAddressAsync()
        {
            var module = await moduleTask.Value;
            var result = await module.InvokeAsync<string?>("GetAddress");
            Console.WriteLine($"GetAddress: {result}");
            return string.IsNullOrEmpty(result) ? null : result;
        }
        public async ValueTask LogoutAsync()
        {
            var module = await moduleTask.Value;
            await module.InvokeVoidAsync("Logout");
        }

        public async ValueTask<BalanceResult?> GetBalanceAsync()
        {
            var module = await moduleTask.Value;
            var result = await module.InvokeAsync<BalanceResult?>("GetBalance");
            Console.WriteLine("sym:" + result?.Symbol);
            return result;
        }

        public async ValueTask DisposeAsync()
        {
            if (moduleTask.IsValueCreated)
            {
                var module = await moduleTask.Value;
                await module.DisposeAsync();
            }
        }

    }
}