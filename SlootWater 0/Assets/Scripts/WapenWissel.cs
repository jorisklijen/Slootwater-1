using UnityEngine;

public class WapenWissel : MonoBehaviour
{
    [Tooltip("Gaat hier van uit van de index van hoe de wapens zijn ge sorteerd onder het wapenHandel object. (van boven naar beneden)")]
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

       
        // scrool weel muis
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

        //toetsen bord nummers 1, 2, 3, 4...
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedWapon = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount <=2)
        {
            selectedWapon = 1;
        }

    // if (Input.GetKeyDown(KeyCode.Alpha+toets nummer ) && transform.childCount >= 2)
    // {
    //     selectedWapon = -wapen index- ;
    // }



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
