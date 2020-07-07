using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Runner.Core {
    public class BackMovement : MonoBehaviour {

        [Range(0,0.09f)][SerializeField] float soilVelocity;
        RawImage soil;

        private void Start() {
            soil = GetComponent<RawImage>();
        }
        private void Update() {
            
            soil.uvRect = new Rect(soil.uvRect.position + new Vector2(soilVelocity,0), soil.uvRect.size);
        }
    }
}
