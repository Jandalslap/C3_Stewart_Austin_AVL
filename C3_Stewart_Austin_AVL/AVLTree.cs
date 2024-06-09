#region Usings
using C3_Stewart_Austin_AVL;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#endregion

namespace C3_Stewart_Austin_AVL
{
    internal class AVLTree
    {
        #region Members
        public Node Root { get; set; }

        public string LoadedListName; // Name of the loaded list
        public string LoadedListType; // Type of the loaded list

        private Stopwatch stopwatch; // Stopwatch to measure time for various operations
        #endregion
        #region Constructor
        public AVLTree()
        {
            Root = null;
            stopwatch = new Stopwatch(); // Initialise the stopwatch
        }
        #endregion
        #region Stopwatch
        // Method to reset and start the stopwatch
        private void StartTimer()
        {
            stopwatch.Reset();
            stopwatch.Start();
        }

        // Method to stop the stopwatch and return the elapsed time
        private TimeSpan StopTimer()
        {
            stopwatch.Stop();
            return stopwatch.Elapsed;
        }
        #endregion
        #region Load Methods
        // Method to load a list of files for selection
        public AVLTree LoadTree()
        {
            int selection; // Declare variabale for number selection
            bool isValidSelection = false; // Flag for user number selection validation
            string input = null; // Initialise input variable
            string[] files = null; // Declare files array variable

            // Create a new instance of Dictionary
            AVLTree avltree = new AVLTree();

            // Display options to the user
            Console.WriteLine("Text file types: ");
            Console.WriteLine("1. Ordered");
            Console.WriteLine("2. Random");

            while (!isValidSelection)
            {
                Console.Write("Enter the number of the file to load: ");               

                // Read user selection as string
                input = Console.ReadLine();

                // Attempt to parse user input as integer
                if (int.TryParse(input, out selection))
                {
                    // Validate user input
                    if (selection == 1 || selection == 2)
                    {
                        isValidSelection = true; // Set flag to true for valid selection
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("Invalid selection. Please try again.");
                    }
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("Invalid input. Please enter a number.");
                }
            }

            // Process selection
            if (int.TryParse(input, out selection))
            {
                if (selection == 1)
                {
                    // Get files from the ordered folder
                    string[] orderedFiles = Directory.GetFiles(@"..\ordered\", "*.txt")
                                                 .OrderBy(filePath => GetNumericPart(filePath))
                                                 .ToArray();

                    // Print available files in folder
                    Console.Clear(); // Clear the console screen
                    Console.WriteLine("Available files:");
                    Console.WriteLine("Ordered");
                    PrintFiles(orderedFiles);

                    // Assign orderedFiles to files
                    files = orderedFiles;
                    // Assign file type
                    avltree.LoadedListType = "Ordered ";
                }
                else if (selection == 2)
                {
                    // Get files from the random folder
                    string[] randomFiles = Directory.GetFiles(@"..\random\", "*.txt")
                                             .OrderBy(filePath => GetNumericPart(filePath))
                                             .ToArray();

                    // Print available files in folder
                    Console.Clear(); // Clear the console screen
                    Console.WriteLine("Available files:");
                    Console.WriteLine("Random");
                    PrintFiles(randomFiles);

                    // Assign randomFiles to files
                    files = randomFiles;

                    // Assign file type
                    avltree.LoadedListType = "Random ";
                }
            }
            // User to select file
            Console.WriteLine();
            int fileChoice = 0; // Initialize fileChoice outside the loop
            while (fileChoice < 1 || fileChoice > files.Length) // While loop to vailidate user input
            {
                Console.Write("Enter the number of the file to load: ");
                string userInput = Console.ReadLine();

                // Check if user input is empty
                if (!string.IsNullOrEmpty(userInput))
                {
                    // Try parsing user input to an integer
                    if (int.TryParse(userInput, out fileChoice))
                    {
                        if (fileChoice < 1 || fileChoice > files.Length)
                        {
                            Console.WriteLine();
                            Console.WriteLine("Invalid file choice. Please enter a number between 1 and " + files.Length + ".");
                        }
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("Invalid input. Please enter a valid number.");
                    }
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("No input entered. Please enter a number.");
                }
            }
            try
            {
                // Pass file choice and list instance to load method
                LoadFileIntoTree(files[fileChoice - 1], avltree);
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine($"Error loading file: {ex.Message}");
            }
            // Return the loaded list
            return avltree;
        }

        // Method to load a file into the dictionary.
        public void LoadFileIntoTree(string filePath, AVLTree avltree)
        {
            string fileName = Path.GetFileName(filePath); // Extract file name from file path
            avltree.LoadedListName = fileName;
            Console.WriteLine($"Loading file: {fileName}");

            StartTimer(); // Start the stopwatch

            HashSet<string> uniqueWords = new HashSet<string>(); // Create a HashSet to store unique words in that can only occur once.

            string[] lines = System.IO.File.ReadAllLines(filePath); // Read all lines from the file
            foreach (string line in lines)
            {
                if (!line.StartsWith("#")) // Ignore lines starting with #
                {
                    string[] words = line.Split(' '); // Split line into words where there are spaces                 

                    foreach (string word in words)
                    {
                        // Convert word to lowercase before checking for duplicates
                        string lowercaseWord = word.ToLower();

                        // Compare lowercase word to words in HashSet for uniqueness
                        if (!uniqueWords.Contains(lowercaseWord))
                        {
                            uniqueWords.Add(lowercaseWord); // Add unique word to hashset for future comparison
                            avltree.Add(lowercaseWord, lowercaseWord.Length); // Use Add method to add word and length into tree
                        }
                    }
                }
            }

            TimeSpan elapsedTime = StopTimer(); // Stop the stopwatch and get the elapsed time

            Console.WriteLine();
            Console.WriteLine($"File {fileName} loaded successfully. Time Taken: {elapsedTime}");
        }

        // Method to print text files to screen in an ordered list
        public static void PrintFiles(string[] files)
        {
            for (int i = 0; i < files.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {Path.GetFileName(files[i])}");
            }
        }

        // Method to parse first portion of text file name for numeric ordering
        public static int GetNumericPart(string filePath)
        {
            // Get the file name without extension
            string fileName = Path.GetFileNameWithoutExtension(filePath);

            // Extract only the numeric part from the file name
            string numericPart = fileName.Split('-')[0];

            // Parse the numeric part to an integer
            return int.Parse(numericPart);
        }
        #endregion
        #region Insert Method
        public string Add(string word, int numLetters)
        {
            StartTimer(); // Start the stopwatch
            TimeSpan elapsedTime;

            // Check if the word already exists using the Find method
            string result = Find(word, numLetters);

            // If the result indicates that the node is found, return a message
            if (!result.Contains("NODE not found"))
            {
                elapsedTime = StopTimer(); // Stop the stopwatch and get the elapsed time
                return "Item: '" + word.ToString() + "' already exists in the tree. Time Taken: " + elapsedTime;
            }

            // Create a new node with the provided data
            Node node = new Node(word, numLetters);

            if (Root == null)
            {
                // Tree is empty
                Root = node;
            }
            else
            {
                Root = InsertNode(Root, node);
            }
            elapsedTime = StopTimer(); // Stop the stopwatch and get the elapsed time

            // Print a message indicating successful insertion along with the elapsed time
            return "Item: '" + word.ToString() + "' inserted into the tree. Time Taken: " + elapsedTime;
        }

        private Node InsertNode(Node tree, Node node)
        {
            // 1. Current sub-tree node is empty, insert node here
            if (tree == null)
            {
                tree = node;
                return tree;
            }
            else if (string.Compare(node.Word, tree.Word) < 0)
            {
                // 2. Traverse the left side, insert when null (step 1) then balance tree
                tree.Left = InsertNode(tree.Left, node);
                tree = BalanceTree(tree);
            }
            else if (string.Compare(node.Word, tree.Word) > 0)
            {
                // 3. Traverse the right side, insert when null (step 1) then balance tree
                tree.Right = InsertNode(tree.Right, node);
                tree = BalanceTree(tree);
            }
            return tree;
        }
        public bool IsEmpty()
        {
            return Root == null; // Check if Root is null
        }
        #endregion
        #region Balance Methods
        private Node BalanceTree(Node current)
        {
            // 1. Obtain a balance reference from height of both left and right sub-trees from current node
            int b_factor = BalanceFactor(current);
            if (b_factor > 1)
            {
                // 2. Left side of tree is unbalanced. Decide a left or right rotation
                if (BalanceFactor(current.Left) > 0)
                {
                    // 3. Left side requires rotation, perform a left sub-tree rotation
                    current = RotateLL(current);
                }
                else
                {
                    // 4. Right side requires rotation, perform a right sub-tree rotation
                    current = RotateLR(current);
                }
            }
            else if (b_factor < -1)
            {
                // 5. Right side of tree is unbalanced. Decide a left or right rotation
                if (BalanceFactor(current.Right) > 0)
                {
                    // 6. Left side requires a rotaton, perform a left sub-tree rotation
                    current = RotateRL(current);
                }
                else
                {
                    // 7. Right side requires a rotation. perform a right sub-tree rotation
                    current = RotateRR(current);
                }
            }
            return current;
        }

        private Node RotateRR(Node parent)
        {
            // Performright rotation on the right side of the sub-tree by swapping the nodes around
            // based on reassigning the parent node to the right side of the sub-tree.
            Node pivot = parent.Right;
            parent.Right = pivot.Left;
            pivot.Left = parent;
            return pivot;
        }

        private Node RotateRL(Node parent)
        {
            // Perform a left rotation on the right side of the sub-tree by swapping the nodes around 
            // based on performing  a left rotation  on the right side of the sub-tree.
            Node pivot = parent.Right;
            parent.Right = RotateLL(pivot);
            return RotateRR(parent);
        }

        private Node RotateLL(Node parent)
        {
            // Perform a left rotation on the left side of the sub-tree by swapping the nodes around
            // based on reassigning the parent node to the left side of the sub-tree.
            Node pivot = parent.Left;
            parent.Left = pivot.Right;
            pivot.Right = parent;
            return pivot;
        }

        private Node RotateLR(Node parent)
        {
            // Perform a right rotation on the left side of the sub-tree by swapping the nodes around 
            // based on performing a right rotation on the left side of the sub-tree.
            Node pivot = parent.Left;
            parent.Left = RotateRR(pivot);
            return RotateLL(parent);
        }

        private int Max(int left, int right)
        {
            return left > right ? left : right;
        }

        private int GetHeight(Node current)
        {
            // Determine the height of the current sub-tree
            int height = 0;
            if (current != null)
            {
                int left = GetHeight(current.Left);
                int right = GetHeight(current.Right);
                int max = Max(left, right);
                height = max + 1;
            }
            return height;
        }

        private int BalanceFactor(Node current)
        {
            // Determine if the sub-tree needs to rotate left or right by finding the height of the 
            // left and right sides of the sub-tree and then taking the difference between the left and the right.

            // A balance factor greater than 1 (+2) indicates the left side is unbalanced.
            // A balance factor less than -1 (-2) indicates the right side is unbalanced.
            // Every other balance factor does not require a rotation.


            int left = GetHeight(current.Left);
            int right = GetHeight(current.Right);
            int b_factor = left - right;

            return b_factor;
        }
        #endregion
        #region Search Methods
        private Node Search(Node tree, Node node)
        {
            if (tree != null)
            {
                // 1. Have not reached the end of a branch
                int comparison = string.Compare(node.Word, tree.Word);
                if (comparison == 0)
                {
                    // 2. Found node
                    return tree;
                }
                else if (comparison < 0)
                {
                    // 3. Traverse left side
                    return Search(tree.Left, node);
                }
                else
                {
                    // 4. Traverse right side
                    return Search(tree.Right, node);
                }
            }
            // 5. Not found
            return null;
        }

        // Method to find root node
        private Node FindRootNode(AVLTree tree)
        {
            return tree.Root;
        }
        // Method to return root node details as string
        public string FindRoot(AVLTree tree)
        {
            TimeSpan elapsedTime = StopTimer(); // Stop the stopwatch and get the elapsed time

            int height = GetHeight(Root);
            int depth = GetDepth(Root);

            FindRootNode(tree);

            return "ROOT NODE found: " + tree.Root.ToString() + ". Height: " + height + ". Depth: " + depth + ". Time Taken: " + elapsedTime;
        }

        public string Find(string word, int numLetters)
        {
            StartTimer(); // Start the stopwatch

            Node node = new Node(word, numLetters);
            node = Search(Root, node);

            TimeSpan elapsedTime = StopTimer(); // Stop the stopwatch and get the elapsed time

            int height = GetNodeHeight(node);
            int depth = GetDepth(node);

            if (node != null && node != Root)
            {
                // Print a message indicating successful find along with the height and depth and elapsed time
                return "Target: " + word.ToString() + ", NODE found: " + node.ToString() + ". Height: " + height + ". Depth: " + depth + ". Time Taken: " + elapsedTime;
            }
            else if (node != null && node == Root)
            {
                // Print a message indicating successful find of the root node along with the height and depth and elapsed time
                return "Target: " + word.ToString() + ", NODE found. This is the ROOT NODE: " + node.ToString() + ". Height: " + height + ". Depth: " + depth + ". Time Taken: " + elapsedTime;
            }
            else
            {
                return "Target: " + word.ToString() + ", NODE not found";
            }
        }
        // Reference (https://www.geeksforgeeks.org/find-the-maximum-depth-or-height-of-a-tree/) on May 16, 2024.
        public int GetNodeHeight(Node node)
        {
            if (node == null)
            {
                return -1; // height of an empty tree is -1
            }
            else
            {
                int leftHeight = GetNodeHeight(node.Left);
                int rightHeight = GetNodeHeight(node.Right);

                // Height of the tree is the maximum height of left and right subtrees, plus 1 for the current node
                return Math.Max(leftHeight, rightHeight) + 1;
            }
        }
        // Method to pass the target node and root node to the FindDepth method
        private int GetDepth(Node node)
        {
            return FindDepth(Root, node);
        }

        // Method to find the depth of a node
        private int FindDepth(Node root, Node node)
        {
            // Base case: If the root is null, return -1 (indicating the node is not found)
            if (root == null)
            {
                return -1;
            }

            // If the current root is the node, return 0 (current depth is 0)
            if (root == node)
            {
                return 0;
            }

            // Check if the node is in the left subtree
            int leftDepth = FindDepth(root.Left, node);

            // If the node is found in the left subtree, add 1 to the depth and return it
            if (leftDepth >= 0)
            {
                return leftDepth + 1;
            }

            // Check if the node is in the right subtree
            int rightDepth = FindDepth(root.Right, node);

            // If the node is found in the right subtree, add 1 to the depth and return it
            if (rightDepth >= 0)
            {
                return rightDepth + 1;
            }

            // If the node is not found in either subtree, return -1
            return -1;
        }
        #endregion
        #region Delete Methods
        public string Remove(string word, int numLetters)
        {
            Node node = new Node(word, numLetters);
            node = Search(Root, node);
            if (node != null)
            {
                if (node == Root)
                {
                    // If the node to remove is the root, handle it separately
                    Root = DeleteRoot(Root);
                    return "Target: " + word.ToString() + ", ROOT NODE removed";
                }
                else
                {
                    Root = Delete(Root, node);
                    return "Target: " + word.ToString() + ", NODE removed";
                }
            }
            else
            {
                return "Target: " + word.ToString() + ", NODE not found";
            }
        }

        private Node Delete(Node tree, Node node)
        {
            if (tree == null)
            {
                // 1. Reached null side of the tree, return to unload stack
                return tree;
            }
            if (string.Compare(node.Word, tree.Word) < 0)
            {
                // 2. Traverse left side to find node
                tree.Left = Delete(tree.Left, node);
            }
            else if (string.Compare(node.Word, tree.Word) > 0)
            {
                // 3. Traverse right side to find node
                tree.Right = Delete(tree.Right, node);
            }
            else
            {
                // 4. Found node to delete
                // Check if node has only one child or no child
                if (tree.Left == null)
                {
                    // 5. Pull right side of tree up
                    return tree.Right;
                }
                else if (tree.Right == null)
                {
                    // 6. Pull left side of tree up
                }
                else
                {
                    // 7. node has two leaf nodes, get the InOrder successor node (the smallest), therefore  traverse right side 
                    // and replace the node found with the current node
                    tree.Word = MinValue(tree.Right).Word;

                    // 8. Traverse the right side of the tree to delete  the InOrder Successor
                    tree.Right = Delete(tree.Right, tree);
                }
            }
            return tree;
        }

        private Node DeleteRoot(Node tree)
        {
            if (tree == null)
            {
                return null;
            }
            // If root has no children, simply return null
            if (tree.Left == null && tree.Right == null)
            {
                return null;
            }
            // If root has only right child, return the right child as new root
            else if (tree.Left == null)
            {
                return tree.Right;
            }
            // If root has only left child, return the left child as new root
            else if (tree.Right == null)
            {
                return tree.Left;
            }
            // If root has both left and right children
            else
            {
                // Find the minimum value in the right subtree
                Node minValue = MinValue(tree.Right);
                // Copy the value of the minimum node to the root
                tree.Word = minValue.Word;
                // Delete the minimum node from the right subtree
                tree.Right = Delete(tree.Right, minValue);
                return tree;
            }
        }
        // Public method to remove subtree
        public string RemoveSubtree(string word, int numLetters)
        {
            Node node = new Node(word, numLetters);
            node = Search(Root, node);
            if (node != null && node != Root)
            {
                // Call DeleteSubtree with both the tree and the node
                Root = DeleteSubtree(Root, node);
                return "Target: " + word.ToString() + ", NODE and SUBTREE removed";
            }
            else if (node != null && node == Root)
            {
                Console.WriteLine();
                Console.WriteLine("*** WARNING ROOT NODE SELECTED ***");
                Console.WriteLine();
                Console.WriteLine("Deleting the ROOT NODE sub-tree will delete the entire tree.");
                Console.WriteLine("Press enter to continue or 'x' to cancel: ");
                string input = Console.ReadLine().Trim().ToLower();
                if (input == "x")
                {
                    // Cancel insert and return to menu
                    Console.Clear(); // Clear the console screen
                    return "Target: " + word.ToString() + ", ROOT NODE and SUBTREE not removed";
                }
                else
                {
                    // Call DeleteSubtree with both the tree and the node
                    Root = DeleteSubtree(Root, node);
                    LoadedListType = "";
                    LoadedListName = "No List Loaded";
                    return "Target: " + word.ToString() + ", ROOT NODE and SUBTREE removed";
                }
            }
            else
            {
                return "Target: " + word.ToString() + ", NODE not found";
            }
        }
        // Private method to delete subtree
        private Node DeleteSubtree(Node tree, Node node)
        {
            if (tree == null)
            {
                // If the tree is empty, return null
                return null;
            }

            // Compare the word of the current node with the word of the node to be deleted
            int comparison = string.Compare(node.Word, tree.Word);
            if (comparison < 0)
            {
                // If the word of the node to be deleted is smaller, go to the left subtree
                tree.Left = DeleteSubtree(tree.Left, node);
            }
            else if (comparison > 0)
            {
                // If the word of the node to be deleted is larger, go to the right subtree
                tree.Right = DeleteSubtree(tree.Right, node);
            }
            else
            {
                // If the current node is the node to be deleted
                // Set its left and right children to null, effectively removing the subtree
                tree.Left = null;
                tree.Right = null;
                // Return null to indicate that this node and its subtree have been deleted
                return null;
            }
            return tree;
        }


        private Node MinValue(Node node)
        {
            // Finds the minimum node in the leftside of the tree
            while (node.Left != null)
            {
                // traverse the tree replacing the minval with the node on the left side of the tree
                node = node.Left;
            }
            return node;
        }
        #endregion
        #region Print Methods
        private string TraversePreOrder(Node node)
        {
            int height = GetNodeHeight(node);
            int depth = GetDepth(node);

            StringBuilder sb = new StringBuilder();
            if (node != null)
            {
                sb.Append("[H:" + height + " D:" + depth + "] " + node.ToString() + "\n");
                sb.Append(TraversePreOrder(node.Left));
                sb.Append(TraversePreOrder(node.Right));
            }
            return sb.ToString();
        }
        private string TraverseInOrder(Node node)
        {
            int height = GetNodeHeight(node);
            int depth = GetDepth(node);

            StringBuilder sb = new StringBuilder();
            if (node != null)
            {
                sb.Append(TraverseInOrder(node.Left));
                sb.Append("[H:" + height + " D:" + depth + "] " + node.ToString() + "\n");
                sb.Append(TraverseInOrder(node.Right));
            }
            return sb.ToString();
        }
        private string TraverseReverseOrder(Node node)
        {
            int height = GetNodeHeight(node);
            int depth = GetDepth(node);

            StringBuilder sb = new StringBuilder();
            if (node != null)
            {
                sb.Append(TraverseReverseOrder(node.Right));
                sb.Append("[H:" + height + " D:" + depth + "] " + node.ToString() + "\n");
                sb.Append(TraverseReverseOrder(node.Left));
            }
            return sb.ToString();
        }

        private string TraversePostOrder(Node node)
        {
            int height = GetNodeHeight(node);
            int depth = GetDepth(node);

            StringBuilder sb = new StringBuilder();
            if (node != null)
            {
                sb.Append(TraversePostOrder(node.Left));
                sb.Append(TraversePostOrder(node.Right));
                sb.Append("[H:" + height + " D:" + depth + "] " + node.ToString() + "\n");
            }
            return sb.ToString();
        }
        public string PreOrder()
        {
            StringBuilder sb = new StringBuilder();
            if (Root == null)
            {
                sb.Append("TREE is EMPTY");
            }
            else
            {
                sb.Append(TraversePreOrder(Root));
            }
            return sb.ToString();
        }

        public string InOrder()
        {
            StringBuilder sb = new StringBuilder();
            if (Root == null)
            {
                sb.Append("TREE is EMPTY");
            }
            else
            {
                sb.Append(TraverseInOrder(Root));
            }
            return sb.ToString();
        }
        public string ReverseOrder()
        {
            StringBuilder sb = new StringBuilder();
            if (Root == null)
            {
                sb.Append("TREE is EMPTY");
            }
            else
            {
                sb.Append(TraverseReverseOrder(Root));
            }
            return sb.ToString();
        }

        public string PostOrder()
        {
            StringBuilder sb = new StringBuilder();
            if (Root == null)
            {
                sb.Append("TREE is EMPTY");
            }
            else
            {
                sb.Append(TraversePostOrder(Root));
            }
            return sb.ToString();
        }
        #endregion     
        #region Functional Test Methods
        // Method to perform functional tests on the list
        public AVLTree functionalTest()
        {
            // Create a list
            AVLTree avltree = new AVLTree();
            // Call the TestFiles method to load one text file for testing
            avltree = TestFiles(@"..\ordered\", "1000-words.txt", avltree);

            return avltree;
        }

        // Method to load test file from folder path into list
        public AVLTree TestFiles(string folderPath, string fileName, AVLTree list)
        {
            // Construct the full file path
            string filePath = Path.Combine(folderPath, fileName);

            // Check if the file exists
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"The specified file '{fileName}' does not exist in the folder '{folderPath}'.");
                return null;
            }

            // Pass file path and list to load method
            LoadFileIntoTree(filePath, list);

            // Return the loaded tree
            return list;
        }
        #endregion
    }
}

