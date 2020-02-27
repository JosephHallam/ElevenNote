using ElevenNote.Data;
using ElevenNote.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Services
{
    public class CategoryService
    {
        private readonly Guid _userId;
        public CategoryService(Guid userId)
        {
            _userId = userId;
        }
        public bool CreateCategory(CategoryItem_Create_Edit model)
        {
            var entity = new Category()
            {
                //use the categoryService to create a guid to set our id equal to
                OwnerId = _userId,
                Name = model.Name,
            };
            //created an instance of the DbContext, added our newly created category, entity, and saved changes.
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Categories.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }
        public IEnumerable<CategoryItem_Create_Edit> GetCategories()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Categories
                    .Where(e => e.OwnerId == _userId)
                    .Select(
                        e =>
                        new CategoryItem_Create_Edit
                        {
                            Id = e.Id,
                            Name = e.Name,
                        }
                        );
                //returns the list of created categories of a user
                return query.ToArray();
            }
        }
        public CategoryItem_Create_Edit GetCategoryById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                //finding a all the notes in DbContext
                var entity =
                    ctx
                    .Categories
                    //Pulling out a single Note with the parameters of it matching the user's ID and the note's ID matching the ID we put in
                    .Single(e => e.Id == id && e.OwnerId == _userId);
                return
                    //returns details of the note
                    new CategoryItem_Create_Edit
                    {
                        Id = entity.Id,
                        Name = entity.Name,
                    };
            }
        }
        public bool UpdateCategory(CategoryItem_Create_Edit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                //same as GetNoteById method
                var entity =
                    ctx
                    .Categories
                    //checks if the id of the model we put in matches an existing category id from the list of categories in DbContext
                    .Single(e => e.Id == model.Id && e.OwnerId == _userId);
                //sets the category's values to the new model's values
                entity.Name = model.Name;
                //returns a true/false value of whether we saved or not
                return ctx.SaveChanges() == 1;
            }
        }
        public bool DeleteCategory(int categoryId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                //
                var entity =
                    ctx
                    .Categories
                    .Single(e => e.Id == categoryId && e.OwnerId == _userId);
                ctx.Categories.Remove(entity);
                return ctx.SaveChanges() == 1;
            }
        }
    }
}
