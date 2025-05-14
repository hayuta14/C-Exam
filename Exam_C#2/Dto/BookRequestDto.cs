using System.ComponentModel.DataAnnotations;

namespace Exam_C_2.Dto
{
    public class BookRequestDto
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public decimal PricePerDay { get; set; }
    }
}
