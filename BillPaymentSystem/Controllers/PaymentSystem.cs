using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Linq;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using BillPaymentSystem.Models;
using System.Globalization;
using BillPaymentSystem;
using static BillPaymentSystem.Models.DataModel;
namespace BillPaymentSystem.Controllers
{
    [ApiController]
    [Route("api/v1/mobile-provider")]

    public class MobileProviderController : ControllerBase
    {
        private readonly BillingDbContext _context;

        public MobileProviderController(BillingDbContext context)
        {
            _context = context;
        }

        [HttpGet("QueryBill")]
        [Authorize]
        public IActionResult QueryBill(string subscriberNo, DateTime month)
        {
            
            var bill = _context.Bills.FirstOrDefault(b => b.Subscriber.SubscriberNo == subscriberNo && b.Month.Year == month.Year && b.Month.Month == month.Month);

            if (bill == null)
                return NotFound("Bill not found");

           
            return Ok(new { BillTotal = bill.TotalAmount, PaidStatus = bill.IsPaid ? "Paid" : "Unpaid" });
        }

        [HttpGet("QueryBillDetailed")]
        [Authorize]
        public IActionResult QueryBillDetailed(string subscriberNo, DateTime month, int pageNumber = 1)
        {
            
            int pageSize = 2;
            
            var bills = _context.Bills
                .Where(b => b.Subscriber.SubscriberNo == subscriberNo && b.Month == month)
                .ToList();

            if (bills.Count == 0)
                return NotFound("No bills found");

           
            int totalPages = (int)Math.Ceiling((double)bills.Count / pageSize);

           
            if (pageNumber < 1 || pageNumber > totalPages)
                return BadRequest("Invalid page number");

            int startIndex = (pageNumber - 1) * pageSize;

            
            var pageBills = bills.Skip(startIndex).Take(pageSize).ToList();

            
            var response = pageBills.Select(bill => new
            {
                BillTotal = bill.TotalAmount,
                SubscriberId = bill.SubscriberId,
                Month = bill.Month,
                IsPaid = bill.IsPaid
            });

            return Ok(response);
        }


    }

    [ApiController]
    [Route("api/v1/banking")]
    public class BankingController : ControllerBase
    {
        private readonly BillingDbContext _context;

        public BankingController(BillingDbContext context)
        {
            _context = context;
        }

        [HttpGet("QueryBill")]
        [Authorize]
        public IActionResult QueryBill(string subscriberNo)
        {
            var unpaidMonths = _context.Bills
                .Where(b => !b.IsPaid && b.Subscriber.SubscriberNo == subscriberNo)
                .Select(b => b.Month);

            if (!unpaidMonths.Any())
                return NotFound("No unpaid bills found");

            return Ok(new { BillsNotPaid = unpaidMonths });
        }
    }


    [ApiController]
        [Route("api/v1/website")]
        public class WebsiteController : ControllerBase
        {
            private readonly BillingDbContext _context;

            public WebsiteController(BillingDbContext context)
            {
                _context = context;
            }

            [HttpPost("PayBill")]
            public IActionResult PayBill(string subscriberNo, DateTime month)
            {
               
                var bill = _context.Bills.FirstOrDefault(b => b.Subscriber.SubscriberNo == subscriberNo && b.Month == month);

                if (bill == null)
                    return NotFound("Bill not found");

                try
                {
                    if (!bill.IsPaid)
                    {
                       
                        bill.IsPaid = true;
                        _context.SaveChanges();
                        return Ok("Payment Status: Successful");
                    }
                    else
                    {
                        
                        return Ok("Payment Status: Already Paid");
                    }
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"An error occurred while processing the payment: {ex.Message}");
                }
            }

        }

        [ApiController]
        [Route("api/v1/admin")]
        public class AdminController : ControllerBase
        {
            private readonly BillingDbContext _context;

            public AdminController(BillingDbContext context)
            {
                _context = context;
            }

            [HttpPost("AddBill")]
            [Authorize]
            public IActionResult AddBill(string subscriberNo, DateTime month)
            {
               
                var subscriber = _context.Subscribers.FirstOrDefault(s => s.SubscriberNo == subscriberNo);

                if (subscriber == null)
                    return NotFound("Subscriber not found");

                try
                {
                    
                    _context.Bills.Add(new Bill { SubscriberId = subscriber.Id, Month = month, TotalAmount = 260, IsPaid = false });
                    _context.SaveChanges();

                    return Ok("Bill added successfully");
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"An error occurred while adding the bill: {ex.Message}");
                }
            }
        }

    
}
