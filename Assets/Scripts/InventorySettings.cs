using UnityEngine;
using UnityEngine.UI;

public class InventorySettings : MonoBehaviour
{
    [SerializeField] private GameObject _player;

    [SerializeField] private GameObject[] _objectKey = new GameObject[3];

    [SerializeField] private Button _slot01;
    [SerializeField] private Button _slot02;

    [SerializeField] private Text _activKeyText;
    [SerializeField] private Text _throwKeyText;
    [SerializeField] private Text _PuaseText;

    [SerializeField] private static Sprite _emptySprite;

    private static int _activKeyNumber;
    private static Button _activSlot;


    void Start()
    {
        _emptySprite = _slot01.GetComponent<Image>().sprite;
        Cursor.visible = false;
    }


    void Update()
    {
        Pause();
        ThrowKey();
    }


   

    public static int GetActivKeyNumber()
    {
        return _activKeyNumber;
    }

    public static Button GetActivSlot()
    {
        return _activSlot;
    }

    public static void NullActivKeyNumber()
    {
        _activKeyNumber = 0;
    }



    public static bool CheckSlotIsEmpty(Button buttonSlot)
    {
        return buttonSlot.GetComponent<Image>().sprite == _emptySprite;
    }

    public static void PutInSlot(Button buttonSlot, Sprite itemSprite)
    {
        buttonSlot.GetComponent<Image>().sprite = itemSprite;
    }

    public static void FreeSloot(Button buttonSlot)
    {
        buttonSlot.GetComponent<Image>().sprite = _emptySprite;
    }





    private void Pause()
    {
        if (Input.GetKeyUp(KeyCode.Tab) && !Cursor.visible)
        {
            Time.timeScale = 0;
            Cursor.visible = true;
            _PuaseText.gameObject.SetActive(true);
            return;
        }

        if (Input.GetKeyUp(KeyCode.Tab) && Cursor.visible)
        {
            Time.timeScale = 1;
            Cursor.visible = false;
            _PuaseText.gameObject.SetActive(false);
            return;
        }
    }




    private void ThrowKey()
    {
        if (Cursor.visible && _activKeyNumber != 0)
        {
            _throwKeyText.gameObject.SetActive(true);

            for (int key = 0; key < _objectKey.Length; key++)
            {
                if (Input.GetKeyDown(KeyCode.F) && _objectKey[key].name.Contains(_activKeyNumber.ToString()))
                {
                    Instantiate(_objectKey[key], new Vector3(_player.transform.position.x, 1, _player.transform.position.z), Quaternion.identity);
                    _activKeyText.gameObject.SetActive(false);
                    ClearSlotAndActivKey();
                    break;
                }
            }
        }
        else
        {
            _throwKeyText.gameObject.SetActive(false);
        }
    }

    private void ClearSlotAndActivKey()
    {
        FreeSloot(_activSlot);
        _activKeyNumber = 0;
        _activSlot = null;
    }





    public void SlotOne()
    {
        KeyActivator(_slot01);
        _activSlot = _slot01;
    }

    public void SlotTwo()
    {
        KeyActivator(_slot02);
        _activSlot = _slot02;
    }

    private void KeyActivator(Button buttonSlot)
    {
        if (buttonSlot.GetComponent<Image>().sprite.name.Contains("key01"))
        {
            _activKeyNumber = 1;
            _activKeyText.text = "Key01 in your hand";
            _activKeyText.gameObject.SetActive(true);
        }
        else if (buttonSlot.GetComponent<Image>().sprite.name.Contains("key02"))
        {
            _activKeyNumber = 2;
            _activKeyText.text = "Key02 in your hand";
            _activKeyText.gameObject.SetActive(true);
        }
        else if (buttonSlot.GetComponent<Image>().sprite.name.Contains("key03"))
        {
            _activKeyNumber = 3;
            _activKeyText.text = "Key03 in your hand";
            _activKeyText.gameObject.SetActive(true);
        }
    }

}
