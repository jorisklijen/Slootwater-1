using UnityEngine;

public class MaakKapot : MonoBehaviour{
    public float timeUntilKapot = 1.0f;

    private void Start(){
        Destroy(gameObject, timeUntilKapot);
    }
}
