using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheFowler
{
    public class SkyBoxTurn : MonoBehaviour
    {

        [SerializeField] private float speed;

        private void Update()
        {
            transform.Rotate(0,speed*Time.deltaTime, 0, Space.World);
        }
    }
}

