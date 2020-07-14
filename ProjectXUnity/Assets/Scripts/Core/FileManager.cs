using System.Collections;
using UnityEngine;
using System.IO;


public class FileManager : MonoBehaviour
{
	string namesFilePath;
	
	public string[] ReadFile() {
		if (namesFilePath == null)
		{
			namesFilePath = Application.dataPath + "/" + "Names.txt";
		}
        string[] namesInFile = File.ReadAllLines(namesFilePath);

        return namesInFile;
    }
}
