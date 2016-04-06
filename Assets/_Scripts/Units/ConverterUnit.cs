using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets._Scripts.GameResources;
using Assets._Scripts.Misc;
using UnityEngine;

namespace Assets._Scripts.Units
{
    [RequireComponent(typeof(Animator), typeof(AudioSource))]
    public class ConverterUnit : Unit
    {
        public const string Wolo = "Wolo";

        public override string Name
        {
            get { return "Converter"; }
        }

        public override int MovementEnergy
        {
            get { return 2; }
        }

        public AudioClip Wololo;

        private AudioSource audioSrc;
        private Animator animator;

        public override List<BuyRequirement> Requirements
        {
            get { return new List<BuyRequirement>()
            {
                new BuyRequirement(typeof(CoinResource), 4f),
                new BuyRequirement(typeof(FoodResource), 4f) 
                };
            }
        }

        public override void Start()
        {
            base.Start();
            audioSrc = GetComponent<AudioSource>();
            animator = GetComponent<Animator>();
            animator.SetBool(Wolo, false);
        }

        public override void Update()
        {
            base.Update();
            animator.SetBool(Wolo, false);
        }

        public override void KillNearbyUnits()
        {
            Collider[] colliders = Physics.OverlapBox(Transform.position, Range);
            bool wololo = false;
            foreach (Collider c in colliders)
            {

                Unit unit = c.GetComponent<CubeUnit>() ?? (Unit)c.GetComponent<SphereUnit>();

                ColorLerpComponent colorLerpComponent = c.GetComponent<ColorLerpComponent>();
                if (colorLerpComponent != null)
                {
                    if (unit != null) colorLerpComponent.OriginalColor = this.PlayerComponent.unitColor;
                    colorLerpComponent.Activate(PlayerComponent.unitColor);
                }

                if (unit == null) continue;
                if (unit.PlayerComponent == this.PlayerComponent) continue;
                unit.PlayerComponent = this.PlayerComponent;

                if (!wololo)
                {
                    audioSrc.clip = Wololo;
                    audioSrc.Play();
                    wololo = true;
                    animator.SetBool(Wolo, true);
                    animator.Play("Wolo");
                }
            }
        }
    }
}
