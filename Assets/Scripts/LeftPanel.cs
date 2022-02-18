using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Constants;

public class LeftPanel : MonoBehaviour
{
    [SerializeField] private Animation panelAnimation;
    [SerializeField] private Animation modePanelAnimation;
    [SerializeField] private Toggle arrowToggle;
    [SerializeField] private Toggle linesToggle;
    [SerializeField] private Button button;
    [SerializeField] private Button startButton;
    [SerializeField] private Button recordsButton;
    [SerializeField] private Button shopButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Button warmUpModeButton;
    [SerializeField] private Button survivalModeButton;
    [SerializeField] private Button speedrunModeButton;
    [SerializeField] private Button countdownModeButton;

    private bool isPanelActive = false;
    private bool isModePanelActive = false;



    private void OnEnable()
    {
        button.onClick.AddListener( OnButtonClicked );
        startButton.onClick.AddListener( OnStartButtonClicked );

        // exitButton.onCLick.AddListener( OnExitButtonClicked  );
    }

    private void OnDisable()
    {
        button.onClick.RemoveAllListeners();
        startButton.onClick.RemoveAllListeners();

        // exitButton.onCLick.RemoveAllListeners();
    }

    private void OnButtonClicked()
    {
        if ( isPanelActive )
        {
            linesToggle.isOn = true;
            panelAnimation.Play( ANIMATION.CLOSE_LEFT_PANEL );
        }
        else
        {
            arrowToggle.isOn = true;
            panelAnimation.Play( ANIMATION.OPEN_LEFT_PANEL );
        }

        isPanelActive = !isPanelActive;
    }

    private void OnStartButtonClicked()
    {
        if ( isModePanelActive )
            modePanelAnimation.Play( ANIMATION.CLOSE_MODE_PANEL );
        else
            modePanelAnimation.Play( ANIMATION.OPEN_MODE_PANEL );

        isModePanelActive = !isModePanelActive;
    }
}
