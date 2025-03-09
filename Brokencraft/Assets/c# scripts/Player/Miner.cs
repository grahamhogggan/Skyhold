using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Miner : MonoBehaviour
{
    private static List<GameObject> blocks;

    public List<GameObject> instanceBlocks;
    private string equippedItem;
    private float equippedValue = 1;
    private double itemCounter;
    [SerializeField] private GameObject equippedObjectViewer;
    private GameObject equippedObjectMeshes;

    [SerializeField] private LayerMask buildLayers;


    private PlayerInventory inventory;
    [SerializeField] private float scrollSpeed = 2;

    private float timeSinceLastAttack = 0;
    private float cooldown = 0;
    private static Miner instance;

    public LayerMask handLayer;
    public Rotator crosshairRotator;
    public TMP_Text interactableText;
    private string lastItemEquipped;

    // Start is called before the first frame update
    public void Start()
    {
        equippedItem = "";
        blocks = instanceBlocks;
        inventory = transform.root.gameObject.GetComponentInChildren<PlayerInventory>();
        instance = this;
    }
    void awake()
    {
        blocks = instanceBlocks;
    }
    public static GameObject GetBlock(string name)
    {
        //if (blocks == null) return null;
        foreach (GameObject block in blocks)
        {
            if (block.name == name)
            {
                return block;
            }
        }
        return null;

    }
    // Update is called once per frame
    void Update()
    {
        timeSinceLastAttack += Time.deltaTime;
        if (timeSinceLastAttack > cooldown)
        {
            crosshairRotator.rot = new Vector3(0, 0, 0);
            crosshairRotator.gameObject.transform.rotation = Quaternion.identity;
        }
        if (Overlord.paused) return;
        if (inventory.GetItemCount() == 0)
        {
            equippedItem = "null";
        }
        else
        {
            equippedValue += Input.GetAxisRaw("Mouse ScrollWheel") * scrollSpeed;
            if (equippedValue >= inventory.GetItemCount())
            {
                equippedValue = 0;
            }
            if (equippedValue < 0)
            {
                equippedValue = inventory.GetItemCount() - 1;
            }

            if (Input.GetKeyDown("1"))
            {
                equippedValue = 0;
            }
            if (Input.GetKeyDown("2"))
            {
                equippedValue = 1;
            }
            if (Input.GetKeyDown("3"))
            {
                equippedValue = 2;
            }
            if (Input.GetKeyDown("4"))
            {
                equippedValue = 3;
            }
            if (Input.GetKeyDown("5"))
            {
                equippedValue = 4;
            }
            if (Input.GetKeyDown("6"))
            {
                equippedValue = 5;
            }
            if (Input.GetKeyDown("7"))
            {
                equippedValue = 6;
            }
            if (Input.GetKeyDown("8"))
            {
                equippedValue = 7;
            }
            if (Input.GetKeyDown("9"))
            {
                equippedValue = 8;
            }
            equippedItem = inventory.GetItem((int)equippedValue);

        }

        //Debug.Log("inventorySlot:" + equippedValue);

        //check interactions
        RaycastHit interactionHit;
        if (Physics.Raycast(transform.position, transform.forward, out interactionHit, 10, buildLayers, QueryTriggerInteraction.Collide))
        {
            Block hit = interactionHit.transform.gameObject.GetComponentInParent<Block>();
            if (hit != null)
            {
                Interactable thing = hit.transform.gameObject.GetComponentInChildren<Interactable>();
                if (thing != null)
                {
                    interactableText.text = thing.interactMessage;
                }
                else
                {
                    interactableText.text = "";
                }
            }
        }
        else
        {
            interactableText.text = "";
        }

        SwitchEquippedItem();
        if (Input.GetMouseButton(0))
        {
            Vector3 weaponStats;
            if (equippedItem == "null")
            {
                weaponStats = new Vector3(1, 0, 0);
            }
            else
            {
                GameObject held = GetBlock(equippedItem);
                weaponStats = held.GetComponent<Weapon>().GetStats();
            }
            if (timeSinceLastAttack > cooldown)
            {
                timeSinceLastAttack = 0;
                cooldown = weaponStats.z;

                equippedObjectViewer.GetComponent<HeldObject>().Attack();

                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.forward, out hit, 10, buildLayers, QueryTriggerInteraction.Collide))
                {
                    Block hitBlock = hit.transform.gameObject.GetComponentInParent<Block>();
                    if (hitBlock != null)
                    {
                        Dense density = hitBlock.GetComponentInChildren<Dense>();
                        if (density != null) cooldown *= density.multiplier;

                        crosshairRotator.transform.rotation = Quaternion.Euler(0, 0, 0);
                        crosshairRotator.gameObject.transform.rotation = Quaternion.Euler(0, 90, 0);
                        crosshairRotator.rot = new Vector3(0, 90 / cooldown, 0);

                        hitBlock.Mine();
                        string alt = hitBlock.GetComponentInChildren<BlockDetails>().alternate;
                        if (alt.Length > 0)
                        {
                            if (alt != "none")
                            {
                                inventory.AddItem(alt);
                            }
                        }
                        else
                        {
                            inventory.AddItem(hitBlock.typeName);

                        }

                    }
                    else
                    {
                        Enemy hitEnemy = hit.transform.gameObject.GetComponent<Enemy>();
                        if (hitEnemy != null)
                        {
                            Debug.Log("take that!");
                            hitEnemy.PlayerDamage(weaponStats.x);
                        }
                    }
                }
            }

        }
        if (Input.GetMouseButtonDown(1) && equippedItem != "null")
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, 10, buildLayers, QueryTriggerInteraction.Collide))
            {
                Block hitBlock = hit.transform.gameObject.GetComponentInParent<Block>();
                if (hitBlock != null)
                {
                    Vector3 normal = hit.normal;
                    Block build = hitBlock.GetAdjacent(normal);
                    /*
                    if(build != null)
                    {
                        Debug.Log("successful adjacent");
                    }
                    else
                    {
                        Debug.Log("no adjacent");
                    }
                    */
                    if (build != null && build.IsCellClear())
                    {
                        inventory.RemoveItem(equippedItem);
                        Quaternion lookRot = Quaternion.LookRotation(transform.position - build.transform.position);
                        double rot = lookRot.eulerAngles.y;
                        int rotation = ((int)rot + 45) / 90;
                        rotation *= 90;
                        build.Place(equippedItem, rotation);
                    }


                }
            }
        }
        if (Input.GetKeyDown("e"))
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, 10, buildLayers, QueryTriggerInteraction.Collide))
            {
                Interactable action = hit.transform.gameObject.GetComponentInChildren<Interactable>();
                if (action != null)
                {
                    action.Action.Invoke();
                }
            }
        }
    }
    void SwitchEquippedItem()
    {
        if (equippedItem == "null")
        {
            equippedObjectViewer.gameObject.SetActive(false);
            return;
        }
        if(equippedItem==lastItemEquipped) return;
        lastItemEquipped = equippedItem;
        equippedObjectViewer.gameObject.SetActive(true);

        GameObject newBlock = GetBlock(equippedItem);
        GameObject meshes = newBlock.GetComponentInChildren<MeshUnifier>().meshRoot;

        Destroy(equippedObjectMeshes);
        equippedObjectMeshes = Instantiate(meshes, equippedObjectViewer.transform.position, equippedObjectViewer.transform.rotation);
        equippedObjectMeshes.transform.SetParent(equippedObjectViewer.transform);
        equippedObjectMeshes.transform.localScale /= 4;
        foreach (Transform potato in equippedObjectMeshes.GetComponentsInChildren<Transform>())
        {
            potato.gameObject.layer = LayerMask.NameToLayer("Hand");
        }
    }
    public int GetEquippedIndex()
    {
        return (int)equippedValue;
    }
    public static string GetEquippedItemName()
    {
        return instance.equippedItem;
    }

}
