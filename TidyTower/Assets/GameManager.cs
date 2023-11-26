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

    [SerializeField] float _KupGelisHizi;

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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
