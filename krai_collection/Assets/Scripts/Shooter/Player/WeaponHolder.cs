using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using krai_shooter;

namespace krai_shooter
{
    public class WeaponHolder : MonoBehaviour
    {
        [SerializeField] private Gun gun;
        //[SerializeField] private Rifle rifle;

        public void OnReloadEnd()
        {
            gun.EndReload();
        }
        public void OnSwitchToRifleEnd()
        {
            gun.EndSwitchingtoRifle();
        }
        public void OnSwitchToPistolEnd()
        {
            gun.EndSwitchingToPistol();
        }
    }
}
