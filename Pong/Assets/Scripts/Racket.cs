using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Racket : MonoBehaviour
{
    #region Members

    [SerializeField]
    private float startPositionOffset;
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float border;

    #endregion
    
    #region Properties

    private float StartPositionOffset {
        get => startPositionOffset;
    }

    private float MoveSpeed {
        get => moveSpeed;
    }

    private float Border {
        get => border;
    }

    #endregion
    
    #region Methods      

    public void Initialize()
    {
        Vector2 startPosition = Vector2.zero;

        startPosition = new Vector2(GameManager.Instance.BottomLeft.x, 0);
        startPosition -= Vector2.right * StartPositionOffset;

        transform.position = startPosition; 
    }

    private void Start()
    {

    }

    private void Update() 
    {
        float horizontalAxis = Input.GetAxis(Constants.HORIZONTAL_AXIS);
        float verticalAxis = Input.GetAxis(Constants.VERTICAL_AXIS) * -1; 
        float move = (horizontalAxis + verticalAxis) * Time.deltaTime * MoveSpeed;

        if(transform.position.y < GameManager.Instance.BottomLeft.y + Border && move > 0)
        {
            move = 0f;
        }

        if(transform.position.y > GameManager.Instance.TopRight.y - Border && move < 0)
        {
            move = 0f;
        }

        transform.Translate(move * Vector2.down);
    }

    #endregion
    
    #region ClassesAndEnums

    #endregion 
}
