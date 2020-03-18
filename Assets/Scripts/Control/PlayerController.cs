using FPS.Combat;
using FPS.Items;
using FPS.Movement;
using FPS.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace FPS.Control
{
    public class PlayerController : MonoBehaviour
    {
        InventoryItems inventoryItems;
        Fighter fighter;
        Mover mover;
        Interaction interaction;
        SceneController sceneController;

        void Awake()
        {
            inventoryItems = GetComponent<InventoryItems>();
            fighter = GetComponent<Fighter>();
            mover = GetComponent<Mover>();
            interaction = GetComponent<Interaction>();
            sceneController = FindObjectOfType<SceneController>();
        }

        void Start()
        {
            inventoryItems.SwitchToRifle();
        }

        void Update()
        {
            if (Input.GetKey(KeyCode.LeftControl))
            {
                mover.Crouch();
            }
            else
            {
                mover.Stand();
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (interaction.CanInteract()) { interaction.Interact(); }
            }

            if (Input.GetKeyDown(KeyCode.F)) { fighter.KnifeAttack(); return; }

            if (Input.GetKeyDown(KeyCode.R)) { fighter.Reload(); return; }

            if (Input.GetKeyDown(KeyCode.Alpha1)) { inventoryItems.SwitchToRifle(); }

            if (Input.GetKeyDown(KeyCode.Alpha2)) { inventoryItems.SwitchToHandgun(); }

            if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))
            {
                if (fighter.ReadyToFire()) { fighter.Fire(transform); }
            }

            if (Input.GetMouseButton(1))
            {
                fighter.OnAiming();
            }
            else
            {
                fighter.OffAiming();
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                sceneController.PauseGame();
            }
        }
    }
}
