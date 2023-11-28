using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] GameObject[] _Kupler;
    int _AktifKupIndex;

    [SerializeField] GameObject[] _KupSoketleri;
    int _AktifKupSoketIndex;

    [SerializeField] Transform[] _Carpistiricilar;
 

    [SerializeField] KameraTakip _KameraTakip;

    public float _KupGelisHizi;

    bool _DokunmaAktif;
    bool _OyunBittimi;
    public int _ToplananKupSayisi;

    int _SahneIndex;

    [Header("---- UI YONETIMI")]
    [SerializeField]
    GameObject[] _Paneller;
    [SerializeField] TextMeshProUGUI _BestScoreText;

    [Header("---- SES YONETIMI")]
    [SerializeField] AudioSource[] _Sesler;
    [SerializeField] UnityEngine.UI.Image[] _ButonGorselleri;
    [SerializeField] Sprite[] _SpriteObjeleri;

    private void Awake()
    {
        SahneIlkIslemleri();
        _SahneIndex = SceneManager.GetActiveScene().buildIndex;

        if (Instance == null)
            Instance = this;
        else
            Destroy(Instance);
    }

    // Start is called before the first frame update
    void Start()
    {


      
                        
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && _DokunmaAktif && !_OyunBittimi) // Dokunmatik giriş kontrolü
        || Input.GetMouseButtonDown(0)) // Fare sol tuşuna basıldığında
        {
            if (_AktifKupIndex != 0)
            {
                _Kupler[_AktifKupIndex - 1].GetComponent<Kup>()._HareketEdebilirMi = false;
                _Kupler[_AktifKupIndex - 1].GetComponent<Rigidbody>().useGravity = true;
                _Sesler[3].Play();
                _DokunmaAktif = false;
                _KameraTakip._Hedef = _Kupler[_AktifKupIndex - 1].transform;
            }
        }
    }

    public void OyunBitti()
    {
        
        if(_ToplananKupSayisi > PlayerPrefs.GetInt("BestScore"))
        {
            PlayerPrefs.SetInt("BestScore", _ToplananKupSayisi);

        }
        _BestScoreText.text = PlayerPrefs.GetInt("BestScore").ToString();
        _OyunBittimi = true;
        _Sesler[2].Play();
        
        PanelAc(3);
        Time.timeScale = 0;
    }

    public void YeniKupGelsin()
    {
        if (!_OyunBittimi)
        {
            if (_AktifKupIndex != 0)
            {
                _Kupler[_AktifKupIndex - 1].tag = "Untagged";
            }

            _Kupler[_AktifKupIndex].transform.SetPositionAndRotation(_KupSoketleri[_AktifKupSoketIndex].transform.position,
                _KupSoketleri[_AktifKupSoketIndex].transform.rotation);

            _Kupler[_AktifKupIndex].SetActive(true);
            _AktifKupIndex++;


            for (int i = 0; i < _KupSoketleri.Length; i++)
            {
                _KupSoketleri[i].transform.position = new Vector3(_KupSoketleri[i].transform.position.x, _KupSoketleri
                    [i].transform.position.y + .1f, _KupSoketleri[i].transform.position.z);

                _Carpistiricilar[i].transform.position = new Vector3(_Carpistiricilar[i].transform.position.x, _Carpistiricilar
                    [i].transform.position.y + .1f, _Carpistiricilar[i].transform.position.z); 
            }

            if(_AktifKupSoketIndex == 1)
            {
                _AktifKupSoketIndex = 0;
            }
            else
            {
                _AktifKupSoketIndex = 1;
            }

            _DokunmaAktif = true;
        }
    }




    public void ButonIslemleri(string ButonDeger)
    {
        switch (ButonDeger)
        {
            case "Durdur":
                _Sesler[1].Play();
               
                PanelAc(2);
                Time.timeScale = 0;
                break;
            case "DevamEt":
                _Sesler[1].Play();
                PanelKapat(2);
                Time.timeScale = 1;
                break;

            case "OyunaBasla":
                _Sesler[1].Play();
                PanelKapat(1);
                PanelAc(0);
                YeniKupGelsin();
                
                break;
            case "Tekrar":
                _Sesler[1].Play();
                SceneManager.LoadScene(_SahneIndex);
                Time.timeScale = 1;
                break;
            
            case "Cikis":
                _Sesler[1].Play();
                PanelAc(4);
                break;
            case "Evet":
                _Sesler[1].Play();
                UnityEngine.Application.Quit();
                break;
            case "Hayir":
                _Sesler[1].Play();
                PanelKapat(4);
                break;

            case "OyunSes":
                _Sesler[1].Play();

                if (PlayerPrefs.GetInt("OyunSes") == 0)
                {
                    PlayerPrefs.SetInt("OyunSes", 1);
                    _ButonGorselleri[0].sprite = _SpriteObjeleri[0];
                    _Sesler[0].mute = false;
                }
                else
                {
                    PlayerPrefs.SetInt("OyunSes", 0);
                    _ButonGorselleri[0].sprite = _SpriteObjeleri[1];
                    _Sesler[0].mute = true;
                }
                break;

            case "EfektSes":
                _Sesler[1].Play();

                if (PlayerPrefs.GetInt("EfektSes") == 0)
                {
                    PlayerPrefs.SetInt("EfektSes", 1);
                    _ButonGorselleri[1].sprite = _SpriteObjeleri[2];

                    for (int i = 1; i < _Sesler.Length; i++)
                    {
                        _Sesler[i].mute = false;
                    }
                }
                else
                {
                    PlayerPrefs.SetInt("EfektSes", 0);
                    _ButonGorselleri[1].sprite = _SpriteObjeleri[3];
                    for (int i = 1; i < _Sesler.Length; i++)
                    {
                        _Sesler[i].mute = true;
                    }
                }
                break;

        }

    }
    void SahneIlkIslemleri()
    {
        _BestScoreText.text = PlayerPrefs.GetInt("BestScore").ToString();

        if (!PlayerPrefs.HasKey("OyunSes"))
        {
            PlayerPrefs.SetInt("OyunSes", 1);
            PlayerPrefs.SetInt("EfektSes", 1);
        }

        if (PlayerPrefs.GetInt("OyunSes") == 1)
        {
            _ButonGorselleri[0].sprite = _SpriteObjeleri[0];
            _Sesler[0].mute = false;
        }
        else
        {
            _ButonGorselleri[0].sprite = _SpriteObjeleri[1];
            _Sesler[0].mute = true;
        }


        if (PlayerPrefs.GetInt("EfektSes") == 1)
        {
            _ButonGorselleri[1].sprite = _SpriteObjeleri[2];

            for (int i = 1; i < _Sesler.Length; i++)
            {
                _Sesler[i].mute = false;
            }
        }
        else
        {
            _ButonGorselleri[1].sprite = _SpriteObjeleri[3];
            for (int i = 1; i < _Sesler.Length; i++)
            {
                _Sesler[i].mute = true;
            }
        }
    }
    void PanelAc(int Index)
    {
        _Paneller[Index].SetActive(true);
    }
    void PanelKapat(int Index)
    {
        _Paneller[Index].SetActive(false);
    }

}
