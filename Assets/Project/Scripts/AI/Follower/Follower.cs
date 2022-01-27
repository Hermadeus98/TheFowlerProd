using System;
using System.Collections;
using System.Collections.Generic;
using QRCode.Extensions;
using Sirenix.OdinInspector;
using UnityEngine;

namespace TheFowler
{
    public class Follower : SerializedMonoBehaviour
    {
        [SerializeField] private CharacterPlugger CharacterPlugger;
        [ReadOnly] public Transform Target => transform;

        private static Dictionary<CharacterPlugger, Follower> Followers = new Dictionary<CharacterPlugger, Follower>();

        private static void Register(Follower follower)
        {
            Followers.Add(follower.CharacterPlugger, follower);
        }

        public static Follower GetFollower(CharacterPlugger plugger)
        {
            return Followers[plugger];
        }

        private void Start()
        {
            Register(this);
        }
    }

    public enum CharacterPlugger
    {
        ABIGAEL,
        PHEOBE,
    }
}
