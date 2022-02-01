using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using Cinemachine;
using DG.Tweening;

public class Ability : CoreComponents
{
    public bool HasGodRemnant { get => _hasGodRemnant; set => _hasGodRemnant = value; }
    public bool HasObtainedPullShoot { get => _hasObtainedPullShoot; set => _hasObtainedPullShoot = value; }
    public bool HasObtainedHyperDash { get => _hasObtainedHyperDash; set => _hasObtainedHyperDash = value; }
    public bool HasObtainedShieldDash { get => _hasObtainedShieldDash; set => _hasObtainedShieldDash = value; }
    public bool HyperShootHasFoundPosition { get => _hyperShootHasFoundPosition; set => _hyperShootHasFoundPosition = value; }
    public bool IsShieldDashing { get => _isShieldDashing; set => _isShieldDashing = value; }
    public bool IsPulling { get => _isPulling; set => _isPulling = value; }
    public bool IsTeleporting { get => _isTeleporting; set => _isTeleporting = value; }
    public bool IsDashing { get => _isDashing; set => _isDashing = value; }

    public Transform ShootPullPoint { get => _shootPullPoint; set => _shootPullPoint = value; }

    public List<IDamageable> DetectedDamageable { get => _detectedDamageable; set => _detectedDamageable = value; }
    public List<IWorld> DetectedWorldEffects { get => _detectedWorldEffects; set => _detectedWorldEffects = value; }

    public Vector2 HyperDashPosition { get => _hyperDashPosiiton; set => _hyperDashPosiiton = value; }

    public Text Text { get => _text; set => _text = value; }

    [SerializeField] private bool _hasGodRemnant;
    [SerializeField] private bool _hasObtainedPullShoot;
    [SerializeField] private bool _hasObtainedHyperDash;
    [SerializeField] private bool _hasObtainedShieldDash;
    [SerializeField] private bool _hyperShootHasFoundPosition;
    [SerializeField] private bool _isShieldDashing;
    [SerializeField] private bool _isPulling;
    [SerializeField] private bool _isDashing;

    [SerializeField] private Transform _shootPullPoint;
    [SerializeField] private GameObject _shootPrefab;
    [SerializeField] private GameObject _shootPrefabHyperDash;
    [SerializeField] private Vector2 _hyperDashPosiiton;

    [Header("Particle Effects")]
    [SerializeField] private ParticleSystem _circleCharge;
    [SerializeField] private ParticleSystem _dashBarrier;
    [SerializeField] private ParticleSystem _smokeTrail;
    [SerializeField] private ParticleSystem _ShockBomb;
    [SerializeField] private ParticleSystem _dustParticle;
    [SerializeField] private ParticleSystem _teleportParticles;
    [SerializeField] private ParticleSystem _circleRetract;
    [SerializeField] private ParticleSystem _pullParticle;

    [Header("Shader Effects")]
    [SerializeField] private Material _teleportMat;
    [SerializeField] private float _fadeMultiplierDown = 2f;
    [SerializeField] private float _fadeMultiplierUp = 0.5f;
    [SerializeField] private bool _isTeleporting;
    private float _fadeFloatShader = 1;

    [Header("DashAfterImage")]
    [SerializeField] Transform _player;
    [SerializeField] private SpriteRenderer _playerSprite;
    [SerializeField] private Transform ghostsParent;
    [SerializeField] private Color trailColor;
    [SerializeField] private Color fadeColor;
    [SerializeField] private float ghostInterval;
    [SerializeField] private float fadeTime;


    private CinemachineImpulseSource _dashCameraShake;

    [SerializeField] private Text _text;

    private List<IDamageable> _detectedDamageable = new List<IDamageable>();
    private List<IWorld> _detectedWorldEffects = new List<IWorld>();

    public void RefillGodRemant() => _hasGodRemnant = true;
    public void UseGodRemnant() => _hasGodRemnant = false;

    public void Start()
    {
        _dashCameraShake = GetComponentInChildren<CinemachineImpulseSource>();
        _teleportMat = GetComponentInParent<SpriteRenderer>().material;
    }

    public void LogicUpdate()
    {
        CheckForDamage();
        CheckForWorldEffects();
    }
    public void Shoot()
    {
        GameObject projectile = Instantiate(_shootPrefab, _shootPullPoint.position, _shootPullPoint.rotation) as GameObject;
        projectile.transform.parent = gameObject.transform;

        UseGodRemnant();
    }

    public void HyperDashShoot()
    {
        GameObject projectile = Instantiate(_shootPrefabHyperDash, _shootPullPoint.position, _shootPullPoint.rotation) as GameObject;
        projectile.transform.parent = gameObject.transform;

    }

    public void SaveHyperDashPosition(Vector2 p_position)
    {
        _hyperDashPosiiton = p_position;
    }

    public void StartHyperDashTeleportShader(float p_fadeController)
    {
        _teleportMat.SetFloat("_FadeAmount", p_fadeController);
    }

    public void PlayHyperDashParticles()
    {
        _ShockBomb.Play();
        _teleportParticles.Play();
        _circleRetract.Play();
    }
    public float FadeControllerDown()
    {
        if(_fadeFloatShader > 0)
        {
            return _fadeFloatShader -= Time.deltaTime * _fadeMultiplierDown;

        }
        else
        {
            return 0;
        }
    }

    public void PlayPullAbilityParticles()
    {
        _pullParticle.Play();
    }

    public float FadeControllerUp()
    {
        if(_fadeFloatShader < 1)
        {
            return _fadeFloatShader += Time.deltaTime * _fadeMultiplierUp;

        }
        else
        {
            _isTeleporting = false;
            return 1;
        }
    }
    
    public void AddToDetected(Collider2D p_collision)
    {
        //Debug.Log("AddedToDetected");
        IDamageable damageable = p_collision.GetComponent<IDamageable>();
        IWorld worldEffects = p_collision.GetComponent<IWorld>();

        if (damageable != null)
        {
            //Debug.Log("Added");
            _detectedDamageable.Add(damageable);
        }

        if(worldEffects != null)
        {
            _detectedWorldEffects.Add(worldEffects);
        }
    }

    public void RemoveFromDetected(Collider2D p_collision)
    {
        //Debug.Log("RemoveFromDetected");
        IDamageable damageable = p_collision.GetComponent<IDamageable>();
        IWorld worldEffects = p_collision.GetComponent<IWorld>();

        if (damageable != null)
        {
            //Debug.Log("Removed");
            _detectedDamageable.Remove(damageable);
        }

        if (worldEffects != null)
        {
            _detectedWorldEffects.Remove(worldEffects);
        }
    }

    private void CheckForDamage()
    {
        foreach (IDamageable item in _detectedDamageable.ToList())
        {
            item.Damage(1f);
            item.Pulled(_isPulling);
            item.Dashed(_isShieldDashing);
        }
    }

    private void CheckForWorldEffects()
    {
        foreach (IWorld item in _detectedWorldEffects.ToList())
        {
            if(_isDashing || _isShieldDashing)
            {
                item.ElectricWater(true);

            }
        }
    }

    public void StartDashParticles()
    {
        GenerateCameraShake(_dashCameraShake);
        _circleCharge.Play();
        _dashBarrier.Play();
        _smokeTrail.Play();
        _ShockBomb.Play();
    }

    public void StartJumpParticleSystem()
    {
        _dustParticle.Play();
    }
    public void SetFoundPositionToTrue() => _hyperShootHasFoundPosition = true;
    public void SetFoundPositionToFalse() => _hyperShootHasFoundPosition = false;
    public void SetShieldDashingToTrue() => _isShieldDashing = true;
    public void SetShieldDashingToFalse() => _isShieldDashing = false;
    public void SetIsPullingToTrue() => _isPulling = true;
    public void SetIsPullingToFalse() => _isPulling = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isShieldDashing || _isDashing)
        {
            core.Ability.AddToDetected(collision);
        }

        if (_isPulling)
        {
            core.Ability.AddToDetected(collision);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        core.Ability.RemoveFromDetected(collision);
    }

    public void UpdateText(int index)
    {
        if(index == 0)
        {
            if (HasGodRemnant)
            {
                _text.text = "Energy Bolt";
            }
            else
            {
                _text.text = "Refill Energy";
            }
        }
        else if(index == 1)
        {
            _text.text = "Hyper Dash";
        }
        else if(index == 2)
        {
            _text.text = "Shield Dash";
        }
    }

    private void GenerateCameraShake(CinemachineImpulseSource source)
    {
        source.GenerateImpulse();
    }

    public void ShowAfterImage(int p_flipIndex, Vector2 p_position)
    {

        Sequence s = DOTween.Sequence();
        bool flipX;

        if(p_flipIndex == 1)
        {
            flipX = true;
        }
        else
        {
            flipX = false;
        }

        for (int i = 0; i < ghostsParent.childCount; i++)
        {
            Transform currentGhost = ghostsParent.GetChild(i);
            s.AppendCallback(() => currentGhost.position = p_position);
            s.AppendCallback(() => currentGhost.GetComponent<SpriteRenderer>().flipX = flipX);
            s.AppendCallback(() => currentGhost.GetComponent<SpriteRenderer>().sprite = _playerSprite.sprite); // A way to find current sprite in dash animation
            s.Append(currentGhost.GetComponent<SpriteRenderer>().material.DOColor(trailColor, 0));
            s.AppendCallback(() => FadeSprite(currentGhost));
            s.AppendInterval(ghostInterval);
        }
    }

    private void FadeSprite(Transform current)
    {
        current.GetComponent<SpriteRenderer>().material.DOKill();
        current.GetComponent<SpriteRenderer>().material.DOColor(fadeColor, fadeTime);
    }

    public void SetIsDashingToTrue() => _isDashing = true;
    public void SetIsDashingToFalse() => _isDashing = false;
}
