using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float rotationSpeed = 90f;
    [SerializeField] private Vector2 screenLimits = new Vector2(8f, 4.25f);
    [SerializeField] private Life life;
    [SerializeField] protected GameObject explosion;
    [SerializeField] private List<Transform> bulletSideSpawns;
    [SerializeField] private SpriteRenderer hullSprite;
    [SerializeField] private Sprite[] damgeShipSprites;

    private float sideShootCoolDown = 1f;
    private bool canSideShoot = true;
    private int currentsHullSprite = 0;

    private Shoot _shoot;
    public Life Life { get => life; }

    private void Awake()
    {
        _shoot = GetComponent<Shoot>();
        life.OnDeath.AddListener(OnDeath);
        life.OnTakeDamage.AddListener(OnTakeDamage);
    }

    private void Update()
    {
        MoveAndRotate();

        if (Input.GetMouseButtonDown(0)) // || Input.GetKeyDown(KeyCode.Space))
        {
            _shoot.ShootBullet();
        }
        if (Input.GetMouseButtonDown(1) && canSideShoot) // || Input.GetKeyDown(KeyCode.Space))
        {
            canSideShoot = false;
            Invoke(nameof(SideShootCoolDown), sideShootCoolDown);
            SideShoot();
        }
    }

    private void MoveAndRotate()
    {
        Vector2 movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        transform.position += (Vector3)movement.normalized * speed * Time.deltaTime;

        Vector2 mousePos = Input.mousePosition;
        Vector2 objectPos = Camera.main.WorldToScreenPoint(transform.position);
        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;
        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        LimitToScreen();
    }

    private void LimitToScreen()
    {
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, -screenLimits.x, screenLimits.x);
        pos.y = Mathf.Clamp(pos.y, -screenLimits.y, screenLimits.y);
        transform.position = pos;
    }

    private void SideShoot()
    {
        foreach (var item in bulletSideSpawns)
        {
            _shoot.ShootBullet(item);
        }
    }

    private void OnDeath()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        GameManager.Instance.GameOver();
        gameObject.SetActive(false);
    }

    private void OnTakeDamage()
    {
        var i = Mathf.Clamp(currentsHullSprite, 0, damgeShipSprites.Length);
        hullSprite.sprite = damgeShipSprites[i];
        currentsHullSprite++;
    }

    private void SideShootCoolDown()
    {
        canSideShoot = true;
    }
}
