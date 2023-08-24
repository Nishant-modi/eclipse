using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    int totalCompanions = 1;
    public int currentCompanionIndex;

    public GameObject[] companion;
    public GameObject currentCompanion;

  

    // Start is called before the first frame update
    void Start()
    {
        totalCompanions = transform.childCount;
        companion = new GameObject[totalCompanions];

        for(int i = 0; i < totalCompanions; i++)
        {
            companion[i] = transform.GetChild(i).gameObject;
            companion[i].SetActive(false);
        }

        //companion[0].SetActive(true);
        //currentCompanion = companion[0];
        //currentCompanionIndex = 0;  

        
    }

    public void ActivateCompanion(int index)
    {
        companion[currentCompanionIndex].SetActive(false);
        currentCompanionIndex = index;
        companion[currentCompanionIndex].SetActive(true);

    }

    public void DeactivateCompanion()
    {
        companion[currentCompanionIndex].SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.E))
        {
            companion[currentCompanionIndex].SetActive(false);
            currentCompanionIndex = (currentCompanionIndex + 1)%totalCompanions;
            companion[currentCompanionIndex].SetActive(true);
        }

    }
}
