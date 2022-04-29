using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.Utilities;
using UnityEngine;

namespace TheFowler
{
    public class AlliesDataView : UIView
    {
        public Dictionary<AllyActor, AllyData> allyDatas = new Dictionary<AllyActor, AllyData>();

        [SerializeField] private AllyData robynData, abiData, phoebeData;

        public AllyData[] datas;

        private float y;

        protected override void OnStart()
        {
            base.OnStart();

            y = datas[0].RectTransform.position.y;
        }

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
            
            data?.Refresh();
        }

        public override void Show()
        {
            base.Show();

            StartCoroutine(ShowIE());
        }

        private IEnumerator ShowIE()
        {
            CanvasGroup.alpha = 1;
            
            for (int i = 0; i < datas.Length; i++)
            {
                var p = datas[i].transform.position;
                datas[i].transform.position = new Vector3(p.x, p.y - 200, p.z);
                datas[i].CanvasGroup.alpha = 0f;
            }
            
            for (int i = 0; i < datas.Length; i++)
            {
                datas[i].CanvasGroup.DOFade(1f, .4f).SetEase(Ease.OutSine);
                datas[i].transform.DOMoveY(y, .4f).SetEase(Ease.OutSine);
                yield return new WaitForSeconds(.4f);
            }
            
            robynData?.Select();
            
            yield break;
        }

        public override void Hide()
        {
            base.Hide();

            CanvasGroup.alpha = 0;
            datas.ForEach(w => w.UnSelect());
        }

        public void RefreshDatas() => datas.ForEach(w => w.Refresh());

        public void StopFury()
        {
            datas.ForEach(w => w.Fury(false));
        }
    }
}
