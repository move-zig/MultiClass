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

namespace MultiClass.Client;

using Microsoft.Extensions.Options;

/// <inheritdoc/>
public class CCFClient : IClient
{
    private readonly string password;

    /// <summary>
    /// Initializes a new instance of the <see cref="CCFClient"/> class.
    /// </summary>
    /// <param name="options">The configuration options.</param>
    public CCFClient(IOptions<CCFClientOptions> options)
    {
        this.password = options.Value.Password;
    }

    /// <inheritdoc/>
    public ClientType Type => ClientType.CCF;

    /// <inheritdoc/>
    public async Task<IEnumerable<Invoice>> RunAsync()
    {
        Console.WriteLine($"Running CCFClient with password {this.password}");

        return new List<Invoice>
        {
            new Invoice { Id = "d5badb06-d913-49df-a354-1eb948df4ef9", City = "New York" },
            new Invoice { Id = "24c1dfaf-d160-458c-b3fe-6171d0554ec6", City = "Washington" },
        };

    }
}
