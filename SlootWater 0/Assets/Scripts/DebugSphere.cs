using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugSphere : MonoBehaviour {
    private void OnDrawGizmos() {
        Gizmos.DrawSphere(transform.position, 0.1f);
    }
}
