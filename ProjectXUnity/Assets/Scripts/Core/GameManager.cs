using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runner.Core {
    public class GameManager : MonoBehaviour {
        bool isPlayerAlive = true;

        public bool IsPlayerAlive() {
            return isPlayerAlive;
        }
    }

}