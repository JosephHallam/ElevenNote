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
    }
}
