using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    [SerializeField] float xp;
    [SerializeField] Cell core;
    public Camera playerCamera;
    public Transform backGround;
    public GameObject gameOver;
    [SerializeField] Animator animator;
    //public bool isLooting = false;
    public bool isDestroing = false;
    public AudioManager audioManager;
    public GameObject shieldFront;
    public GameObject shieldBack;
    public List<Sprite> armorSprites;


    void Update()
    {
        if (core.isDestroyed)
        {
            gameOver.SetActive(true);
            audioManager.SoundPlay0();
            this.gameObject.SetActive(false);
            Time.timeScale = 0;
        }
        if (Input.GetMouseButtonUp(0))
        {
            animator.SetBool("IsMoving", false);
            //Debug.Log(false);
        }
    }
    void FixedUpdate()
    {
        Moving();

    }
    void Moving()
    {
        if (Input.GetMouseButton(0))
        {
            var oldPosition = transform.position;

            Vector3 objPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            objPosition.z = transform.position.z;
            transform.position = Vector3.MoveTowards(transform.position, objPosition, speed * Time.deltaTime);
            Vector3 direction = (objPosition - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle - 90);

            //backGround.position = Vector3.MoveTowards(transform.position, (transform.position - objPosition).normalized, Time.deltaTime);
            Vector3 deltaMovement = oldPosition - transform.position;
            backGround.position = new Vector2(backGround.position.x + deltaMovement.x * 0.1f, backGround.position.y + deltaMovement.y * 0.1f);

            //backGround.position = Vector3.MoveTowards(backGround.position, transform.position, speed * Time.deltaTime/3);
            //backGround.position = (transform.position - objPosition).normalized;

            if (!animator.GetBool("IsMoving"))
            {
                animator.SetBool("IsMoving", true);
                //Debug.Log(true);
            }
        }
        else if (Input.GetMouseButton(1))
        {
            Vector3 objPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            objPosition.z = transform.position.z;
            transform.position = Vector3.MoveTowards(transform.position, objPosition, speed * Time.deltaTime / 2);

            Vector2 direction = (objPosition - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle + 90);
        }
        //else backGround.position = transform.position;
    }
}
