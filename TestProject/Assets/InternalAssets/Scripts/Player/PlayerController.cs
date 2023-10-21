using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public FloatReference speed;
    public VariableJoystick varJoystick;
    public Rigidbody2D rb;
    public Transform armWeapon;
    public WeaponStats CurrentWeapon;
    public CircleCollider2D Sight;
    public FloatVariable maxHP;
    public BoolVariable isTurnedRight;
    public FloatVariable currentHealth;

    private List<Collider2D> targets = new List<Collider2D>();
    private Collider2D currentTarget = null;
    private Quaternion defaultArmTransform;

    void Start()
    {
        isTurnedRight.SetValue(true);
        currentHealth.SetValue(maxHP);
        defaultArmTransform = armWeapon.rotation;
        Sight.radius = CurrentWeapon.range;
    }

    void FixedUpdate()
    {
        //ѕередвижение
        Vector3 direction = new Vector3(varJoystick.Horizontal, varJoystick.Vertical);
        rb.MovePosition(transform.position + direction * speed.Value);

        //ѕрицеливание
        if (currentTarget != null)
        {
            //поворот руки с оружием
            if (isTurnedRight.Value)
            {
                armWeapon.transform.right = currentTarget.transform.position - armWeapon.transform.position;
            }
            else armWeapon.transform.right = armWeapon.transform.position - currentTarget.transform.position;


            //поворот персонажа
            if ((currentTarget.transform.position.x < transform.position.x && isTurnedRight.Value)
                || (currentTarget.transform.position.x > transform.position.x && !isTurnedRight.Value))
            {
                PlayerFlip();
            }
                
        }
        else if (targets.Count > 0)
        {
            ChooseTarget();
        }
        //нет целей в зоне видимости
        else
        {
            armWeapon.rotation = defaultArmTransform;
            if ((direction.x > 0 && !isTurnedRight.Value) || (direction.x < 0 && isTurnedRight.Value))
                PlayerFlip();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            targets.Add(other);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            if (targets.Contains(other))
            {
                targets.Remove(other);
                currentTarget = null;
            }
        }
    }

    public void UpdateSight()
    {
        targets.Clear();
        Sight.radius = 0f;
        Sight.radius = CurrentWeapon.range;
        ChooseTarget();
    }

    public void ChooseTarget()
    {
        float minDist = float.MaxValue;
        currentTarget = null;
        foreach (Collider2D col in targets)
        {
            float dist = Vector3.Distance(col.transform.position, transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                currentTarget = col;
            }
        }
    }

    public void PlayerFlip()
    {
        transform.localScale = new Vector3(transform.localScale.x*(-1), transform.localScale.y, 1);
        isTurnedRight.SetValue(!isTurnedRight.Value);
    }

    public void TakeDamage(float damage)
    {
        currentHealth.SetValue(currentHealth.Value-damage);
        if (currentHealth.Value <= 0)
        {
            Death();
        }
    }

    public void Death()
    {
        Destroy(gameObject);
    }
}
