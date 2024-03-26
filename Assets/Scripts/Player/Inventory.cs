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

    void CheckDuplicates()
    {
        var tagGroups = Items.GroupBy(x => x.tag).ToDictionary(x => x.Key, x => x.ToList());

        foreach (var tagGroup in tagGroups)
        {
            var count = tagGroup.Value.Count;

            if (count > 1)
            {
                var firstItem = tagGroup.Value.First();
                var groupedItems = tagGroup.Value.GroupBy(x => x).Where(x => x.Count() > 1).SelectMany(x => x).ToList();

                foreach (var groupedItem in groupedItems)
                {
                    Items.Remove(groupedItem);
                }

                Items.Add(firstItem);
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
                var slot = SlotList[index];
                slot.GetComponent<Image>().sprite = tagGroup.Value[0].GetComponent<Item>().ItemData.Icon;

                slot.transform.GetChild(1).GetComponent<TMP_Text>().text = tagGroup.Value.Count.ToString();

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
}
