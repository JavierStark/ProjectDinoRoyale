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

    [SerializeField] private Name[] names;
    [SerializeField] private Face[] faces;

    public void Reset() {
        FileManager fileManager = FindObjectOfType<FileManager>();
        for (int i = 0; i<names.Length; i++) {
            names[i].dinoName = fileManager.ReadFile()[i];
            names[i].inUse = false;
        }
        for (int i = 0; i<faces.Length; i++) {
            faces[i].inUse = false;
        }
    }

    public string GetName() {
        string name = null;
        while (name == null) {
            int randomIndex = Random.Range(0, names.Length);
            if (!names[randomIndex].inUse) {
                name = names[randomIndex].dinoName;
                names[randomIndex].inUse = true;
            }
        }
        return name;
    }

    public Sprite GetFace() {
        Sprite face = null;
        while (face == null) {
            int randomIndex = Random.Range(0, faces.Length);
            if (!faces[randomIndex].inUse) {
                face = faces[randomIndex].dinoSprite;
                faces[randomIndex].inUse = true;
            }            
        }

        return face;
    }


}
