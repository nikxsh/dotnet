using System;
using System.Collections;
using System.Collections.Generic;
using Tools.Models;

namespace DataStructures
{
    public class BinaryTreeExample
    {
        public BinaryTreeExample()
        {
			BinaryTree<Student> binaryTree = null;
			var compareChoice = 12; //(11 By Id | 12  By Marks)

			binaryTree = new BinaryTree<Student>()
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
						foreach (var item in binaryTree)
							Console.Write("{0} ", item.Marks);
						Console.WriteLine("");
						Console.WriteLine("---------------------------------");
						break;

					case 2:
						if (!binaryTree.IsEmpty)
						{
							var treeTraversalObject = new TreeTraversal<Student>();
							var bfsList = treeTraversalObject.GetLeverOrder(binaryTree.Root);

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
						if (!binaryTree.IsEmpty)
						{
							var treeTraversalObject = new TreeTraversal<Student>();
							var dfsList = treeTraversalObject.DepthFirstTraversal(binaryTree.Root);

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
						binaryTree.Add(new Student { StudentId = Id, Marks = Marks, CompareChoice = compareChoice });
						Console.WriteLine("Record Added!");
						Console.WriteLine("---------------------------------");
						break;

					case 5:
						Console.Write("Enter Id : ");
						var itemToSearch = int.Parse(Console.ReadLine());
						var data = binaryTree.Find(new Student { StudentId = itemToSearch });
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
						binaryTree.Edit(new Student { StudentId = searchId, Marks = editedMarks });
						Console.WriteLine("Record Edited!");
						Console.WriteLine("---------------------------------");
						break;


					case 7:
						Console.Write("Enter Node Value : ");
						var itemToDelete = int.Parse(Console.ReadLine());
						var check = false;
						Console.WriteLine("");
						if (compareChoice == 11)
							check = binaryTree.Remove(new Student { StudentId = itemToDelete });
						else if (compareChoice == 12)
							check = binaryTree.Remove(new Student { Marks = itemToDelete });

						if (check)
							Console.WriteLine("Record Deleted!");
						else
							Console.WriteLine("Failed to Delete the record!");
						Console.WriteLine("---------------------------------");
						break;

					case 8:
						binaryTree.ConvertToThreadedBST(binaryTree.Root, null);
						var tBst = binaryTree.GetThreadedInorderList();
						Console.Write("Inorder : ");
						foreach (var item in tBst)
							Console.Write("{0} ", item.Value.Marks);

						Console.WriteLine("---------------------------------");
						break;
				}
			} while (choice != 10);
		}
    }

	class BinaryTree<T> : ICollection<T>, IEnumerable<T> where T : class, IComparable<T>
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
			Root = root;
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
					Root = succesorNode;
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
			Array.Copy(tempArray, 0, array, arrayIndex, NumberOfNodes);
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


	class TreeTraversal<T> where T : class
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

	class Node<T> where T : class
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
			Count = 0;
		}
		public Node(T value)
		{
			Value = value;
			Height = 1;
			Count = 1;
		}

		public Node(T value, Node<T> parent)
		{
			Value = value;
			Parent = parent;
			Height = 1;
			Count = 1;
		}

		public Node(T value, Node<T> parent, Node<T> left, Node<T> right, int height = 1, int count = 1)
		{
			Value = value;
			Parent = parent;
			Left = left;
			Right = right;
			Height = height;
			Count = count;
		}

		public bool IsRoot { get { return Parent == null; } }

		public bool IsLeaf { get { return Left == null && Right == null; } }

		public bool IsVisited { get; set; }

		public override string ToString()
		{
			return Value.ToString();
		}
	}
}
