using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    #region Members

    [SerializeField]
    private float moveSpeed = 10;


    #endregion
    
    #region Properties

    private float MoveSpeed{
        get => moveSpeed;
        set => moveSpeed = value;
    }

    private Vector2 Direction {
        get;set;
    }

    private float Radius {
        get;set;
    }

    private float SpeedToAdd {
        get;set;
    }

    #endregion
    
    #region Methods 

    private void Start()
    {
        Direction = Vector2.one.normalized;
        Radius = GetComponent<CircleCollider2D>().radius;
        SpeedToAdd = MoveSpeed/100f;
    }

    private void Update() 
    {
        Move();
        TryBounceFromBottomEdge();
        TryBounceFromTopEdge();
        TryBounceFromRightEdge(); 
        
        if(GameManager.Instance.IsMainMenu == true)
        {
            TryBounceFromLeftEdge();
        }
        else
        {
            TryKillPlayer();     
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetComponent<Racket>()!=null)
        {
            float touchPoint = other.transform.InverseTransformPoint(transform.position).y;
            Vector2 additionalVector = new Vector2(0,touchPoint*10);
            Direction = (new Vector2(-Direction.x,Direction.y) + additionalVector).normalized;
            MoveSpeed += SpeedToAdd; 
            if(GameAction.Instance!=null)
            {
                Debug.Log("Odbicie");
                GameAction.Instance.NotifyOnBounceBall();
            }            
        }
    }

    private void TryKillPlayer()
    {
        if(transform.position.x < GameManager.Instance.BottomLeft.x +Radius && Direction.x <0)
        {
            Destroy(gameObject);
            if(GameAction.Instance!=null)
            {
                GameAction.Instance.NotifyOnPlayerLose();
            }            
        }
    }

    private void TryBounceFromBottomEdge()
    {
        if(transform.position.y < GameManager.Instance.BottomLeft.y + Radius + GameManager.Instance.BorderOffset && Direction.y <0)
        {
            Direction = new Vector2(Direction.x,-Direction.y);
        }
    }

    private void TryBounceFromTopEdge()
    {
        if(transform.position.y > GameManager.Instance.TopRight.y -Radius - GameManager.Instance.BorderOffset && Direction.y >0)
        {
            Direction = new Vector2(Direction.x,-Direction.y);
        }
    }

    private void TryBounceFromRightEdge()
    {
        if(transform.position.x > GameManager.Instance.TopRight.x - Radius - GameManager.Instance.BorderOffset && Direction.x >0)
        {
            Direction = new Vector2(-Direction.x,Direction.y);
        }
    }

    private void TryBounceFromLeftEdge()
    {
        if(transform.position.x < GameManager.Instance.BottomLeft.x + Radius + GameManager.Instance.BorderOffset && Direction.x <0)
        {
            Direction = new Vector2(-Direction.x,Direction.y);
        }
    }

    private void Move()
    {
        transform.Translate(Direction * Time.deltaTime * MoveSpeed);
    }

    #endregion
    
    #region ClassesAndEnums

    #endregion
}
