using TMPro;
using UnityEngine;

public class Bar : MonoBehaviour
{
    [SerializeField] private TMP_Text barText = null;
    [SerializeField] private Transform barTransform = null;

    public void SetBar(int _currwentValue, int _maxValue)
    {
        barText.text = $"{_currwentValue}/{_maxValue}";
        Vector3 _currentScale = barTransform.localScale;
        _currentScale.x = (float)_currwentValue / _maxValue;
        barTransform.localScale = _currentScale;
    }
}
