using Exam_C_2.Context;
using Exam_C_2.Dto;
using Exam_C_2.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Exam_C_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalController : ControllerBase
    {
        private readonly ApplicationContextDb _context;
        public RentalController(ApplicationContextDb contextDb) {
            _context = contextDb;
        }
        // GET: api/<RentalController>
        [HttpGet("{value}")]
        public async Task<IActionResult> Get([FromRoute] RentedBookRequestDto value)
        {
            var report = (from rental in _context.Rentals
                          join rentalDetail in _context.RentalDetails on rental.RentalId equals rentalDetail.RentalId
                          join book in _context.ComicBooks on rentalDetail.ComicBookId equals book.ComicBookId
                          join customer in _context.Customers on rental.CustomerId equals customer.CustomerId
                          where rental.RentalDate >= value.RentalDate && rental.RentalDate <= value.ReturnDate
                          select new RentalResponseDTO
                          {
                              BookName = book.Title,
                              RentalDate = rental.RentalDate,
                              ReturnDate = rental.ReturnDate,
                              CustomerName = customer.FullName,
                              Quantity = rentalDetail.Quantity
                          }).ToList();
            return Ok(report);
        }


        // POST api/<RentalController>
        [HttpPost("rent")]
        public async Task<string> Post([FromBody] RentalRequestDto value)
        {
            Rental newRental = new Rental();
            RentalDetail newRentalDetail = new RentalDetail();
            Customer customer = await _context.Customers.FindAsync(value.CustomerId);
            ComicBook comicBook = await _context.ComicBooks.FindAsync(value.ComicBookId);
            newRental.CustomerId = value.CustomerId;
            newRental.RentalDate = DateTime.Now;
            newRental.ReturnDate = value.ReturnDate;
            newRental.Status = "Rented";

            var rentalEntry = await _context.Rentals.AddAsync(newRental);
            Rental rental = rentalEntry.Entity;

            newRentalDetail.RentalId = rental.RentalId;
            newRentalDetail.PricePerDay = comicBook.PricePerDay;
            newRentalDetail.Quantity = value.Quantity;
            newRentalDetail.ComicBookId = value.ComicBookId;
            var rentalDetailEntry = await _context.RentalDetails.AddAsync(newRentalDetail);
            // Save changes to the database
            await _context.SaveChangesAsync();

            return "Rental created successfully";
        }
    }
}
