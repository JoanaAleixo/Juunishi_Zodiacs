using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Dialogue;

public class MenuManager : MonoBehaviour
{
    [Header("MainMenu")]
    [SerializeField] GameObject[] _mainMenuElements;

    [Header("Inventory")]
   
    [SerializeField] GameObject _inventoryStorage;
    [SerializeField] GameObject _itemToStorage;
    [SerializeField] Inventory _inventoryInfo;
    [SerializeField] List<GameObject> _itensInStorage = new List<GameObject>();

    [SerializeField] GameObject _itemDescriptionBox;


    public static MenuManager instance;

   
    public List<GameObject> ItensInStorage { get => _itensInStorage; set => _itensInStorage = value; }
    public GameObject ItemDescriptionBox { get => _itemDescriptionBox; set => _itemDescriptionBox = value; }
    public Inventory InventoryInfo { get => _inventoryInfo; set => _inventoryInfo = value; }

    private void Awake()
    {
        //singletone
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;

        }

        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))        
        {
            InventorySystem();
        }
    }
    public void Option(int switchNumb)
    {
       
        foreach (var item in _mainMenuElements)
        {
            item.SetActive(false);
        }
        _mainMenuElements[switchNumb].SetActive(true);
    }

  public void InventorySystem()
    {
        foreach (var item in InventoryInfo.InventoryDic.Keys)
        {
            GameObject Item = Instantiate(_itemToStorage, _inventoryStorage.transform.position, _itemToStorage.transform.rotation) as GameObject;
            Item.transform.SetParent(_inventoryStorage.transform, false);
            Item.transform.position = Camera.main.WorldToScreenPoint(_itemToStorage.transform.position);

            ItensInStorage.Add(Item);

           Image itemIcon = Item.transform.GetChild(0).GetChild(0).GetComponent<Image>();
            itemIcon.sprite = item.IconForBag;

            TextMeshProUGUI itemToName = Item.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>();        
            itemToName.text = item.ItemName;

            TextMeshProUGUI itemAmount = Item.transform.GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>();
            InventoryInfo.InventoryDic.TryGetValue(item, out int itemValue);
            itemAmount.text = itemValue.ToString();

            OpenItemDescription thisItem = Item.transform.GetChild(1).GetComponent<OpenItemDescription>();
            thisItem.ThisItemOnButton = item;

        }
    }

    public void CloseInventory()
    {
        foreach (var item in _itensInStorage)
        {
            Destroy(item);
            
        }
        _itensInStorage.Clear();
    }

  
}

