using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class InteractionWithObjects : MonoBehaviour
{
    [SerializeField] private Text _preesEText;
    [SerializeField] private Text _activKeyText;

    [SerializeField] private Button _slot01;
    [SerializeField] private Button _slot02;

    [SerializeField] private Sprite _keys01;
    [SerializeField] private Sprite _keys02;
    [SerializeField] private Sprite _keys03;

    private GameObject _currentKey;
    private GameObject _currentDoor;


    void Update()
    {
        PutKey();
        OpenDoor();
    }


    private void PutKey()
    {
        if (Input.GetKeyDown(KeyCode.E) && !Cursor.visible && _currentKey != null)
        {
            if (InventorySettings.CheckSlotIsEmpty(_slot01))
            {
                DefineKey(_currentKey, _slot01);
                _preesEText.gameObject.SetActive(false);

                Destroy(_currentKey.gameObject);
            }
            else if (InventorySettings.CheckSlotIsEmpty(_slot02))
            {
                DefineKey(_currentKey, _slot02);
                _preesEText.gameObject.SetActive(false);

                Destroy(_currentKey.gameObject);
            }
            else
            {
                Debug.Log("Inventory full");
            }
        }
    }

    private void DefineKey(GameObject key, Button buttonSlot)
    {
        if (key.name.Contains("01"))
        {
            InventorySettings.PutInSlot(buttonSlot, _keys01);
        }
        else if (key.name.Contains("02"))
        {
            InventorySettings.PutInSlot(buttonSlot, _keys02);
        }
        else if (key.name.Contains("03"))
        {
            InventorySettings.PutInSlot(buttonSlot, _keys03);
        }
    }




    private void OpenDoor()
    {
        if (_currentDoor != null && Input.GetKeyDown(KeyCode.E) && !Cursor.visible && InventorySettings.GetActivKeyNumber() != 0)
        {
            string activKeyNumber = InventorySettings.GetActivKeyNumber().ToString();

            if (_currentDoor.name.Contains(activKeyNumber))
            {
                RightDoor();
            }
            else
            {
                WrongDoor();
            }
        }
    }

    private void RightDoor()
    {
        _currentDoor.GetComponent<Renderer>().material.color = Color.green;

        InventorySettings.NullActivKeyNumber();
        Button currentActivSlot = InventorySettings.GetActivSlot();
        InventorySettings.FreeSloot(currentActivSlot);

        _activKeyText.gameObject.SetActive(false);
        WinGame.AddCountOpenDoors();
    }

    private void WrongDoor()
    {
        StartCoroutine(PaitRedDoor());
        InventorySettings.NullActivKeyNumber();
        _activKeyText.gameObject.SetActive(false);
    }

    IEnumerator PaitRedDoor()
    {
        Color oldColor = _currentDoor.GetComponent<Renderer>().material.color;
        GameObject _wrongDoor = _currentDoor;
        _wrongDoor.GetComponent<Renderer>().material.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        _wrongDoor.GetComponent<Renderer>().material.color = oldColor;
    }




    private void OnTriggerStay(Collider other)
    {
        Vector3 direction = other.transform.position - transform.position;
        if (Vector3.Dot(transform.forward, direction) > 0.5f)
        {
            if (other.name.Contains("Key"))
            {
                _preesEText.gameObject.SetActive(true);
                _currentKey = other.gameObject;
            }
            else if (other.name.Contains("Door"))
            {
                _preesEText.gameObject.SetActive(true);
                _currentDoor = other.gameObject;
            }
        }
        else
        {
            _preesEText.gameObject.SetActive(false);
            _currentKey = null;
            _currentDoor = null;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (_preesEText.gameObject.activeSelf)
        {
            _preesEText.gameObject.SetActive(false);
            _currentKey = null;
            _currentDoor = null;
        }
    }
}
