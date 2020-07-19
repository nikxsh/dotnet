using System;

namespace DataStructures
{
    class HeapExample
    {
        public HeapExample()
        {
			int[] inputArray;
            Console.WriteLine("-------- Tree Structures --------");
            Console.WriteLine(" 1. Min Heap");
			Console.WriteLine(" 2. Max Heap");
			Console.WriteLine(" 3. Example of Max Heap");
			Console.WriteLine(" 4. Exit");
			Console.WriteLine("---------------------------------");

			var heapObject = new Heap();
            int choice;
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

		public class Heap
		{
			public int Size { get; set; }

			public int[] HeapArray;
			public int Position { get; set; }
			public Heap()
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
	}
}
