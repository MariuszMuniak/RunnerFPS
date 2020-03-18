using FPS.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagazineDisplay : MonoBehaviour
{
    Fighter fighter;

    private void Awake()
    {
        fighter = GameObject.FindWithTag("Player").GetComponent<Fighter>();
    }

    private void Update()
    {
        GetComponent<Text>().text = $"{ fighter.GetCurrenAmmo() } / { fighter.GetCurrentWeapon().GetMagazineSize() }";
    }
}
