using QRCode.Extensions;
using Sirenix.OdinInspector;
using UnityEngine;

namespace TheFowler
{
    public class PlayerSpawn : SerializedMonoBehaviour
    {
        [ShowInInspector] public static PlayerSpawn current;
        public static PlayerSpawn defaultPlayerSpawn;

        [SerializeField] private Transform 
            AbigaelSpawnTransform,
            PhoebeSpawnTransform;

        [SerializeField, ReadOnly] public Vector3 RobynSpawnPosition, AbigaelSpawnPosition, PhoebeSpawnPosition;

        private void Awake()
        {
            current = this;

            if (defaultPlayerSpawn.IsNull())
                defaultPlayerSpawn = this;

            //SecurityFallBox.Instance.playerSpawn = transform;
        }

        public void SpawnRobyn()
        {
            if (Player.Robyn.IsNotNull())
            {
                ReplaceRobyn();
                return;
            }

            var robyn = Instantiate<Robyn>(Spawnables.Instance.Robyn);
            robyn.pawnTransform.position = current.RobynSpawnPosition;
            robyn.pawnTransform.rotation = current.transform.rotation;
        }

        public void SpawnAbigael()
        {
            if (Player.Abigael.IsNotNull())
            {
                ReplaceAbigael();
                return;
            }
            
            var abigael = Instantiate<Abigael>(Spawnables.Instance.Abigael);
            abigael.pawnTransform.position = current.AbigaelSpawnPosition;
            abigael.pawnTransform.rotation = current.AbigaelSpawnTransform.transform.rotation;
        }
        
        public void SpawnPheobe()
        {
            if (Player.Pheobe.IsNotNull())
            {
                ReplacePheobe();
                return;
            }
            
            var phoebe = Instantiate<Pheobe>(Spawnables.Instance.Pheobe);
            phoebe.pawnTransform.position = current.PhoebeSpawnPosition;
            phoebe.pawnTransform.rotation = current.PhoebeSpawnTransform.transform.rotation;
        }

        public void ReplaceRobyn()
        {
            if(!CheckCurrent())
            {
                return;
            }
            
            var robyn = Player.Robyn;
            robyn.gameObject.SetActive(false);
            robyn.pawnTransform.position = RobynSpawnPosition;
            robyn.pawnTransform.rotation = current.transform.rotation;
            robyn.gameObject.SetActive(true);
        }

        public void ReplaceAbigael()
        {
            if(!CheckCurrent())
            {
                return;
            }
            
            var abigael = Player.Abigael;
            abigael.gameObject.SetActive(true);
            abigael.pawnTransform.position = current.AbigaelSpawnPosition;
            abigael.pawnTransform.rotation = current.AbigaelSpawnTransform.transform.rotation;
        }
        
        public void ReplacePheobe()
        {
            if(!CheckCurrent())
            {
                return;
            }
            
            var pheobe = Player.Pheobe;
            pheobe.gameObject.SetActive(true);
            pheobe.pawnTransform.position = current.PhoebeSpawnPosition;
            pheobe.pawnTransform.rotation = current.PhoebeSpawnTransform.transform.rotation;
        }

        [Button]
        private void BakeSpawnPosition()
        {
            RobynSpawnPosition = GetSpawnPosition(transform);
            AbigaelSpawnPosition = GetSpawnPosition(AbigaelSpawnTransform);
            PhoebeSpawnPosition = GetSpawnPosition(PhoebeSpawnTransform);
        }
        
        private bool CheckCurrent()
        {
            if (PlayerSpawn.current.IsNull())
            {
                PlayerSpawn.current = PlayerSpawn.defaultPlayerSpawn;
                //Debug.LogError("There is no Player Spawn Available.");
                return false;
            }

            return true;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, GetSpawnPosition(transform));
            Gizmos.DrawWireSphere(RobynSpawnPosition, .25f);
            
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(AbigaelSpawnTransform.position, GetSpawnPosition(AbigaelSpawnTransform));
            Gizmos.DrawWireSphere(AbigaelSpawnPosition, .25f);
            
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(PhoebeSpawnTransform.position,  GetSpawnPosition(PhoebeSpawnTransform));
            Gizmos.DrawWireSphere(PhoebeSpawnPosition, .25f);
        }

        private Vector3 GetSpawnPosition(Transform from)
        {
            var down = from.TransformDirection(Vector3.down);

            if (Physics.Raycast(from.position, down, out var hit, Mathf.Infinity))
            {
                return hit.point;
            }

            return Vector3.zero;
        }
    }
}
