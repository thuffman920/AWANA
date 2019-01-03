namespace AWANA
{
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
}