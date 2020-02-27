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
    public class NoteController : ApiController
    {
        private NoteService CreateNoteService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var noteService = new NoteService(userId);
            return noteService;
        }
        [HttpGet]
        public IHttpActionResult Get()
        {
            NoteService noteService = CreateNoteService();
            var notes = noteService.GetNotes();
            return Ok(notes);
        }
        [HttpPost]
        public IHttpActionResult Post(NoteCreate note)
        {
            //returns a bad request if you send the wrong kind of model
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var service = CreateNoteService();
            //returns an internal server error if a note cannot be created from the valid model
            if (!service.CreateNote(note))
            {
                return InternalServerError();
            }
            //a note was created from the valid model
            return Ok();
        }
        public IHttpActionResult Get(int id)
        {
            NoteService noteService = CreateNoteService();
            var note = NoteService.GetNoteById(id);
            return Ok(note);
        }
    }
}
