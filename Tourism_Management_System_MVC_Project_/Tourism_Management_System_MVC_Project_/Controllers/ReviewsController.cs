using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using Tourism_Management_System_MVC_Project_.Models;

namespace Tourism_Management_System_MVC_Project_.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly HttpClient _httpClient;

        public ReviewsController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // GET: Reviews/Index
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("https://localhost:7273/api/reviews");
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var reviews = JsonConvert.DeserializeObject<List<Reviews>>(jsonString);
                return View(reviews); // Return the list of reviews
            }
            return NotFound(); // Handle case where no reviews are found
        }

        // GET: Reviews/Create
        public IActionResult Create()
        {
            return View(); // Return the view to create a new review
        }

        // POST: Reviews/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Reviews review)
        {
            if (ModelState.IsValid)
            {
                var jsonContent = JsonConvert.SerializeObject(review);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("https://localhost:7273/api/reviews", content);
                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "Review created successfully!";
                    return RedirectToAction("Index"); // Redirect to the list of reviews
                }
                ModelState.AddModelError("", "Review creation failed");
            }
            return View(review); // Return to the create view if there are validation errors
        }

        // GET: Reviews/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var response = await _httpClient.GetAsync($"https://localhost:7273/api/reviews/{id}");
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var review = JsonConvert.DeserializeObject<Reviews>(jsonString);
                return View(review); // Return the details view with review information
            }
            return NotFound("Review not found."); // Handle if the review is not found
        }

        // GET: Reviews/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _httpClient.GetAsync($"https://localhost:7273/api/reviews/{id}");
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var review = JsonConvert.DeserializeObject<Reviews>(jsonString);
                return View(review); // Return the edit view with review information
            }
            return NotFound("Review not found."); // Handle if the review is not found
        }

        // POST: Reviews/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Reviews review)
        {
            if (id != review.ReviewId) // Ensure the review ID matches
            {
                return BadRequest("Review ID mismatch.");
            }
            if (ModelState.IsValid)
            {
                var jsonContent = JsonConvert.SerializeObject(review);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"https://localhost:7273/api/reviews/{id}", content);
                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "Review updated successfully!";
                    return RedirectToAction("Index"); // Redirect to the list of reviews
                }
                // Handle error response
                ModelState.AddModelError("", "Review update failed. Please try again.");
            }
            return View(review); // Return to the edit view if there are validation errors
        }

        // GET: Reviews/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.GetAsync($"https://localhost:7273/api/reviews/{id}");
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var review = JsonConvert.DeserializeObject<Reviews>(jsonString);
                return View(review); // Return the delete view with review information
            }
            return NotFound("Review not found."); // Handle if the review is not found
        }

        // POST: Reviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _httpClient.DeleteAsync($"https://localhost:7273/api/reviews/{id}");
            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Review deleted successfully!";
                return RedirectToAction("Index"); // Redirect to the list of reviews
            }
            return View("Error"); // Show error page if deletion fails
        }
    }
}
