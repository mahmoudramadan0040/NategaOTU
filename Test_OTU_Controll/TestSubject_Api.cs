using ControlOctoberTechnologyUniversitySystem.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Test_OTU_Controll
{
    public  class TestSubject_Api: IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _httpClient;
        public TestSubject_Api(WebApplicationFactory<Program> factory)
        {
            _httpClient = factory.CreateClient();
        }
        [Fact]
        public async Task Test_Get_All_Subject()
        {
            var request = new HttpRequestMessage(new HttpMethod("GET"), "/api/subjects");
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        } 
        [Fact]
        public async Task Test_Get_Subject_ById()
        {
            var Id = "0148cdfa-a7db-4e51-0284-08dcda40b8ff";
            var request = new HttpRequestMessage(new HttpMethod("GET"), $"api/subjects/subject/{Id}");
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        [Fact]
        public async Task Test_Create_Subject()
        {
            var subject = new Subject
            {
                Name = "Test Subject",
                
            };
            var content = JsonContent.Create(subject);
            var response = await _httpClient.PostAsync("/api/subjects", content);
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        [Fact]
        public async Task Test_Update_Subject()
        {
            var Id = "0148cdfa-a7db-4e51-0284-08dcda40b8ff";
            var subject = new Subject
            {
                Name = "Test Subject",
                
            };
            var content = JsonContent.Create(subject);
            var response = await _httpClient.PutAsync($"/api/subjects/{Id}", content);  
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        [Fact]
        public async Task Test_Delete_Subject()
        {
            var Id = "0148cdfa-a7db-4e51-0284-08dcda40b8ff";
            var response = await _httpClient.DeleteAsync($"/api/subjects/{Id}");
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        
    }
}
