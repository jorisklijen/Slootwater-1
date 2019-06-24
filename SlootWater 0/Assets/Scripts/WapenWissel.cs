using UnityEngine;

public class WapenWissel : MonoBehaviour
{
    public int selectedWapon = 0;

    // Start is called before the first frame update
    void Start()
    {
        SelectedWapon();
    }

    // Update is called once per frame
    void Update()
    {
        int priviusSelectedWapon = selectedWapon;

        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (selectedWapon >= transform.childCount - 1)
            {
                selectedWapon = 0;
            }
            else
            {
                selectedWapon++;
            }
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (selectedWapon <= 0)
            {
                selectedWapon = transform.childCount - 1;
            }
            else
            {
                selectedWapon--;
            }
        }

        if (priviusSelectedWapon != selectedWapon)
        {
            SelectedWapon();
        }

    }

    void SelectedWapon()
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if (i == selectedWapon)
            {
                weapon.gameObject.SetActive(true);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
            i++;
        }

    }
}
