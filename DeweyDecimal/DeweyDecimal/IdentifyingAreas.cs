using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DeweyDecimal
{
    static class IdentifyingAreas
    {
        // Dewey Decimal System call numbers and descriptions
        public static Dictionary<string, string> callNumbersAndDesc = new Dictionary<string, string>() {
            { "000", "Computer science, information and general works" },
            { "100", "Philosophy and psychology" },
            { "200", "Religion" },
            { "300", "Social sciences" },
            { "400", "Language" },
            { "500", "Pure Science" },
            { "600", "Technology" },
            { "700", "Arts and recreation" },
            { "800", "Literature" },
            { "900", "History and geography" }
        };

        // different lines being drawn with their y location
        public static Dictionary<string, float> pens = new Dictionary<string, float>()
        {
            { "red", 0 },
            { "blue", 0 },
            { "green", 0 },
            { "yellow", 0 }
        };

        // y location for possible points for line to connect to
        public static List<float> yLocation = new List<float>() { 21.0F, 81.0F, 141.0F, 201.0F, 261.0F, 321.0F, 381.0F};

        // if the left column has call numbers or descriptions used for differentiating later
        public static bool isLeftColumnCallNumbers;

        private static Random random = new Random();
        public static Button currentLeftButton;
        public static Button currentRightButton;

        /// <summary>
        /// Starts game
        /// </summary>
        /// <param name="leftButtons"></param>
        /// <param name="numberOfLeftButtons"></param>
        /// <param name="rightButtons"></param>
        /// <param name="numberOfRightButtons"></param>
        public static void GenerateRandomCallNumbers(Button[] leftButtons, int numberOfLeftButtons, Button[] rightButtons, int numberOfRightButtons)
        {
            //reseting variables
            pens["red"] = 0;
            pens["blue"] = 0;
            pens["green"] = 0;
            pens["yellow"] = 0;
            isLeftColumnCallNumbers = false;
            SetRightButtonColors(rightButtons, numberOfRightButtons);

            if (random.Next(0, 2) == 0)
            {
                isLeftColumnCallNumbers = true;
            }
           
            SetColumns(leftButtons, numberOfLeftButtons, rightButtons, numberOfRightButtons);
        }

        /// <summary>
        /// Reset right column buttons to default colours
        /// </summary>
        /// <param name="rightButtons"></param>
        /// <param name="numberOfRightButtons"></param>
        public static void SetRightButtonColors(Button[] rightButtons, int numberOfRightButtons)
        {
            for (int i = 0; i < numberOfRightButtons; i++)
            {
                rightButtons[i].Text = (i + 1).ToString();
                rightButtons[i].BackColor = Color.FromArgb(59, 148, 94);
                rightButtons[i].ForeColor = Color.White;
            }
        }

        /// <summary>
        /// Randomizing questions and possible answers for matching
        /// </summary>
        /// <param name="leftButtons"></param>
        /// <param name="numberOfLeftButtons"></param>
        /// <param name="rightButtons"></param>
        /// <param name="numberOfRightButtons"></param>
        public static void SetColumns(Button[] leftButtons, int numberOfLeftButtons, Button[] rightButtons, int numberOfRightButtons)
        {
            List<string> leftColumn = new List<string>();
            List<string> rightColumn = new List<string>();

            // populating left column list and start of right
            while (leftColumn.Count < numberOfLeftButtons)
            {
                int num = random.Next(0, 9);
                string str;
                string rightStr;

                if (isLeftColumnCallNumbers)
                {
                    str = callNumbersAndDesc.ElementAt(num).Key;
                    rightStr = callNumbersAndDesc.ElementAt(num).Value;
                } 
                else
                {
                    str = callNumbersAndDesc.ElementAt(num).Value;
                    rightStr = callNumbersAndDesc.ElementAt(num).Key;
                }
                

                if (!leftColumn.Contains(str))
                {
                    leftColumn.Add(str);
                    rightColumn.Add(rightStr);
                }
            }

            // populating right column list with fake answers
            while (rightColumn.Count < numberOfRightButtons)
            {
                int num = random.Next(0, 9);
                string rightStr;
                if (isLeftColumnCallNumbers)
                {
                    rightStr = callNumbersAndDesc.ElementAt(num).Value;
                }
                else
                {
                    rightStr = callNumbersAndDesc.ElementAt(num).Key;
                }
                
                if (!rightColumn.Contains(rightStr))
                {
                    rightColumn.Add(rightStr);
                }
            }

            // Populates left columns
            for (int i = 0; i < numberOfLeftButtons; i++)
            {
                leftButtons[i].Text = leftColumn[i];
            }

            Shuffle(rightColumn); //shuffling right column so that answers don't line up

            // Populates right columns
            for (int i = 0; i < numberOfRightButtons; i++)
            {
                rightButtons[i].Text = rightColumn[i];
            }
        }

        /// <summary>
        /// Shuffle a list into a random order
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
       
        //-----------------------------------------------------------------------------------------------------------------------
        // User input and scoring Methods
        //-----------------------------------------------------------------------------------------------------------------------
        #region
        /// <summary>
        /// Method called on each call number button click
        /// </summary>
        /// <param name="btnSender"></param>
        public static void ActivateLeftButton(object btnSender)
        {
            // sets the button to the selected call number
            if (btnSender != null)
            {
                // if previous one was selected, deselect it
                if (currentLeftButton != null)
                {
                    currentLeftButton.FlatStyle = FlatStyle.Flat;
                    currentLeftButton = null;
                }

                // highlight selected button and set it to currentLeftButton
                if (currentLeftButton != (Button)btnSender)
                {
                    currentLeftButton = (Button)btnSender;
                    currentLeftButton.FlatStyle = FlatStyle.Popup;
                }
            }
        }

        /// <summary>
        /// Method called when a button on the sorted list if pressed
        /// </summary>
        /// <param name="btnSender"></param>
        public static void ActivateRightButton(object btnSender, int numberOfRightButtons, Button[] rightButtons, Button[] leftButtons)
        {
            if (btnSender == null)
            {
                return;
            }

            // if no call number was pressed, return
            if (currentLeftButton == null)
            {
                return;
            }

            currentRightButton = (Button)btnSender;

            CheckIfMatched(numberOfRightButtons, rightButtons);

            currentRightButton.BackColor = currentLeftButton.BackColor;
            currentRightButton.ForeColor = currentLeftButton.ForeColor;

            DrawLine(numberOfRightButtons, rightButtons);

            currentLeftButton.FlatStyle = FlatStyle.Flat;
            currentLeftButton = null;
        }

        /// <summary>
        /// Method to draw lines between matched columns by setting the location end of the line to be drawn
        /// </summary>
        /// <param name="numberOfRightButtons"></param>
        /// <param name="rightButtons"></param>
        public static void DrawLine(int numberOfRightButtons, Button[] rightButtons)
        {
            if (currentLeftButton.BackColor == Color.Red)
            {
                for (int i = 0; i < numberOfRightButtons; i++)
                {
                    if (rightButtons[i].BackColor == Color.Red)
                    {
                        pens["red"] = yLocation[i];
                        
                        //removes old lines if they are pointing in the same place
                        if (pens["blue"] == pens["red"])
                        {
                            pens["blue"] = 0;
                        }

                        if (pens["green"] == pens["red"])
                        {
                            pens["green"] = 0;
                        }

                        if (pens["yellow"] == pens["red"])
                        {
                            pens["yellow"] = 0;
                        }
                    }
                }
            }

            if (currentLeftButton.BackColor == Color.Blue)
            {
                for (int i = 0; i < numberOfRightButtons; i++)
                {
                    if (rightButtons[i].BackColor == Color.Blue)
                    {
                        pens["blue"] = yLocation[i];

                        //removes old lines if they are pointing in the same place
                        if (pens["red"] == pens["blue"])
                        {
                            pens["red"] = 0;
                        }

                        if (pens["green"] == pens["blue"])
                        {
                            pens["green"] = 0;
                        }

                        if (pens["yellow"] == pens["blue"])
                        {
                            pens["yellow"] = 0;
                        }
                    }
                }
            }

            if (currentLeftButton.BackColor == Color.FromArgb(0, 192, 0))
            {
                for (int i = 0; i < numberOfRightButtons; i++)
                {
                    if (rightButtons[i].BackColor == Color.FromArgb(0, 192, 0))
                    {
                        pens["green"] = yLocation[i];

                        //removes old lines if they are pointing in the same place
                        if (pens["blue"] == pens["green"])
                        {
                            pens["blue"] = 0;
                        }

                        if (pens["red"] == pens["green"])
                        {
                            pens["red"] = 0;
                        }

                        if (pens["yellow"] == pens["green"])
                        {
                            pens["yellow"] = 0;
                        }
                    }
                }
            }

            if (currentLeftButton.BackColor == Color.FromArgb(192, 192, 0))
            {
                for (int i = 0; i < numberOfRightButtons; i++)
                {
                    if (rightButtons[i].BackColor == Color.FromArgb(192, 192, 0))
                    {
                        pens["yellow"] = yLocation[i];

                        //removes old lines if they are pointing in the same place
                        if (pens["blue"] == pens["yellow"])
                        {
                            pens["blue"] = 0;
                        }

                        if (pens["green"] == pens["yellow"])
                        {
                            pens["green"] = 0;
                        }

                        if (pens["red"] == pens["yellow"])
                        {
                            pens["red"] = 0;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Checks if the number is already in the user sorted shelf
        /// </summary>
        public static void CheckIfMatched(int numberOfRightButtons, Button[] rightButtons)
        {
            for (int i = 0; i < numberOfRightButtons; i++)
            {
                if (rightButtons[i].BackColor == currentLeftButton.BackColor)
                {
                    rightButtons[i].BackColor = Color.FromArgb(59, 148, 94);
                }
            }
        }

        /// <summary>
        /// Method to check if the columns are matched correctly
        /// </summary>
        /// <param name="leftButtons"></param>
        /// <param name="numberOfLeftButtons"></param>
        /// <param name="rightButtons"></param>
        /// <param name="numberOfRightButtons"></param>
        /// <returns></returns>
        public static bool CheckAnswer(Button[] leftButtons, int numberOfLeftButtons, Button[] rightButtons, int numberOfRightButtons)
        {
            int answers = 0; //numbers of answers counter

            for (int i = 0; i < numberOfLeftButtons; i++)
            {
                for (int j = 0; j < numberOfRightButtons; j++)
                {
                    if (leftButtons[i].BackColor == rightButtons[j].BackColor)
                    {
                        answers++;
                        if (isLeftColumnCallNumbers)
                        {
                            if (rightButtons[j].Text != callNumbersAndDesc[leftButtons[i].Text])
                            {
                                return false;
                            }
                        } 
                        else
                        {
                            if (leftButtons[i].Text != callNumbersAndDesc[rightButtons[j].Text])
                            {
                                return false;
                            }
                        }
                        
                    }
                }
            }
            if (answers != 4)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Show the correct answer
        /// </summary>
        /// <param name="numberOfLeftButtons"></param>
        /// <param name="leftButtons"></param>
        /// <param name="numberOfRightButtons"></param>
        /// <param name="rightButtons"></param>
        public static void ViewCorrectAnswers(int numberOfLeftButtons, Button[] leftButtons, int numberOfRightButtons, Button[] rightButtons)
        {
            string answer = "";
            int j;

            //calucating and showing correct answer
            if (isLeftColumnCallNumbers)
            {
                for (int i = 0; i < numberOfLeftButtons; i++)
                {
                    j = i + 1;
                    answer += j + ". " + leftButtons[i].Text;
                    answer += "\t -> " + callNumbersAndDesc[leftButtons[i].Text];
                    answer += "\n";
                }
                MessageBox.Show(answer, "Answer", MessageBoxButtons.OK);
                return;
            }
            for (int i = 0; i < numberOfLeftButtons; i++)
            {
                j = i + 1;
                answer += j + ". " + callNumbersAndDesc[rightButtons[i].Text];
                answer += "\n->" + rightButtons[i].Text;
                answer += "\n";
            }
            MessageBox.Show(answer, "Answer", MessageBoxButtons.OK);
            return;
        }
        #endregion
    }
}
