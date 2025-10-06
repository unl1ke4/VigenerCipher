# Program documentation
## General information
	Program name: VigenereCipher
	Programming language: C#
	Application type: Console
	Design pattern: MVC (Model-View-Controller)
	Development environment: Visual Studio
	Program goal: Implementation of the algorithm for encrypting and decrypting text using the Vigenere method, with the option for the user to select the operating mode via the console interface.

## Description of the Vigenere algorithm
	The Vigenere method is a polyalphabetic substitution cipher that uses a keyword to shift each letter of the plaintext.
	Encryption is performed using the following formula:
		C_i=(P_i+K_i )  mod26
	Decryption:
		P_i=(C_i-K_i+26)  mod26
	Meaning:
		P_i - plain text letter
		C_i - cipher text letter
		K_i - key letter

## Description of the Vigenere algorithm
	The program implements the MVC model, which separates logic, interface, and control:
		• Model - VigenereModel.cs
		  Contains an encryption and decryption algorithm.
		• View - VigenereView.cs
		  Provides interaction with the user via the console displays menus, reads text and keys, displays results.
		• Controller - VigenereController.cs
		  Manages the logic of the work receives data from View, processes it through Model, and displays the result.

## Program logic
	1. The user chooses the mode.
	2. The program requests the text and the key.
	3. Performs encryption or decryption.
	4. Outputs the result.
	5. Returns to the main menu.

## Testing
	• Unit tests
	   Check the correctness of the encryption and decryption algorithm.
	• Integration tests
	   They check the interaction between Controller + View + Model.