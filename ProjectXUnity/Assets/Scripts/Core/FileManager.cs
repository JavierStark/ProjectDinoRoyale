using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class FileManager : MonoBehaviour
{
    string namesFilePath = "F:/UnityProyectos/ProjectX/ProjectX/ProjectXUnity/Assets/Names.txt";

    private void Awake() {
        Debug.Log(namesFilePath);
    }

    private void Start() {
        ReadFile();
    }


    public string[] ReadFile() {
        string[] namesInFile = File.ReadAllLines(namesFilePath);
        foreach(string name in namesInFile) {
            print(name);
        }
        return namesInFile;
    }
}
