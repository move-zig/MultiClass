// The MIT License (MIT)
//
// Copyright 2023 Dave Welsh (davewelsh79@gmail.com)
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to
// deal in the Software without restriction, including without limitation the
// rights to use, copy, modify, merge, publish, distribute, sublicense, and/or
// sell copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
// DEALINGS IN THE SOFTWARE.

namespace MultiClass;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MultiClass.Client;

public class Program
{
    private static async Task Main(string[] args)
    {
        var builder = Host.CreateDefaultBuilder(args);
        ConfigureServices(builder);

        using var host = builder.Build();

        var program = host.Services.GetRequiredService<Program>();
        await program.RunAsync();
    }

    /// <summary>
    /// Registers classes with the DI container.
    /// </summary>
    /// <remarks>We don't register <see cref="CCFClient"/> or <see cref="FourRefuelClient"/>.</remarks>
    /// <param name="builder">The host builder.</param>
    private static void ConfigureServices(IHostBuilder builder)
    {
        builder.ConfigureServices((HostBuilderContext context, IServiceCollection services) =>
        {
            // We don't register 
            services.AddOptions<CCFClientOptions>()
               .Bind(context.Configuration.GetSection(CCFClientOptions.ConfigurationSectionName))
               .ValidateDataAnnotations();
            services.AddOptions<FourRefuelClientOptions>()
               .Bind(context.Configuration.GetSection(FourRefuelClientOptions.ConfigurationSectionName))
               .ValidateDataAnnotations();
            services.AddSingleton<IClientFactory, ClientFactory>();
            services.AddSingleton<Program>();
        });
    }

    private readonly IClientFactory clientFactory;

    /// <summary>
    /// Initializes a new instance of the <see cref="Program"/> class.
    /// </summary>
    /// <param name="clientFactory">A factory for creating <see cref="IClient"/>s.</param>
    public Program(IClientFactory clientFactory)
    {
        this.clientFactory = clientFactory;
    }

    /// <summary>
    /// Main entry point.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
    public async Task RunAsync()
    {
        IClient ccfClient = this.clientFactory.GetClient(ClientType.CCF);
        await ccfClient.RunAsync();

        IClient fourRefuelClient = this.clientFactory.GetClient(ClientType.FourRefuel);
        await fourRefuelClient.RunAsync();
    }
}
