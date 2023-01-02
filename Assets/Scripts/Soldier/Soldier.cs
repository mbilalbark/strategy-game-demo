using System.Collections.Generic;
using UnityEngine;
using STGD.Core.PubSub;
using STGD.Core.ObjectPooling;
using STGD.Core.Pathfinding;
using System.Collections;
using static UnityEngine.GraphicsBuffer;

namespace STGD.Core.Base
{
    [RequireComponent(typeof(SoldierModel))]
    public class Soldier: Unit, IInitialize<GameObject, Transform, Vector2Int, Soldier>
    {
        private SoldierModel soldierModel;
        private Coroutine movementCoroutine, attackCoroutine;
        List<Node> path;
        public SoldierModel SoldierModel { get => soldierModel; set => soldierModel = value; }

        public void Init(GameObject go, Transform tr, Vector2Int position, IPool pool)
        {
            gameObject.SetActive(true);
            SoldierModel = GetComponent<SoldierModel>();
            SoldierModel.Position = position;
            SoldierModel.Pool = pool;
            SoldierModel.Health = SoldierModel.settings.Health;
        }

        public IEnumerator Move()
        {
            foreach (Node pathNode in path)
            {
                float distance = pathNode.GetDistance(SoldierModel.Position);
                Vector2 nodePos = pathNode.GetCordinate();
                Vector2 selfPos = transform.position;
                float time = distance / SoldierModel.Speed;
                float tempTime = 0.0f;
                while (tempTime < time)
                {
                    transform.position = Vector3.Lerp(selfPos, nodePos, tempTime / time);
                    tempTime += Time.deltaTime;
                    yield return 0;
                }
                SoldierModel.Position = pathNode.GetIntCordinate();
            }
            StopMovement();
        }

        public void SetPath(List<Node> path, bool isMove, bool isAttack, Unit targetUnit = null)
        {
            StopMovement();
            StopAttack();
            
            this.path = path;

            if (isMove)
                movementCoroutine = StartCoroutine(Move());

            if (isAttack)
                attackCoroutine = StartCoroutine(Attack(targetUnit));
        }

        private void StopMovement() 
        {
            if (path != null)
                path.Clear();
            
            if (movementCoroutine != null)
                StopCoroutine(movementCoroutine);

            movementCoroutine = null;
        }

        private void StopAttack()
        {
            if (attackCoroutine != null)
                StopCoroutine(attackCoroutine);
            attackCoroutine = null;
        }

        public IEnumerator Attack(Unit target)
        {
            movementCoroutine = StartCoroutine(Move());
            yield return movementCoroutine;
           
            while(target.UnitModel.Health > 0)
            {
                yield return new WaitForSeconds(soldierModel.AttackSpeed);
                target.Damage(soldierModel.Damage);
            }
            StopAttack();
        }

        public void End()
        {
            StopAttack();
            StopMovement();
            gameObject.SetActive(false);
        }
    }
}
