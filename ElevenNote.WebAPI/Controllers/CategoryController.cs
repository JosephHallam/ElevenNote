using ElevenNote.Models;
using ElevenNote.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ElevenNote.WebAPI.Controllers
{
        [Authorize]
    public class CategoryController : ApiController
    {
        private CategoryService CreateCategoryService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var noteService = new CategoryService(userId);
            return noteService;
        }
        public IHttpActionResult Get()
        {
            CategoryService categoryService = CreateCategoryService();
            var categories = categoryService.GetCategories();
            return Ok(categories);
        }

        public IHttpActionResult Post(CategoryItem_Create_Edit category)
        {
            //returns a bad request if you send the wrong kind of model
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var service = CreateCategoryService();
            //returns an internal server error if a note cannot be created from the valid model
            if (!service.CreateCategory(category))
            {
                return InternalServerError();
            }
            //a note was created from the valid model
            return Ok();
        }
        //references the GetNoteById method in NoteService to send the user information about the note
        public IHttpActionResult Get(int id)
        {
            CategoryService categoryService = CreateCategoryService();
            var category = categoryService.GetCategoryById(id);
            return Ok(category);
        }
        public IHttpActionResult Put(CategoryItem_Create_Edit note)
        {
            //if the user inputted model is not valid
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var service = CreateCategoryService();
            //if the DbContext did not save the changes we made for some reason
            if (!service.UpdateCategory(note))
            {
                return InternalServerError();
            }
            //good :)
            return Ok();
        }
        public IHttpActionResult Delete(int id)
        {
            var service = CreateCategoryService();
            if (!service.DeleteCategory(id))
            {
                return InternalServerError();
            }
            return Ok();
        }
    }
}
