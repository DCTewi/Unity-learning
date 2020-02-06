using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static event System.EventHandler OnEnemyDead;

    private State<Enemy> currentState;

    private Animator animator;

    private int health;
    private float speed;
    private Vector2 direction;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        ChangeState(new WanderState());

        health = 10;
    }

    private void OnEnable()
    {
        MagicBall.OnMagicHit += OnHitByMagicBall;
    }

    private void OnDisable()
    {
        MagicBall.OnMagicHit -= OnHitByMagicBall;
    }

    private void Update()
    {
        currentState.Execute(this);
    }

    private void ChangeState(State<Enemy> state)
    {
        currentState = state;
        currentState.Enter(this);
    }

    private void OnHitByMagicBall(MagicBall.MagicHitArgs args)
    {
        if (args.Target = gameObject)
        {
            health = Mathf.Clamp(health - args.Damage, 0, health);
            Debug.Log(string.Format("Damaged {0}, health left: {1}.", args.Damage, health));
            if (health == 0)
            {
                ChangeState(new DeadState());
                Destroy(gameObject, 5);
            }
        }
    }

    private class WanderState : State<Enemy>
    {
        public override void Enter(Enemy e)
        {
            Debug.Log("Now in WanderState", e.gameObject);
            e.speed = 6f;
            e.animator.SetFloat("Speed", e.speed);
        }
        public override void Execute(Enemy e)
        {
            int wandering = Random.Range(0, 101);
            if (wandering < 3) // Change Direction
            {
                e.direction = new Vector2(Random.Range(-1, 1), Random.Range(-1, 1)).normalized;
                if (e.direction.x > 0 == e.transform.localScale.x > 0)
                {
                    var scale = e.transform.localScale;
                    scale.x *= -1;
                    e.transform.localScale = scale;
                }
            }
            else // Going
            {
                e.transform.Translate(e.direction * e.speed * Time.deltaTime);
                // Debug.Log(string.Format("Trying moving to {0}", e.direction.ToString()));
            }

            if ((Player.Instance.transform.position - e.transform.position).magnitude < 20f)
            {
                e.ChangeState(new ChaseState());
            }
        }
    }

    private class ChaseState : State<Enemy>
    {
        public override void Enter(Enemy e)
        {
            Debug.Log("Now in ChaseState", e.gameObject);
            e.speed = 14f;
            e.animator.SetFloat("Speed", e.speed);
        }
        public override void Execute(Enemy e)
        {
            e.direction = (Player.Instance.transform.position - e.transform.position).normalized;
            e.transform.Translate(e.direction * e.speed * Time.deltaTime);
            // Debug.Log(string.Format("Trying moving to {0}", e.direction.ToString()));

            if ((Player.Instance.transform.position - e.transform.position).magnitude < 6f)
            {
                e.ChangeState(new AttackState());
            }

            if ((Player.Instance.transform.position - e.transform.position).magnitude > 30f)
            {
                e.ChangeState(new WanderState());
            }
        }
    }

    private class AttackState : State<Enemy>
    {
        public override void Enter(Enemy e)
        {
            Debug.Log("Now in AttackState", e.gameObject);
            e.speed = 0f;
            e.animator.SetFloat("Speed", e.speed);

            int atkID = Random.Range(1, 3);
            int[] frame = { 0, 82, 105 };
            e.animator.SetTrigger("Attack" + atkID);

            e.StartCoroutine(WaitingAttackAnime(frame[atkID] / 60f, e));
        }

        private IEnumerator WaitingAttackAnime(float time, Enemy e)
        {
            yield return new WaitForSecondsRealtime(time);

            if ((Player.Instance.transform.position - e.transform.position).magnitude <= 10f)
            {
                Player.Instance.ChangeHealth(-1);
            }
            e.ChangeState(new WanderState());

        }

        public override void Execute(Enemy e)
        {
            // Get Attack Info...
        }
    }

    private class DeadState : State<Enemy>
    {
        public override void Enter(Enemy e)
        {
            e.animator.SetTrigger("Dead");
            OnEnemyDead.Invoke(this, System.EventArgs.Empty);
            MagicBall.OnMagicHit -= e.OnHitByMagicBall;
        }

        public override void Execute(Enemy e)
        {
            // Dead, Do nothing
        }
    }
}
