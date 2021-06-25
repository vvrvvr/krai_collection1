using DG.Tweening;
using TMPro;
using UnityEngine;
using krai_shooter;

namespace krai_shooter
{
    public class Gun : MonoBehaviour
    {
        [SerializeField] private float rayLength = 100f;
        [SerializeField] private LayerMask layerMaskInteract;
        [HideInInspector] public GameObject raycastedObj;
        [SerializeField] private ParticleSystem muzzleFlash;
        [SerializeField] private ParticleSystem muzzleFlashRifle;
        [SerializeField] private bool isAutoFire;
        [SerializeField] private float fireRate = 15f;
        [SerializeField] private GameObject Rifle;
        [SerializeField] private GameObject pistol;
        [SerializeField] private GameObject ammoText;
        private TextMeshProUGUI ammoTextText;
        private bool isReload;
        private float nextTimeToFire;
        private PlayerController playerController;
        private bool isSwitchingWeapons;
        private int gunPowerupAmmo = 30;


        //ammo
        private int currentAmmo = 10;
        private int ammoCapacity = 10;
        private int ammoToDisplay;

        //animation
        [SerializeField] private Animator animator;
        private void Start()
        {
            playerController = PlayerController.Singleton;
            ammoTextText = ammoText.GetComponent<TextMeshProUGUI>();
        }
        void Update()
        {
            animator.SetBool("isShoot", false);
            animator.SetBool("isShootRifle", false);

            if (!isSwitchingWeapons && !playerController.controllerPauseState)
            {
                if (isAutoFire) // ган поверап
                {
                    if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire) //shoot rifle
                    {
                        currentAmmo -= 1;
                        ammoToDisplay -= 1;
                        if (currentAmmo <= 0)
                        {
                            animator.SetBool("isSwitchPistol", true);
                            currentAmmo = 10;
                            isSwitchingWeapons = true;
                            isAutoFire = false;
                            SoundManager.Singleton.SwitchSound();
                        }
                        ammoTextText.text = $"{ammoToDisplay}";
                        Shoot();
                        //отдача и тряска
                        playerController.RecoilRifle();
                        Camera.main.transform.DOShakePosition(0.05f, 0.3f, 3, 8, false, true);

                        animator.SetBool("isShootRifle", true);
                        SoundManager.Singleton.PlayShootSound();

                    }
                }
                else //пистолет
                {
                    if (Input.GetKeyDown(KeyCode.R))
                    {
                        isReload = true;
                        currentAmmo = 0;
                        animator.SetBool("isReload", true);
                        SoundManager.Singleton.PlayReloadSound();

                    }

                    if (Input.GetButtonDown("Fire1")) //shoot pistol
                    {
                        if (!isReload)
                        {
                            currentAmmo -= 1;
                            if (currentAmmo <= 0)
                                isReload = true;
                            Shoot();
                            // Debug.Log("shooted");
                            Camera.main.transform.DOShakePosition(0.15f, 0.5f, 5, 10, false, true);
                            //Camera.main.transform.DOShakeRotation(0.1f, new Vector3(-2f, 0, 0), 5, 90, false);
                            //Camera.main.transform.DOLocalRotate(new Vector3(Camera.main.transform.localRotation.x + 1f, Camera.main.transform.localRotation.y, Camera.main.transform.localRotation.z), 1f);
                            playerController.Recoil();
                            animator.SetBool("isShoot", true);
                            SoundManager.Singleton.PlayShootSound();
                        }
                        else
                        {
                            animator.SetBool("isReload", true);
                            SoundManager.Singleton.PlayReloadSound();
                        }

                    }
                }
            }
        }

        private void Shoot()
        {
            nextTimeToFire = Time.time + 1 / fireRate;
            if (isAutoFire)
            {
                muzzleFlashRifle.Play();
            }
            else
            {
                muzzleFlash.Play();
            }

            RaycastHit hit;
            Vector3 fwd = Camera.main.transform.TransformDirection(Vector3.forward);

            if (Physics.Raycast(Camera.main.transform.position, fwd, out hit, rayLength, layerMaskInteract.value))
            {
                //Debug.Log("interactable");
                if (hit.collider.CompareTag("shootable"))
                {
                    raycastedObj = hit.collider.gameObject;
                    var popit = raycastedObj.GetComponent<PopitGeneral>();
                    if (popit != null)
                        popit.StartPopit();
                }
                else if (hit.collider.CompareTag("button"))
                {
                    raycastedObj = hit.collider.gameObject;
                    var button = raycastedObj.GetComponent<PopitButton>();
                    button.StartButton();
                }
                else if (hit.collider.CompareTag("gun"))
                {
                    Debug.Log(hit.collider.tag);
                    raycastedObj = hit.collider.gameObject;
                    var popit = raycastedObj.GetComponent<PopitGeneral>();
                    if (popit != null)
                        popit.StartPopit();
                    GunPowerup();
                    //можно звук поверапа добавить
                    SoundManager.Singleton.GunPowerupSound();
                }
                else if (hit.collider.CompareTag("slider"))
                {
                    Debug.Log(hit.collider.tag);
                    raycastedObj = hit.collider.gameObject;
                    var slider = raycastedObj.GetComponent<Slider>();
                    if (slider != null)
                        slider.StartSlider(hit.point);
                }
            }
            else
            {

            }
        }
        public void EndReload()
        {
            isReload = false;
            animator.SetBool("isReload", false);
            isReload = false;
            currentAmmo = ammoCapacity;
        }

        private void GunPowerup()
        {
            if (!isAutoFire)
            {
                animator.SetBool("isSwitchRifle", true);
                isSwitchingWeapons = true;
                SoundManager.Singleton.SwitchSound();

            }
            else
            {
                currentAmmo += gunPowerupAmmo;
                ammoToDisplay += gunPowerupAmmo;
                var probability = new[] { 0, 1 }; //вероятность выпадения  - 50 проц
                var value = probability[Random.Range(0, probability.Length)];
                if (value == 1)
                {
                    int addValue = Random.Range(-7, 10);
                    ammoToDisplay += addValue;
                }
                ammoTextText.text = $"{ammoToDisplay}";
                ScaleEffect();
                Debug.Log(currentAmmo);
            }

        }
        public void EndSwitchingtoRifle()
        {
            animator.SetBool("isSwitchRifle", false);
            Rifle.SetActive(true);
            pistol.SetActive(false);
            Debug.Log("pistol");
            isSwitchingWeapons = false;
            ammoText.SetActive(true);
            ScaleEffect();
            currentAmmo = gunPowerupAmmo;
            ammoToDisplay = currentAmmo;
            var probability = new[] { 0, 1 }; //вероятность выпадения  - 50 проц
            var value = probability[Random.Range(0, probability.Length)];
            if (value == 1)
            {
                int addValue = Random.Range(-7, 10);
                ammoToDisplay += addValue;
            }


            ammoTextText.text = $"{ammoToDisplay}";
            isAutoFire = true;
            Debug.Log(currentAmmo);
        }
        public void EndSwitchingToPistol()
        {
            animator.SetBool("isSwitchPistol", false);
            isSwitchingWeapons = false;
            ammoToDisplay = 0;
            Rifle.SetActive(false);
            ammoText.SetActive(false);
        }

        private void ScaleEffect()
        {
            Sequence scale = DOTween.Sequence();
            scale.Append(ammoTextText.transform.DOScale(new Vector3(1.3f, 1.3f, 1.3f), 0.2f));
            scale.Append(ammoTextText.transform.DOScale(Vector3.one, 0.1f));
        }
    }
}
