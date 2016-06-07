using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System;
using System.IO;
using System.Text.RegularExpressions;

public class GetTxtFile : MonoBehaviour {

	public string content = string.Empty;
	//public List <string> linesOfCode_list = new List <string> ();
	public GettingKeysDown gkd_scr;

	void Start () {

		//TextAsset txt = (TextAsset)Resources.Load("HackingCode_1", typeof(TextAsset));
		//content = txt.text;

		try
		{
			string line;
			// Create a new StreamReader, tell it which file to read and what encoding the file
			// was saved as
			StreamReader theReader = new StreamReader("Assets/Resources/HackingCode_1.txt", Encoding.Default);
			// Immediately clean up the reader after this block of code is done.
			// You generally use the "using" statement for potentially memory-intensive objects
			// instead of relying on garbage collection.
			// (Do not confuse this with the using directive for namespace at the 
			// beginning of a class!)
			using (theReader)
			{
				// While there's lines left in the text file, do this:
				do
				{
					line = theReader.ReadLine();
					//line = Regex.Replace(line, @"^\s+$[\r\n]*", "", RegexOptions.Multiline);
					if (line != null)
					{
						// Do whatever you need to do with the text line, it's a string now
						// In this example, I split it into arguments based on comma
						// deliniators, then send that array to DoStuff()
						DoStuff(line);
					}
				}
				while (line != null);
				// Done reading, close the reader and return true to broadcast success    
				theReader.Close();
			}
		}
		// If anything broke in the try block, we throw an exception with information
		// on what didn't work
		catch (Exception e)
		{
			Debug.Log (e.Message);
			//Console.WriteLine("{0}\n", e.Message);
		}
	}


	void DoStuff (string alala) {

		gkd_scr.hackingCode_list.Add (alala);
	}

}

