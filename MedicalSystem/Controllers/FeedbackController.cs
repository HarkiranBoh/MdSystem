using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MedicalSystem.Models;
using Microsoft.AspNetCore.Authorization;

namespace MedicalSystem.Controllers
{
    
    public class FeedbackController : Controller
    {
        private readonly IFeedbackRepository _feedbackRepository;

        //dependancy injection add the interface Feedback repository as parameter
        public FeedbackController(IFeedbackRepository feedbackRepository)
        {
            _feedbackRepository = feedbackRepository;

        }

        //the index method returns the index view in the feedback folder view
        public IActionResult Index()
        {
            return View();
        }
        //Http post method
        [HttpPost]
        //takes the feedback parameter
        public IActionResult Index(Feedback feedback)
        {
            {
                //checks of model state or entered data is valid through model binding
                if (ModelState.IsValid)
                {
                    //add feedback to the database 
                    _feedbackRepository.AddFeedback(feedback);
                    //return to the FeedbackComplete view
                    return RedirectToAction("FeedbackComplete");
                }
                return View(feedback);
            }
        }
        //return the feedback complete view
        public IActionResult FeedbackComplete()
        {
            return View();
        }
    }
}