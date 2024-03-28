using System.Collections;
using System.Collections.Generic;
using System.Linq;
using StarterAssets;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public List<Item> Items;

    [SerializeField] GameObject _inventory;
    [SerializeField] GameObject _pauseMenu;
    public List<GameObject> SlotList;
    public int AvailableSlots = 6;
    public int Money;
    [SerializeField] TMP_Text _moneyText;

    StarterAssetsInputs _starterAssetsInputs;

    GameObject _slot;

    private void Awake()
    {
        _starterAssetsInputs = GetComponent<StarterAssetsInputs>();
    }

    public void OnInventory(InputValue value)
    {
        _inventory.SetActive(true);
        _pauseMenu.SetActive(false);
        _starterAssetsInputs.IsLocked = true;
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        _moneyText.text = Money.ToString();
        CheckDuplicates();
    }

    public void OpenInventory()
    {
        _inventory.SetActive(true);
        _pauseMenu.SetActive(false);
        CheckDuplicates();
    }


    public void CloseInventory()
    {
        _inventory.SetActive(false);
        _starterAssetsInputs.IsLocked = false;
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void CheckDuplicates()
    {
        var tagGroups = Items.GroupBy(x => x.tag).ToDictionary(x => x.Key, x => x.ToList());
        var tempItems = new List<Item>(Items);

        foreach (var tagGroup in tagGroups)
        {
            var count = tagGroup.Value.Count;

            if (count > 1)
            {
                var firstItem = tagGroup.Value.First();
                var groupedItems = tagGroup.Value.GroupBy(x => x).Where(x => x.Count() > 1).SelectMany(x => x).ToList();

                foreach (var groupedItem in groupedItems)
                {
                    tempItems.Remove(groupedItem);
                }

                tempItems.Add(firstItem);
            }
        }

        UpdateInventory(tagGroups);
    }

    void UpdateInventory(Dictionary<string, List<Item>> tagGroups)
    {
        for (int i = 0; i < SlotList.Count; i++)
        {
            SlotList[i].SetActive(false);
        }

        int index = 0;
        foreach (var tagGroup in tagGroups)
        {
            if (index < SlotList.Count)
            {
                SlotList[index].SetActive(true);
                SlotList[index].GetComponent<Item>().ItemData = tagGroup.Value[0].GetComponent<Item>().ItemData;
                index++;
            }
        }

        index = 0;
        foreach (var tagGroup in tagGroups)
        {
            if (index < SlotList.Count)
            {
                _slot = SlotList[index];
                _slot.GetComponent<Image>().sprite = tagGroup.Value[0].GetComponent<Item>().ItemData.Icon;
                _slot.GetComponent<Item>().Tag = tagGroup.Key;

                _slot.transform.GetChild(1).GetComponent<TMP_Text>().text = tagGroup.Value.Count.ToString();

                index++;
            }
        }
    }


    public void AddItem(Item item, int amount)
    {
        for (int i = 1; i < amount + 1; i++)
        {
            Items.Add(item);
        }
    }

    public void RemoveItem(Item item, int amount)
    {
        int count;
        int.TryParse(item.transform.GetChild(1).GetComponent<TMP_Text>().text, out count);
        if (count <= amount)
        {
            item.transform.gameObject.SetActive(false);
        }
        else
        {
            item.transform.GetChild(1).GetComponent<TMP_Text>().text = (count - amount).ToString();
        }
        for (int i = 0; i < amount; i++)
        {
            Items.Remove(item);
        }
    }
}
