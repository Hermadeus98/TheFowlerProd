using QRCode.Extensions;
using Sirenix.OdinInspector;
using UnityEngine;

namespace TheFowler
{
    public class PlayerSpawn : SerializedMonoBehaviour
    {
        public static PlayerSpawn current;
        public static PlayerSpawn defaultPlayerSpawn;

        [SerializeField] private Transform 
            AbigaelSpawnTransform,
            PhoebeSpawnTransform;

        private void Awake()
        {
            current = this;

            if (defaultPlayerSpawn.IsNull())
                defaultPlayerSpawn = this;
        }

        [Button]
        public void SpawnPlayer()
        {
            if (Player.Robyn.IsNotNull())
            {
                ReplacePlayer();
                return;
            }

            var player = Instantiate<Robyn>(Spawnables.Instance.Robyn);
            player.pawnTransform.position = current.transform.position;
            player.pawnTransform.rotation = current.transform.rotation;
        }

        public void SpawnAbigael()
        {
            if (Player.Abigael.IsNotNull())
            {
                ReplaceAbigael();
                return;
            }
            
            var abigael = Instantiate<Abigael>(Spawnables.Instance.Abigael);
            abigael.pawnTransform.position = current.AbigaelSpawnTransform.transform.position;
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
            phoebe.pawnTransform.position = current.PhoebeSpawnTransform.transform.position;
            phoebe.pawnTransform.rotation = current.PhoebeSpawnTransform.transform.rotation;
        }

        [Button]
        public void ReplacePlayer()
        {
            if(!CheckCurrent())
            {
                return;
            }
            
            var robyn = Player.Robyn;
            robyn.gameObject.SetActive(true);
            robyn.pawnTransform.position = current.transform.position;
            robyn.pawnTransform.rotation = current.transform.rotation;
        }

        [Button]
        public void ReplaceAbigael()
        {
            if(!CheckCurrent())
            {
                return;
            }
            
            var abigael = Player.Abigael;
            abigael.gameObject.SetActive(true);
            abigael.pawnTransform.position = current.AbigaelSpawnTransform.transform.position;
            abigael.pawnTransform.rotation = current.AbigaelSpawnTransform.transform.rotation;
        }
        
        [Button]
        public void ReplacePheobe()
        {
            if(!CheckCurrent())
            {
                return;
            }
            
            var pheobe = Player.Pheobe;
            pheobe.gameObject.SetActive(true);
            pheobe.pawnTransform.position = current.PhoebeSpawnTransform.transform.position;
            pheobe.pawnTransform.rotation = current.PhoebeSpawnTransform.transform.rotation;
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
    }
}
