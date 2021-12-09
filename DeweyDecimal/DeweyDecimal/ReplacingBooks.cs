using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DeweyDecimal
{
    class ReplacingBooks
    {
        //Fields
        private static Random random = new Random();
        public static Button currentCallButton;
        public static Button currentOrderButton;

        //-----------------------------------------------------------------------------------------------------------------------
        // Quicksort Methods
        //-----------------------------------------------------------------------------------------------------------------------
        #region
        /// <summary>
        /// Quicksort partition method
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        static int Partition(List<string> arr, int start, int end)
        {
            int pivot = end;
            int i = start, j = end;
            string temp;
            while (i < j)
            {
                while (i < end && string.Compare(arr[i], arr[pivot]) < 0)
                    i++;
                while (j > start && string.Compare(arr[j], arr[pivot]) > 0)
                    j--;

                if (i < j)
                {
                    temp = arr[i];
                    arr[i] = arr[j];
                    arr[j] = temp;
                }
            }
            temp = arr[pivot];
            arr[pivot] = arr[j];
            arr[j] = temp;
            return j;
        }

        /// <summary>
        /// Quicksort method
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        public static void Quicksort(List<string> arr, int start, int end)
        {
            if (start < end)
            {
                int pivotIndex = Partition(arr, start, end);
                Quicksort(arr, start, pivotIndex - 1);
                Quicksort(arr, pivotIndex + 1, end);
            }
        }
        #endregion
        //-----------------------------------------------------------------------------------------------------------------------
        // End of Quicksort Methods
        //-----------------------------------------------------------------------------------------------------------------------

        //-----------------------------------------------------------------------------------------------------------------------
        // Setup Methods
        //-----------------------------------------------------------------------------------------------------------------------
        #region
        /// <summary>
        /// Generate random call numbers
        /// </summary>
        public static List<string> GenerateRandomCallNumbers(int numberOfCallNumbers)
        {
            List<string> callNumbers = new List<string>();
            callNumbers.Clear();
            //Generating random call numbers and populating list
            for (int i = 0; i < numberOfCallNumbers; i++)
            {
                var callNumberBuilder = new StringBuilder();
                callNumberBuilder.Append(RandomNumber(3));

                //Random if there is a decimal or not
                if (Int32.Parse(RandomNumber(1)) > 5)
                {
                    callNumberBuilder.Append(".");
                    callNumberBuilder.Append(random.Next(0, 999));
                }

                callNumberBuilder.Append(" ");
                callNumberBuilder.Append(RandomString(3));
                callNumbers.Add(callNumberBuilder.ToString());
            }
            return callNumbers;
        }

        /// <summary>
        /// Set unsorted call number buttons for user to sort
        /// </summary>
        public static void setOrderNumberButtons(Button[] buttons, int numberOfCallNumbers)
        {
            for (int i = 0; i < numberOfCallNumbers; i++)
            {
                buttons[i].Text = (i + 1).ToString();
                buttons[i].BackColor = Color.FromArgb(151, 204, 184);
                buttons[i].ForeColor = Color.FromArgb(24, 38, 40);
            }
        }

        /// <summary>
        /// Set unsorted call number buttons for user to sort
        /// </summary>
        public static void setCallNumberButtons(Button[] buttons, List<string> callNumbers, int numberOfCallNumbers)
        {
            for (int i = 0; i < numberOfCallNumbers; i++)
            {
                buttons[i].Text = callNumbers[i];
                buttons[i].BackColor = Color.FromArgb(59, 148, 94);
                buttons[i].ForeColor = Color.White;
            }
        }

        /// <summary>
        /// Generate a random number of a given length
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string RandomNumber(int length)
        {
            const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        /// <summary>
        /// Generate a random string of a given length
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        #endregion
        //-----------------------------------------------------------------------------------------------------------------------
        // Setup Methods
        //-----------------------------------------------------------------------------------------------------------------------


        //-----------------------------------------------------------------------------------------------------------------------
        // User input and scoring Methods
        //-----------------------------------------------------------------------------------------------------------------------
        #region
        /// <summary>
        /// Method called on each call number button click
        /// </summary>
        /// <param name="btnSender"></param>
        public static void ActivateCallButton(object btnSender)
        {
            // sets the button to the selected call number
            if (btnSender != null)
            {
                // if previous one was selected, deselect it
                if (currentCallButton != null)
                {
                    currentCallButton.BackColor = Color.FromArgb(59, 148, 94);
                    currentCallButton.ForeColor = Color.White;
                    currentCallButton = null;
                }

                // highlight selected button and set it to currentCallButton
                if (currentCallButton != (Button)btnSender)
                {
                    currentCallButton = (Button)btnSender;
                    currentCallButton.BackColor = Color.FromArgb(59, 222, 155);
                }
            }
        }

        /// <summary>
        /// Method called when a button on the sorted list if pressed
        /// </summary>
        /// <param name="btnSender"></param>
        public static void ActivateOrderButton(object btnSender, int numberOfCallNumbers, Button[] orderButtons, Button[] callButtons)
        {
            if (btnSender != null)
            {
                currentOrderButton = (Button)btnSender;
                CheckIfNumberUsed(btnSender, numberOfCallNumbers, orderButtons);
                // if no call number was pressed, return
                if (currentCallButton == null)
                {
                    //checks if order button already has a call number placed on it
                    if (currentOrderButton.Text.Length > 2)
                    {
                        currentCallButton = (Button)btnSender;
                        currentCallButton.BackColor = Color.FromArgb(59, 222, 155);
                    }
                    return;
                }


                //checks if order button already has a call number placed on it
                if (currentOrderButton.Text.Length > 2)
                {
                    UngreyOut(numberOfCallNumbers, callButtons);
                }
                currentOrderButton.Text = currentCallButton.Text;
                currentOrderButton.BackColor = Color.FromArgb(59, 148, 94);
                currentOrderButton.ForeColor = Color.White;

                // when move is made on only the user sorted list, move the call number to new location
                // and replace previous one with number in row
                for (int i = 0; i < numberOfCallNumbers; i++)
                {
                    if (currentCallButton == orderButtons[i])
                    {
                        currentCallButton.Text = (i + 1).ToString();
                    }
                }

                currentCallButton.BackColor = Color.FromArgb(151, 204, 184);
                currentCallButton.ForeColor = Color.FromArgb(24, 38, 40);
                currentCallButton = null;
            }
        }

        /// <summary>
        /// Checks if the number is already in the user sorted shelf
        /// </summary>
        public static void CheckIfNumberUsed(object btnSender, int numberOfCallNumbers, Button[] orderButtons)
        {
            Button button = (Button)btnSender;
            for (int i = 0; i < numberOfCallNumbers; i++)
            {
                if (orderButtons[i] != button)
                {
                    if (orderButtons[i].Text.Equals(button.Text))
                    {
                        orderButtons[i].Text = (i + 1).ToString();
                        orderButtons[i].BackColor = Color.FromArgb(151, 204, 184);
                        orderButtons[i].ForeColor = Color.FromArgb(24, 38, 40);
                    }
                }

            }
        }

        /// <summary>
        /// puts color back into the call number button if it taken off the sorted shelf
        /// </summary>
        public static void UngreyOut(int numberOfCallNumbers, Button[] callButtons)
        {
            for (int i = 0; i < numberOfCallNumbers; i++)
            {
                if (callButtons[i].Text.Equals(currentOrderButton.Text))
                {
                    callButtons[i].BackColor = Color.FromArgb(59, 148, 94);
                    callButtons[i].ForeColor = Color.White;
                }
            }

        }

        /// <summary>
        /// Check that user sorted order is correct
        /// </summary>
        public static bool checkOrder(int numberOfCallNumbers, Button[] orderButtons, List<string> callNumbers)
        {
            for (int i = 0; i < numberOfCallNumbers; i++)
            {
                if (!orderButtons[i].Text.Equals(callNumbers[i]))
                {
                    return false;
                }
            }
            return true;
        }
        #endregion
        //-----------------------------------------------------------------------------------------------------------------------
        // End of User input and scoring Methods
        //-----------------------------------------------------------------------------------------------------------------------

    }
}
