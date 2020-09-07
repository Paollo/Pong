using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserNamePanel : MonoBehaviour
{
    #region Members

    [SerializeField]
    private InputField userNameField;   
    
    #endregion
    
    #region Properties

    private InputField UserNameField {
        get => userNameField;
    }

    private System.Action<string> OnClickButton {
        get;
        set;
    }

    #endregion
    
    #region Methods

    public void Initialize(System.Action<string> onNextClickButton) 
    {
        OnClickButton = onNextClickButton;
    }

    public void ClicButton()
    {
        OnClickButton(UserNameField.text);
    }

    #endregion
    
    #region ClassesAndEnums

    #endregion 
}
