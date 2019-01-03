using System;
using System.Diagnostics;
using System.IO;

namespace Console
{
    class AWANA_UI
    {
        private Reader reader;
        private Book[] listBooks;
        private Passage[] fullListPassages;
        private Passage[] listPassages;
        private string bookName;
        private int chapter;
        private int[] indexing;
        private int[] sizing;

        public AWANA_UI(string bookName, int chapter, int[] verse)
        {
            reader = new Reader();
            listBooks = reader.getBooks();
            fullListPassages = reader.getPassages();
            this.bookName = bookName;
            this.chapter = chapter;
            listPassages = new Passage[verse.Length];
            int i = 0;
            int j = 0;
            while (i < fullListPassages.Length && j < verse.Length) {
                if (fullListPassages[i].getBookName().Equals(bookName) && fullListPassages[i].getChapter() == chapter && 
                    fullListPassages[i].getVerse() == verse[j]) {
                    listPassages[j] = fullListPassages[i];
                    j++;
                }
                i++;
            }
            getFullSet();
        }

        public void setPercentage(double percentage)
        {
            int[] newIndexing = new int[sizing.Length];
            for (int i = 0; i < newIndexing.Length; i++)
                newIndexing[i] = i;
            MergeSort sorting = new MergeSort(sizing, newIndexing);
            int numbSize = (int)(sizing.Length * percentage);
            newIndexing = sorting.getIndex();
            int[] arrayIndex = new int[listPassages.Length];
            for (int i = 0; i < numbSize; i++) {
                int count = indexing[0];
                for (int j = 0; j < listPassages.Length; j++) {
                    if (newIndexing[sizing.Length - i - 1] < count) {
                        arrayIndex[j]++;
                        break;
                    }
                    count += indexing[j + 1];
                }
            }
            for (int i = 0; i < listPassages.Length; i++)
                listPassages[i].setPercentage(1.0 * arrayIndex[i] / (indexing[i] - 1));
        }

        private void getFullSet()
        {
            int count = 0;
            indexing = new int[listPassages.Length];
            for (int i = 0; i < listPassages.Length; i++) {
                int next = listPassages[i].getFillInBlankSmall().Length + 1;
                indexing[i] = next;
                count += next;
            }
            if (listPassages.Length > 1)
                sizing = new int[count + 4];
            else
                sizing = new int[count + 3];
            count = 0;
            for (int i = 0; i < listPassages.Length; i++) {
                string[] fullListing = listPassages[i].getDelimits();
                for (int j = 0; j < fullListing.Length; j++) {
                    sizing[count] = fullListing[j].Length;
                    count++;
                }
                sizing[count] = 0;
                count++;
            }
        }

        public char[] getLetters()
        {
            char[] fullLetters = new char[sizing.Length + 1];
            int count = 0;
            for (int i = 0; i < listPassages.Length; i++) {
                char[] nextLetters = listPassages[i].getLetters();
                for (int j = 0; j < nextLetters.Length; j++) {
                    fullLetters[count] = nextLetters[j];
                    count++;
                }
                fullLetters[count] = ' ';
                count++;
            }
            fullLetters[count] = '(';
            fullLetters[count + 1] = ' ';
            fullLetters[count + 2] = ':';
            if (listPassages.Length > 1) {
                fullLetters[count + 3] = '-';
                fullLetters[count + 4] = ')';
            } else
                fullLetters[count + 3] = ')';
            return fullLetters;
        }

        public string[] getFillInBlankSmall()
        {
            string[] fillInBlankSm = new string[sizing.Length + 1];
            int count = 0;
            for (int i = 0; i < listPassages.Length; i++) {
                string[] nextFillInBlankSm = listPassages[i].getFillInBlankSmall();
                for (int j = 0; j < nextFillInBlankSm.Length; j++) {
                    fillInBlankSm[count] = nextFillInBlankSm[j];
                    count++;
                }
                fillInBlankSm[count] = "";
                count++;
            }
            fillInBlankSm[count] = "";
            fillInBlankSm[count + 1] = listPassages[0].getBookName();
            fillInBlankSm[count + 2] = "" + listPassages[0].getChapter();
            fillInBlankSm[count + 3] = "" + listPassages[0].getVerse();
            if (listPassages.Length > 1)
                fillInBlankSm[count + 4] = "" + listPassages[listPassages.Length - 1].getVerse();
            return fillInBlankSm;
        }

        public string[] getFillInBlankLarge()
        {
            string[] fillInBlankLg = new string[sizing.Length + 1];
            int count = 0;
            for (int i = 0; i < listPassages.Length; i++) {
                string[] nextFillInBlankLg = listPassages[i].getFillInBlankLarge();
                for (int j = 0; j < nextFillInBlankLg.Length; j++) {
                    fillInBlankLg[count] = nextFillInBlankLg[j];
                    count++;
                }
                fillInBlankLg[count] = "";
                count++;
            }
            fillInBlankLg[count] = "";
            fillInBlankLg[count + 1] = listPassages[0].getBookName();
            fillInBlankLg[count + 2] = "" + listPassages[0].getChapter();
            fillInBlankLg[count + 3] = "" + listPassages[0].getVerse();
            if (listPassages.Length > 1)
                fillInBlankLg[count + 4] = "" + listPassages[listPassages.Length - 1].getVerse();
            return fillInBlankLg;
        }

        public string[] getRandomization()
        {
            string[] randomization = new string[sizing.Length + 1];
            int count = 0;
            for (int i = 0; i < listPassages.Length; i++) {
                string[] nextRandomization = listPassages[i].getDelimits();
                for (int j = 0; j < nextRandomization.Length; j++) {
                    randomization[count] = nextRandomization[j];
                    count++;
                }
                randomization[count] = "";
                count++;
            }
            Random rand = new Random();
            for (int i = 0; i < count; i++) {
                int randI = rand.Next(count - 1);
                if (!randomization[i].Equals("") && !randomization[randI].Equals("")) {
                    string temp = randomization[i];
                    randomization[i] = randomization[randI];
                    randomization[randI] = temp;
                }
            }
            randomization[count] = "";
            randomization[count + 1] = listPassages[0].getBookName();
            randomization[count + 2] = "" + listPassages[0].getChapter();
            randomization[count + 3] = "" + listPassages[0].getVerse();
            if (listPassages.Length > 1)
                randomization[count + 4] = "" + listPassages[listPassages.Length - 1].getVerse();
            return randomization;
        }
    }

    class MergeSort
    {
        private int[] array;
        private int[] index;

        public MergeSort(int[] array, int[] index)
        {
            this.array = array;
            this.index = index;
            mergeSorting(0, array.Length - 1);
        }

        public void merge(int left, int mid, int right)
        {
            int[] array_temp = new int[array.Length];
            int[] index_temp = new int[index.Length];
            int left_end = (mid - 1);
            int tmp_pos = left;
            int num_elements = (right - left + 1);

            while ((left <= left_end) && (mid <= right)) {
                if (array[left] <= array[mid]) {
                    array_temp[tmp_pos] = array[left];
                    index_temp[tmp_pos++] = index[left++];
                } else {
                    array_temp[tmp_pos] = array[mid];
                    index_temp[tmp_pos++] = index[mid++];
                }
            }
            while (left <= left_end) {
                array_temp[tmp_pos] = array[left];
                index_temp[tmp_pos++] = index[left++];
            }
            while (mid <= right) {
                array_temp[tmp_pos] = array[mid];
                index_temp[tmp_pos++] = index[mid++];
            }
            for (int i = 0; i < num_elements; i++) {
                array[right] = array_temp[right];
                index[right] = index_temp[right];
                right--;
            }
        }

        public void mergeSorting(int left, int right)
        {
            if (right > left) {
                int mid = (right + left) / 2;
                mergeSorting(left, mid);
                mergeSorting((mid + 1), right);
                merge(left, (mid + 1), right);
            }
        }

        public int[] getArray()
        {
            return this.array;
        }

        public int[] getIndex()
        {
            return this.index;
        }
    }

    class Book
    {
        private string bookName;
        private int[] chapters;

        public Book(string bookName, int[] chapters)
        {
            this.bookName = bookName;
            this.chapters = chapters;
        }

        public string getBookName()
        {
            return this.bookName;
        }

        public void setBookName(string bookName)
        {
            this.bookName = bookName;
        }

        public int[] getChapters()
        {
            return this.chapters;
        }

        public void setChapters(int[] chapters)
        {
            this.chapters = chapters;
        }
    }

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
            while ((next = reader.ReadLine()) != null) {
                string passage = next.Substring(0, next.IndexOf("\t"));
                next = next.Substring(next.IndexOf("\t") + 1, next.Length - next.IndexOf("\t") - 1);
                string book = passage.Substring(0, passage.LastIndexOf(" "));
                if (!prev_Book.Equals(book)) {
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
                if (prev_Chapter != chapter) {
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

    class Passage
    {
        private string bookName;
        private int chapter;
        private int verse;
        private string passage;
        private char[] letters;
        private string[] delimitation;
        private double percentage;

        public Passage(string bookName, int chapter, int verse, string passage)
        {
            this.bookName = bookName;
            this.chapter = chapter;
            this.verse = verse;
            this.passage = passage;
            this.getDelimitation();
        }

        public string getBookName()
        {
            return bookName;
        }

        public int getChapter()
        {
            return chapter;
        }

        public int getVerse()
        {
            return verse;
        }

        public string[] getDelimits()
        {
            return delimitation;
        }

        public void setPercentage(double percentage)
        {
            this.percentage = percentage;
        }

        public char[] getLetters()
        {
            return letters;
        }

        public string[] getFillInBlankSmall()
        {
            string[] result = new string[delimitation.Length];
            string[] smallWord = { "the", "The", "of", "Of", "A", "a", "and", "And" };
            for (int i = 0; i < delimitation.Length; i++) {
                bool isSmall = false;
                for (int j = 0; j < smallWord.Length; j++) {
                    if (delimitation[i].Equals(smallWord[j])) {
                        result[i] = delimitation[i];
                        isSmall = true;
                        break;
                    }
                }
                if (!isSmall && delimitation[i].Length != 0)
                    result[i] = "_______";
            }
            return result;
        }

        public string[] getFillInBlankLarge()
        {
            string[] result = new string[delimitation.Length];
            int[] size = new int[delimitation.Length];
            int[] index = new int[delimitation.Length];
            for (int i = 0; i < size.Length; i++) {
                size[i] = delimitation[i].Length;
                index[i] = i;
            }
            MergeSort sorting = new MergeSort(size, index);
            index = sorting.getIndex();
            int len_index = (int)(delimitation.Length * this.percentage);
            int[] size_index = new int[len_index];
            for (int i = 0; i < len_index; i++)
                size_index[i] = index[delimitation.Length - 1 - i];
            for (int i = 0; i < delimitation.Length; i++) {
                bool isLarge = false;
                for (int j = 0; j < len_index; j++) {
                    if (i == size_index[j]) {
                        result[i] = delimitation[i];
                        isLarge = true;
                        break;
                    }
                }
                if (!isLarge && !delimitation[i].Equals(""))
                    result[i] = "_______";
            }
            return result;
        }

        private void getDelimitation()
        {
            string[] result = new string[passage.Length];
            char[] new_letters = new char[passage.Length];
            char[] delimit = { ' ', ',', '.', ';', ':', '?', '!', '(', ')' };
            int index = 0;
            int count = 0;
            for (int i = 0; i < passage.Length; i++) {
                for (int j = 0; j < delimit.Length; j++) {
                    if (passage[i] == delimit[j]) {
                        result[count] = passage.Substring(index, i - index);
                        new_letters[count] = passage[i];
                        index = i + 1;
                        count++;
                        break;
                    }
                }
            }
            delimitation = new string[count];
            letters = new char[count];
            for (int i = 0; i < count; i++) {
                delimitation[i] = result[i];
                letters[i] = new_letters[i];
            }
        }

        public string toString()
        {
            string result = passage + " (" + this.bookName + " " + this.chapter + ":" + this.verse + ")";
            return result;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            int[] verses = { 16, 17, 18 };
            AWANA_UI user_interface = new AWANA_UI("John", 3, verses);
            char[] fullLetters = user_interface.getLetters();
            user_interface.setPercentage(.2);
            string[] fillInBlankLg = user_interface.getFillInBlankLarge();
            for (int i = 0; i < fillInBlankLg.Length; i++)
                Debug.Write(fillInBlankLg[i] + fullLetters[i]);
            Debug.WriteLine("");
            string[] fillInBlankSm = user_interface.getFillInBlankSmall();
            for (int i = 0; i < fillInBlankSm.Length; i++)
                Debug.Write(fillInBlankSm[i] + fullLetters[i]);
            Debug.WriteLine("");
            string[] randomization = user_interface.getRandomization();
            for (int i = 0; i < randomization.Length; i++)
                Debug.Write(randomization[i] + fullLetters[i]);
            Debug.WriteLine("");
        }
    }
}