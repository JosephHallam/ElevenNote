using ElevenNote.Data;
using ElevenNote.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Services
{
    public class NoteService
    {
        private readonly Guid _userId;
        //creates a guid for the ID of the note
        public NoteService(Guid userId)
        {
            _userId = userId;
        }
        //brought in model is used to se the title and the contents of the note
        public bool CreateNote(NoteCreate model)
        {
            var entity = new Note()
            {
                //use the noteservice to create a guid to set our id equal to
                OwnerId = _userId,
                //use the brought in model to set the title and content
                Title = model.Title,
                Content = model.Content,
                //time created is whenever this method is run
                CreatedUtc = DateTimeOffset.Now
            };
            //created an instance of the DbContext, added our newly created note, entity, and saved changes.
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Notes.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }
        //creates a list of created notes in the form of a query
        public IEnumerable<NoteListItem> GetNotes()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Notes
                    .Where(e => e.OwnerId == _userId)
                    .Select(
                        e =>
                        new NoteListItem
                        {
                            NoteId = e.NoteId,
                            Title = e.Title,
                            CreatedUtc = e.CreatedUtc
                        }
                        );
                //returns the list of created notes of a user
                return query.ToArray();
            }
        }
        public NoteDetail GetNoteById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                //finding a all the notes in DbContext
                var entity =
                    ctx
                    .Notes
                    //Pulling out a single Note with the parameters of it matching the user's ID and the note's ID matching the ID we put in
                    .Single(e => e.NoteId == id && e.OwnerId == _userId);
                return
                    //returns details of the note
                    new NoteDetail
                    {
                        NoteId = entity.NoteId,
                        Title = entity.Title,
                        Content = entity.Content,
                        CreatedUtc = entity.CreatedUtc,
                        ModifiedUtc = entity.ModifiedUtc
                    };
            }
        }
        public bool UpdateNote (NoteEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                //same as GetNoteById method
                var entity =
                    ctx
                    .Notes
                    //checks if the id of the model we put in matches an existing note id from the list of notes in DbContext
                    .Single(e => e.NoteId == model.NoteId && e.OwnerId == _userId);
                //sets the note's values to the new model's values and sets the ModifiedUtc property to whenever the method is called
                entity.Title = model.Title;
                entity.Content = model.Content;
                entity.ModifiedUtc = DateTimeOffset.UtcNow;
                //returns a true/false value of whether we saved or not
                return ctx.SaveChanges() == 1;
            }
        }
    }
}
