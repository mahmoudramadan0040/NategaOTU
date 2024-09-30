
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI.SS.Formula.Functions;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Xunit;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Microsoft.AspNetCore.Mvc.Testing;
namespace ControllOTU_Test
{
    public class UnitTestDepartmentApi : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _httpClient;

        public UnitTestDepartmentApi(WebApplicationFactory<Program> factory)
        {
            _httpClient = factory.CreateClient();
        }
        [Theory]
        [InlineData("GET")]
        public async Task Test_Get_All_Department(string method)
        {
            // arrange 
           /* var request = new HttpRequestMessage(new HttpMethod(method),"/api/departments");
            // act 
            var response = await _httpClient.SendAsync(request);

            // assert 
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);*/

        }
    }
}
