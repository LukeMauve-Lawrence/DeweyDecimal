using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DeweyDecimal
{
    static class FindCallNumbers
    {
        // constants
        public static int NUMBER_OF_TOP_LEVELS = 7;
        public static int NUMBER_OF_LEVELS = 3;
        public static int NUMBER_OF_OPTIONS = 4;
        // File path (root directory of project + file name)
        private static string FILE_PATH = Directory.GetParent(Directory.GetParent(Directory.GetParent(Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory)).FullName).FullName).FullName + @"\dewey.txt";

        // globals
        public static int currentLevel;
        public static int numberOfTopLevelNodes;
        public static int[] selected = new int[NUMBER_OF_LEVELS];
        public static string[] answers = new string[NUMBER_OF_LEVELS];
        static Node root;
        public static Random random = new Random();
        public static List<int> level1 = new List<int>();
      
        // Scoring variables
        public static int INCORRECT_POINT = -500;
        public static int CORRECT_POINT = 250;
        public static int TIME_BONUS = 10000;
        public static int seconds;
        public static int highScore = 0;
        public static int sessionPoints = 0;
        
        //-----------------------------------------------------------------------------------------------------------------------
        // Tree
        //-----------------------------------------------------------------------------------------------------------------------
        #region
        /// <summary>
        /// Populate tree
        /// </summary>
        /// <param name="buttons"></param>
        public static void PopulateNodes(Button[] buttons)
        {
            // root node of tree
            root = newNode("Dewey Decimal", "System");

            // reset current level to 0 when populating tree
            currentLevel = 0;

            // file to read from to get tree data
            string[] lines = File.ReadAllLines(FILE_PATH);

            int q = 0; // level 1 index
            int w = 0; // level 2 index

            foreach (string line in lines)
            {
                string[] words = line.Split('+');

                // case is the number associated with call number level (level 1, 2, 3)
                switch (words[0])
                {
                    case "1":
                        root.child.Add(newNode(words[1], words[2]));
                        w = 0;
                        q++;
                        break;
                    case "2":
                        root.child[q-1].child.Add(newNode(words[1], words[2]));
                        w++;                      
                        break;
                    case "3":
                        root.child[q-1].child[w-1].child.Add(newNode(words[1], words[2]));
                        break;
                }              
            }
            numberOfTopLevelNodes = q;
            PopulateButtons(buttons);     
        }

        /// <summary>
        /// Represents a node of an n-ary tree
        /// </summary>
        public class Node
        {
            public string key;
            public string value;
            public List<Node> child = new List<Node>();
        };

        /// <summary>
        /// Utility function to create a new tree node
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Node newNode(string key, string value)
        {
            Node temp = new Node();
            temp.key = key;
            temp.value = value;
            return temp;
        }
        #endregion
        //-----------------------------------------------------------------------------------------------------------------------
        // End of Tree
        //-----------------------------------------------------------------------------------------------------------------------

        //-----------------------------------------------------------------------------------------------------------------------
        // Set up buttons and answers
        //-----------------------------------------------------------------------------------------------------------------------
        #region
        /// <summary>
        /// Populate buttons will call numbers and descriptions from tree
        /// </summary>
        /// <param name="buttons"></param>
        public static void PopulateButtons(Button[] buttons)
        {
            string[] calls = new string[NUMBER_OF_OPTIONS];

            // select which level of call numbers to populate calls array with
            switch (currentLevel)
            {
                case 0:
                    for (int i = 0; i < NUMBER_OF_OPTIONS; i++)
                    {
                        calls[i] = root.child[level1[i]].key + "\n" + root.child[level1[i]].value;
                    }
                    break;
                case 1:
                    for (int i = 0; i < NUMBER_OF_OPTIONS; i++)
                    {
                        calls[i] = root.child[selected[0]].child[i].key + "\n" + root.child[selected[0]].child[i].value;
                    }
                    break;
                case 2:
                    for (int i = 0; i < NUMBER_OF_OPTIONS; i++)
                    {
                        calls[i] = root.child[selected[0]].child[selected[1]].child[i].key + "\n" + root.child[selected[0]].child[selected[1]].child[i].value;
                    }
                    break;
            }

            // populate buttons with calls array values
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].Text = calls[i];
            }
        }

        /// <summary>
        /// Populates level1 list with unique random numbers
        /// </summary>
        public static void PopulateRandomLevel1()
        {
            level1.Clear();
            while (level1.Count != 4)
            {
                int r = random.Next(0, NUMBER_OF_TOP_LEVELS);
                if (!level1.Contains(r))
                {
                    level1.Add(r);
                }
            }
            level1.Sort();
        }

        /// <summary>
        /// Set array answers to contain answers for comparisons
        /// </summary>
        public static void SetAnswers()
        {
            answers[0] = root.child[selected[0]].key + "\n" + root.child[selected[0]].value;
            answers[1] = root.child[selected[0]].child[selected[1]].key + "\n" + root.child[selected[0]].child[selected[1]].value;
            answers[2] = root.child[selected[0]].child[selected[1]].child[selected[2]].key + "\n" + root.child[selected[0]].child[selected[1]].child[selected[2]].value;
        }

        /// <summary>
        /// Set label to top-level dewey description
        /// </summary>
        /// <param name="label"></param>
        public static void SetToFindLabel(Label label)
        {
            // randomly choose which top level category from list of call numbers being used in [level1]
            while (true)
            {
                int r = random.Next(0, NUMBER_OF_TOP_LEVELS);
                if (level1.Contains(r))
                {
                    selected[0] = r;
                    break;
                }
            }

            // randomly selected description
            for (int i = 1; i < NUMBER_OF_LEVELS; i++)
            {
                selected[i] = random.Next(0, NUMBER_OF_LEVELS);
            }

            SetAnswers();
            label.Text = "Where could I find\n" + root.child[selected[0]].child[selected[1]].child[selected[2]].value + "?";
        }
        #endregion
        //-----------------------------------------------------------------------------------------------------------------------
        // End of Set up buttons and answers
        //-----------------------------------------------------------------------------------------------------------------------

        //-----------------------------------------------------------------------------------------------------------------------
        // Check answers
        //-----------------------------------------------------------------------------------------------------------------------
        #region
        /// <summary>
        /// Method for when an option is selected
        /// </summary>
        /// <param name="btnSender"></param>
        /// <param name="buttons"></param>
        /// <returns></returns>
        public static bool ActivateButton(object btnSender, Button[] buttons, Label label)
        {
            Button btn = (Button)btnSender;

            if (CheckAnswer(btn.Text))
            {                
                currentLevel++;
                AddPoints(label);               
                if (currentLevel == 3)
                {                  
                    MessageBox.Show("Congrats" +
                        "\nYou scored: " + 
                        sessionPoints, "Winner!");
                    return true;
                }
                PopulateButtons(buttons);
            }
            else
            {
                currentLevel++;           
                MinusPoints(label);
                MessageBox.Show("Wrong!" +
                    "\nCorrect answer was:\n\n" + 
                    answers[currentLevel - 1], "Incorrect!");
                if (currentLevel == 3)
                {
                    return true;
                }
                PopulateButtons(buttons);
            }
            return false;
        }

        /// <summary>
        /// Checks if the correct answer was selected
        /// </summary>
        /// <param name="answer"></param>
        /// <returns></returns>
        public static bool CheckAnswer(string answer)
        {
            if (!answer.Equals(answers[currentLevel]))
            {
                return false;
            }
            return true;
        }
        #endregion
        //-----------------------------------------------------------------------------------------------------------------------
        // End of checking answers
        //-----------------------------------------------------------------------------------------------------------------------

        //-----------------------------------------------------------------------------------------------------------------------
        // Point system
        //-----------------------------------------------------------------------------------------------------------------------
        #region
        /// <summary>
        /// Minus points from current points
        /// </summary>
        public static void MinusPoints(Label label)
        {
            // minus points
            sessionPoints += INCORRECT_POINT;
            label.Text = sessionPoints.ToString();
        }

        /// <summary>
        /// Add points and update highscore
        /// </summary>
        public static void AddPoints(Label label)
        {
            if (seconds == 0)
            {
                seconds = 1;
            }
            // calculate points
            sessionPoints += CORRECT_POINT + (TIME_BONUS / seconds);

            // check if it is a new high score
            HighScore(sessionPoints);

            // add points
            label.Text = sessionPoints.ToString();
        }  

        /// <summary>
        /// Update Highscore
        /// </summary>
        /// <returns></returns>
        public static void HighScore(int score)
        {
            // check if it is a new high score
            if (score > highScore)
            {
                highScore = score;
            }
        }
        #endregion
        //-----------------------------------------------------------------------------------------------------------------------
        // End of Point System
        //-----------------------------------------------------------------------------------------------------------------------
    }
}
