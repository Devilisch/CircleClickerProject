using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Constants;

public class LeftPanel : MonoBehaviour
{
    public Animation panelAnimation;
    public Animation modePanelAnimation;
    public Toggle arrowToggle;
    public Toggle linesToggle;
    public Button button;
    public Button startButton;
    public Button recordsButton;
    public Button shopButton;
    public Button exitButton;
    public Button warmUpModeButton;
    public Button survivalModeButton;
    public Button speedrunModeButton;
    public Button countdownModeButton;

    private bool isPanelActive = false;
    private bool isModePanelActive = false;



    private void OnEnable()
    {
        button.onClick.AddListener( SwitchPanel );
        startButton.onClick.AddListener( SwitchModePanel );
    }

    private void OnDisable()
    {
        button.onClick.RemoveAllListeners();
        startButton.onClick.RemoveAllListeners();
    }

    private void SwitchPanel()
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

    private void SwitchModePanel()
    {
        if ( isModePanelActive )
            modePanelAnimation.Play( ANIMATION.CLOSE_MODE_PANEL );
        else
            modePanelAnimation.Play( ANIMATION.OPEN_MODE_PANEL );

        isModePanelActive = !isModePanelActive;
    }
}
