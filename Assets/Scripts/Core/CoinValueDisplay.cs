using FPS.Control;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinValueDisplay : MonoBehaviour
{
     InventoryItems player;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").GetComponent<InventoryItems>();
    }

    void Update()
    {
        GetComponent<Text>().text = player.GetMoney().ToString();
    }
}
