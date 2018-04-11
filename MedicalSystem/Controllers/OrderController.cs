using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MedicalSystem.Models;
using MedicalSystem.ViewModels;
using PayPal.Payments;
using BraintreeHttp;
using PayPal.Core;
using System;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MedicalSystem.Controllers
{
    //set the route of the view connected to this controller
    [Route("Order/[controller]/[action]/{data?}")]
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly Checkout _checkout;


        //dependancy injecton constructor 
        public OrderController(IOrderRepository orderRepository, Checkout checkout)
        {
            _orderRepository = orderRepository;
            _checkout = checkout;
            
        }
        //returns the checkout page - takes parameter called data.
        [HttpGet]
        public IActionResult Checkout(string data)
        {
            var items2 = _checkout.GetShoppingCartTotal(data);
            var items = _checkout.GetEquipment(data);
            //get user information when logged in from httpcontext
            string userId = HttpContext.User.Identity.Name;
            //get the logged in user details
            var loggedInUser = _checkout.GetLoggedInUserDetails(userId);

            
            //in the variable called CheckoutViewModel create a new checkout view model object setting the properties defined.
            var CheckoutViewModel = new CheckoutViewModel
            {
                UserName = loggedInUser.UserName,
                FirstName = loggedInUser.FirstName,
                LastName = loggedInUser.LastName,
                HospitalName = loggedInUser.HospitalName,
                Email = loggedInUser.Email,
                AddressLine1 = loggedInUser.AddressLine1,
                AddressLine2 = loggedInUser.AddressLine2,
                PostCode = loggedInUser.PostCode,
                ShoppingCartItems = items,
                ShoppingCartId = data,
                ShoppingCartTotal = items2
            };

            //return the view with the variable - checkoutview passed in
            return View(CheckoutViewModel);
        }

        //post atribute
        [HttpPost]
        public IActionResult ConfirmOrder(CheckoutViewModel checkout)
        {
            //create an order object with the properties set
            var order = new Models.Order
            {

                Name = checkout.FirstName + " " + checkout.LastName,
                HospitalName = checkout.HospitalName,
                HospitalAdress = checkout.AddressLine1 + " " + checkout.AddressLine2,
                PostCode = checkout.PostCode,
                ShoppingCartId = checkout.ShoppingCartId,
                SubTotal = checkout.ShoppingCartTotal
            };

            try
            {
                var payment = CreatePayment(order);
                payment.Wait();
                //create the order 
                _orderRepository.CreateOrder(order);
               
                //then return the view.
                return View(order);
                
            }
            catch {
                return View("~/Views/Order/PaymentFailed.cshtml");

            }
                
           
        }


        //Paypal payment
        public async Task<Payment> CreatePayment(Models.Order order)
        {
            var environment = new SandboxEnvironment("AR97yArZjZHa8H0n_dMEdESg42-bGHjOKefiBPsnkmjKsdEHPaj2Q44PvqrOIYWXLEHTMUoU7qDIGiFK", "EO8bO7FIVq-JhiybhWN3rYTD-gRrvvC63q9OH2_v7zlY6gWEtB-p0kPceAn6f9TCq1Vu-D0fpGzVcx2N");
            var client = new PayPalHttpClient(environment);


            var payment = new Payment()
            {
                Intent = "sale",
               
                Transactions = new List<Transaction>()
                {
                   
                    new Transaction()
                    {
                        ItemList = new ItemList()
                        {
                            Items = new List<Item>()
                            {
                             new Item
                             {
                                 Description = "medical equipment",
                                 Price = (order.SubTotal).ToString(),
                                 Currency = "GBP",
                                 Quantity = "1",
                             }   
                            }
                        },
                        
                        Amount = new Amount()
                        {
                            Total = (order.SubTotal).ToString(),
                            
                            Currency = "GBP"
                        }
                    }
                },
                RedirectUrls = new RedirectUrls()
                {
                    CancelUrl = Url.Action("Cancel", "Order", null, Request.Scheme),
                    ReturnUrl = "https://example.com/return"
                },
                   Payer = new Payer()
                {
                       
                    PaymentMethod = "paypal"
                }
            };

           

            //send payment to PayPal
            PaymentCreateRequest request = new PaymentCreateRequest();
            request.RequestBody(payment);
            Payment result = null;
            try
            {
                HttpResponse response = await client.Execute(request);
                var statusCode = response.StatusCode;
                return response.Result<Payment>();
            }
            catch (HttpException httpException)
            {
                var statusCode = httpException.StatusCode;
                var debugId = httpException.Headers.GetValues("PayPal-Debug-Id").FirstOrDefault();
            }

            return result;
        }

        
      }
}
