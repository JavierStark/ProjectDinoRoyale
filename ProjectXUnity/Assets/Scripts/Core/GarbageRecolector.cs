using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runner.Core {
    public class GarbageRecolector : MonoBehaviour {
        private void OnTriggerEnter2D(Collider2D collision) {
            Destroy(collision.gameObject);
        }
    }
}
