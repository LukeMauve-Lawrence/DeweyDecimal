# Dewey Decimal Educator
Third Year Varsity final program

This program is to help teach people about the Dewey Decimal System.
Which is a system used in libraries to store and sort books.

It consists of **3** main sections

## Replacing Books
Teaches the user how to order the books in the Dewey Decimal System.<br />
Populates a list with randomly generated call numbers from the Dewey Decimal System.<br />
It then populates a row of buttons with those call numbers and asks the user to sort them into the correct order. <br />
<br />
![This is an image](https://github.com/LukeMauve-Lawrence/DeweyDecimal/blob/main/Screenshots/ReplacingBooks1.PNG) <br />
<br />
They can do so by click on a call number button and then selecting the position they wish to set it on the sorted row. <br />
<br />
![This is an image](https://github.com/LukeMauve-Lawrence/DeweyDecimal/blob/main/Screenshots/ReplacingBooks2.PNG) <br />
<br />
When the user is happy with the sorting of the call numbers, they can press the check button to see if they sorted it correctly. <br />

## Identifying Areas
Matching columns of either call numbers or their descriptions to their counterpart. <br />
Uses a data dictionary to store call numbers with their descriptions. <br />
Randomly populates 2 columns of buttons. One column having call numbers, the other having descriptions. <br />
The user can select a button on the left column and then click a button from the right column. <br />
<br />
![This is an image](https://github.com/LukeMauve-Lawrence/DeweyDecimal/blob/main/Screenshots/IdentifyingAreas1.PNG) <br />
<br />
This will match the 2 cells to each other and draw a line to show that they are connected. Lines are drawn using Pens on a canvas. <br />
<br />
![This is an image](https://github.com/LukeMauve-Lawrence/DeweyDecimal/blob/main/Screenshots/IdentifyingAreas2.PNG) <br />
<br />
When they user is happy with their matching of the columns, they can select the check button. <br />
If they matched it correctly, they will be awarded points, else they were wrong, they will lose points. <br />

## Finding Call Numbers
Generates a random call number and displays its description. (eg. 811 - American poetry in English). <br />
Then four buttons below are populated with text of top level call numbers and descriptions, of which only one will be correct <br />
(eg. 000 - Computer science, information, and general works; 500 - Science; 600 - Technology; 800 - Literature (Belles-lettres) and rhetoric). <br />
<br />
![This is an image](https://github.com/LukeMauve-Lawrence/DeweyDecimal/blob/main/Screenshots/FindingCallNumbers1.PNG) <br />
<br />
The user must select the correct option to get it right. (eg. 700 for Decorative Arts). <br />
After a selection is made, the next level will be populated onto the buttons <br />
(eg. 810 - American literature in English, 820 - English and Old English (Anglo-Saxon) literatures, 830 - German literature and literatures of related languages, 840 - French literature and literatures of related Romance languages). <br />
<br />
![This is an image](https://github.com/LukeMauve-Lawrence/DeweyDecimal/blob/main/Screenshots/FindingCallNumbers2.PNG) <br />
<br />
Once they select again, they will proceed to the last level, level 3 and the buttons will be populated again <br />
(eg. 811 - American poetry in English, 812 - American drama in English, 813 - American fiction in English, 814 - American essays in English). <br />
<br />
![This is an image](https://github.com/LukeMauve-Lawrence/DeweyDecimal/blob/main/Screenshots/FindingCallNumbers3.PNG)<br />
<br />
Data for these call numbers and descriptions are stored in a text file. <br />
Upon loading the page, the data from the text file will loaded into memory using a generic tree structure <br />
