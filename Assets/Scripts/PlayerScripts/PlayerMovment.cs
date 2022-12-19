using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovment : MonoBehaviour
{
    [SerializeField] private float force;
    [SerializeField] private float borderPadding;
    [Range(0, 90)]
    [SerializeField] private float maximumDeflectionAngle;

    private Rigidbody playerRigidbody;

    private bool isMotion;
    bool isGame = true;
    private void Start()
    {
        PlayerHealth.singleton.DeadAction += IsGame;
        playerRigidbody = GetComponent<Rigidbody>();
        InputController.singleton.playerMoveAction += SetMotion;
        InputController.singleton.ñlickAction += IsMotion;
    }
    private void SetMotion(Vector2 Direction)
    {
        playerRigidbody.velocity = Direction * force;
    }

    private void Update()
    {
        if (isGame)
        {
            transform.rotation = Quaternion.Euler(0, 0, maximumDeflectionAngle * -playerRigidbody.velocity.x / force);

            if (transform.position.y != 0 && isMotion)
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, 0), force * Time.deltaTime);
            }

            if (Mathf.Abs(transform.position.x) > borderPadding)
            {
                transform.position = new Vector2(Mathf.Clamp(borderPadding, -borderPadding, transform.position.x), transform.position.y);
            }
        }
    }

    private void IsMotion(bool Value)
    {
        if (Value)
        {
            isMotion = true;
            playerRigidbody.useGravity = false;
            playerRigidbody.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
        }
        else
        {
            isMotion = false;
            playerRigidbody.useGravity = true;
            playerRigidbody.constraints = RigidbodyConstraints.FreezePositionZ;
            playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, force * 2);

            if (!isGame)
            {
                playerRigidbody.useGravity = false;
                playerRigidbody.constraints = RigidbodyConstraints.FreezeAll;
            }
        }
    }
    private void IsGame()
    {
        isGame = false;
    }
}
