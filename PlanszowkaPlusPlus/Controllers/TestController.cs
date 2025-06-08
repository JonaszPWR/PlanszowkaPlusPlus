using Microsoft.AspNetCore.Mvc;
using PlanszowkaPlusPlus.Services;

namespace PlanszowkaPlusPlus.Controllers
{
    public class TestController : Controller
    {
        private readonly EmailService _emailService;

        public TestController(EmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpGet("test-mail")]
        public async Task<IActionResult> SendTestMail()
        {
            await _emailService.SendEmailAsync(
                "danrum12w3@gmail.com", // <- wpisz adres, na który ma przyjść testowy mail
                "Test e-mail z Planszówki",
                "Gratulacje! To jest testowa wiadomość e-mail z Twojej aplikacji ASP.NET."
            );

            return Ok("E-mail został wysłany.");
        }
    }
}
