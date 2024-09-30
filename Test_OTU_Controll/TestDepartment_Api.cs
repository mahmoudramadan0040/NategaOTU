using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Net;
using FluentAssertions;
using ControlOctoberTechnologyUniversitySystem.Models;
using System.Text.Json;
using System.Text;
using System.Net.Http.Json;
using Azure;
namespace Test_OTU_Controll
{
    public class UnitTestDepartmentApi: IClassFixture<WebApplicationFactory<Program>>
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
            var request = new HttpRequestMessage(new HttpMethod(method), "/api/departments");
            // act 
            var response = await _httpClient.SendAsync(request);

            // assert 
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        }
        [Fact]
        public async Task Test_Get_Department_ById()
        {
            var Id = "0148cdfa-a7db-4e51-0284-08dcda40b8ff";
            var request = new HttpRequestMessage(new HttpMethod("GET"), $"api/departments/department/{Id}");
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        [Fact] 
        public async Task Test_Create_department()
        {
            // arrange 
            var request = new HttpRequestMessage(HttpMethod.Post, $"api/departments/department");

            var newDepartment = new
            {
                Name = "Test Department",
                
            };
            // Serialize the object to JSON
            var jsonContent = new StringContent(
                JsonSerializer.Serialize(newDepartment),
                Encoding.UTF8,
                "application/json"
            );

            request.Content = jsonContent;
            // act
            var response = await _httpClient.SendAsync(request);
            var postContent = await response.Content.ReadFromJsonAsync<Department>();

            Guid departmentId = (Guid)(postContent?.Id);
            
            // assert 
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Contains("Test Department", responseString);

            // clean the department
            await DeleteDepartment(departmentId);
        }
        [Fact] 
        public async Task Test_Delete_department()
        {
            // arrange 
            var post_department_request = new HttpRequestMessage(HttpMethod.Post, $"api/departments/department");
            var newDepartment = new
            {
                Name = "Test Department",

            };
            // Serialize the object to JSON
            var jsonContent = new StringContent(
                JsonSerializer.Serialize(newDepartment),
                Encoding.UTF8,
                "application/json"
            );

                       
            post_department_request.Content = jsonContent;
            var postResponse = await _httpClient.SendAsync(post_department_request);
            postResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            var postContent = await postResponse.Content.ReadFromJsonAsync<Department>();
            
            var departmentId = postContent?.Id;
            


            var delete_request = new HttpRequestMessage(HttpMethod.Delete, $"api/departments/department/{departmentId}");
            // act
            var delete_response = await _httpClient.SendAsync(delete_request);
            delete_response.StatusCode.Should().Be(HttpStatusCode.Accepted);

        }
        [Fact]
        public async Task Test_Update_Department()
        {


            Guid department_id = await CreateDepartment();

            var UpdateDepartment = new
            {
                Name = "Test Department Update",

            };
            // Serialize the object to JSON
            var jsonContent = new StringContent(
                JsonSerializer.Serialize(UpdateDepartment),
                Encoding.UTF8,
                "application/json"
            );
            var update_request = new HttpRequestMessage(HttpMethod.Put, $"api/departments/department/{department_id}");
            update_request.Content = jsonContent;
            // act
            var update_response = await _httpClient.SendAsync(update_request);
            update_response.StatusCode.Should().Be(HttpStatusCode.Accepted);
            var responseString = await update_response.Content.ReadAsStringAsync();
            Assert.Contains("Test Department Update", responseString);

            // clean the department
            await DeleteDepartment(department_id);
        }




        // shared function help test cases 
        public async Task<Guid> CreateDepartment()
        {
            // arrange 
            var post_department_request = new HttpRequestMessage(HttpMethod.Post, $"api/departments/department");
            var newDepartment = new
            {
                Name = "Test Department",

            };
            // Serialize the object to JSON
            var jsonContent = new StringContent(
                JsonSerializer.Serialize(newDepartment),
                Encoding.UTF8,
                "application/json"
            );


            post_department_request.Content = jsonContent;
            var postResponse = await _httpClient.SendAsync(post_department_request);
            
            var postContent = await postResponse.Content.ReadFromJsonAsync<Department>();

            Guid departmentId = (Guid)(postContent?.Id);

            return departmentId;
        }
        public async Task DeleteDepartment(Guid depatment_id)
        {
            var delete_request = new HttpRequestMessage(HttpMethod.Delete, $"api/departments/department/{depatment_id}");
            // act
            var delete_response = await _httpClient.SendAsync(delete_request);
        }
        // i want to create a function to get the department id 


    }
}