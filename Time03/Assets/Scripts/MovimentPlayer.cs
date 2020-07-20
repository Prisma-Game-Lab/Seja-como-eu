using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovimentPlayer : MonoBehaviour
{
    public float MovimentSpeed;
    public float RotationSpeed;
    public float DashSpeed;
    public float dashCooldown;
    private AudioSource Footsteps;
    public AudioClip dashsound;
    private AudioClip pegadas;

    [HideInInspector]
    public bool dashing = false;
    
    private bool dashEnabled = true;

    public float tempoDash = 1.0f;

    private Rigidbody _rb;
    private Animator anim;

    private Vector3 previousDirection;
    private Quaternion Rotation => Quaternion.LookRotation(RotationDirection);
    private Vector3 RotationDirection => Vector3.RotateTowards(transform.forward, previousDirection, RotationSpeed * Time.deltaTime, 0.0f);
    //teclado
    private Vector2 kInput;
    private GeneralCounts Counts;
    private string CurrentScene;

    // Start is called before the first frame update
    void Start()
    {
        Footsteps = GetComponent<AudioSource>();
        pegadas = Footsteps.clip;
        _rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        Counts = SaveSystem.GetInstance().generalCounts;
        CurrentScene = SceneManager.GetActiveScene().name;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        
        if(dashing == false)
        {
            float translationV = 0;
            float translationH = 0;

            translationV = Input.GetAxisRaw("Vertical") *  MovimentSpeed;
            translationH = Input.GetAxisRaw("Horizontal") *  MovimentSpeed;

            translationV *= Time.deltaTime;
            translationH *= Time.deltaTime;

            transform.position += Vector3.Normalize(Direction())* MovimentSpeed * Time.deltaTime;

    
            if((translationV != 0) || (translationH != 0)){
                previousDirection = Vector3.Normalize(Direction());

                anim.SetBool("Idle",false);

                Footsteps.pitch = Random.Range(0.75f, 1.25f);
                if(!Footsteps.isPlaying) {
                Footsteps.Play();
                }
            }
            else
            {
                anim.SetBool("Idle",true);
                Footsteps.Stop();
            }  

                  
        }

        //garante que Player virará para sua direção, mesmo em dash
        transform.rotation = Rotation;  
    }

    private void Update()
    {
        if(!dashing && dashEnabled)
        {
            Dash(previousDirection);
        }
    }

    private Vector3 Direction()
    {
        float h = Input.GetAxisRaw("Horizontal") ;
        float v = Input.GetAxisRaw("Vertical") ;
        return new Vector3(h, 0, v);
    }


    private void Dash(Vector3 dir)
    {
        if (Input.GetAxis("Dash") == 1)
        {
            anim.SetBool("Dash", true);
            Footsteps.clip = dashsound;
            Footsteps.volume = Random.Range(0.68f, 0.72f); 
            Footsteps.pitch = Random.Range(0.95f, 1.05f);
            Footsteps.Play();
            DashCounter();
            _rb.AddForce(dir * DashSpeed, ForceMode.Impulse);
            dashing = true;
            dashEnabled = false;
            StartCoroutine(StopTheDash());

        }
    }

    IEnumerator StopTheDash() {

        yield return new WaitForSeconds(tempoDash);
        Footsteps.volume = 0.26f;
        Footsteps.clip = pegadas;
        _rb.velocity = Vector3.zero; //eliminar qualquer força restante de outro dash
        anim.SetBool("Dash", false);
        dashing = false;
        yield return new WaitForSeconds(dashCooldown);
        dashEnabled = true;

    }

    private void DashCounter() {
        switch(CurrentScene)
        {
            case "Carinho":
            case "CorredorCarinho" :
                Counts.Stats["CarinhoDashCount"]++;
                return;

            case "Tristeza":
            case "CorredorTristeza":
                Counts.Stats["TristezaDashCount"]++;
                return;

            case "Expressividade":
            case "CorredorExpressividade":
                Counts.Stats["ExpressividadeDashCount"]++;
                return;

            case "MDM":
                Counts.Stats["MDMDashCount"]++;
                return;

            case "Hub":
                Counts.Stats["HubDashCount"]++;
                return;
            default:
                return;
        }
        /*
        if(CurrentScene.Contains("Carinho")) {
            Counts.Stats["CarinhoDashCount"]++;
            return;
        }
        if(CurrentScene.Contains("Tristeza")) {
            Counts.Stats["TristezaDashCount"]++;
            return;
        }
        if(CurrentScene.Contains("Expressividade")) {
            Counts.Stats["ExpressividadeDashCount"]++;
            return;
        }
        if(CurrentScene.Contains("MDM")) {
            Counts.Stats["MDMDashCount"]++;
            return;
        }
        if(CurrentScene.Contains("Hub")) {
            Counts.Stats["HubDashCount"]++;
            return;
        }
        */
    }

    public void DesligaAnim() {
        anim.SetBool("Idle",true);
        Footsteps.Stop();
    }

}