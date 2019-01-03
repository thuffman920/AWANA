using System;

namespace AWANA
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
            while (i < fullListPassages.Length && j < verse.Length)
            {
                if (fullListPassages[i].getBookName().Equals(bookName) && fullListPassages[i].getChapter() == chapter &&
                    fullListPassages[i].getVerse() == verse[j])
                {
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
            for (int i = 0; i < numbSize; i++)
            {
                int count = indexing[0];
                for (int j = 0; j < listPassages.Length; j++)
                {
                    if (newIndexing[sizing.Length - i - 1] < count)
                    {
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
            for (int i = 0; i < listPassages.Length; i++)
            {
                int next = listPassages[i].getFillInBlankSmall().Length + 1;
                indexing[i] = next;
                count += next;
            }
            if (listPassages.Length > 1)
                sizing = new int[count + 4];
            else
                sizing = new int[count + 3];
            count = 0;
            for (int i = 0; i < listPassages.Length; i++)
            {
                string[] fullListing = listPassages[i].getDelimits();
                for (int j = 0; j < fullListing.Length; j++)
                {
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
            for (int i = 0; i < listPassages.Length; i++)
            {
                char[] nextLetters = listPassages[i].getLetters();
                for (int j = 0; j < nextLetters.Length; j++)
                {
                    fullLetters[count] = nextLetters[j];
                    count++;
                }
                fullLetters[count] = ' ';
                count++;
            }
            fullLetters[count] = '(';
            fullLetters[count + 1] = ' ';
            fullLetters[count + 2] = ':';
            if (listPassages.Length > 1)
            {
                fullLetters[count + 3] = '-';
                fullLetters[count + 4] = ')';
            }
            else
                fullLetters[count + 3] = ')';
            return fullLetters;
        }

        public string[] getFillInBlankSmall()
        {
            string[] fillInBlankSm = new string[sizing.Length + 1];
            int count = 0;
            for (int i = 0; i < listPassages.Length; i++)
            {
                string[] nextFillInBlankSm = listPassages[i].getFillInBlankSmall();
                for (int j = 0; j < nextFillInBlankSm.Length; j++)
                {
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
            for (int i = 0; i < listPassages.Length; i++)
            {
                string[] nextFillInBlankLg = listPassages[i].getFillInBlankLarge();
                for (int j = 0; j < nextFillInBlankLg.Length; j++)
                {
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
            for (int i = 0; i < listPassages.Length; i++)
            {
                string[] nextRandomization = listPassages[i].getDelimits();
                for (int j = 0; j < nextRandomization.Length; j++)
                {
                    randomization[count] = nextRandomization[j];
                    count++;
                }
                randomization[count] = "";
                count++;
            }
            Random rand = new Random();
            for (int i = 0; i < count; i++)
            {
                int randI = rand.Next(count - 1);
                if (!randomization[i].Equals("") && !randomization[randI].Equals(""))
                {
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
}
