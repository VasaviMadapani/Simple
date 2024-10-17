using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using Tourism_Management_System_MVC_Project_.Models;

namespace Tourism_Management_System_MVC_Project_.IServices
{
    public class UserController : Controller
    {
        private readonly HttpClient _httpClient;

        public UserController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // GET: Users/Index
      //  [Authorize]
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("https://localhost:7273/api/users");
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var users = JsonConvert.DeserializeObject<List<Users>>(jsonString);
                return View(users); // Return the list of users
            }
            return NotFound(); // Handle case where no users are found
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View(); // Return the view to create a new user
        }

        // POST: Users/Create
        [HttpPost]
        //[Authorize]--
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Users user)
        {
            if (ModelState.IsValid)
            {
                var jsonContent = JsonConvert.SerializeObject(user);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("https://localhost:7273/api/users/register", content);
                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "User created successfully!";
                    return RedirectToAction("Index"); // Redirect to the list of users
                }
                ModelState.AddModelError("", "User creation failed");
            }
            return View(user); // Return to the create view if there are validation errors
        }

        // GET: Users/Details/5
      //  [Authorize]
        public async Task<IActionResult> Details(int id)
        {
            var response = await _httpClient.GetAsync($"https://localhost:7273/api/users/{id}");
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var user = JsonConvert.DeserializeObject<Users>(jsonString);
                return View(user); // Return the details view with user information
            }
            return NotFound(); // Handle if the user is not found
        }
        // GET: Users/Edit/5
      //  [HttpGet]
       // [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _httpClient.GetAsync($"https://localhost:7273/api/users/{id}");
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var user = JsonConvert.DeserializeObject<Users>(jsonString);
                return View(user); // Return the edit view with user information
            }
            return NotFound("User not found."); // Handle if the user is not found
        }

        // POST: Users/Edit/5
        [HttpPost]
      //  [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Users user)
        {
            if (id != user.UserID) // Ensure the user ID matches
            {
                return BadRequest("User ID mismatch.");
            }

            if (ModelState.IsValid)
            {
                var jsonContent = JsonConvert.SerializeObject(user);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"https://localhost:7273/api/users/{id}", content);

                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "User updated successfully!";
                    return RedirectToAction("Index"); // Redirect to the list of users
                }

                // Handle error response
                ModelState.AddModelError("", "User update failed. Please try again.");
            }

            return View(user); // Return to the edit view if there are validation errors
        }
        [HttpGet]
        public   async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.GetAsync($"https://localhost:7273/api/users/{id}");
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var user = JsonConvert.DeserializeObject<Users>(jsonString);
                return View(user); // Return the edit view with user information
            }
            return NotFound("User not found."); // Handle if the user is not found
        }

        // POST: Users/Delete/5
        [HttpPost,ActionName("Delete")]
       // [Authorize]
      //  [ValidateAntiForgeryToken]
        public    async Task<IActionResult> DeleteConfirmed(int id)
            {
                var response = await _httpClient.DeleteAsync($"https://localhost:7273/api/users/{id}");
                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "User deleted successfully!";
                    return RedirectToAction("Index"); // Redirect to the list of users
                }
                return View("Error"); // Show error page if deletion fails
            }
        }
    }

