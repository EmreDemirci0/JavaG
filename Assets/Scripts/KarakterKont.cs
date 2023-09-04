using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KarakterKont : MonoBehaviour
{
    public Sprite[] beklemeanim;
    public Sprite[] ziplaanim;
    public Sprite[] yurumeanim;

    

    GameObject kamera;
    Vector3 kameraSonPos;
    Vector3 kameraIlkPos;

    SpriteRenderer spriterendere;
    Rigidbody2D fizik;

    int beklemeAnimSayac = 0;
    int yurumeAnimSayac = 0;

    Vector3 vec;
    float horizontal = 0;
    float beklemeAnimZaman = 0;
    float yurumeAnimZaman = 0;
    public float speed = 0;

    bool ZipliyorMu = false;
    void Start()
    {
        spriterendere = GetComponent<SpriteRenderer>();
        fizik = GetComponent<Rigidbody2D>();
        // fizik.gravityScale = 2;
        kamera = GameObject.FindGameObjectWithTag("MainCamera");
        kameraIlkPos = kamera.transform.position - transform.position;
    }
    void FixedUpdate()
    {
        KarakterHareket();
        animasyon();
    }
    void Update()
    {   
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(ZipliyorMu==false)
            { 
                fizik.AddForce( new Vector2(0,500));
            }
            ZipliyorMu = true;
        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        ZipliyorMu = false;
    }
    void LateUpdate()
    {
        kameraKontrol();
    }
    void KarakterHareket()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vec = new Vector3(horizontal*speed,fizik.velocity.y,0);
        fizik.velocity = vec;


    }
    void animasyon()
    {   
        if(ZipliyorMu==false)
        {
              if (horizontal == 0)//BEKLİYOR//d =1; a=-1;
              {
                  beklemeAnimZaman += Time.deltaTime;
                  if (beklemeAnimZaman > 0.07f)
                  {
                      spriterendere.sprite = beklemeanim[beklemeAnimSayac++];
                      if (beklemeAnimSayac == beklemeanim.Length)
                      {
                          beklemeAnimSayac = 0;
                      }
                      beklemeAnimZaman = 0;
                  }
             
              }
              else if (horizontal > 0)//d =1; a=-1;
              {
                  yurumeAnimZaman += Time.deltaTime;
                  if (yurumeAnimZaman > 0.02f)
                  {
                      spriterendere.sprite = yurumeanim[yurumeAnimSayac++];
                      if (yurumeAnimSayac == yurumeanim.Length)
                      {
                          yurumeAnimSayac = 0;
                      }
                      yurumeAnimZaman = 0;
                  }
                  transform.localScale = new Vector3(1, 1, 1);
              }
              else if (horizontal < 0)//d =1; a=-1;
              {
                  yurumeAnimZaman += Time.deltaTime;
                  if (yurumeAnimZaman > 0.02f)
                  {
                      spriterendere.sprite = yurumeanim[yurumeAnimSayac++];
                      if (yurumeAnimSayac == yurumeanim.Length)
                      {
                          yurumeAnimSayac = 0;
                      }
                      yurumeAnimZaman = 0;
                  }
                  transform.localScale = new Vector3(-1, 1, 1);
              }
        }

        else
        {
                // Debug.Log(fizik.velocity.y);//0dan büyük iken yukarı cıkıyodur 0dan kucuk ise asagı iniyordur
                if (fizik.velocity.y>0)
                {
                    spriterendere.sprite = ziplaanim[0];
                }
                else //if (fizik.velocity.y < 0)
                {
                    spriterendere.sprite = ziplaanim[1];
                }
                if(horizontal>0)
                {
                    transform.localScale = new Vector3(1, 1, 1);

                }
                else if (horizontal<0)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                }

        }
       

    }
    void kameraKontrol()
    {
        kameraSonPos = kameraIlkPos + transform.position;
        // kamera.transform.position = kameraSonPos;
        kamera.transform.position =Vector3.Lerp( kamera.transform.position,kameraSonPos,1f);

    }
}
