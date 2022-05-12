using Microsoft.AspNetCore.Components;
using System.Dynamic;

namespace AElfBlazor.SampleApp.Pages
{
    public partial class Index
    {
        [Inject]
        public AElfService AElfService { get; set; } = default!;

        public bool HasExtension { get; set; }
        public bool IsConnected { get; set; }
        public string? SelectedAddress { get; set; }
        public string? RpcResult { get; set; }



        protected override async Task OnInitializedAsync()
        {
            HasExtension = await AElfService.HasNightElfAsync();

            if (HasExtension)
            {
                var testnetUrl = "https://explorer-test.aelf.io/chain";

                await AElfService.InitializeNightElfAsync("Blazor Sample App", testnetUrl);

                IsConnected = await AElfService.IsConnectedAsync();
            }

        }

        public async Task LoginAsync()
        {
            await AElfService.LoginAsync();
            IsConnected = await AElfService.IsConnectedAsync();
        }

        public async Task LogoutAsync()
        {
            await AElfService.LogoutAsync();
            SelectedAddress = null;
            IsConnected = false;
        }

        public async Task GetSelectedAddress()
        {
            SelectedAddress = await AElfService.GetAddressAsync();
            Console.WriteLine($"Address: {SelectedAddress}");
        }

        public async Task GetBalance()
        {
            var result = await AElfService.GetBalanceAsync();
            RpcResult = $"Balance result: {result?.Symbol} : {result?.ELFBalance}";
        }

        public async Task ExecuteSmartContractAsync()
        {
            var address = "address";
            string functionName = "Method";
            dynamic payload = new ExpandoObject();
            payload.property = "value";

            var txId = await AElfService.ExecuteSmartContractAsync(address, functionName, payload);
        }
    }
}
