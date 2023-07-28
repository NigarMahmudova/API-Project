using EducationApp.UI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static EducationApp.UI.ViewModels.GroupVM;

namespace EducationApp.UI.Controllers
{
    public class GroupController : Controller
    {
        private HttpClient _client;
        public GroupController()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri("https://localhost:7090/api/");
        }
        public async Task<IActionResult> Index()
        {
            using (HttpClient client = new HttpClient())
            {
                using (var response = await client.GetAsync("https://localhost:7090/api/groups/all"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();

                        GroupVM vm = new GroupVM
                        {
                            Groups = JsonConvert.DeserializeObject<List<GroupVMItem>>(content)
                        };

                        return View(vm);
                    }
                }
            }

            return View("Error");
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(GroupCreateVM vm)
        {
            if (!ModelState.IsValid) return View();

            using (HttpClient client = new HttpClient())
            {
                StringContent requestContent = new StringContent(JsonConvert.SerializeObject(vm), System.Text.Encoding.UTF8, "application/json");

                using (var response = await client.PostAsync("https://localhost:7090/api/groups", requestContent))
                {
                    if (response.IsSuccessStatusCode)
                        return RedirectToAction("index");
                    else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        var errorVM = JsonConvert.DeserializeObject<ErrorVM>(responseContent);

                        foreach (var item in errorVM.Errors)
                            ModelState.AddModelError(item.Key, item.ErrorMessage);

                        return View();
                    }
                }
            }

            return View("Error");
        }

        public async Task<IActionResult> Edit(int id)
        {
            using (var response = await _client.GetAsync($"groups/{id}"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var vm = JsonConvert.DeserializeObject<GroupEditVM>(content);

                    return View(vm);
                }
            }

            return View("Error");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, GroupEditVM vm)
        {
            if (!ModelState.IsValid) return View();

            var requestContent = new StringContent(JsonConvert.SerializeObject(vm), System.Text.Encoding.UTF8, "application/json");

            using (var response = await _client.PutAsync($"groups/{id}", requestContent))
            {
                if (response.IsSuccessStatusCode)
                    return RedirectToAction("index");
                else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
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
            using (var response = await _client.DeleteAsync($"groups/{id}"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();

                    GroupVM vm = new GroupVM
                    {
                        Groups = JsonConvert.DeserializeObject<List<GroupVMItem>>(content)
                    };

                    return View(vm);
                }
            }

            return View("Error");
        }
    }
}
