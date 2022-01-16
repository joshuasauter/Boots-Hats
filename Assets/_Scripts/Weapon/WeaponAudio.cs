using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAudio : AudioPlayer
{
    [SerializeField]
    private AudioClip shootBulletClip = null, outOfBulletsClip = null;

    [SerializeField]
    private AudioClip unloadWeaponClip = null, reloadWeaponClip = null;

    public void PlayShootSound()
    {
        PlayClip(shootBulletClip);
    }

    public void PlayNoBulletSound()
    {
        PlayClip(outOfBulletsClip);
    }

    public void PlayUnloadWeaponSound()
    {
        PlayClip(unloadWeaponClip);
    }

    public void PlayReloadWeaponSound()
    {
        PlayClip(reloadWeaponClip);
    }
}
