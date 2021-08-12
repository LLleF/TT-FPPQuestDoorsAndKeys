using UnityEngine;
using UnityEngine.UI;

public class WinGame : MonoBehaviour
{
    private const int _maxCountDoors = 3;
    private static int _countOpenDoors;

    [SerializeField] private Text _winGameText;

    void Update()
    {
        RequirementsForVictory();
    }

    public static void AddCountOpenDoors()
    {
        _countOpenDoors++;
    }



    private void RequirementsForVictory()
    {
        if (_countOpenDoors == _maxCountDoors)
        {
            _winGameText.gameObject.SetActive(true);
            Time.timeScale = 0;
            Cursor.visible = true;
        }
    }

 
}
