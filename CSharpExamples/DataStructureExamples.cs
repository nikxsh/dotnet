using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DotNetDemos.CSharpExamples
{
    internal enum DataStructureChoice { BinaryTree = 1, Heaps, Sort };
    public class DataStructureExamples
    {
        public void DoAction(int choice)
        {
            switch ((DataStructureChoice)choice)
            {
                case DataStructureChoice.BinaryTree:
                    BinaryTree();
                    break;

                case DataStructureChoice.Heaps:
                    Heaps();
                    break;


                case DataStructureChoice.Sort:
                    Sorting();
                    break;
            }

        }

        public void BinaryTree()
        {
            BinaryTree<Student> binaryTreeObject = null;
            var compareChoice = 12; //(11 By Id | 12  By Marks)

            binaryTreeObject = new BinaryTree<Student>()
            {
                new Student { StudentId = 1001, Marks = 10, CompareChoice = compareChoice },
                new Student { StudentId = 1002, Marks = 10, CompareChoice = compareChoice },
                new Student { StudentId = 1003, Marks = 20, CompareChoice = compareChoice },
                new Student { StudentId = 1004, Marks = 30, CompareChoice = compareChoice },
                new Student { StudentId = 1005, Marks = 40, CompareChoice = compareChoice },
                new Student { StudentId = 1006, Marks = 50, CompareChoice = compareChoice },
                new Student { StudentId = 1007, Marks = 50, CompareChoice = compareChoice },
                new Student { StudentId = 1008, Marks = 25, CompareChoice = compareChoice },
                new Student { StudentId = 1009, Marks = 80, CompareChoice = compareChoice },
                new Student { StudentId = 1010, Marks = 75, CompareChoice = compareChoice },
                new Student { StudentId = 1011, Marks = 5, CompareChoice = compareChoice },
                new Student { StudentId = 1012, Marks = 85, CompareChoice = compareChoice },
                new Student { StudentId = 1013, Marks = 65, CompareChoice = compareChoice },
                new Student { StudentId = 1014, Marks = 57, CompareChoice = compareChoice },
                new Student { StudentId = 1015, Marks = 57, CompareChoice = compareChoice }
            };
            Console.WriteLine(" Binary Tree Created ! ");

            var choice = -1;
            Console.WriteLine("-------- Tree Structures --------");
            Console.WriteLine(" 1. Display (Inorder) ");
            Console.WriteLine(" 2. Display (Breadth First) ");
            Console.WriteLine(" 3. Display (Depth First) ");
            Console.WriteLine(" 4. Add ");
            Console.WriteLine(" 5. Find ");
            Console.WriteLine(" 6. Edit ");
            Console.WriteLine(" 7. Remove ");
            Console.WriteLine(" 8. Convert To Threaded BST ");
            Console.WriteLine(" 10. Exit");
            Console.WriteLine("---------------------------------");

            do
            {
                choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        Console.Write("Inorder : ");
                        foreach (var item in binaryTreeObject)
                            Console.Write("{0} ", item.Marks);
                        Console.WriteLine("");
                        Console.WriteLine("---------------------------------");
                        break;

                    case 2:
                        if (!binaryTreeObject.IsEmpty)
                        {
                            var treeTraversalObject = new TreeTraversal<Student>();
                            var bfsList = treeTraversalObject.GetLeverOrder(binaryTreeObject.Root);

                            foreach (var item in bfsList)
                                if (item.Count == 0)
                                    Console.WriteLine("");
                                else
                                    Console.Write("{0} ", item.Value.Marks);
                            //Console.Write(" [{0}-{1}-{2}] ", item.Value.StudentId, item.Value.Marks, item.Count);
                        }
                        else
                            Console.WriteLine("Tree is Empty!!");
                        Console.WriteLine("---------------------------------");
                        break;

                    case 3:
                        if (!binaryTreeObject.IsEmpty)
                        {
                            var treeTraversalObject = new TreeTraversal<Student>();
                            var dfsList = treeTraversalObject.DepthFirstTraversal(binaryTreeObject.Root);

                            foreach (var item in dfsList)
                                Console.WriteLine(" {0}-{1}-{2} ", item.Value.StudentId, item.Value.Marks, item.Count);
                        }
                        else
                            Console.WriteLine("Tree is Empty!!");
                        Console.WriteLine("---------------------------------");
                        break;

                    case 4:
                        Console.Write("Enter Id : ");
                        var Id = int.Parse(Console.ReadLine());
                        Console.WriteLine("");
                        Console.Write("Enter Marks : ");
                        var Marks = int.Parse(Console.ReadLine());
                        binaryTreeObject.Add(new Student { StudentId = Id, Marks = Marks, CompareChoice = compareChoice });
                        Console.WriteLine("Record Added!");
                        Console.WriteLine("---------------------------------");
                        break;

                    case 5:
                        Console.Write("Enter Id : ");
                        var itemToSearch = int.Parse(Console.ReadLine());
                        var data = binaryTreeObject.Find(new Student { StudentId = itemToSearch });
                        if (data != null)
                            Console.WriteLine("Id: {0} - Marks: {1} ", data.Value.StudentId, data.Value.Marks);
                        else
                            Console.WriteLine("No Record Found");
                        Console.WriteLine("---------------------------------");
                        break;

                    case 6:
                        Console.Write("Enter Id : ");
                        var searchId = int.Parse(Console.ReadLine());
                        Console.WriteLine("");
                        Console.Write("Enter New Marks : ");
                        var editedMarks = int.Parse(Console.ReadLine());
                        binaryTreeObject.Edit(new Student { StudentId = searchId, Marks = editedMarks });
                        Console.WriteLine("Record Edited!");
                        Console.WriteLine("---------------------------------");
                        break;


                    case 7:
                        Console.Write("Enter Node Value : ");
                        var itemToDelete = int.Parse(Console.ReadLine());
                        var check = false;
                        Console.WriteLine("");
                        if (compareChoice == 11)
                            check = binaryTreeObject.Remove(new Student { StudentId = itemToDelete });
                        else if (compareChoice == 12)
                            check = binaryTreeObject.Remove(new Student { Marks = itemToDelete });

                        if (check)
                            Console.WriteLine("Record Deleted!");
                        else
                            Console.WriteLine("Failed to Delete the record!");
                        Console.WriteLine("---------------------------------");
                        break;

                    case 8:
                        binaryTreeObject.ConvertToThreadedBST(binaryTreeObject.Root, null);
                        var tBst = binaryTreeObject.GetThreadedInorderList();
                        Console.Write("Inorder : ");
                        foreach (var item in tBst)
                            Console.Write("{0} ", item.Value.Marks);

                        Console.WriteLine("---------------------------------");
                        break;
                }
            } while (choice != 10);
        }

        public void Heaps()
        {
            int[] inputArray;
            var choice = -1;
            Console.WriteLine("-------- Tree Structures --------");
            Console.WriteLine(" 1. Min Heap");
            Console.WriteLine(" 2. Max Heap");
            Console.WriteLine(" 3. Example of Max Heap");
            Console.WriteLine(" 4. Exit");
            Console.WriteLine("---------------------------------");

            var heapObject = new Heaps();
            do
            {
                choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        inputArray = new int[] { 35, 33, 42, 10, 14, 19, 27, 44, 26, 31 };

                        Console.Write(" Input Array: ");
                        for (int i = 0; i < inputArray.Length; i++)
                            Console.Write(" {0} ", inputArray[i]);

                        heapObject.Size = inputArray.Length;
                        heapObject.CreateHeap(inputArray);

                        Console.WriteLine("");
                        Console.Write(" Mean Heap: ");
                        for (int i = 1; i < heapObject.Size + 1; i++)
                            Console.Write(" {0} ", heapObject.HeapArray[i]);
                        Console.WriteLine("");
                        Console.Write(" Extract Mean: ");
                        for (int i = 1; i < heapObject.Size + 1; i++)
                            Console.Write(" {0} ", heapObject.ExtractMinOrMax());
                        Console.WriteLine("");
                        Console.WriteLine("---------------------------------");
                        break;

                    case 2:
                        inputArray = new int[] { 35, 33, 42, 10, 14, 19, 27, 44, 26, 31 };

                        Console.Write(" Input Array: ");
                        for (int i = 0; i < inputArray.Length; i++)
                            Console.Write(" {0} ", inputArray[i]);

                        heapObject.Size = inputArray.Length;
                        heapObject.CreateHeap(inputArray, IsMinHeap: false);

                        Console.WriteLine("");
                        Console.Write(" Max Heap: ");
                        for (int i = 1; i < heapObject.Size + 1; i++)
                            Console.Write(" {0} ", heapObject.HeapArray[i]);
                        Console.WriteLine("");
                        Console.Write(" Extract Max: ");
                        for (int i = 1; i < heapObject.Size + 1; i++)
                            Console.Write(" {0} ", heapObject.ExtractMinOrMax(IsExtractMin: false));

                        Console.WriteLine("");
                        Console.WriteLine("---------------------------------");
                        break;

                    case 3:
                        //Given ‘N’ win­dows where each win­dow con­tains cer­tain num­ber of tick­ets at each win­dow.
                        //Price of a ticket is equal to num­ber of tick­ets remain­ing at that win­dow. Write an algo­rithm
                        //to sell ‘k’ tick­ets from these win­dows in such a man­ner so that it gen­er­ates the max­i­mum revenue.
                        var numberOfTickets = new int[] { 5, 1, 7, 10, 11, 9 };

                        heapObject.Size = numberOfTickets.Length;
                        heapObject.CreateHeap(numberOfTickets, IsMinHeap: false);

                        int ticketToSell = 4;
                        var maxRevenue = 0;
                        for (int i = 0; i < ticketToSell; i++)
                            maxRevenue += heapObject.ExtractMinOrMax(IsExtractMin: false);

                        Console.WriteLine(" Max Revenue : {0}", maxRevenue);
                        Console.WriteLine("---------------------------------");
                        break;
                }
            } while (choice != 4);
        }

        public void Sorting()
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

            var sortObject = new Sort();
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
                        foreach (var item in sortObject.BubbleSort(inputArray))
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
                        foreach (var item in sortObject.SelectionSort(inputArray))
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
                        foreach (var item in sortObject.InsertionSort(inputArray))
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
                        foreach (var item in sortObject.MergeSort(inputArray, 0, inputArray.Length - 1))
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
                        foreach (var item in sortObject.QuickSort(inputArray, 0, inputArray.Length - 1))
                            Console.Write("{0} ", item);
                        Console.WriteLine("");
                        Console.WriteLine("---------------------------------");
                        break;
                }
            } while (choice != 10);
        }
    }

    internal class Sort
    {
        //The worst-case runtime complexity is O(n2)
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

        //The algorithm works by selecting the smallest unsorted item and then swapping it with the item in the next position to be filled.
        //The selection sort works as follows: you look through the entire array for the smallest element, once you find it you swap it(the smallest element)
        //with the first element of the array.Then you look for the smallest element in the remaining array(an array without the first element) and swap it
        //with the second element.Then you look for the smallest element in the remaining array(an array without first and second elements) and swap it with the
        //third element, and so on.Here is an example,
        //29*, 64, 73, 34, 20*, 
        //20, 64*, 73, 34, 29*, 
        //20, 29, 73*, 34*, 64 
        //20, 29, 34, 73*, 64* 
        //20, 29, 34, 64, 73 
        //The worst-case runtime complexity is O(n2).
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


        //12, 11, 13, 5, 6
        //Let us loop for i = 1 (second element of the array) to 5 (Size of input array)
        //i = 1. Since 11 is smaller than 12, move 12 and insert 11 before 12
        //11, 12, 13, 5, 6
        //i = 2. 13 will remain at its position as all elements in A[0..I - 1] are smaller than 13
        //11, 12, 13, 5, 6
        //i = 3. 5 will move to the beginning and all other elements from 11 to 13 will move one position ahead of their current position.
        //5, 11, 12, 13, 6
        //i = 4. 6 will move to position after 5, and elements from 11 to 13 will move one position ahead of their current position.
        //5, 6, 11, 12, 13
        //Time Complexity: O(n*n)
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

        //MergeSort is a Divide and Conquer algorithm.It divides input array in two halves, calls itself for the two halves and then merges the
        //two sorted halves.The merge() function is used for merging two halves.The merge(arr, l, m, r) is key process that assumes that arr[l..m]
        //and arr[m + 1..r] are sorted and merges the two sorted sub-arrays into one
        //12, 11, 13, 5, 6, 7
        //{12,11,13}  {5,6,7}
        //{12,11} {13}  {5,6} {7}
        //{11,12} {13} {5,6} {7}
        //{11,12,13} {5,6,7}
        //{5,6,7,11,12,13} 
        //Time complexity of Merge Sort is \Theta(nLogn)
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

        //arr[] = {10, 80, 30, 90, 40, 50, 70}
        //Indexes:  0   1   2   3   4   5   6 
        //low = 0, high =  6, pivot = arr[h] = 70
        //Initialize index of smaller element, i = -1

        //Traverse elements from j = low to high-1
        //j = 0 : Since arr[j] <= pivot, do i++ and swap(arr[i], arr[j])
        //i = 0 
        //arr[] = {10, 80, 30, 90, 40, 50, 70} // No change as i and j  are same

        //j = 1 : Since arr[j] > pivot, do nothing
        //// No change in i and arr[]

        //j = 2 : Since arr[j] <= pivot, do i++ and swap(arr[i], arr[j])
        //i = 1
        //arr[] = {10, 30, 80, 90, 40, 50, 70} // We swap 80 and 30 

        //j = 3 : Since arr[j] > pivot, do nothing
        //// No change in i and arr[]

        //j = 4 : Since arr[j] <= pivot, do i++ and swap(arr[i], arr[j])
        //i = 2
        //arr[] = {10, 30, 40, 90, 80, 50, 70} // 80 and 40 Swapped
        //j = 5 : Since arr[j] <= pivot, do i++ and swap arr[i] with arr[j]
        //i = 3
        //arr[] = {10, 30, 40, 50, 80, 90, 70} // 90 and 50 Swapped 

        //We come out of loop because j is now equal to high-1.
        //Finally we place pivot at correct position by swapping
        //arr[i + 1] and arr[high] (or pivot) 
        //arr[] = {10, 30, 40, 50, 70, 90, 80} // 80 and 70 Swapped 

        //Now 70 is at its correct place.All elements smaller than
        //70 are before it and all elements greater than 70 are after
        //it.
        //after x.All this should be done in linear time.

        //Best case and Average Case Theta(nlogn)
        //Worst case Theta(n2)
        public int[] QuickSort(int[] inputArray, int low, int high)
        {
            if(low < high)
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

    internal class Heaps
    {
        public int Size { get; set; }

        public int[] HeapArray;
        public int Position { get; set; }
        public Heaps()
        {
            Position = 0;
        }

        public void CreateHeap(int[] inputArray, bool IsMinHeap = true)
        {
            HeapArray = new int[Size + 1];

            for (int i = 0; i < inputArray.Length; i++)
            {
                if (Position == 0)
                {
                    HeapArray[Position + 1] = inputArray[i];
                    Position = 2;
                }
                else
                {
                    HeapArray[Position++] = inputArray[i];
                    if (IsMinHeap)
                        BubleUpMinHeap();
                    else
                        BubleUpMaxHeap();
                }
            }
        }

        private void BubleUpMinHeap()
        {
            int checkPoint = Position - 1;

            while (checkPoint > 0 && HeapArray[checkPoint / 2] > HeapArray[checkPoint])
            {
                Swap(checkPoint, checkPoint / 2);
                checkPoint /= 2;
            }
        }

        private void BubleUpMaxHeap()
        {
            int checkPoint = Position - 1;

            while ((checkPoint > 0) && (checkPoint / 2 > 0) && (HeapArray[checkPoint / 2] < HeapArray[checkPoint]))
            {
                Swap(checkPoint, checkPoint / 2);
                checkPoint /= 2;
            }
        }

        public int ExtractMinOrMax(bool IsExtractMin = true)
        {
            //Replace last node of Heap with Root node
            int min = HeapArray[1];
            HeapArray[1] = HeapArray[Position - 1];
            HeapArray[Position - 1] = 0;
            Position--;
            if (IsExtractMin)
                MinHeapSinkDown(1);
            else
                MaxHeapSinkDown(1);
            return min;
        }

        private void MinHeapSinkDown(int index)
        {
            int temp = HeapArray[index];
            int smallestItemAt = index;

            //Check If replaced ele­ment is greater than any of its left child node
            if (2 * index < Position && HeapArray[smallestItemAt] > HeapArray[2 * index])
                smallestItemAt = 2 * index;


            //Check If replaced ele­ment is greater than any of its Right child node
            if (2 * index + 1 < Position && HeapArray[smallestItemAt] > HeapArray[2 * index + 1])
                smallestItemAt = 2 * index + 1;

            if (smallestItemAt != index)
            {
                Swap(index, smallestItemAt);
                //Go down till leaf node
                MinHeapSinkDown(smallestItemAt);
            }
        }

        private void MaxHeapSinkDown(int index)
        {
            int temp = HeapArray[index];
            int largestItemAt = index;

            //Check If replaced ele­ment is greater than any of its left child node
            if (2 * index < Position && HeapArray[largestItemAt] < HeapArray[2 * index])
                largestItemAt = 2 * index;


            //Check If replaced ele­ment is greater than any of its Right child node
            if (2 * index + 1 < Position && HeapArray[largestItemAt] < HeapArray[2 * index + 1])
                largestItemAt = 2 * index + 1;

            if (largestItemAt != index)
            {
                Swap(index, largestItemAt);
                //Go down till leaf node
                MinHeapSinkDown(largestItemAt);
            }
        }

        private void Swap(int TargetIndex, int SourceIndex)
        {
            HeapArray[TargetIndex] = HeapArray[TargetIndex] + HeapArray[SourceIndex];
            HeapArray[SourceIndex] = HeapArray[TargetIndex] - HeapArray[SourceIndex];
            HeapArray[TargetIndex] = HeapArray[TargetIndex] - HeapArray[SourceIndex];
        }
    }

    internal class TreeTraversal<T> where T : class
    {
        List<Node<T>> outputList = null;

        public TreeTraversal()
        {
            outputList = new List<Node<T>>();
        }

        public List<Node<T>> GetLeverOrder(Node<T> root)
        {
            var queue = new Queue<Node<T>>();
            queue.Enqueue(root);

            while (true)
            {
                var nodeCount = queue.Count;

                if (nodeCount == 0) break;

                while (nodeCount > 0)
                {
                    var node = queue.Dequeue();
                    outputList.Add(node);
                    if (node.Left != null)
                        queue.Enqueue(node.Left);
                    if (node.Right != null)
                        queue.Enqueue(node.Right);

                    nodeCount--;
                }
                outputList.Add(new Node<T>(0));
            }
            return outputList;
        }

        public List<Node<T>> BreadthFirstTraversal(Node<T> root)
        {
            var queue = new Queue<Node<T>>();
            queue.Enqueue(root);
            while (queue.Count > 0)
            {
                var node = queue.Dequeue();
                outputList.Add(node);
                if (node.Left != null)
                    queue.Enqueue(node.Left);
                if (node.Right != null)
                    queue.Enqueue(node.Right);
            }
            return outputList;
        }

        public List<Node<T>> DepthFirstTraversal(Node<T> root)
        {
            var stack = new Stack<Node<T>>();
            stack.Push(root);
            while (stack.Count > 0)
            {
                var node = stack.Pop();
                outputList.Add(node);
                if (node.Left != null)
                    stack.Push(node.Left);
                if (node.Right != null)
                    stack.Push(node.Right);
            }
            return outputList;
        }
    }

    internal class BinaryTree<T> : ICollection<T>, IEnumerable<T> where T : class, IComparable<T>
    {
        public Node<T> Root { get; set; }
        public int NumberOfNodes { get; set; }
        public bool IsEmpty { get { return Root == null; } }

        public BinaryTree()
        {
            NumberOfNodes = 0;
        }

        public BinaryTree(Node<T> root)
        {
            this.Root = root;
            NumberOfNodes = 0;
        }

        private int Height(Node<T> node)
        {
            if (node == null)
                return 0;

            return node.Height;
        }

        public void Add(T item)
        {
            if (Root == null)
            {
                Root = new Node<T>(item);
                ++NumberOfNodes;
            }
            else
                Root = RecursiveInsert(Root, new Node<T>(item));
            //InsertNode(item);
        }

        public bool Remove(T item)
        {

            var itemToRemove = Find(item);

            if (itemToRemove == null)
                return false;
            else
            {
                Root = RecursiveDelete(Root, itemToRemove);
                return true;
            }
        }

        private void InsertNode(T item)
        {
            var tempNode = Root;
            bool IsFound = false;

            while (!IsFound)
            {
                if (item.Equals(tempNode)) return;
                int comparedValue = item.CompareTo(tempNode.Value);
                if (comparedValue < 0)
                {
                    if (tempNode.Left == null)
                    {
                        tempNode.Left = new Node<T>(item);
                        tempNode.Left.Parent = tempNode;
                        ++NumberOfNodes;
                        IsFound = true;
                    }
                    else
                        tempNode = tempNode.Left;
                }
                else if (comparedValue > 0)
                {
                    if (tempNode.Right == null)
                    {
                        tempNode.Right = new Node<T>(item);
                        tempNode.Right.Parent = tempNode;
                        ++NumberOfNodes;
                        IsFound = true;
                    }
                    else
                        tempNode = tempNode.Right;
                }
                else
                {
                    tempNode.Right = new Node<T>(item);
                    tempNode.Right.Parent = tempNode;
                    ++NumberOfNodes;
                    IsFound = true;
                }
            }
        }

        private bool DeleteNode(T item)
        {
            var itemToRemove = Find(item);

            if (itemToRemove == null)
                return false;


            var parentNode = itemToRemove.Parent;
            //If leaf node
            if (itemToRemove.Left == null && itemToRemove.Right == null)
            {
                if (parentNode.Left != null && itemToRemove.Value.Equals(parentNode.Left.Value))
                    parentNode.Left = null;
                else if (parentNode.Right != null && itemToRemove.Value.Equals(parentNode.Right.Value))
                    parentNode.Right = null;
            }
            //If It has either left child only
            else if (itemToRemove.Left != null && itemToRemove.Right == null)
            {
                var tempChild = itemToRemove.Left;
                if (itemToRemove.Value.Equals(parentNode.Left.Value))
                    parentNode.Left = tempChild;
            }
            //If it has right child only
            else if (itemToRemove.Right != null && itemToRemove.Left == null)
            {
                var tempChild = itemToRemove.Right;
                if (itemToRemove.Value.Equals(parentNode.Right.Value))
                    parentNode.Right = tempChild;
            }
            //If it has both left and right child
            else if (itemToRemove.Right != null && itemToRemove.Left != null)
            {
                var succesorNode = GetSuccessorNode(itemToRemove.Right);
                if (parentNode == null)
                {
                    ReArrangeParentChilds(parentNode, succesorNode, itemToRemove);
                    this.Root = succesorNode;
                }
                else if (parentNode.Right.Value.Equals(itemToRemove.Value))
                {
                    parentNode.Right = succesorNode;
                    ReArrangeParentChilds(parentNode, succesorNode, itemToRemove);
                }
                else if (parentNode.Left.Value.Equals(itemToRemove.Value))
                {
                    parentNode.Left = succesorNode;
                    ReArrangeParentChilds(parentNode, succesorNode, itemToRemove);
                }
            }
            return true;
        }

        private Node<T> RecursiveInsert(Node<T> ancestorNode, Node<T> itemNodeToInsert)
        {
            if (ancestorNode == null) return itemNodeToInsert;

            int comparedValue = itemNodeToInsert.Value.CompareTo(ancestorNode.Value);
            if (comparedValue == 0)
            {
                ancestorNode.Count++;
            }
            else if (comparedValue < 0)
            {
                ancestorNode.Left = RecursiveInsert(ancestorNode.Left, itemNodeToInsert);
                ancestorNode.Left.Parent = ancestorNode;
            }
            else
            {
                ancestorNode.Right = RecursiveInsert(ancestorNode.Right, itemNodeToInsert);
                ancestorNode.Right.Parent = ancestorNode;
            }

            return BalanceTreeAfterInsert(ancestorNode, itemNodeToInsert);
        }

        private Node<T> RecursiveDelete(Node<T> ancestorNode, Node<T> itemNodeToRemove)
        {

            int comparedValue = itemNodeToRemove.Value.CompareTo(ancestorNode.Value);
            if (comparedValue < 0)
                ancestorNode.Left = RecursiveDelete(ancestorNode.Left, itemNodeToRemove);
            else if (comparedValue > 0)
                ancestorNode.Right = RecursiveDelete(ancestorNode.Right, itemNodeToRemove);
            else
            {

                if (ancestorNode.Left == null && ancestorNode.Right == null)
                {
                    if (ancestorNode.Parent.Left != null && ancestorNode.Value.Equals(ancestorNode.Parent.Left.Value))
                        ancestorNode.Parent.Left = null;
                    else if (ancestorNode.Parent.Right != null && ancestorNode.Value.Equals(ancestorNode.Parent.Right.Value))
                        ancestorNode.Parent.Right = null;

                    ancestorNode = null;
                }
                //If It has either left child only
                else if (ancestorNode.Left != null && ancestorNode.Right == null)
                {
                    var tempChild = ancestorNode.Left;
                    tempChild.Parent = ancestorNode.Parent;
                    if (tempChild.Value.CompareTo(ancestorNode.Parent.Value) > 0)
                        ancestorNode.Parent.Right = tempChild;
                    else
                        ancestorNode.Parent.Left = tempChild;

                    ancestorNode = null;
                }
                //If it has right child only
                else if (ancestorNode.Right != null && ancestorNode.Left == null)
                {
                    var tempChild = ancestorNode.Right;
                    if (tempChild.Value.CompareTo(ancestorNode.Parent.Value) > 0)
                        ancestorNode.Parent.Right = tempChild;
                    else
                        ancestorNode.Parent.Left = tempChild;

                    ancestorNode = null;
                }
                //If it has both left and right child
                else if (ancestorNode.Right != null && ancestorNode.Left != null)
                {
                    var succesorNode = GetSuccessorNode(ancestorNode.Right);
                    if (ancestorNode.Parent == null)
                    {
                        succesorNode.Parent.Left = null;
                        ReArrangeParentChilds(ancestorNode.Parent, succesorNode, itemNodeToRemove);
                        Root = ancestorNode = succesorNode;
                    }
                    else if (succesorNode.Value.CompareTo(ancestorNode.Parent.Value) > 0)
                    {
                        ancestorNode.Parent.Right = succesorNode;
                        ReArrangeParentChilds(ancestorNode.Parent, succesorNode, itemNodeToRemove);
                        ancestorNode = succesorNode;
                    }
                    else if (succesorNode.Value.CompareTo(ancestorNode.Parent.Value) < 0)
                    {
                        ancestorNode.Parent.Left = succesorNode;
                        ReArrangeParentChilds(ancestorNode.Parent, succesorNode, itemNodeToRemove);
                        ancestorNode = succesorNode;
                    }
                }
            }
            return BalanceTreeAfterRemove(ancestorNode);
        }

        private int GetBalance(Node<T> node)
        {
            if (node == null)
                return 0;

            return Height(node.Left) - Height(node.Right);
        }

        private int MaxOf(int a, int b)
        {
            return (a > b) ? a : b;
        }

        private Node<T> BalanceTreeAfterInsert(Node<T> node, Node<T> itemNode)
        {
            //Get the balance factor of this ancestor node to check whether
            //this node became unbalanced in the tree
            node.Height = MaxOf(Height(node.Left), Height(node.Right)) + 1;
            int balance = GetBalance(node);

            // If this node becomes unbalanced, then there are 4 cases

            // Left of Left node Case
            //---------------------------------------------------------------
            //             z                                      y  
            //            / \                                   /   \
            //           y  T4        Right Rotate(z)          x      z
            //          / \          - - - - - - - - ->      /  \    /  \ 
            //         x T3                                 T1  T2  T3  T4
            //        / \
            //      T1  T2
            //---------------------------------------------------------------
            if (balance > 1 && itemNode.Value.CompareTo(node.Left.Value) < 0)
                return RightRotate(node);

            //Right of Right Node Case
            //---------------------------------------------------------------
            //         z                                      y
            //       /   \                                  /   \ 
            //      T1    y           Left Rotate(z)      z       x
            //          /  \         - - - - - - - ->    /  \    /  \
            //         T2   x                           T1  T2  T3  T4
            //             / \
            //           T3  T4
            //---------------------------------------------------------------
            if (balance < -1 && itemNode.Value.CompareTo(node.Right.Value) > 0)
                return LeftRotate(node);

            //Right of Left Node Case
            //---------------------------------------------------------------
            //           z                               z                                     x                                                                                    x
            //          / \                            /   \                                 /   \ 
            //         y  T4  Left Rotate (y)         x    T4        Right Rotate(z)       y       z
            //        / \      - - - - - - - - ->    /  \           - - - - - - - ->      / \     / \
            //       T1   x                         y    T3                             T1  T2   T3  T4
            //      / \                            / \
            //    T2   T3                        T1   T2
            //---------------------------------------------------------------

            if (balance > 1 && itemNode.Value.CompareTo(node.Left.Value) > 0)
            {
                node.Left = LeftRotate(node.Left);
                return RightRotate(node);
            }

            //Left of Right Node Case
            //        z                              z                                         x  
            //      /  \                            / \                                      /    \ 
            //     T1   y      Right Rotate (y)   T1   x        Left Rotate(z)              z      y
            //         / \    - - - - - - - - ->      /  \     - - - - - - - ->            / \    / \
            //       x    T4                         T2   y                              T1  T2  T3  T4
            //      / \                                  /  \
            //    T2   T3                               T3   T4
            //---------------------------------------------------------------
            if (balance < -1 && itemNode.Value.CompareTo(node.Right.Value) < 0)
            {
                node.Right = RightRotate(node.Right);
                return LeftRotate(node);
            }

            return node;
        }

        private Node<T> BalanceTreeAfterRemove(Node<T> node)
        {
            if (node == null) return null;
            //Get the balance factor of this ancestor node to check whether
            //this node became unbalanced in the tree
            node.Height = MaxOf(Height(node.Left), Height(node.Right)) + 1;
            int balance = GetBalance(node);

            // If this node becomes unbalanced, then there are 4 cases

            // Left of Left node Case
            //---------------------------------------------------------------
            //             z                                      y  
            //            / \                                   /   \
            //           y  T4        Right Rotate(z)          x      z
            //          / \          - - - - - - - - ->      /  \    /  \ 
            //         x T3                                 T1  T2  T3  T4
            //        / \
            //      T1  T2
            //---------------------------------------------------------------
            if (balance > 1 && GetBalance(Root.Left) >= 0)
                return RightRotate(node);

            //Right of Right Node Case
            //---------------------------------------------------------------
            //         z                                      y
            //       /   \                                  /   \ 
            //      T1    y           Left Rotate(z)      z       x
            //          /  \         - - - - - - - ->    /  \    /  \
            //         T2   x                           T1  T2  T3  T4
            //             / \
            //           T3  T4
            //---------------------------------------------------------------
            if (balance < -1 && GetBalance(Root.Right) <= 0)
                return LeftRotate(node);

            //Right of Left Node Case
            //---------------------------------------------------------------
            //           z                               z                                     x                                                                                    x
            //          / \                            /   \                                 /   \ 
            //         y  T4  Left Rotate (y)         x    T4        Right Rotate(z)       y       z
            //        / \      - - - - - - - - ->    /  \           - - - - - - - ->      / \     / \
            //       T1   x                         y    T3                             T1  T2   T3  T4
            //      / \                            / \
            //    T2   T3                        T1   T2
            //---------------------------------------------------------------

            if (balance > 1 && GetBalance(Root.Left) < 0)
            {
                node.Left = LeftRotate(node.Left);
                return RightRotate(node);
            }

            //Left of Right Node Case
            //        z                              z                                         x  
            //      /  \                            / \                                      /    \ 
            //     T1   y      Right Rotate (y)   T1   x        Left Rotate(z)              z      y
            //         / \    - - - - - - - - ->      /  \     - - - - - - - ->            / \    / \
            //       x    T4                         T2   y                              T1  T2  T3  T4
            //      / \                                  /  \
            //    T2   T3                               T3   T4
            //---------------------------------------------------------------
            if (balance < -1 && GetBalance(Root.Right) > 0)
            {
                node.Right = RightRotate(node.Right);
                return LeftRotate(node);
            }

            return node;
        }

        //---------------------------------------------------------------
        //             z                                      y  
        //            / \                                   /   \
        //           y  T4        Right Rotate(z)          x      z
        //          / \          - - - - - - - - ->      /  \    /  \ 
        //         x T3                                 T1  T2  T3  T4
        //        / \
        //      T1  T2
        //---------------------------------------------------------------
        private Node<T> RightRotate(Node<T> node)
        {
            var tempRoot = node.Left;
            var tempNode = tempRoot.Right;

            tempRoot.Right = node;

            if (node.Parent == null)
            {
                tempRoot.Parent = null;
                Root = tempRoot;
            }
            node.Parent = tempRoot;
            node.Left = tempNode;

            node.Height = MaxOf(Height(node.Left), Height(node.Right)) + 1;
            tempRoot.Height = MaxOf(Height(tempRoot.Left), Height(tempRoot.Right)) + 1;
            return tempRoot;
        }

        //---------------------------------------------------------------
        //         z                                      y
        //       /   \                                  /   \ 
        //      T1    y           Left Rotate(z)      z       x
        //          /  \         - - - - - - - ->    /  \    /  \
        //         T2   x                           T1  T2  T3  T4
        //             / \
        //           T3  T4
        //---------------------------------------------------------------
        private Node<T> LeftRotate(Node<T> node)
        {
            var tempRoot = node.Right;
            var tempNode = node.Right.Left;

            tempRoot.Left = node;
            if (node.Parent == null)
            {
                tempRoot.Parent = null;
                Root = tempRoot;
            }
            node.Parent = tempRoot;
            node.Right = tempNode;

            node.Height = MaxOf(Height(node.Left), Height(node.Right)) + 1;
            tempRoot.Height = MaxOf(Height(tempRoot.Left), Height(tempRoot.Right)) + 1;
            return tempRoot;
        }

        private void ReArrangeParentChilds(Node<T> parentNode, Node<T> succesorNode, Node<T> itemToRemove)
        {
            succesorNode.Parent = parentNode;
            succesorNode.Left = itemToRemove.Left;
            succesorNode.Left.Parent = succesorNode;
            if (itemToRemove.Right != succesorNode)
            {
                succesorNode.Right = itemToRemove.Right;
                succesorNode.Right.Parent = succesorNode;
            }
        }

        public void Edit(T item)
        {
            var itemToEdit = Find(item);

            if (itemToEdit == null)
                return;

            itemToEdit.Value = item;
        }

        public bool Contains(T item)
        {
            if (IsEmpty)
                return false;

            var tempNode = Root;

            while (tempNode != null)
            {
                int comparedValue = tempNode.Value.CompareTo(item);
                if (comparedValue == 0)
                    return true;
                else if (comparedValue > 0)
                    tempNode = tempNode.Right;
                else if (comparedValue < 0)
                    tempNode = tempNode.Left;
            }

            return false;
        }

        public void ConvertToThreadedBST(Node<T> root, Node<T> previous)
        {
            if (root == null) return;
            else
            {
                ConvertToThreadedBST(root.Right, previous);
                if (root.Right == null && previous != null)
                {
                    root.Right = previous;
                    root.IsThreaded = true;
                }
                ConvertToThreadedBST(root.Left, root);
            }
        }

        public List<Node<T>> GetThreadedInorderList()
        {
            var nodes = new List<Node<T>>();
            //first go to most left node
            var currentNode = GetLeftMostNode(Root);

            while (currentNode != null)
            {
                nodes.Add(currentNode);
                //check if node has a right thread
                if (currentNode.IsThreaded)
                    currentNode = currentNode.Right;
                else // else go to left most node in the right subtree
                    currentNode = GetLeftMostNode(currentNode.Right);
            }
            return nodes;
        }

        private Node<T> GetLeftMostNode(Node<T> node)
        {
            if (node == null)
                return null;
            else
                while (node.Left != null)
                    node = node.Left;

            return node;
        }

        public int Count
        {
            get
            {
                return NumberOfNodes;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public void Clear()
        {
            Root = null;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            T[] tempArray = new T[NumberOfNodes];
            int counter = 0;
            foreach (var item in this)
            {
                tempArray[counter] = item;
                ++counter;
            }
            Array.Copy(tempArray, 0, array, arrayIndex, this.NumberOfNodes);
        }

        private Node<T> GetSuccessorNode(Node<T> root)
        {
            Node<T> successor = root;

            while (root.Left != null)
            {
                successor = root.Left;
                root = root.Left;
            }
            return successor;
        }

        public Node<T> Find(T item)
        {
            foreach (var node in InOrderTraversal(Root))
                if (node.Value.CompareTo(item) == 0)
                    return node;
            return null;
        }

        private IEnumerable<Node<T>> InOrderTraversal(Node<T> node)
        {
            if (node.Left != null)
            {
                foreach (var leftNode in InOrderTraversal(node.Left))
                    yield return leftNode;
            }
            yield return node;
            if (node.Right != null)
            {
                foreach (var rightNode in InOrderTraversal(node.Right))
                    yield return rightNode;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (var tempNode in InOrderTraversal(Root))
            {
                yield return tempNode.Value;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            foreach (var tempNode in InOrderTraversal(Root))
            {
                yield return tempNode.Value;
            }
        }

    }

    internal class Node<T> where T : class
    {
        public T Value { get; set; }
        public Node<T> Parent { get; set; }
        public Node<T> Left { get; set; }
        public Node<T> Right { get; set; }
        public int Height { get; set; }
        public int Count { get; set; }
        public bool IsThreaded { get; set; }
        public Node(int check)
        {
            this.Count = 0;
        }
        public Node(T value)
        {
            this.Value = value;
            this.Height = 1;
            this.Count = 1;
        }

        public Node(T value, Node<T> parent)
        {
            this.Value = value;
            this.Parent = parent;
            this.Height = 1;
            this.Count = 1;
        }

        public Node(T value, Node<T> parent, Node<T> left, Node<T> right, int height = 1, int count = 1)
        {
            this.Value = value;
            this.Parent = parent;
            this.Left = left;
            this.Right = right;
            this.Height = height;
            this.Count = count;
        }

        public bool IsRoot { get { return Parent == null; } }

        public bool IsLeaf { get { return Left == null && Right == null; } }

        public bool IsVisited { get; set; }

        public override string ToString()
        {
            return Value.ToString();
        }
    }

    internal class Student : IEquatable<Student>, IComparable<Student>
    {
        public int StudentId { get; set; }
        public int Marks { get; set; }
        public int CompareChoice { get; set; }

        public bool Equals(Student other)
        {
            if (other == null)
                return false;
            else if (this.Marks == other.Marks && this.StudentId == other.StudentId)
                return true;
            else
                return false;
        }

        public int CompareTo(Student other)
        {
            if (CompareChoice == 0)
            {
                if (this.StudentId == other.StudentId)
                    return 0;
                else if (this.StudentId > other.StudentId)
                    return 1;
                else
                    return -1;
            }
            else
            {
                if (this.Marks == other.Marks)
                    return 0;
                else if (this.Marks > other.Marks)
                    return 1;
                else
                    return -1;
            }
        }
    }

}
