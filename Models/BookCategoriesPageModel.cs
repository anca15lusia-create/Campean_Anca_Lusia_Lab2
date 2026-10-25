using Microsoft.AspNetCore.Mvc.RazorPages;
using Campean_Anca_Lusia_Lab2.Models;

namespace Campean_Anca_Lusia_Lab2.Models
{
    public class BookCategoriesPageModel : PageModel
    {
        public List<AssignedCategoryData> AssignedCategoryDataList
        { get; set; } = new();

        public void PopulateAssignedCategoryData(
            Campean_Anca_Lusia_Lab2Context context,
            Book book)
        {
            var allCategories = context.Category.ToList();

            var bookCategories = book.BookCategories?
                .Select(bc => bc.CategoryID)
                .ToHashSet() ?? new HashSet<int>();

            AssignedCategoryDataList = allCategories
                .Select(category => new AssignedCategoryData
                {
                    CategoryID = category.ID,
                    Name = category.CategoryName,
                    Assigned = bookCategories.Contains(category.ID)
                })
                .ToList();
        }

        public void UpdateBookCategories(
            Campean_Anca_Lusia_Lab2Context context,
            string[]? selectedCategories,
            Book bookToUpdate)
        {
            bookToUpdate.BookCategories ??= new List<BookCategory>();

            var selectedIDs = (selectedCategories ?? Array.Empty<string>())
                .Select(int.Parse)
                .ToHashSet();

            var existingIDs = bookToUpdate.BookCategories
                .Select(bc => bc.CategoryID)
                .ToHashSet();

            foreach (var category in context.Category.ToList())
            {
                if (selectedIDs.Contains(category.ID))
                {
                    if (!existingIDs.Contains(category.ID))
                    {
                        bookToUpdate.BookCategories.Add(new BookCategory
                        {
                            BookID = bookToUpdate.ID,
                            CategoryID = category.ID
                        });
                    }
                }
                else
                {
                    var categoryToRemove = bookToUpdate.BookCategories
                        .FirstOrDefault(bc => bc.CategoryID == category.ID);

                    if (categoryToRemove != null)
                    {
                        bookToUpdate.BookCategories.Remove(categoryToRemove);
                        context.Remove(categoryToRemove);
                    }
                }
            }
        }
    }
}