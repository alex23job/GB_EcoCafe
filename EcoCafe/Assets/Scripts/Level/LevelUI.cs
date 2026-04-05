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
    [SerializeField] private Text _pollutText;

    [SerializeField] private GameObject _statPanel;
    [SerializeField] private Text _statTitle;
    [SerializeField] private Text _statManyPlus;
    [SerializeField] private Text _statManyMinus;
    [SerializeField] private Text _statPollutPlus;
    [SerializeField] private Text _statPollutMinus;

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

    public void ViewPollution(int value)
    {
        _pollutText.text = $"{value} %";
    }

    public void ViewDayStat(DayStat stat)
    {
        _statPanel.SetActive(true);
        _statTitle.text = $"{stat.NumberDay} день завершён !";
        _statManyPlus.text = $"Получено монет : {stat.ManyPlus}";
        _statManyMinus.text = $"Истрачено монет : {stat.ManyMinus}";
        _statPollutPlus.text = $"Получено загрязнения : {stat.PollutPlus}%";
        _statPollutMinus.text = $"Очищено загрязнения : {stat.PollutMinus}%";
    }
}
