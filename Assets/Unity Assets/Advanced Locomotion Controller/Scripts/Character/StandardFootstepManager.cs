using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FasTPS;

namespace FasTPS
{
    public class StandardFootstepManager : MonoBehaviour
    {
        public AudioSource[] ConcreteFootsteps;
        public AudioSource[] GrassFootsteps;
        public AudioSource[] WoodFootsteps;
        public AudioSource[] DirtFootsteps;
        public AudioSource[] GravelFootsteps;
        string currentGround;

        public void FootstepSounds(bool IsSprinting)
        {
            foreach (AudioSource source in ConcreteFootsteps)
            {
                source.Stop();
            }
            foreach (AudioSource source in GrassFootsteps)
            {
                source.Stop();
            }
            foreach (AudioSource source in WoodFootsteps)
            {
                source.Stop();
            }
            foreach (AudioSource source in DirtFootsteps)
            {
                source.Stop();
            }
            foreach (AudioSource source in GravelFootsteps)
            {
                source.Stop();
            }
            int random = Random.Range(0, 2);
            if (currentGround == "Wood")
            {
                if (IsSprinting)
                {
                    WoodFootsteps[random + 2].Play();
                }
                else
                {
                    WoodFootsteps[random].Play();
                }
            }
            else if (currentGround == "Dirt")
            {
                if (IsSprinting)
                {
                    DirtFootsteps[random + 2].Play();
                }
                else
                {
                    DirtFootsteps[random].Play();
                }
            }
            else if (currentGround == "Gravel")
            {
                if (IsSprinting)
                {
                    GravelFootsteps[random + 2].Play();
                }
                else
                {
                    GravelFootsteps[random].Play();
                }
            }
            else if (currentGround == "Grass")
            {
                if (IsSprinting)
                {
                    GrassFootsteps[random + 2].Play();
                }
                else
                {
                    GrassFootsteps[random].Play();
                }
            }
            else
            {
                if (IsSprinting)
                {
                    ConcreteFootsteps[random + 2].Play();
                }
                else
                {
                    ConcreteFootsteps[random].Play();
                }
            }
        }
        public void OnTriggerEnter(Collider col)
        {
            if (col.gameObject.tag == "Wood")
            {
                currentGround = col.gameObject.tag;
            }
            else if (col.gameObject.tag == "Dirt")
            {
                currentGround = col.gameObject.tag;
            }
            else if (col.gameObject.tag == "Gravel")
            {
                currentGround = col.gameObject.tag;
            }
            else if (col.gameObject.tag == "Grass")
            {
                currentGround = col.gameObject.tag;
            }
            else
            {
                currentGround = "Concrete";
            }
        }
        public void OnTriggerExit(Collider col)
        {
            currentGround = "Concrete";
        }
    }
}
