using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private Vector3[] playerPos;

    private int positionIndex = 1;
    private GameManager manager;

    void Start()
    {
        manager = GameManager.ins;
    }

    void Update()
    {
        if (Input.GetButtonDown("Horizontal"))
        {
            Debug.Log("Boom!");
            float _xMove = Input.GetAxisRaw("Horizontal");
            if (_xMove > 0)
            {
                MoveRight();
            }
            else if (_xMove < 0)
            {
                MoveLeft();
            }
        }
    }

    void OnTriggerEnter(Collider _col)
    {
        if (_col.tag == "Danger")
        {
            manager.PlayerDied();
            Destroy(gameObject);
        }

        if (_col.tag == "Start Line")
        {
            manager.IncreaseScore();
            manager.IncreaseRotationSpeed();
        }

        if (_col.tag == "Spawn Line")
        {
            manager.ShowHiddenBlocks();
            manager.ShowFinishLine();
            Destroy(_col.gameObject);
        }
    }

    public void MoveRight()
    {
        if (transform.position == playerPos[playerPos.Length - 1])
        {
            return;
        }
        else
        {
            positionIndex++;
            transform.position = playerPos[positionIndex];
            Debug.Log("[PLAYER] Moved right.");
        }
    }

    public void MoveLeft()
    {
        if (transform.position == playerPos[0])
        {
            return;
        }
        else
        {
            positionIndex--;
            transform.position = playerPos[positionIndex];
            Debug.Log("[PLAYER] Moved left.");
        }
    }

}
