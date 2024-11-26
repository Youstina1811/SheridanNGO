using Microsoft.AspNetCore.Mvc;
using Stripe;

public class PaymentController : Controller
{
    private readonly StripeClient _stripeClient;

    public PaymentController(StripeClient stripeClient)
    {
        _stripeClient = stripeClient;
    }

    [HttpGet] // Make sure the action is accessible with GET or POST as required
    public IActionResult CreateCharge()
    {
        var options = new PaymentIntentCreateOptions
        {
            Amount = 2000, // Amount in cents (e.g., $20.00)
            Currency = "usd",
        };
        var service = new PaymentIntentService(_stripeClient);
        PaymentIntent intent = service.Create(options);

        // Handle the response and return a simple confirmation or the intent object
        return Json(intent); // You can return a view or a JSON response for testing
    }
}
