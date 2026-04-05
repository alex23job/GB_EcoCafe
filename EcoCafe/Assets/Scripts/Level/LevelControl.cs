using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelControl : MonoBehaviour
{
    [SerializeField] private LevelUI _levelUI;
    [SerializeField] private CabbageBoxControl _cabbageBox;
    [SerializeField] private TreeControl[] _treeSet;

    private DayStat _dayStat;

    private float _timer = 1f;
    private int _countSecond = 0;

    private int _countMany = 0;
    private int _pollution = 0;

    private List<int> _saleIdObjects = new List<int>();

    // Start is called before the first frame update
    void Start()
    {
        _levelUI.ViewMany(_countMany);
        _levelUI.ViewPollution(_pollution);
        _dayStat.NumberDay = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (_timer > 0)
        {
            _timer -= Time.deltaTime;
        }
        else
        {
            _timer = 1f;
            _countSecond++;
            if (_countSecond > 180)
            {   //  прошло 3 минуты, а в игре 1 день
                _countSecond = 0;
                _cabbageBox.CreateBox();
                UpdateTreeSet();
                UpdatePollution();

                _levelUI.ViewDayStat(_dayStat);
                _dayStat.AddingDay();
            }
        }
    }

    private void UpdateTreeSet()
    {
        bool viewCrona = _pollution < 50f;
        foreach(TreeControl tree in _treeSet) tree.ViewCrona(viewCrona);
    }

    private void UpdatePollution()
    {
        if (_pollution > 3) _pollution -= 3;
        _levelUI.ViewPollution(_pollution);
        _dayStat.PollutMinus = 3;
    }

    public void ChangePollution(int value)
    {
        int newPollut = _pollution + value;
        if (_pollution <= 100)
        {
            _pollution = newPollut;
        }
        _dayStat.PollutPlus += value;
        _levelUI.ViewPollution(_pollution);
    }

    public void ViewHint(string hintText)
    {
        if (_levelUI != null)
        {
            _levelUI.ViewHint(hintText);
        }
    }

    public void HideHint()
    {
        _levelUI.HideHint();
    }

    public void ChangeMany(int count)
    {
        int newMany = _countMany + count;
        if (newMany < 0)
        {   //  всё плохо -> проигрыш ???
            
        }
        else
        {
            _countMany = newMany;
            _levelUI.ViewMany(_countMany);
            if (count > 0) _dayStat.ManyPlus += count;
            else _dayStat.ManyMinus += -count;
        } 
    }

    public bool CheckManyAndSale(int count, int saleID)
    {
        if (count < _countMany)
        {
            ChangeMany(-count);
            if (_saleIdObjects.Contains(saleID) == false) _saleIdObjects.Add(saleID);
            return true;
        }
        return false;
    }
}

public struct DayStat
{
    public int NumberDay;
    public int ManyPlus;
    public int ManyMinus;
    public int PollutPlus;
    public int PollutMinus;

    public void AddingDay()
    {
        NumberDay++;
        ManyPlus = 0;
        ManyMinus = 0;
        PollutPlus = 0;
        PollutMinus = 0;
    }
}
