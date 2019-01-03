using System;
using System.IO;

namespace AWANA
{
    class Reader
    {
        private Book[] books;
        private Passage[] passages;

        public Reader()
        {
            books = new Book[66];
            readInLines();
        }

        private void readInLines()
        {
            StreamReader reader = new StreamReader("C:\\Users\\thuffman\\Documents\\Visual Studio 2017\\Projects\\AWANA\\bible.txt");
            int count = 0;
            int count_Book = 0;
            string next = "";
            while ((next = reader.ReadLine()) != null)
                count++;
            reader = new StreamReader("C:\\Users\\thuffman\\Documents\\Visual Studio 2017\\Projects\\AWANA\\bible.txt");
            passages = new Passage[count];
            count = 0;
            string prev_Book = "Genesis";
            int prev_Chapter = 1;
            int prev_Verse = 1;
            int[] max_chapters = new int[150];
            while ((next = reader.ReadLine()) != null)
            {
                string passage = next.Substring(0, next.IndexOf("\t"));
                next = next.Substring(next.IndexOf("\t") + 1, next.Length - next.IndexOf("\t") - 1);
                string book = passage.Substring(0, passage.LastIndexOf(" "));
                if (!prev_Book.Equals(book))
                {
                    max_chapters[prev_Chapter - 1] = prev_Verse;
                    int[] newMax_Chapters = new int[prev_Chapter];
                    for (int i = 0; i < prev_Chapter; i++)
                        newMax_Chapters[i] = max_chapters[i];
                    Book newBook = new Book(prev_Book, newMax_Chapters);
                    books[count_Book] = newBook;
                    count_Book++;
                    max_chapters = new int[150];
                    prev_Chapter = 1;
                    prev_Book = book;
                }
                passage = passage.Substring(passage.LastIndexOf(" ") + 1, passage.Length - passage.LastIndexOf(" ") - 1);
                int chapter = Convert.ToInt32(passage.Substring(0, passage.IndexOf(":")));
                if (prev_Chapter != chapter)
                {
                    max_chapters[prev_Chapter - 1] = prev_Verse;
                    prev_Chapter = chapter;
                    prev_Verse = 1;
                }
                passage = passage.Substring(passage.IndexOf(":") + 1, passage.Length - passage.IndexOf(":") - 1);
                prev_Verse = Convert.ToInt32(passage);
                Passage newPassage = new Passage(book, chapter, prev_Verse, next);
                passages[count] = newPassage;
                count++;
            }
            max_chapters[prev_Chapter - 1] = prev_Verse;
            int[] newMax_Chapters1 = new int[prev_Chapter];
            for (int i = 0; i < prev_Chapter; i++)
                newMax_Chapters1[i] = max_chapters[i];
            Book newBook1 = new Book(prev_Book, newMax_Chapters1);
            books[count_Book] = newBook1;
            reader.Close();
        }

        public Passage[] getPassages()
        {
            return this.passages;
        }

        public Book[] getBooks()
        {
            return this.books;
        }
    }
}
