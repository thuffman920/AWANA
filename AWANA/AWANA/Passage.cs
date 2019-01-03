namespace AWANA
{
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
            for (int i = 0; i < delimitation.Length; i++)
            {
                bool isSmall = false;
                for (int j = 0; j < smallWord.Length; j++)
                {
                    if (delimitation[i].Equals(smallWord[j]))
                    {
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
            for (int i = 0; i < size.Length; i++)
            {
                size[i] = delimitation[i].Length;
                index[i] = i;
            }
            MergeSort sorting = new MergeSort(size, index);
            index = sorting.getIndex();
            int len_index = (int)(delimitation.Length * this.percentage);
            int[] size_index = new int[len_index];
            for (int i = 0; i < len_index; i++)
                size_index[i] = index[delimitation.Length - 1 - i];
            for (int i = 0; i < delimitation.Length; i++)
            {
                bool isLarge = false;
                for (int j = 0; j < len_index; j++)
                {
                    if (i == size_index[j])
                    {
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
            for (int i = 0; i < passage.Length; i++)
            {
                for (int j = 0; j < delimit.Length; j++)
                {
                    if (passage[i] == delimit[j])
                    {
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
            for (int i = 0; i < count; i++)
            {
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
}
