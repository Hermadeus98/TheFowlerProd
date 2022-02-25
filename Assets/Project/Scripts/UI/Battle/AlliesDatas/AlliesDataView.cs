using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheFowler
{
    public class AlliesDataView : UIView
    {
        public Dictionary<AllyActor, AllyData> allyDatas = new Dictionary<AllyActor, AllyData>();

        [SerializeField] private AllyData robynData, abiData, phoebeData;
        
        public void Initialize(AllyActor robyn, AllyActor abi, AllyActor phoebe)
        {
            allyDatas = new Dictionary<AllyActor, AllyData>();

            InitializeData(robyn, robynData);
            InitializeData(abi, abiData);
            InitializeData(phoebe, phoebeData);
        }

        private void InitializeData(AllyActor actor, AllyData data)
        {
            if (actor != null)
            {
                if (actor.isParticipant)
                {
                    data.gameObject.SetActive(true);
                    allyDatas.Add(actor, data);
                    data.Register(actor);
                }
                else
                {
                    data.gameObject.SetActive(false);
                }
            }
            else
            {
                data.gameObject.SetActive(false);
            }
        }
    }
}
