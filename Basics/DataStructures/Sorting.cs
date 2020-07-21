using System;

namespace DataStructures
{
    public class SortingExmaple
    {
        public SortingExmaple()
        {
            int[] inputArray;
            var choice = -1;
            Console.WriteLine("-------- Tree Structures --------");
            Console.WriteLine(" 1. Bubble Sort");
            Console.WriteLine(" 2. Selection Sort");
            Console.WriteLine(" 3. Insertion Sort");
            Console.WriteLine(" 4. Merge Sort");
            Console.WriteLine(" 5. Quick Sort");
            Console.WriteLine(" 10. Exit");
            Console.WriteLine("---------------------------------");

            do
            {
                choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        inputArray = new int[] { 7, 5, 2, 4, 3, 9 };
                        Console.Write("Original Array: ");
                        foreach (var item in inputArray)
                            Console.Write($" {item} ");

                        Console.WriteLine();

                        Console.Write("Bubble Sort Output: ");
                        foreach (var item in BubbleSort(inputArray))
                            Console.Write($" {item} ");

                        Console.WriteLine();
                        Console.WriteLine("---------------------------------");
                        break;

                    case 2:
                        inputArray = new int[] { 29, 64, 73, 34, 20 };
                        Console.Write("Original Array: ");
                        foreach (var item in inputArray)
                            Console.Write($" {item} ");

                        Console.WriteLine();

                        Console.Write("Selection Sort Output: ");
                        foreach (var item in SelectionSort(inputArray))
                            Console.Write($" {item} ");

                        Console.WriteLine();
                        Console.WriteLine("---------------------------------");
                        break;

                    case 3:
                        inputArray = new int[] { 12, 11, 13, 5, 6 };
                        Console.Write("Original Array: ");
                        foreach (var item in inputArray)
                            Console.Write($" {item} ");

                        Console.WriteLine();

                        Console.Write("Insertion Sort Output: ");
                        foreach (var item in InsertionSort(inputArray))
                            Console.Write("{0} ", item);
                        Console.Write($" {item} ");

                        Console.WriteLine();
                        Console.WriteLine("---------------------------------");
                        break;
                    case 4:
                        inputArray = new int[] { 12, 11, 13, 5, 6, 7 };
                        Console.Write("Original Array: ");
                        foreach (var item in inputArray)
                            Console.Write($" {item} ");

                        Console.WriteLine();

                        Console.Write("Merge Sort Output: ");
                        foreach (var item in MergeSort(inputArray, 0, inputArray.Length - 1))
                            Console.Write($" {item} ");

                        Console.WriteLine();
                        Console.WriteLine("---------------------------------");
                        break;
                    case 5:
                        inputArray = new int[] { 10, 80, 30, 90, 40, 50, 70 };
                        Console.Write("Original Array: ");
                        foreach (var item in inputArray)
                            Console.Write($" {item} ");

                        Console.WriteLine();

                        Console.Write("Quick Sort Output: ");
                        foreach (var item in QuickSort(inputArray, 0, inputArray.Length - 1))
                            Console.Write($" {item} ");

                        Console.WriteLine();
                        Console.WriteLine("---------------------------------");
                        break;
                }
            } while (choice != 10);
        }

        /// <summary>
        /// - Best: O(n2)
        ///   Average: O(n2)
        ///	  Worst case: O(n2)
        ///	  Space: O(1)
        /// </summary>
        public int[] BubbleSort(int[] inputArray)
        {
            for (int i = inputArray.Length - 1; i >= 0; i--)
            {
                for (int j = 1; j <= i; j++)
                {
                    if (inputArray[j - 1] > inputArray[j])
                    {
                        int temp = inputArray[j - 1];
                        inputArray[j - 1] = inputArray[j];
                        inputArray[j] = temp;
                    }
                }
            }
            return inputArray;
        }

        /// <summary>
        /// - The algorithm works by selecting the smallest unsorted item and then swapping it with the item in the next position to be filled.
        ///   The selection sort works as follows: you look through the entire array for the smallest element, once you find it you swap it(the smallest element)
        ///   with the first element of the array.Then you look for the smallest element in the remaining array(an array without the first element) and swap it
        ///   with the second element.Then you look for the smallest element in the remaining array(an array without first and second elements) and swap it with the
        ///   third element, and so on.Here is an example,
        ///  
        /// - Example
        ///		29*, 64, 73, 34, 20*, 
        /// 	20, 64*, 73, 34, 29*, 
        /// 	20, 29, 73*, 34*, 64 
        /// 	20, 29, 34, 73*, 64* 
        /// 	20, 29, 34, 64, 73 
        /// 	
        /// - Best: O(n2)
        ///   Average: O(n2)
        ///	  worst case: O(n2)
        ///	  space: O(1)
        /// </summary>
        public int[] SelectionSort(int[] inputArray)
        {
            for (int i = 0; i < inputArray.Length - 1; i++)
            {
                int min = i;
                for (int j = i + 1; j < inputArray.Length; j++)
                    if (inputArray[j] < inputArray[min])
                        min = j;

                int temp = inputArray[min];
                inputArray[min] = inputArray[i];
                inputArray[i] = temp;
            }
            return inputArray;
        }


        /// <summary>
        /// - Example
        ///		12, 11, 13, 5, 6
        ///		Let us loop for i = 1 (second element of the array) to 5 (Size of input array)
        ///		i = 1, Since 11 is smaller than 12, move 12 and insert 11 before 12
        ///		11, 12, 13, 5, 6
        ///		i = 2, 13 will remain at its position as all elements in A[0..I - 1] are smaller than 13
        ///		11, 12, 13, 5, 6
        ///		i = 3, 5 will move to the beginning and all other elements from 11 to 13 will move one position ahead of their current position.
        ///		5, 11, 12, 13, 6
        ///		i = 4, 6 will move to position after 5, and elements from 11 to 13 will move one position ahead of their current position.
        ///		5, 6, 11, 12, 13
        ///		
        /// - Best: O(n)
        ///   Average: O(n2)
        ///	  worst case: O(n2)
        ///	  space: O(1)
        /// </summary>
        public int[] InsertionSort(int[] inputArray)
        {
            int key = 0, j;
            for (int i = 1; i < inputArray.Length; i++)
            {
                key = inputArray[i];
                j = i - 1;

                while (j >= 0 && inputArray[j] > key)
                {
                    inputArray[j + 1] = inputArray[j];
                    j = j - 1;
                }

                inputArray[j + 1] = key;
            }
            return inputArray;
        }


        /// <summary>
        ///  - MergeSort is a Divide and Conquer algorithm.It divides input array in two halves, calls itself for the two halves and then merges the
        ///    two sorted halves.The merge() function is used for merging two halves.The merge(arr, l, m, r) is key process that assumes that arr[l..m]
        ///    and arr[m + 1..r] are sorted and merges the two sorted sub-arrays into one
        ///    
        ///  - Example
        ///		12, 11, 13, 5, 6, 7
        ///		{12,11,13}  {5,6,7}
        ///		{12,11} {13}  {5,6} {7}
        ///		{12} {11} {13} {5} {6} {7}
        ///		{11, 12} {13} {5} {6} {7}
        ///		{11,12,13} {5,6,7}
        ///		{5,6,7,11,12,13} 
        ///		
        /// - https://www.geeksforgeeks.org/wp-content/uploads/Merge-Sort-Tutorial.png
        /// - Best, average and worst Case: O(nlogn)
        ///	  space: O(n)
        /// </summary>
        public int[] MergeSort(int[] inputArray, int first, int last)
        {
            if (first < last)
            {
                int median = (last + first) / 2;
                MergeSort(inputArray, first, median);
                MergeSort(inputArray, median + 1, last);
                inputArray = Merge(inputArray, first, median, last);
            }
            return inputArray;
        }
        private int[] Merge(int[] input, int first, int median, int last)
        {
            int start = first, end = median + 1, k, index = first;
            int[] temp = new int[last - first + 1];

            while (start <= median && end <= last)
            {
                if (input[start] < input[end])
                {
                    temp[index] = input[start];
                    start++;
                }
                else
                {
                    temp[index] = input[end];
                    end++;
                }
                index++;
            }

            if (start > median)
            {
                while (end <= last)
                {
                    temp[index] = input[end];
                    index++;
                    end++;
                }
            }
            else
            {
                while (start <= median)
                {
                    temp[index] = input[start];
                    index++;
                    start++;
                }
            }

            k = start;
            while (k < index)
            {
                input[k] = temp[k];
                k++;
            }

            return input;
        }

        /// <summary>
        ///  - Like Merge Sort, QuickSort is a Divide and Conquer algorithm. It picks an element as pivot and partitions the given array around the picked pivot. 
        ///  - Example
        ///		arr[] = {10, 80, 30, 90, 40, 50, 70}
        ///		Indexes:  0   1   2   3   4   5   6 
        ///		low = 0, high =  6, pivot = arr[high] >> 70
        ///		Initialize index of smaller element, i = -1
        ///		
        ///		Traverse elements from j = low to high-1
        ///		if arr[j] <= pivot, do i++ and swap(arr[i], arr[j])
        ///		
        ///     j = 0
        ///		i = 0
        ///		arr[] = {10, 80, 30, 90, 40, 50, 70} // No change as i and j  are same
        ///		
        ///		j = 1 : Since arr[j] > pivot, do nothing
        ///		80 <= 70
        ///		// No change in i and arr[]
        ///		
        ///		j = 2 : Since arr[j] <= pivot, do i++ and swap(arr[i], arr[j])
        ///		i = 1
        ///		30 <= 70
        ///		arr[] = {10, 30, 80, 90, 40, 50, 70} // We swap 80 and 30 
        ///		
        ///		j = 3 : Since arr[j] > pivot, do nothing
        ///		90 <= 70
        ///		// No change in i and arr[]
        ///		
        ///		j = 4 : Since arr[j] <= pivot, do i++ and swap(arr[i], arr[j])
        ///		i = 2
        ///		40 <= 70
        ///		arr[] = {10, 30, 40, 90, 80, 50, 70} // 80 and 40 Swapped
        ///		
        ///		j = 5 : Since arr[j] <= pivot, do i++ and swap arr[i] with arr[j]
        ///		i = 3
        ///		50 <= 70
        ///		arr[] = {10, 30, 40, 50, 80, 90, 70} // 90 and 50 Swapped 
        ///		
        ///		We come out of loop because j is now equal to high-1.
        ///		Finally we place pivot at correct position by swapping
        ///		j = 6
        ///		i = 4
        ///		swap arr[i] and arr[j]
        ///		arr[] = {10, 30, 40, 50, 70, 90, 80} // 80 and 70 Swapped 
        /// 
        ///		Now 70 is at its correct place. All elements smaller than 70 are before it and all elements greater than 70 are after it.
        ///		after x. All this should be done in linear time.
        /// 
        /// - Best and Average Case: O(nlogn)
        ///	  worst case: O(n2)
        ///	  space: O(logn)
        ///	  
        /// - O(log N) basically means time goes up linearly while the n goes up exponentially. So if it takes 1 second to compute 10 elements, 
        ///   it will take 2 seconds to compute 100 elements, 3 seconds to compute 1000 elements, and so on.
        /// </summary>
        public int[] QuickSort(int[] input, int low, int high)
        {
            if (low < high)
            {
                int median = Partition(input, low, high);
                QuickSort(input, low, median - 1);
                QuickSort(input, median + 1, high);
            }
            return input;
        }

        //https://www.interviewbit.com/tutorial/quicksort-algorithm/
        private int Partition(int[] input, int low, int high)
        {
            int pivot = input[high];
            int i = low - 1;

            for (int j = low; j <= high - 1; j++)
            {
                if (input[j] < pivot)
                {
                    i++;
                    var temp1 = input[i];
                    input[i] = input[j];
                    input[j] = temp1;
                }
            }

            var temp2 = input[i + 1];
            input[i + 1] = input[high];
            input[high] = temp2;

            return i + 1;
        }
    }
}
