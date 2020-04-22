using UnityEngine;

public class AutoSave : MonoBehaviour
{

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.R))
        {
            CloutHandler.c_Amount = 0;
            PlayerPrefs.SetInt("Clout", CloutHandler.c_Amount);
            PlayerPrefs.Save();
        }
    }

    void OnDisable()
    {
        Debug.Log("Saved!");
        PlayerPrefs.SetInt("Clout", CloutHandler.c_Amount);
        PlayerPrefs.Save();
    }
}
