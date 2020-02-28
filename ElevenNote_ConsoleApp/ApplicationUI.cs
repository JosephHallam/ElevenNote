using Braintree;
using ElevenNote.Data;
using ElevenNote.Models;
using ElevenNote.Services;
using ElevenNote.WebAPI.Models;
using ImTools;
using Microsoft.AspNet.WebHooks.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote_Console
{
    public class ApplicationUI
    {
        Guid _userID;
        string emailInput;
        public void Run()
        {
            RunMenu();
        }
        public void RunMenu()
        {
            bool isRunning = true;
            while (isRunning)
            {
                Console.Clear();
                Console.WriteLine($"Welcome to the ElevenNote Note-Taking System. What would you like to do?         Logged In As:{emailInput}\n" +
                    $"1. Login\n" +
                    "2. Display All Notes\n" +
                    "3. Display All Categories\n" +
                    "4. Search Notes by ID\n" +
                    "5. Search Categories by ID\n" +
                    "6. Add New Note\n" +
                    "7. Add New Category\n" +
                    "8. Delete Note\n" +
                    "9. Delete Category\n" +
                    "10. Exit Program");
                string input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        Console.WriteLine("Enter your email (e.g. johnsmith@gmail.com)");
                        emailInput = Console.ReadLine();
                        Login(emailInput);
                        break;
                    case "2":
                        DisplayAllNotes();
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    case "3": DisplayAllCategories();
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    case "4":
                        Console.WriteLine("Enter the ID of the note you want to view");
                        int noteId = Int32.Parse(Console.ReadLine());
                        GetNoteById(noteId);
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    case "5":
                        Console.WriteLine("Enter the username you want to search for");
                        string username = Console.ReadLine();
                        GetUserID(username);
                        break;
                    case "6":
                        Console.WriteLine("Please enter the title of the Note.");
                        string title = Console.ReadLine();
                        Console.WriteLine("Great. Please enter the id of the category of the note.");
                        int id = Int32.Parse(Console.ReadLine());
                        Console.WriteLine("Last step! What are the contents of the note?");
                        string content = Console.ReadLine();
                        AddNote(title,id,content);
                        Console.WriteLine("Great, your note was added. Press any key to continue...");
                        Console.ReadLine();
                        break;
                    case "7":
                        break;
                    case "8":
                        break;
                    case "9":
                        break;
                    case "10":
                        isRunning = false;
                        break;
                    default:
                        Console.WriteLine("Please enter a number 1-9");
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadLine();
                        Console.Clear();
                        break;
                }
            }
        }
        public void Login(string emailInput)
        {
            Guid result;
            using (MD5 md5 = MD5.Create())
            {
                byte[] hash = md5.ComputeHash(Encoding.Default.GetBytes(emailInput));
                result = new Guid(hash);
            }
            _userID = result;
            Console.WriteLine("Ok. Logged In.");
            Console.WriteLine("Press any key to continue...");
            Console.ReadLine();
        }
        public NoteService GetNoteService()
        {
            NoteService noteService = new NoteService(_userID);
            return noteService;
        }
        public CategoryService GetCategoryService()
        {
            CategoryService categoryService = new CategoryService(_userID);
            return categoryService;
        }
        public void DisplayAllNotes()
        {
            var noteService = GetNoteService();
            var entity = noteService.GetNotes();
            List<string> noteTitleList = new List<string>();
            foreach (NoteListItem n in entity)
            {
                noteTitleList.Add(Convert.ToString(n.NoteId));
                noteTitleList.Add(n.Title);
            }
            string noteTitleListAsString = string.Join(" ", noteTitleList);
            Console.WriteLine(noteTitleListAsString);
        }
        public void DisplayAllCategories()
        {

        }
        public void GetCategoryById(int id)
        {

        }
        public void GetNoteById(int id)
        {
            var service = GetNoteService();
            var entity = service.GetNoteById(id);
            Console.WriteLine(entity.Title);
            Console.WriteLine(entity.Content);
        }
        public void AddNote(string title, int id, string content)
        {
            NoteCreate model = new NoteCreate
            {
                CategoryId = id,
                Title = title,
                Content = content
            };
            var service = GetNoteService();
            service.CreateNote(model);
        }

        public Guid GetUserID(string username)
        {
            string userId;
            using (var ctx = new ApplicationDbContext())
            {
                userId = ctx.Users.Where(e => e.UserName == username).Single().Id;
            }
            Guid id = Guid.Parse(userId);
            return id;
        }
        public void RegisterUser(RegisterBindingModel)
    }
}
