using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Hosting;
using System.Net;
using Microsoft.AspNetCore.Http;
using Registration.ViewModel;
using Registration.WEBAPI;

namespace Registration.Controllers
{
    public class RegistrationController : Controller
    {
        private string domainName = "";


        public IActionResult Login() {
            HttpContext.Session.Clear();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(User user)
        {
            domainName = "https://" + HttpContext.Request.Host.Value;
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync(domainName + "/api/Registration/Login", content))
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        var receivedUser = JsonConvert.DeserializeObject<User>(apiResponse);
                        if (receivedUser != null)
                        {
                            HttpContext.Session.SetString("IsUserLogged", "Yes");
                            HttpContext.Session.SetInt32("UserId", receivedUser.Id);
                            return View("Welcome");
                        }
                        else {
                            return View(user);
                        }
                    }
                }
            }

            return View(user);
        }

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(User user)
        {
            domainName = "https://" + HttpContext.Request.Host.Value;
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync(domainName+ "/api/Registration/SignUp", content))
                {
                    if(response.StatusCode == HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        var receivedUser = JsonConvert.DeserializeObject<User>(apiResponse);
                        HttpContext.Session.SetString("IsUserLogged", "Yes");
                        HttpContext.Session.SetInt32("UserId", receivedUser.Id);
                        return View("CompleteRegistration");
                    }
                }
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CompleteRegistration(UserAddress userAddress)
        {
            domainName = "https://" + HttpContext.Request.Host.Value;
            userAddress.User.Id = (int)HttpContext.Session.GetInt32("UserId");
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(userAddress), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync(domainName + "/api/Registration/CompleteProfile", content))
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        return View("Welcome");
                    }
                }
            }
            return View(userAddress);
        }

        
        public IActionResult Welcome()
        {
            return View();
        }

    }
}