using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class FileManager : MonoBehaviour
{
    string namesFilePath = "F:/UnityProyectos/ProjectX/ProjectX/ProjectXUnity/Assets/Names.txt";

    public string[] ReadFile() {
        string[] namesInFile = File.ReadAllLines(namesFilePath);

        return namesInFile;
    }
}
