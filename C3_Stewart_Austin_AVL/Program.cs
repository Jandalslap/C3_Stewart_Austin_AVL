#region Usings
using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
#endregion

namespace C3_Stewart_Austin_AVL
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Splash Screen
            // Print the splash screen
            //Console.WriteLine("**************************************************");
            //Console.WriteLine("**              COMP605 C3 AVL                  **");
            //Console.WriteLine("**************************************************");
            //Console.WriteLine("Task: Implement AVL Tree Data Structures in C# ");
            //Console.WriteLine("           and test for complexity.");
            //Console.WriteLine("--------------------------------------------------");
            #endregion
            #region Variables         
            AVLTree avltree = new AVLTree(); // Create an instance of the avl tree
            string loadedListName = "No List Loaded"; // Variable to track the name of the loaded list
            string loadedListType = ""; // Variable to track the type of the loaded list
            bool exit = false; // Flag to control program exit           
            #endregion
            // While loop to return to menu until valid option is selected
            while (!exit)
            {
                #region Menu
                Console.WriteLine("**********");
                Console.WriteLine("** Menu **"); // Display menu options
                Console.WriteLine("**********");
                Console.WriteLine("1. Load");
                Console.WriteLine("2. Insert");
                Console.WriteLine("3. Find");
                Console.WriteLine("4. Delete");
                Console.WriteLine("5. Print");
                Console.WriteLine("6. Test");
                Console.WriteLine("7. Clear");
                Console.WriteLine("8. Exit");
                Console.WriteLine("**********");
                // Print the currently loaded list type and name beneath the menu
                Console.WriteLine($"Current List: {loadedListType}{loadedListName}");
                Console.WriteLine();
                Console.Write("Enter Menu Choice: ");
                string choice = Console.ReadLine(); // Read user input for menu choice
                #endregion
                switch (choice)
                {
                    #region Select
                    case "1": // Load file option
                        Console.Clear(); // Clear the console screen
                        int selection; // Declare variabale for number selection
                        bool isValidSelection = false; // Flag for user number selection validation
                        string input = null; // Initialise input variable
                        Console.Clear(); // Clear the console screen
                        Console.WriteLine("***************");
                        Console.WriteLine("** Load Tree **");
                        Console.WriteLine("***************");
                        Console.WriteLine();
                        avltree = avltree.LoadTree(); // Call the LoadTree method to load a file into the tree and update tree with the new instance.                                             
                        loadedListName = avltree.LoadedListName; // Get the name of the loaded list
                        loadedListType = avltree.LoadedListType; // Get the type of the loaded list
                        Console.WriteLine("Press any key to return to menu.");
                        Console.ReadKey();
                        Console.Clear(); // Clear the console screen
                        break;
                    #endregion
                    #region Insert
                    case "2": // Insert word option
                        Console.Clear(); // Clear the console screen
                        Console.WriteLine("*****************");
                        Console.WriteLine("** Insert Item **");
                        Console.WriteLine("*****************");
                        Console.WriteLine();
                        input = null; // Initialise input variable
                        bool validInput = false; // Ensure flag is false from any prior selections
                                                 // Loop for user input validation
                        while (!validInput)
                        {
                            Console.Write("Enter item to insert: ");
                            input = Console.ReadLine().Trim().ToLower(); // Read user input and trim whitespace and convert to lowercase
                            Console.WriteLine();
                            if (string.IsNullOrWhiteSpace(input))
                            {
                                // If nothing entered or only white space
                                Console.WriteLine("Invalid entry. Please enter a valid item without spaces.");
                            }
                            else
                            {
                                // If no list loaded change list name to custom
                                if (loadedListName == "No List Loaded")
                                {
                                    loadedListName = "Custom List";
                                }
                                string result = avltree.Add(input, input.Length); // Add to tree
                                Console.WriteLine(result);
                                validInput = true; // Change valid input flag to true to exit loop
                            }
                        }
                        Console.WriteLine("Press any key to return to menu.");
                        Console.ReadKey();
                        Console.Clear(); // Clear the console screen
                        break;
                    #endregion
                    #region Find
                    case "3": // Find item option
                        isValidSelection = false; // Flag for user number selection validation
                        selection = 0; // Initialise input variable

                        if (avltree.IsEmpty())
                        {
                            Console.WriteLine();
                            Console.WriteLine("The tree is empty. Please load or create a custom list first.");
                            Console.WriteLine("Press any key to return to menu.");
                            Console.ReadKey();
                            Console.Clear(); // Clear the console screen
                            continue;
                        }
                        else
                        {
                            Console.Clear(); // Clear the console screen
                            Console.WriteLine("****************");
                            Console.WriteLine("** Find Item **");
                            Console.WriteLine("****************");
                            Console.WriteLine();
                            // Display insert options to the user                           
                            Console.WriteLine("Find options: ");
                            Console.WriteLine("1. Find Item");
                            Console.WriteLine("2. Find Root");
                            Console.WriteLine("3. Back");
                            Console.WriteLine($"Current List: {loadedListType}{loadedListName}");

                            // Loop for user input validation
                            while (!isValidSelection)
                            {                               
                                Console.Write("Please select: ");

                                // Read user selection as string
                                input = Console.ReadLine();

                                // Attempt to parse user input as integer
                                if (int.TryParse(input, out selection))
                                {
                                    // Validate user input
                                    if (selection == 1 || selection == 2 || selection == 3)
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

                            if (selection == 1)
                            {
                                Console.Clear(); // Clear the console screen
                                Console.WriteLine("***************");
                                Console.WriteLine("** Find Item **");
                                Console.WriteLine("***************");
                                Console.WriteLine();

                                bool isValid = false;

                                while (!isValid)
                                {
                                    Console.Write("Enter item to find: ");
                                    string itemToFind = Console.ReadLine().Trim().ToLower(); // Read user input for word to find. Trim whitespace and convert to lowercase.                                                        
                                    if (string.IsNullOrEmpty(itemToFind))
                                    {
                                        Console.WriteLine();
                                        Console.WriteLine("Please enter a valid item with no spaces.");
                                    }
                                    else
                                    {
                                        string result = avltree.Find(itemToFind, itemToFind.Length); // Capture the return value of the search
                                        Console.WriteLine();
                                        Console.WriteLine(result); // Print the result
                                        isValid = true;
                                    }
                                }
                            }
                            else if (selection == 2)
                            {
                                Console.Clear(); // Clear the console screen
                                Console.WriteLine("********************");
                                Console.WriteLine("** Find Root Item **");
                                Console.WriteLine("********************");
                                Console.WriteLine();
                                string result = avltree.FindRoot(avltree);
                                Console.WriteLine(result);

                            }
                            else if (selection == 3)
                            {
                                // Cancel insert and return to menu
                                Console.Clear(); // Clear the console screen
                                continue;
                            }
                        }
                        Console.WriteLine();
                        Console.WriteLine("Press any key to return to menu.");
                        Console.ReadKey();
                        Console.Clear(); // Clear the console screen
                        break;
                    #endregion
                    #region Delete
                    case "4": // Delete options                      
                        selection = 0; ; // Declare variabale for number selection
                        isValidSelection = false; // Flag for user number selection validation
                        input = null; // Initialise input variable

                        if (avltree.IsEmpty())
                        {
                            Console.WriteLine();
                            Console.WriteLine("The tree is empty. Please load or create a custom list first.");
                            Console.WriteLine("Press any key to return to menu.");
                            Console.ReadKey();
                            Console.Clear(); // Clear the console screen
                            continue;
                        }
                        else
                        {
                            Console.Clear(); // Clear the console screen
                            // Display insert options to the user
                            Console.WriteLine();
                            Console.WriteLine("Delete options: ");
                            Console.WriteLine("1. Delete node");
                            Console.WriteLine("2. Delete root node");
                            Console.WriteLine("3. Delete sub-tree");
                            Console.WriteLine("4. Back");

                            while (!isValidSelection)
                            {
                                Console.Write("Please select: ");                              

                                // Read user selection as string
                                input = Console.ReadLine();

                                // Attempt to parse user input as integer
                                if (int.TryParse(input, out selection))
                                {
                                    // Validate user input
                                    if (selection == 1 || selection == 2 || selection == 3 || selection == 4)
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
                            if (selection == 1)
                            {
                                Console.Clear(); // Clear the console screen
                                Console.WriteLine("*****************");
                                Console.WriteLine("** Delete Node **");
                                Console.WriteLine("*****************");
                                Console.WriteLine();
                                bool isValid = false;
                                while (!isValid)
                                {
                                    Console.Write("Enter item to delete: ");
                                    string nodeToDelete = Console.ReadLine().Trim().ToLower(); // Read user input for item to delete. Trim whitespace and convert to lowercase.
                                    if (string.IsNullOrEmpty(nodeToDelete))
                                    {
                                        Console.WriteLine("Please enter a valid item with no spaces.");
                                    }
                                    else
                                    {
                                        string result = avltree.Remove(nodeToDelete, nodeToDelete.Length); // Pass the word and length to the Remove function
                                        Console.WriteLine();
                                        Console.WriteLine(result); // Print the result
                                        isValid = true;
                                    }
                                }                              
                            }
                            if (selection == 2)
                            {
                                Console.Clear(); // Clear the console screen
                                Console.WriteLine("**********************");
                                Console.WriteLine("** Delete Root Node **");
                                Console.WriteLine("**********************");
                                Console.WriteLine();
                                validInput = false; // Ensure flag is false from any prior selections
                                Node nodeToDelete = avltree.Root;
                                string result = avltree.Remove(nodeToDelete.Word, nodeToDelete.NumLetters); // Pass the root node and length to the Remove function
                                Console.WriteLine(result); // Print the result
                            }
                            if (selection == 3)
                            {
                                Console.Clear(); // Clear the console screen
                                Console.WriteLine("*********************");
                                Console.WriteLine("** Delete Sub-tree **");
                                Console.WriteLine("*********************");
                                Console.WriteLine();
                                validInput = false; // Ensure flag is false from any prior selections
                                bool isValid = false;
                                while (!isValid)
                                {
                                    Console.Write("Enter node to delete sub-tree: ");
                                    string nodeToDelete = Console.ReadLine().Trim().ToLower(); // Read user input for item to delete. Trim whitespace and convert to lowercase.
                                    if (string.IsNullOrEmpty(nodeToDelete))
                                    {
                                        Console.WriteLine("Please enter a valid item with no spaces.");
                                    }
                                    else
                                    {
                                        string result = avltree.RemoveSubtree(nodeToDelete, nodeToDelete.Length); // Pass the word and length to the RemoveSubtree function
                                        Console.WriteLine();
                                        if (avltree.IsEmpty())
                                        {
                                            loadedListType = avltree.LoadedListType;
                                            loadedListName = avltree.LoadedListName;
                                            Console.WriteLine(result); // Print the result
                                            Console.WriteLine(avltree.PreOrder());
                                        }
                                        else
                                        {
                                            Console.WriteLine(result); // Print the result                                      
                                        }
                                        isValid = true;
                                    }
                                }
                            }
                            if (selection == 4)
                            {
                                // Cancel delete and return to menu
                                Console.Clear(); // Clear the console screen
                                continue;
                            }
                        }
                        Console.WriteLine("Press any key to return to menu.");
                        Console.ReadKey();
                        Console.Clear(); // Clear the console screen
                        break;
                    #endregion
                    #region Print
                    case "5": // Print list option                       
                        if (avltree.IsEmpty())
                        {
                            Console.WriteLine();
                            Console.WriteLine("The tree is empty. Please load or create a custom list first.");
                            Console.WriteLine("Press any key to return to menu.");
                            Console.ReadKey();
                            Console.Clear(); // Clear the console screen
                            continue;
                        }
                        else
                        {
                            Console.Clear(); // Clear the console screen
                            Console.WriteLine("****************");
                            Console.WriteLine("** Print List **");
                            Console.WriteLine("****************");
                            Console.WriteLine();

                            isValidSelection = false; // Flag for user number selection validation
                            input = null; // Initialise input variable
                            selection = 0; // Initialise input variable

                            // Loop for user input validation
                            while (!isValidSelection)
                            {
                                // Display insert options to the user                           
                                Console.WriteLine("Print options: ");
                                Console.WriteLine("1. Pre-Order");
                                Console.WriteLine("2. In-Order");
                                Console.WriteLine("3. Reverse-Order");
                                Console.WriteLine("4. Post-Order");
                                Console.WriteLine("5. Back");
                                Console.WriteLine($"Current List: {loadedListType}{loadedListName}");
                                Console.Write("Please select: ");

                                // Read user selection as string
                                input = Console.ReadLine();

                                // Attempt to parse user input as integer
                                if (int.TryParse(input, out selection))
                                {
                                    // Validate user input
                                    if (selection == 1 || selection == 2 || selection == 3 || selection == 4 || selection == 5)
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
                            if (selection == 1)
                            {
                                Console.Clear(); // Clear the console screen
                                Console.WriteLine("*********************");
                                Console.WriteLine("** Print Pre-Order **");
                                Console.WriteLine("*********************");
                                Console.WriteLine();
                                Console.WriteLine($"Printing List: {loadedListType}{loadedListName}");
                                Console.WriteLine();
                                Console.WriteLine(avltree.PreOrder());
                            }
                            else if (selection == 2)
                            {
                                Console.Clear(); // Clear the console screen
                                Console.WriteLine("********************");
                                Console.WriteLine("** Print In-Order **");
                                Console.WriteLine("********************");
                                Console.WriteLine();
                                Console.WriteLine($"Printing List: {loadedListType}{loadedListName}");
                                Console.WriteLine();
                                Console.WriteLine(avltree.InOrder());
                            }
                            else if (selection == 3)
                            { 
                                Console.Clear(); // Clear the console screen
                                Console.WriteLine("*************************");
                                Console.WriteLine("** Print Reverse-Order **");
                                Console.WriteLine("*************************");
                                Console.WriteLine();
                                Console.WriteLine($"Printing List:{loadedListType}{loadedListName}");
                                Console.WriteLine();
                                Console.WriteLine(avltree.ReverseOrder());
                            }
                            else if (selection == 4)
                            {
                                Console.Clear(); // Clear the console screen
                                Console.WriteLine("**********************");
                                Console.WriteLine("** Print Post-Order **");
                                Console.WriteLine("**********************");
                                Console.WriteLine();
                                Console.WriteLine($"Printing List:{loadedListType}{loadedListName}");
                                Console.WriteLine();
                                Console.WriteLine(avltree.PostOrder());
                            }
                            else if (selection == 5)
                            {
                                // Cancel insert and return to menu
                                Console.Clear(); // Clear the console screen
                                continue;
                            }
                        }
                        Console.WriteLine();
                        Console.WriteLine("Press any key to return to menu.");
                        Console.ReadKey();
                        Console.Clear(); // Clear the console screen
                        break;
                    #endregion
                    #region Test
                    case "6": // Functional test option
                        Console.Clear(); // Clear the console screen
                        Console.WriteLine();
                        Console.WriteLine("Running functional tests for avl tree data structure...");
                        Console.WriteLine();
                        Console.WriteLine("********************");
                        Console.WriteLine("** Load File Test **");
                        Console.WriteLine("********************");
                        Console.WriteLine();
                        Console.WriteLine("Press any key to begin.");
                        Console.ReadKey(true);
                        Console.WriteLine();
                        avltree = avltree.functionalTest();
                        loadedListName = avltree.LoadedListName; // Get the name of the loaded list
                        loadedListType = avltree.LoadedListType; // Get the type of the loaded list
                        string testItem = "deane_austin"; // Initialise variable for test item
                        string testDeleteItem = "mack_austin"; // Initialise variable to test delete item
                        string testitemToFind = "deane_austin"; // Initialise variable to test find item

                        Console.WriteLine("Press any key to continue.");
                        Console.ReadKey(true);
                        Console.Clear(); // Clear the console screen

                        // Test PrintDictionary method
                        Console.WriteLine("*********************");
                        Console.WriteLine("** Print List Test **");
                        Console.WriteLine("*********************");
                        Console.WriteLine();
                        Console.WriteLine($"Printing List: {loadedListType}{loadedListName}");
                        Console.Write("Press enter to print tree Pre-Order..."); // Prompts user to print when ready to allow scroll to read previous output first
                        Console.ReadLine();
                        Console.WriteLine();
                        Console.WriteLine("*********************");
                        Console.WriteLine("** Print Pre-Order **");
                        Console.WriteLine("*********************");
                        Console.WriteLine();
                        Console.WriteLine(avltree.PreOrder());
                        Console.WriteLine();
                        Console.Write("Press enter to print tree In-Order..."); // Prompts user to print when ready to allow scroll to read previous output first
                        Console.ReadLine();
                        Console.WriteLine();
                        Console.WriteLine("********************");
                        Console.WriteLine("** Print In-Order **");
                        Console.WriteLine("********************");
                        Console.WriteLine();
                        Console.WriteLine(avltree.InOrder());
                        Console.WriteLine();
                        Console.Write("Press enter to print tree Post-Order..."); // Prompts user to print when ready to allow scroll to read previous output first
                        Console.ReadLine();
                        Console.WriteLine();
                        Console.WriteLine("**********************");
                        Console.WriteLine("** Print Post-Order **");
                        Console.WriteLine("**********************");
                        Console.WriteLine();
                        Console.WriteLine(avltree.PostOrder());
                        Console.WriteLine();
                        Console.WriteLine("Print List method test completed.");
                        Console.WriteLine("Press any key to continue.");
                        Console.ReadKey(true);
                        Console.Clear(); // Clear the console screen

                        // Test InsertNode method
                        Console.WriteLine("**********************");
                        Console.WriteLine("** Insert Item Test **");
                        Console.WriteLine("**********************");
                        Console.WriteLine();
                        Console.WriteLine($"Insert item: {testItem}");
                        string testResult = avltree.Add(testItem, testItem.Length); // Calls Add method passing test item and length
                        Console.WriteLine(testResult); // Should receive 'Item inserted into the tree'.
                        Console.WriteLine();

                        Console.Write("Press enter to print..."); // Prompts user to print when ready to allow scroll to read previous output first
                        Console.ReadLine();
                        Console.WriteLine(avltree.PreOrder());
                        Console.WriteLine();
                        Console.WriteLine("Insert Item method test completed.");
                        Console.WriteLine("Press any key to continue.");
                        Console.ReadKey(true);
                        Console.Clear(); // Clear the console screen

                        Console.WriteLine("********************");
                        Console.WriteLine("** Find Item Test **");
                        Console.WriteLine("********************");
                        Console.WriteLine();
                        Console.WriteLine($"Find item: {testitemToFind}");
                        testResult = avltree.Find(testitemToFind, testitemToFind.Length); // Pass word and length of item to find 
                        Console.WriteLine(testResult); // Should receive 'Node not found or list empty'. 
                        Console.WriteLine();
                        testitemToFind = "brought";
                        Console.WriteLine($"Find item: {testitemToFind}");
                        testResult = avltree.Find(testitemToFind, testitemToFind.Length); // Pass word and length of item to find
                        Console.WriteLine(testResult); // Print result - Should receive 'Node found Height: Depth:'
                        Console.WriteLine();
                        Console.WriteLine($"Find Root:");
                        testResult = avltree.FindRoot(avltree); // Pass tree to find root node
                        Console.WriteLine(testResult); // Print result - Should receive 'ROOT NODE found Height: Depth:'
                        Console.WriteLine();
                        Console.WriteLine("Find Item method test completed.");
                        Console.WriteLine("Press any key to continue.");
                        Console.ReadKey(true);
                        Console.Clear(); // Clear the console screen

                        Console.WriteLine("**********************");
                        Console.WriteLine("** Delete Item Test **");
                        Console.WriteLine("**********************");
                        Console.WriteLine();
                        Console.WriteLine($"Delete item: {testDeleteItem}");
                        testResult = avltree.Remove(testDeleteItem, testDeleteItem.Length); // Pass word and length of item to delete  
                        Console.WriteLine(testResult); // Should receive 'NODE not found'.
                        Console.WriteLine();
                        testDeleteItem = "wished";
                        Console.WriteLine($"Delete item: {testDeleteItem}");
                        testResult = avltree.Remove(testDeleteItem, testDeleteItem.Length); // Pass word and length of item to delete 
                        Console.WriteLine(testResult); // Should receive 'NODE removed'.
                        Console.WriteLine();
                        testDeleteItem = "my";
                        Console.WriteLine($"Delete root node:");
                        testResult = avltree.Remove(testDeleteItem, testDeleteItem.Length); // Pass word and length of root item to delete 
                        Console.WriteLine(testResult); // Should receive 'ROOT NODE removed'. 
                        Console.WriteLine();
                        testDeleteItem = "people";
                        Console.WriteLine($"Delete subtree: ");
                        testResult = avltree.RemoveSubtree(testDeleteItem, testDeleteItem.Length); // Pass word and length of item to delete subtree 
                        Console.WriteLine(testResult); // Should receive 'NODE and SUBTREE removed'.
                        Console.WriteLine();
                        testDeleteItem = "myself";
                        Console.WriteLine($"Delete root subtree:");
                        testResult = avltree.RemoveSubtree(testDeleteItem, testDeleteItem.Length); // Pass root node and length to delete 
                        Console.WriteLine(testResult); // Should receive warning and 'ROOT NODE and SUBTREE not removed'.
                        Console.WriteLine();
                        Console.Write("Press enter to print..."); // Prompts user to print when ready to allow scroll to read previous output first
                        Console.ReadLine();
                        Console.WriteLine(avltree.PreOrder()); // Call print method on current tree
                        Console.WriteLine();
                        Console.WriteLine("Delete Item method test completed.");
                        Console.WriteLine("Press any key to continue.");
                        Console.ReadKey(true);
                        Console.Clear(); // Clear the console screen

                        Console.WriteLine("*********************");
                        Console.WriteLine("** Clear List Test **");
                        Console.WriteLine("*********************");
                        Console.WriteLine();
                        Console.WriteLine($"Current List: {avltree.LoadedListName}");
                        Console.WriteLine();
                        Console.WriteLine("Clearing List...");
                        avltree = new AVLTree(); // Create a new instance of the list to clear loaded text file
                        loadedListType = "";
                        loadedListName = "No List Loaded";
                        Console.WriteLine($"Current List: {loadedListName}");
                        Console.WriteLine();
                        Console.Write("Press enter to print..."); // Prompts user to print when ready to allow scroll to read previous output first
                        Console.ReadLine();
                        Console.WriteLine(avltree.PreOrder()); // Call print method on current list
                        Console.WriteLine();
                        Console.WriteLine("Functional tests for list methods completed.");
                        Console.WriteLine("Press any key to return to menu.");
                        Console.ReadKey();
                        Console.Clear(); // Clear the console screen
                        break;
                    #endregion
                    #region Clear
                    case "7": // Clear tree option
                        if (loadedListName == "No List Loaded")
                        {
                            Console.WriteLine();
                            Console.WriteLine("The tree is empty. Please load or create a custom list first.");
                            Console.WriteLine("Press any key to return to menu.");
                            Console.ReadKey();
                            Console.Clear(); // Clear the console screen
                            continue;
                        }
                        else
                        {
                            Console.Clear(); // Clear the console screen
                            Console.WriteLine("****************");
                            Console.WriteLine("** Clear Tree **");
                            Console.WriteLine("****************");
                            Console.WriteLine();
                            Console.WriteLine($"Current List:{loadedListType}{loadedListName}");
                            Console.Write("Press enter to clear tree or x to cancel: "); // Prompts user to print when ready
                            input = Console.ReadLine();
                            if (input == "x" || input == "X")
                            {
                                Console.Clear(); // Clear the console screen
                                break;
                            }
                            else
                            {
                                avltree = new AVLTree(); // Create a new instance of the tree to clear loaded text file
                                loadedListType = "";
                                loadedListName = "No List Loaded";
                                Console.WriteLine();
                                Console.WriteLine(avltree.InOrder()); // Print empty tree
                                Console.WriteLine("Press any key to return to menu.");
                                Console.ReadKey();
                                Console.Clear(); // Clear the console screen
                                break;
                            }
                        }
                    #endregion
                    #region Exit
                    case "8": // Exit option
                        exit = true; // Set exit flag to true to exit the program
                        Console.WriteLine();
                        Console.WriteLine("Exiting program...");
                        Console.WriteLine("Press any key to exit.");
                        Console.ReadKey();
                        break;
                    default: // Invalid choice
                        Console.WriteLine("Invalid choice. Press any key to try again.");
                        Console.ReadKey();
                        Console.Clear(); // Clear the console screen
                        break;
                        #endregion
                }
            }
        }
    }
}

