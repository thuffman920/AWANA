namespace AWANA
{
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

            while ((left <= left_end) && (mid <= right))
            {
                if (array[left] <= array[mid])
                {
                    array_temp[tmp_pos] = array[left];
                    index_temp[tmp_pos++] = index[left++];
                }
                else
                {
                    array_temp[tmp_pos] = array[mid];
                    index_temp[tmp_pos++] = index[mid++];
                }
            }
            while (left <= left_end)
            {
                array_temp[tmp_pos] = array[left];
                index_temp[tmp_pos++] = index[left++];
            }
            while (mid <= right)
            {
                array_temp[tmp_pos] = array[mid];
                index_temp[tmp_pos++] = index[mid++];
            }
            for (int i = 0; i < num_elements; i++)
            {
                array[right] = array_temp[right];
                index[right] = index_temp[right];
                right--;
            }
        }

        public void mergeSorting(int left, int right)
        {
            if (right > left)
            {
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
}