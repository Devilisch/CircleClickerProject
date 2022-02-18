using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ChangeButtons : MonoBehaviour
{
    [SerializeField] private Button plusButton;
    [SerializeField] private Button minusButton;
    [SerializeField] private Text labelText;
    [SerializeField] private Text labelShadowText;

    private string _textString = "";
    private float _value = 1;

    public float Value
    {
        set
        {
            if ( value != _value )
            {
                _value = value;
                labelText.text = _textString + _value;
                labelShadowText.text = _textString + _value;
            }
        }

        get => _value;
    }

    public string DefaultText
    {
        set
        {
            if ( value != _textString && !string.IsNullOrEmpty( value ) )
            {
                _textString = value;
                labelText.text = _textString + _value;
                labelShadowText.text = _textString + _value;
            }
        }

        get => _textString;
    }



    public void InitiateButtons( UnityAction minusButtonAction, UnityAction plusButtonAction )
    {
        minusButton.onClick.AddListener( minusButtonAction );
        plusButton.onClick.AddListener( plusButtonAction );
    }

    private void OnDisable()
    {
        minusButton.onClick.RemoveAllListeners();
        plusButton.onClick.RemoveAllListeners();
    }
}
