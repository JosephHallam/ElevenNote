using ElevenNote.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote_Console
{
    class ProgramUI
    {
        ApplicationDbContext _context = new ApplicationDbContext();
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
                "1. Display All Notes\n" +
                "2. Display All Categories\n" +
                "3. Search Notes by ID\n" +
                "4. Search Categories by ID\n" +
                "5. Add New Note\n" +
                "6. Add New Category\n" +
                "7. Delete Note\n" +
                "8. Delete Category\n" +
                "9. Exit Program");
                string input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        break;
                    case "2":
                        break;
                    case "3":
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
        public void DisplayAllNotes()
        {
            List<Note> noteList = _context.Notes.ToList();
            string noteListAsString = string.Join(" ", noteList);
            Console.WriteLine(noteListAsString);
        }
        public void DisplayAllCategories()
        {

        }
        public Category GetCategoryById(int id)
        {

        }
        public Note GetNoteById(int id)
        {

        }
    }
}
