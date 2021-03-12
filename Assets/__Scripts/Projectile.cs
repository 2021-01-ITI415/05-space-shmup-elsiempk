﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    private BoundsCheck bndCheck;
    private Renderer rend;

    [Header("Set Dynamically")]
    public Rigidbody rigid;
    [SerializeField]
    private WeaponType _type;

    // This public property masks the field _type and takes action when it is set
    public WeaponType type
    {
        get
        {
            return (_type);
        }
        set
        {
            SetType(value);
        }
    }
    private void Awake()
    {
        bndCheck = GetComponent<BoundsCheck>();
        rend = GetComponent<Renderer>();
        rigid = GetComponent<Rigidbody>();
    }

    private float xnot, initInit;
    private Vector3 heroPos;

    private void Start()
    {
        if(this._type == WeaponType.phaser)
        {
            heroPos = Hero.S.transform.position;
            xnot = heroPos.x;
            initInit = Time.time;
        }
    }

    public float vel;

    private void Update()
    {
        if (bndCheck.offUp)
        {
            Destroy(gameObject);
        }

        if(this._type == WeaponType.phaser)
        {
            Vector3 tempPos = heroPos;

            float age = Time.time - initInit;
            float theta = Mathf.PI * 2 * age / Weapon.waveFreq;
            float sin = Mathf.Sin(theta);

            tempPos.x = xnot + Weapon.waveWidth * sin;
            this.rigid.position = tempPos;
        }
    }

    ///<summary>
    /// Sets the _type private field and colors this projectile to match the
    /// WeaponDefinition.
    /// </summary>
    /// <param name="eType">The WeaponType to use.</param>
    public void SetType(WeaponType eType)
    {
        // Set the _type
        _type = eType;
        WeaponDefinition def = Main.GetWeaponDefinition(_type);
        rend.material.color = def.projectileColor;
    }
}
