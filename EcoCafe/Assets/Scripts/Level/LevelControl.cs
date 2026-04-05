using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelControl : MonoBehaviour
{
    [SerializeField] private LevelUI _levelUI;
    [SerializeField] private CabbageBoxControl _cabbageBox;
    [SerializeField] private TreeControl[] _treeSet;
 

    private float _timer = 1f;
    private int _countSecond = 0;

    private int _countMany = 0;
    private float _pollution = 0;

    private List<int> _saleIdObjects = new List<int>();

    // Start is called before the first frame update
    void Start()
    {
        _levelUI.ViewMany(_countMany);
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
            }
        }
    }

    private void UpdateTreeSet()
    {
        bool viewCrona = _pollution < 50f;
        foreach(TreeControl tree in _treeSet) tree.ViewCrona(viewCrona);
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
