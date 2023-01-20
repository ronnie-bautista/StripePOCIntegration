using Microsoft.AspNetCore.Mvc;
using Stripe;
using StripePOCIntegration.ApplicationShared;
using StripePOCIntegration.Models;
using System.Diagnostics;

namespace StripePOCIntegration.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStripeAppService _stripeService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IStripeAppService stripeService, ILogger<HomeController> logger)
        {
            _stripeService = stripeService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(PaymentIntentCreateRequest request)
        {
            var paymentIntentService = new PaymentIntentService();
            var paymentIntent = paymentIntentService.Create(new PaymentIntentCreateOptions
            {
                Amount = CalculateOrderAmount(request.Items),
                Currency = "usd",
                AutomaticPaymentMethods = new PaymentIntentAutomaticPaymentMethodsOptions
                {
                    Enabled = true,
                },
            });

            return Json(new { clientSecret = paymentIntent.ClientSecret });
        }

        private int CalculateOrderAmount(Item[] items)
        {
            // Replace this constant with a calculation of the order's amount
            // Calculate the order total on the server to prevent
            // people from directly manipulating the amount on the client
            return 1400;
        }

        public async Task<ActionResult<StripeCustomer>> AddStripeCustomer([FromBody] AddStripeCustomer customer, CancellationToken ct)
        {
            StripeCustomer createdCustomer = await _stripeService.AddStripeCustomerAsync(
                customer,
                ct);

            return StatusCode(StatusCodes.Status200OK, createdCustomer);
        }

        [HttpPost]
        public async Task<ActionResult<StripePayment>> AddStripePayment(AddStripePayment payment, CancellationToken ct)
        {
            StripePayment createdPayment = await _stripeService.AddStripePaymentAsync(
                payment,
                ct);

            return StatusCode(StatusCodes.Status200OK, createdPayment);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}