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
    public  class TestStudent_Api: IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _httpClient;
        public TestStudent_Api(WebApplicationFactory<Program> factory)
        {
            _httpClient = factory.CreateClient();
        }
        [Fact]
        public async Task Test_Get_All_Student()
        {
            var request = new HttpRequestMessage(new HttpMethod("GET"), "/api/students");
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        [Fact]
        public async Task Test_Get_Student_ById()
        {
            // arrange 
            var studentId = await CreateStudent();
            // act
            var request = new HttpRequestMessage(new HttpMethod("GET"), $"api/students/student/{studentId}");
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            // clean student that created
            await DeleteStudent(studentId);
        }
        // test get student by filter 
        [Fact]
        public async Task Test_Get_Student_By_Filter()
        {
            var request = new HttpRequestMessage(new HttpMethod("GET"), "/api/students?StudentStatus=Fresh");
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        // test student not found
        [Fact]
        public async Task Test_Student_Not_Found()
        {
            var Id = Guid.NewGuid();
            var request = new HttpRequestMessage(new HttpMethod("GET"), $"api/students/student/{Id}");
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
        [Fact]
        public async Task Test_Create_Student()
        {
            var content = new MultipartFormDataContent();
            content.Add(new StringContent("20180557"), "student_id");
            content.Add(new StringContent("501"), "student_setId");
            content.Add(new StringContent("mahmoudramadan"), "fullname");
            content.Add(new StringContent("mahmoud"), "firstname");
            content.Add(new StringContent("ramadan"), "lastname");
            content.Add(new StringContent("01017392148"), "phone");
            content.Add(new StringContent("Fresh"), "StudentStatus");
            content.Add(new StringContent("0"), "graduated");
            content.Add(new StringContent(""), "StudentContraint");
            
            var response = await _httpClient.PostAsync("/api/students", content);
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            // clean student that created
            var postContent = await response.Content.ReadFromJsonAsync<Student>();
            Guid studentId = (Guid)(postContent?.Id);
            await DeleteStudent(studentId);
        }
        [Fact]
        public async Task Test_Delete_Student() 
        {
            var Id = "0148cdfa-a7db-4e51-0284-08dcda40b8ff";
            var response = await _httpClient.DeleteAsync($"/api/students/{Id}");
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.Accepted, response.StatusCode);
        }

        // help function to create student and return the id
        public async Task<Guid> CreateStudent()
        {
            var content = new MultipartFormDataContent();
            content.Add(new StringContent("20180557"), "student_id");
            content.Add(new StringContent("501"), "student_setId");
            content.Add(new StringContent("mahmoudramadan"), "fullname");
            content.Add(new StringContent("mahmoud"), "firstname"); 
            content.Add(new StringContent("ramadan"), "lastname");
            content.Add(new StringContent("01017392148"), "phone");
            content.Add(new StringContent("Fresh"), "StudentStatus");
            content.Add(new StringContent("Fresh"), "graduated");
            content.Add(new StringContent(""), "StudentContraint");
            var response = await _httpClient.PostAsync("/api/students", content);
            var postContent = await response.Content.ReadFromJsonAsync<Student>();
            Guid studentId = (Guid)(postContent?.Id);
            return studentId;
        }
        // help function to delete student
        public async Task DeleteStudent(Guid studentId)
        {
            await _httpClient.DeleteAsync($"/api/students/{studentId}");
        }

    }
}
