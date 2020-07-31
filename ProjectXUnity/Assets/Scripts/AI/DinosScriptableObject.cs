using System.Collections.Generic;
using System.Linq;
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

    List<string> dinoNames = new List<string>();
    [SerializeField] private Face[] faces;

    public void Reset() {
        dinoNames.Clear();
        foreach (string name in ServerManager.instance.GetNicknames())
		{
            dinoNames.Add(name);
		}

		for (int i = 0; i < faces.Length; i++)
		{
			faces[i].inUse = false;
		}
	}

	public string GetName()
	{
        string name = dinoNames.ElementAt(Random.Range(0, dinoNames.Count));
        dinoNames.Remove(name);
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
