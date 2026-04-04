using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelUI : MonoBehaviour
{
    [SerializeField] private GameObject _hintPanel;
    [SerializeField] private Text _hintText;
    [SerializeField] private Text _manyText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ViewHint(string hintText)
    {
        _hintPanel.SetActive(true);
        _hintText.text = hintText;
        Invoke("HideHint", 5f);
    }

    public void HideHint()
    {
        _hintPanel.SetActive(false);
    }

    public void ViewMany(int value)
    {
        _manyText.text = value.ToString();
    }
}
