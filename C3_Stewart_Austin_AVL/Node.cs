#region Usings
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#endregion

namespace C3_Stewart_Austin_AVL
{
    internal class Node
    {
        #region Members
        // Properties to store the word, number of letters, and references to the left and right nodes
        public string Word { get; set; } // Stores the word associated with the node
        public int NumLetters { get; set; } // Stores the number of letters in the word
        public Node Left { get; set; } // Reference to the left node in the tree
        public Node Right { get; set; } // Reference to the right node in the tree
        #endregion
        #region Constructors
        // Default constructor to initialise properties
        public Node()
        {
            Word = null;
            NumLetters = 0;
            Left = null;
            Right = null;
        }

        // Parameterised constructor to set word, number of letters, and initialise references
        public Node(string word, int numLetters)
        {
            this.Word = word;
            this.NumLetters = numLetters;
            Left = null;
            Right = null;
        }
        #endregion
        #region Print Method
        // Method to generate a string representation of the node, including the word and its length
        public override string ToString()
        {
            return Word.ToString() + " " + NumLetters.ToString();
        }
        #endregion
    }
}

