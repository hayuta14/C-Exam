using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Exam_C_2.Dto
{
    public class BookRequestDto
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public decimal PricePerDay { get; set; }
    }
    public class CustomerRequestDto
    {
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
    }

    public class RentalRequestDto
    {
        [Required]
        public int CustomerId { get; set; }
        [Required]
        public int ComicBookId { get; set; }
        [Required]
        public DateTime ReturnDate { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public int PricePerDay { get; set; }
    }

    public class RentedBookRequestDto
    {
        [Required]
        public DateTime RentalDate { get; set; }
        [Required]
        public DateTime ReturnDate { get; set; }
    }
 }
