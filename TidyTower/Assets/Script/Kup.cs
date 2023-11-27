using UnityEngine;

public class Kup : MonoBehaviour
{

    public bool _HareketEdebilirMi;
    float _GelisHizi;

    // Start is called before the first frame update
    void Start()
    {
        _GelisHizi = GameManager.Instance._KupGelisHizi;
    }

    // Update is called once per frame
    void Update()
    {
        if (_HareketEdebilirMi)
            transform.Translate(_GelisHizi * Time.deltaTime * Vector3.forward); //ileriye dogru gitmesini istiyoruz uretilir uretilmez.
    }

    private void OnTriggerEnter(Collider other) // Kaybettigin nokta
    {
        if (other.CompareTag("Carpistirici") || other.CompareTag("Zemin"))
            GameManager.Instance.OyunBitti();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Kup"))
        {
            GameManager.Instance._ToplananKupSayisi++;
            GameManager.Instance.YeniKupGelsin();
            if (GameManager.Instance._ToplananKupSayisi == 1)
                collision.gameObject.tag = "Untagged";
        }
    }
}
