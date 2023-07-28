using EducationApp.UI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static EducationApp.UI.ViewModels.GroupVM;
using static EducationApp.UI.ViewModels.StudentVM;

namespace EducationApp.UI.Controllers
{
    public class StudentController : Controller
    {
        private HttpClient _client;
        public StudentController()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri("https://localhost:7090/api/");
        }
        public async Task<IActionResult> Index()
        {
            using (var response = await _client.GetAsync($"products/all"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();

                    StudentVM vm = new StudentVM
                    {
                        Students = JsonConvert.DeserializeObject<List<StudentVMItem>>(content)
                    };

                    return View(vm);
                }
            }

            return View("Error");
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Groups = await _getGroups();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(StudentCreateVM vm)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Groups = await _getGroups();
                return View();
            }

            StringContent requestContent = new StringContent(JsonConvert.SerializeObject(vm), System.Text.Encoding.UTF8, "application/json");

            using (var response = await _client.PostAsync("products", requestContent))
            {
                if (response.IsSuccessStatusCode)
                    return RedirectToAction("index");
                else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    ViewBag.Groups = await _getGroups();
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var errorVM = JsonConvert.DeserializeObject<ErrorVM>(responseContent);
                    foreach (var item in errorVM.Errors)
                        ModelState.AddModelError(item.Key, item.ErrorMessage);

                    return View();
                }
            }

            return View("error");
        }

        public async Task<IActionResult> Edit(int id)
        {
            using (var response = await _client.GetAsync($"students/{id}"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var vm = JsonConvert.DeserializeObject<StudentEditVM>(content);

                    ViewBag.Groups = await _getGroups();
                    return View(vm);
                }
            }

            return View("Error");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, StudentEditVM vm)
        {
            if (!ModelState.IsValid) return View();

            var requestContent = new StringContent(JsonConvert.SerializeObject(vm), System.Text.Encoding.UTF8, "application/json");

            using (var response = await _client.PutAsync($"students/{id}", requestContent))
            {
                if (response.IsSuccessStatusCode)
                    return RedirectToAction("index");
                else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    ViewBag.Groups = await _getGroups();
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var errorVM = JsonConvert.DeserializeObject<ErrorVM>(responseContent);

                    foreach (var item in errorVM.Errors)
                        ModelState.AddModelError(item.Key, item.ErrorMessage);

                    return View();
                }
            }

            return View("error");
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            using (var response = await _client.DeleteAsync($"students/{id}"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();

                    StudentVM vm = new StudentVM
                    {
                        Students = JsonConvert.DeserializeObject<List<StudentVMItem>>(content)
                    };

                    return View(vm);
                }
            }

            return View("Error");
        }

        private async Task<List<GroupVMItem>> _getGroups()
        {
            List<GroupVMItem> data = new List<GroupVMItem>();
            using (var response = await _client.GetAsync("brands/all"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    data = JsonConvert.DeserializeObject<List<GroupVMItem>>(content);
                }
            }
            return data;
        }

    }
}
