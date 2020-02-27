using ElevenNote.Data;
using ElevenNote.Models;
using ElevenNote.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote_Console
{
    public class ApplicationUI
    {
        Guid _userID;
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
                System.Console.WriteLine("Welcome to the ElevenNote Note-Taking System. What would you like to do?\n" +
                    "1. Login\n" +
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
                        string userInput = Console.ReadLine();
                        Login(userInput);
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
                        break;
                    case "5":
                        break;
                    case "6":
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
            _userID = Guid.Parse(emailInput);
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
                noteTitleList.Add(n.Title);
            }
            string noteTitleListAsString = string.Join(" ", noteTitleList);
            Console.WriteLine(noteTitleListAsString);
        }
        public void DisplayAllCategories()
        {
            List<Category> categoryList = _context.Categories.ToList();
            List<string> categoryNameList = new List<string>();
            foreach (Category c in categoryList)
            {
                categoryNameList.Add(c.Name);
            }
            string categoryListAsString = string.Join(" ", categoryNameList);
            Console.WriteLine(categoryListAsString);
        }
        public void GetCategoryById(int id)
        {

        }
        public void GetNoteById(int id)
        {

        }
    }
}
