using UnityEngine;

public class KeyManager : MonoBehaviour
{
    public static KeyManager Instance;
    public GameObject purpleKeyImage;
    public GameObject redKeyImage;
    public GameObject blueKeyImage;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    void Start()
    {
        Keys.hasPurpleKey = false;
        Keys.hasBlueKey = false;
        Keys.hasRedKey = false;

        purpleKeyImage.SetActive(false);
        redKeyImage.SetActive(false);
        blueKeyImage.SetActive(false);
    }
}