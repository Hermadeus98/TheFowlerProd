using QRCode.Extensions;
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

        [Button]
        public Robyn ReplacePlayer()
        {
            var robyn = Player.Robyn;
            robyn.pawnTransform.position = current.transform.position;
            robyn.pawnTransform.rotation = current.transform.rotation;
            return robyn;
        }
    }
}
