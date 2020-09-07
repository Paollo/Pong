using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuGUI : MonoBehaviour
{
     #region Members



    #endregion
    
    #region Properties

    

    #endregion
    
    #region Methods 

    public void StartNewGame()
    {
        if(GameManager.Instance!=null)
        {
            GameManager.Instance.StartNewGame();
        }
    }

    public void QuitGame()
    {
        if(GameManager.Instance!=null)
        {
            GameManager.Instance.QuitGame();
        }
    }

    #endregion
    
    #region ClassesAndEnums

    #endregion
}
