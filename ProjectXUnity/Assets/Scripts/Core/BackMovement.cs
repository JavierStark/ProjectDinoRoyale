using UnityEngine;

namespace Runner.Core {

    public class BackMovement : MonoBehaviour {
        Material material;
        Vector2 offset;
        [Range(0,100)][SerializeField] float velocity = 1;

        private void Awake() {
            material = GetComponent<Renderer>().material;
        }

        private void Update() {
            if (GameManager.instance.IsPlayerAlive) {
                offset = new Vector2(velocity, 0);
                material.mainTextureOffset += offset*Time.deltaTime*GameManager.instance.GlobalMultiplier();
            }                   
        }

    }
}
