using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dinos")]
public class DinosScriptableObject : ScriptableObject
{
    [System.Serializable]
    public class Name {
        public string dinoName;
        public bool inUse = false;
    }
    [System.Serializable]
    public class Face {
        public Sprite dinoSprite;
        public bool inUse = false;
    }

    public Name[] names;
    public Face[] faces;

    public void Reset() {
        for (int i = 0; i<names.Length; i++) {
            names[i].inUse = false;
        }
        for (int i = 0; i<faces.Length; i++) {
            faces[i].inUse = false;
        }
        Debug.Log("reset");
    }

    public string GetName() {
        Debug.Log("get name");
        string name = null;
        while (name == null) {
            int randomIndex = Random.Range(0, names.Length);
            if (!names[randomIndex].inUse) {
                name = names[randomIndex].dinoName;
                names[randomIndex].inUse = true;
            }
        }
        Debug.Log(name);
        return name;
    }

    public Sprite GetFace() {
        Debug.Log("get face");
        Sprite face = null;
        while (face == null) {
            int randomIndex = Random.Range(0, faces.Length);
            if (!faces[randomIndex].inUse) {
                face = faces[randomIndex].dinoSprite;
                faces[randomIndex].inUse = true;
            }            
        }
        Debug.Log(face);
        return face;
    }
}
