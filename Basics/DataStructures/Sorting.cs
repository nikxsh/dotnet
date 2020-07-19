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
                            Console.Write("{0} ", item);

                        Console.WriteLine("");

                        Console.Write("Bubble Sort Output: ");
                        foreach (var item in BubbleSort(inputArray))
                            Console.Write("{0} ", item);

                        Console.WriteLine("");
                        Console.WriteLine("---------------------------------");
                        break;

                    case 2:
                        inputArray = new int[] { 29, 64, 73, 34, 20 };
                        Console.Write("Original Array: ");
                        foreach (var item in inputArray)
                            Console.Write("{0} ", item);

                        Console.WriteLine("");

                        Console.Write("Selection Sort Output: ");
                        foreach (var item in SelectionSort(inputArray))
                            Console.Write("{0} ", item);

                        Console.WriteLine("");
                        Console.WriteLine("---------------------------------");
                        break;

                    case 3:
                        inputArray = new int[] { 12, 11, 13, 5, 6 };
                        Console.Write("Original Array: ");
                        foreach (var item in inputArray)
                            Console.Write("{0} ", item);

                        Console.WriteLine("");

                        Console.Write("Insertion Sort Output: ");
                        foreach (var item in InsertionSort(inputArray))
                            Console.Write("{0} ", item);
                        Console.WriteLine("");
                        Console.WriteLine("---------------------------------");
                        break;
                    case 4:
                        inputArray = new int[] { 12, 11, 13, 5, 6, 7 };
                        Console.Write("Original Array: ");
                        foreach (var item in inputArray)
                            Console.Write("{0} ", item);

                        Console.WriteLine("");

                        Console.Write("Merge Sort Output: ");
                        foreach (var item in MergeSort(inputArray, 0, inputArray.Length - 1))
                            Console.Write("{0} ", item);
                        Console.WriteLine("");
                        Console.WriteLine("---------------------------------");
                        break;
                    case 5:
                        inputArray = new int[] { 10, 80, 30, 90, 40, 50, 70 };
                        Console.Write("Original Array: ");
                        foreach (var item in inputArray)
                            Console.Write("{0} ", item);

                        Console.WriteLine("");

                        Console.Write("Quick Sort Output: ");
                        foreach (var item in QuickSort(inputArray, 0, inputArray.Length - 1))
                            Console.Write("{0} ", item);
                        Console.WriteLine("");
                        Console.WriteLine("---------------------------------");
                        break;
                }
            } while (choice != 10);
        }

		/// <summary>
		/// - Best: O(n)
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
		///		{11,12} {13} {5,6} {7}
		///		{11,12,13} {5,6,7}
		///		{5,6,7,11,12,13} 
		///		
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
		private int[] Merge(int[] inputArray, int first, int median, int last)
		{
			int[] subArray1 = new int[median - first + 1];
			int[] subArray2 = new int[last - median];

			//Copy to Subarrays
			for (int x = 0; x < subArray1.Length; x++)
				subArray1[x] = inputArray[first + x];
			for (int y = 0; y < subArray2.Length; y++)
				subArray2[y] = inputArray[median + 1 + y];

			//Merge Temp Arrays
			int i = 0, j = 0, k = first;

			while (i < subArray1.Length && j < subArray2.Length)
			{
				if (subArray1[i] <= subArray2[j])
				{
					inputArray[k] = subArray1[i];
					i++;
				}
				else
				{
					inputArray[k] = subArray2[j];
					j++;
				}
				k++;
			}

			//Copy remaining elements of Subarray1
			while (i < subArray1.Length)
			{
				inputArray[k] = subArray1[i];
				i++;
				k++;
			}


			//Copy remaining elements of Subarray2
			while (j < subArray2.Length)
			{
				inputArray[k] = subArray2[j];
				j++;
				k++;
			}

			return inputArray;
		}

		/// <summary>
		///   - Example
		///		arr[] = {10, 80, 30, 90, 40, 50, 70}
		///		Indexes:  0   1   2   3   4   5   6 
		///		low = 0, high =  6, pivot = arr[h] = 70
		///		Initialize index of smaller element, i = -1
		///		
		///		Traverse elements from j = low to high-1
		///		j = 0 : Since arr[j] <= pivot, do i++ and swap(arr[i], arr[j])
		///		i = 0 
		///		arr[] = {10, 80, 30, 90, 40, 50, 70} // No change as i and j  are same
		///		
		///		j = 1 : Since arr[j] > pivot, do nothing
		///		// No change in i and arr[]
		///		
		///		j = 2 : Since arr[j] <= pivot, do i++ and swap(arr[i], arr[j])
		///		i = 1
		///		arr[] = {10, 30, 80, 90, 40, 50, 70} // We swap 80 and 30 
		///		
		///		j = 3 : Since arr[j] > pivot, do nothing
		///		// No change in i and arr[]
		///		
		///		j = 4 : Since arr[j] <= pivot, do i++ and swap(arr[i], arr[j])
		///		i = 2
		///		arr[] = {10, 30, 40, 90, 80, 50, 70} // 80 and 40 Swapped
		///		j = 5 : Since arr[j] <= pivot, do i++ and swap arr[i] with arr[j]
		///		i = 3
		///		arr[] = {10, 30, 40, 50, 80, 90, 70} // 90 and 50 Swapped 
		///		
		///		We come out of loop because j is now equal to high-1.
		///		Finally we place pivot at correct position by swapping
		///		arr[i + 1] and arr[high] (or pivot) 
		///		arr[] = {10, 30, 40, 50, 70, 90, 80} // 80 and 70 Swapped 
		/// 
		///		Now 70 is at its correct place. All elements smaller than 70 are before it and all elements greater than 70 are after it.
		///		after x.All this should be done in linear time.
		/// 
		/// - Best and Average Case: O(nlogn)
		///	  worst case: O(n2)
		///	  space: O(logn)
		/// </summary>
		public int[] QuickSort(int[] inputArray, int low, int high)
		{
			if (low < high)
			{
				int median = Partition(inputArray, low, high);
				QuickSort(inputArray, low, median - 1);
				QuickSort(inputArray, median + 1, high);
			}
			return inputArray;
		}

		private int Partition(int[] inputArray, int low, int high)
		{
			int pivot = inputArray[high];
			int i = low - 1; //Smaller element's index

			for (int j = low; j <= high - 1; j++)
			{
				if (inputArray[j] <= pivot)
				{
					i++;

					int temp1 = inputArray[i];
					inputArray[i] = inputArray[j];
					inputArray[j] = temp1;
				}
			}

			int temp2 = inputArray[i + 1];
			inputArray[i + 1] = inputArray[high];
			inputArray[high] = temp2;

			return i + 1;
		}
	}
}
