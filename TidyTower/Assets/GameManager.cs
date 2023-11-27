using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] GameObject[] _Kupler;
    int _AktifKupIndex;

    [SerializeField] GameObject[] _KupSoketleri;
    int _AktifKupSoketIndex;

    [SerializeField] Transform[] _Carpistiricilar;
    int _AktifCarpistiriciIndex;

    public float _KupGelisHizi;

    bool _DokunmaAktif;
    bool _OyunBittimi;
    public int _ToplananKupSayisi;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(Instance);
    }

    // Start is called before the first frame update
    void Start()
    {
        YeniKupGelsin(); //oyuna basla ui yapildiginda bu kalkacak...
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && _DokunmaAktif) // Dokunmatik giriş kontrolü
        || Input.GetMouseButtonDown(0)) // Fare sol tuşuna basıldığında
        {
            if (_AktifKupIndex != 0)
            {
                _Kupler[_AktifKupIndex - 1].GetComponent<Kup>()._HareketEdebilirMi = false;
                _Kupler[_AktifKupIndex - 1].GetComponent<Rigidbody>().useGravity = true;
                _DokunmaAktif = false;
            }
        }
    }

    public void OyunBitti()
    {
        _OyunBittimi = true;
        Debug.Log("Kaybettin");
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

    
}
