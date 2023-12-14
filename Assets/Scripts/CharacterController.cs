using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerStuff;
using Interaction;
using UnityEditor;

public class CharacterScript : MonoBehaviour
{
    [SerializeField]
    PlayerStats playerInfo;
    [SerializeField]
    float speed, multiplierSpeedRun;

    [SerializeField]
    int slotSelected;
    int defaultSlotSelect = 0;


    Animator animatorController;
    Rigidbody2D rb;
    float lastMoveX, lastMoveY;

    public delegate void ControlDelegate();
    [SerializeField]
    private ControlDelegate controlDelegate;
    [SerializeField]
    float movement;
    float moveX, moveY;

    bool isPulling, isPushing;
    ItemStats itemUsing;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        lastMoveX = 0; lastMoveY = 0;
        animatorController = GetComponent<Animator>();
        animatorController.SetFloat("active", 1f);
        animatorController.SetFloat("Gender", 0f);
        controlDelegate = null;
        if (playerInfo == null)
            playerInfo = new PlayerStats();
    }

    // Update is called once per frame
    void Update()
    {
        if (controlDelegate != null)
            controlDelegate();
    }


    public void Action()
    {
        // if im not running, and I press the push/pull button and im not already pushing
        if (movement < 1 && Input.GetAxis("Fire2") > 0)
        {
            // start an animation of pushing (all item actions do this)
            animatorController.SetBool("Push", true);
            rb.velocity = Vector2.zero;
            AllowController(false);
            StartCoroutine(TimeStopActionAnimation());
        }
    }

    IEnumerator TimeStopActionAnimation()
    {
        Collider2D colission = Physics2D.OverlapBox((Vector2)transform.position + (Mathf.Abs(lastMoveX) > Mathf.Abs(lastMoveY) ?
                (lastMoveX > 0 ? Vector2.right : Vector2.left) : (lastMoveY > 0 ? Vector2.up : Vector2.down)),
                Vector3.one / 2, 0f);
        if (colission == null)
        {
            Debug.Log("theres nothing to interact");
            if (itemUsing.type == TypeItem.Bucket)
            {
                UseBucket();
            }
            yield return new WaitForSeconds(1f);
        }
        else
        {
            Interactable t = colission.GetComponent<Interactable>();
            if (t != null)
            {
                TypeInteraction sender = TypeInteraction.None;
                switch (itemUsing.type)
                {
                    case TypeItem.Bucket:
                        if(itemUsing.quantity == 0)
                            sender = TypeInteraction.None;
                        else
                            sender = TypeInteraction.BucketFill;
                        break;
                    case TypeItem.Hoe:
                        sender = TypeInteraction.Hoe;
                        break;
                    case TypeItem.Seed:
                        sender = TypeInteraction.Seeds;
                        break;
                }
                if (t.DoInteraction(sender))
                {
                    if(sender == TypeInteraction.Seeds)
                        ManagerCharacterAndObjectives.instance.UseSeed(itemUsing, colission.GetComponent<PossibleFarm>());

                    yield return new WaitForSeconds(1.5f);
                }
                else
                    yield return new WaitForSeconds(1f);
            }
            else
                yield return new WaitForSeconds(1f);
        }

        animatorController.SetBool("Push", false);
        AllowController(true);
    }

    public void AllowController(bool allowMovement)
    {
        if (allowMovement)
        {
            // Temporary to test
            animatorController.SetFloat("active", 1f);
            animatorController.SetFloat("Gender", 0f);

            controlDelegate = Movement;
            controlDelegate += QuickInventoryChange;
            controlDelegate += Action;
            controlDelegate += OpenInventory;
        }
        else
        {
            rb.velocity = Vector2.zero;
            controlDelegate = null;
        }
    }

    private void Movement()
    {
        moveX = Input.GetAxis("Horizontal"); moveY = Input.GetAxis("Vertical");

        // Stay facing the same direction last input
        if (moveX > 0.15f || moveX < -0.15f)
        {
            lastMoveX = moveX;
            lastMoveY = 0;
        }
        else if (moveY > 0.15f || moveY < -0.15f)
        {
            lastMoveY = moveY;
            lastMoveX = 0;
        }


        if (movement > 0 && Input.GetAxis("Fire1") > 0)
        {
            // Debug.Log("Accelerating");
            movement = 2;
        }
        else if (movement >= 1 && Input.GetAxis("Fire1") <= 0.1f)
        {
            //Debug.Log("Descelerating");
            if (Mathf.Abs(moveX) > Mathf.Abs(moveY))
                movement = moveX;
            else
                movement = moveY;
        }
        else
        {
            // check highest direction movement
            if (Mathf.Abs(moveX) > Mathf.Abs(moveY))
                movement = moveX;
            else
                movement = moveY;
        }

        // Cause movement is used in controller and we need positive values, we change the value to absolute,
        // to never have negatives
        movement = Mathf.Abs(movement);
        
        // Set animator values
        animatorController.SetFloat("Movement", movement);
        animatorController.SetFloat("moveX", lastMoveX);
        animatorController.SetFloat("moveY", lastMoveY);

        // Rigidbody movement
        float speedX = 0;
        if (moveX > 0.1f || moveX < -0.1f)
            speedX = moveX > 0.1f ? speed : -speed;
        float speedY = 0;
        if (moveY > 0.1f || moveY < -0.1f)
            speedY = moveY > 0.1f ? speed : -speed;
        rb.velocity = movement > 1 ? new Vector2(speedX, speedY) * multiplierSpeedRun : new Vector2(speedX, speedY);

    }

    private void QuickInventoryChange()
    {
        if (Input.GetKeyDown("1") || Input.GetKeyDown(KeyCode.Keypad1))
        {
            ManagerCharacterAndObjectives.instance.SetSlotQuickInventory(0);
        }
        if (Input.GetKeyDown("2") || Input.GetKeyDown(KeyCode.Keypad2))
        {
            ManagerCharacterAndObjectives.instance.SetSlotQuickInventory(1);
        }
        if (Input.GetKeyDown("3") || Input.GetKeyDown(KeyCode.Keypad3))
        {
            ManagerCharacterAndObjectives.instance.SetSlotQuickInventory(2);
        }
        if (Input.GetKeyDown("4") || Input.GetKeyDown(KeyCode.Keypad4))
        {
            ManagerCharacterAndObjectives.instance.SetSlotQuickInventory(3);
        }
        if (Input.GetKeyDown("5") || Input.GetKeyDown(KeyCode.Keypad5))
        {
            ManagerCharacterAndObjectives.instance.SetSlotQuickInventory(4);
        }
    }

    private void OpenInventory()
    {
        if (Input.GetAxis("Fire3") > 0.1f)
            ManagerCharacterAndObjectives.instance.OpenInventory();
    }

    public void ReceiveItemUsing(ItemStats item)
    {
        itemUsing = item;
    }

    // Bucket used on empty space, add particle effects of water splashing on floor
    public void UseBucket()
    {
        ManagerCharacterAndObjectives.instance.UseBucket();
    }

    // For customization
    public void ChangeGender()
    {
        float gender = animatorController.GetFloat("Gender");
        // If its male, change to female
        if (gender == 0)
            animatorController.SetFloat("Gender", 1f);
        else
            animatorController.SetFloat("Gender", 0f);

        animatorController.SetFloat("Movement", 2f);
        StartCoroutine(StopRunning());
    }

    IEnumerator StopRunning()
    {
        yield return new WaitForSeconds(2.5f);
        animatorController.SetFloat("Movement", 0f);
    }

    public void ChangeSkinColor(Color skinColor)
    {
        GetComponent<SpriteRenderer>().color = skinColor;
        animatorController.SetTrigger("Jump");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position + (Vector3)(Mathf.Abs(lastMoveX) > Mathf.Abs(lastMoveY) ?
                (lastMoveX > 0 ? Vector2.right : Vector2.left) : (lastMoveY > 0 ? Vector2.up : Vector2.down)),
                Vector3.one / 2);
    }
}
