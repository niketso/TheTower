using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public bool active;

    private EnemyType myType;
    public EnemyType MyType { get => myType; protected set => myType = value; }

    public System.Action OnMyLevelChange;
    [SerializeField] private int myLevel;

    public int MyLevel 
    { 
        get => myLevel;
        set 
        {
            myLevel = value;

            if (OnMyLevelChange != null)
                OnMyLevelChange.Invoke();
        } 
    }

    protected GameObject player;
    protected SpriteRenderer sprite;
    protected Animator anim;
    protected bool pooled;
    [SerializeField] protected Transform rightLimit;
    [SerializeField] protected Transform leftLimit;
    [SerializeField] protected float speed;

    public Transform RightLimit 
    {
        get { return rightLimit; }
        set { rightLimit = value; }
    }

    public Transform LeftLimit 
    {
        get { return leftLimit; }
        set { leftLimit = value; }
    }

    protected virtual void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        player = GameManager.instance.Player;
        anim = GetComponent<Animator>();

        pooled = GetComponent<EnemyHealth>().pooled;

        ToggleActiveState(GameManager.instance.CurrentFloor);
        GameManager.instance.OnCurrentFloorChanged += ToggleActiveState;
    }

    protected void CheckBounds()
    {
        if (transform.position.x <= leftLimit.position.x) {
            transform.position = new Vector2(leftLimit.position.x , transform.position.y);
        }
        else if (transform.position.x >= rightLimit.position.x) {
            transform.position = new Vector2(rightLimit.position.x , transform.position.y);
        }
    }

    protected void GoLeft()
    {
        transform.Translate(new Vector3(-speed * Time.deltaTime , 0f , 0f));
        sprite.flipX = false;
    }

    protected void GoRight()
    {
        transform.Translate(new Vector3(speed * Time.deltaTime , 0f , 0f));
        sprite.flipX = true;
    }

    private void ToggleActiveState(int level)
    {
        active = level == MyLevel;
    }

    public virtual void PlaySound() { }
}
