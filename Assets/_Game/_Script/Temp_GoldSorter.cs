using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temp_GoldSorter : MonoBehaviour
{
    // Start is called before the first frame update




    public List<GameObject> GoldLocations;
    public int Currentndex=0;
    public GameObject GoldPrefab;
    public List<GameObject> GoldObjects;

    public Player Player;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void InstGold()
    {
        GoldObjects.Add(Instantiate(GoldPrefab, GoldLocations[Currentndex].transform.position, new Quaternion(0f, 0f, 0f, 0)));
        Currentndex++;
    }


    private void OnTriggerEnter(Collider other)
    {
        int len = GoldObjects.Count -1 ;
        for (int i = len; i >=0; i--)
        {
            Destroy(GoldObjects[i]);
        }
        GoldObjects.Clear();
        Player.CurrentAmount = len;
        ButtonManager.Instance.UpdateCashText();
        Currentndex = 0;
    }
}
