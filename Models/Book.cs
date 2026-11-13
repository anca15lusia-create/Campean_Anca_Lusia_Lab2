using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Campean_Anca_Lusia_Lab2.Models
{
    public class Book
    {
        public int ID { get; set; }

        [Display(Name = "Book Title")]
        [Required(ErrorMessage = "Titlul cartii este obligatoriu.")]
        [StringLength(150, MinimumLength = 3, ErrorMessage = "Titlul trebuie sa aiba intre 3 si 150 de caractere.")]
        public string Title { get; set; } = string.Empty;

        [Display(Name = "Author")]
        public int? AuthorID { get; set; }

        public Authors? Author { get; set; }

        [Column(TypeName = "decimal(6, 2)")]
        [Range(typeof(decimal), "0.01", "500", ErrorMessage = "Pretul trebuie sa fie intre 0.01 si 500.")]
        public decimal Price { get; set; }

        [DataType(DataType.Date)]
        public DateTime PublishingDate { get; set; }

        public int? PublisherID { get; set; }

        public Publisher? Publisher { get; set; }

        public ICollection<BookCategory>? BookCategories { get; set; }
    }
}