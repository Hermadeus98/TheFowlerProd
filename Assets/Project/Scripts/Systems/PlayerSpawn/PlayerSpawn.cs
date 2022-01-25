using Sirenix.OdinInspector;

namespace TheFowler
{
    public class PlayerSpawn : SerializedMonoBehaviour
    {
        public static PlayerSpawn current;

        private void Awake()
        {
            current = this;
        }

        [Button]
        public Robyn SpawnPlayer()
        {
            var player = Instantiate<Robyn>(Spawnables.Instance.Robyn);
            player.pawnTransform.position = transform.position;
            player.pawnTransform.rotation = transform.rotation;
            return player;
        }

        [Button]
        public Robyn ReplacePlayer()
        {
            var robyn = Player.Robyn;
            robyn.pawnTransform.position = transform.position;
            robyn.pawnTransform.rotation = transform.rotation;
            return robyn;
        }
    }
}
