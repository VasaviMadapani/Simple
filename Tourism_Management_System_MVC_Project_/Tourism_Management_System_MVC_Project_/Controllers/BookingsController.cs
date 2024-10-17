using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using Tourism_Management_System_MVC_Project_.Models;

namespace Tourism_Management_System_MVC_Project_.Controllers
{
    public class BookingsController : Controller
    {
        private readonly HttpClient _httpClient;

        public BookingsController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // GET: Bookings/Index
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync("https://localhost:7273/api/Booking");
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var bookings = JsonConvert.DeserializeObject<List<Bookings>>(jsonString);
                return View(bookings); // Return the list of bookings
            }
            return NotFound(); // Handle case where no bookings are found
        }

        // GET: Bookings/Create
        public IActionResult Create()
        {
            return View(); // Return the view to create a new booking
        }

        // POST: Bookings/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Bookings booking)
        {
            if (ModelState.IsValid)
            {
                var jsonContent = JsonConvert.SerializeObject(booking);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("https://localhost:7273/api/Booking", content);
                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "Booking created successfully!";
                    return RedirectToAction("Index"); // Redirect to the list of bookings
                }
                ModelState.AddModelError("", "Booking creation failed");
            }
            return View(booking); // Return to the create view if there are validation errors
        }

        // GET: Bookings/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var response = await _httpClient.GetAsync($"https://localhost:7273/api/Booking/{id}");
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var booking = JsonConvert.DeserializeObject<Bookings>(jsonString);
                return View(booking); // Return the details view with booking information
            }
            return NotFound("Booking not found."); // Handle if the booking is not found
        }

        // GET: Bookings/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _httpClient.GetAsync($"https://localhost:7273/api/Booking/{id}");
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var booking = JsonConvert.DeserializeObject<Bookings>(jsonString);
                return View(booking); // Return the edit view with booking information
            }
            return NotFound("Booking not found."); // Handle if the booking is not found
        }

        // POST: Bookings/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Bookings booking)
        {
            if (id != booking.BookingId) // Ensure the booking ID matches
            {
                return BadRequest("Booking ID mismatch.");
            }
            if (ModelState.IsValid)
            {
                var jsonContent = JsonConvert.SerializeObject(booking);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"https://localhost:7273/api/Booking/{id}", content);
                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "Booking updated successfully!";
                    return RedirectToAction("Index"); // Redirect to the list of bookings
                }
                // Handle error response
                ModelState.AddModelError("", "Booking update failed. Please try again.");
            }
            return View(booking); // Return to the edit view if there are validation errors
        }

        // GET: Bookings/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.GetAsync($"https://localhost:7273/api/Booking/{id}");
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var booking = JsonConvert.DeserializeObject<Bookings>(jsonString);
                return View(booking); // Return the delete view with booking information
            }
            return NotFound("Booking not found."); // Handle if the booking is not found
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _httpClient.DeleteAsync($"https://localhost:7273/api/Booking/{id}");
            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Booking deleted successfully!";
                return RedirectToAction("Index"); // Redirect to the list of bookings
            }
            return View("Error"); // Show error page if deletion fails
        }
    }
}
