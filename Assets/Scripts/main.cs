using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;






/*
 
**************************************TAREAS PENDIENTES**************************************
* 
* 
* 


1- AUDIOS
- volumen al iniciar de 0.20
- PlayerPrefs("musica") > por defecto si no existe HasKey = 1
- botón activar/desactivar local, no funciona al iniciar 1ra vez


2 APLICAR IDIOMA
   - Interface

----
JUEGO (funcionamiento)








*/
// SCRIPT PRINCIPAL DEL JUEGO, USADO EN "PORTADA"
// **********************************************



public class main : MonoBehaviour    {


    // acceso scripts
    public OL1AScript ol1ascript1;
    public puntuacion puntuacion1;
    public GameObject Funciones;


    public GameObject Canvas1;
    public Canvas Canvasopciones;
    public Canvas Canvasranking;

    public Text PuntosRanking, Fbkuser, 
                PuntosPortada;  // texto con puntos de portada

    public GameObject Panelrank;


    public GameObject PanelOpciones;



// BOTONES PORTADA
    public Button boton_start, boton_ranking, boton_opciones;


	public Image Sliderfx_on, Sliderfx_off, Slidermusica_on,Slidermusica_off,
                 Slideronline_on, Slideronline_off;

    public Slider SliderVol;
    public AudioClip musicafondo;




    public Toggle Musica;
    public Toggle fxm;
    public Camera Maincamera;
    public Color Colorskin;

    public AudioSource musicam;
    public Image bandera1, bandera2, bandera3;

    public Dropdown Dropskin;

    public Text Txtbstart1, Txtbopc, Txtbcr, Textomusica, Textofectos, IntroCredit;

    public string[] TStart = new string[] { "Empezar", "Start", "Nouveau jeu"};
    public string[] TCredit = new string[] { "Créditos", "Credits", "Auteur" };
    public string[] TPoints = new string[] { "Puntos", "Points", "Points" };

    public string[] TOptions = new string[] { "Opciones", "Options", "Configurer" };
    public string[] TMusic = new string[] { "Musica", "Music", "Musique" };
    public string[] TFx = new string[] { "Efectos", "Fx", "Effects" };
    public string[] TVolumen = new string[] { "Volumen", "Volume", "Le son" };

    public string[] TCredittxt = new string[] { "Hola, bienvenido a <First Quiz Soccer>, un juego de preguntas de OL1 Software. Pon a prueba tus conocimientos sobre el fútbol en este juego de preguntas gratis. Por favor califica mi juego en Google Play y compártelo.", "Hi, welcome to <First Quiz Soccer>, a quiz game by OL1 Software.   Test your knowledge about soccer in this free quiz game. Please rate my game on Google Play and share it.", "Bonjour, bienvenue sur <First Quiz Soccer>, un jeu de quiz réalisé par OL1 Software. Testez vos connaissances sur le football dans ce jeu de quiz gratuit. Veuillez évaluer mon jeu sur Google Play et le partager." };



    private Text texto1, texto2, texto3, texto4, texto5;
    private Image imagen1;
 

    //public TextMeshProUGUI Txtbstart1;

    public Image Imagen1, Fbkimagen;

    public List<string> Listauno = new List<string>() { "dato1", "dato2" };
    public int i;


    public int idioma;

    public int PuntosTotalMain;

    public float NumeroFloat;

    










    // Use this for initialization
// **************************************************************************
    void Start()
    {


        // PLAYERPREFS
        PlayerPrefs.SetInt("juegoempezado", 0);

    // Acceso a los otros scripts
    puntuacion1 = Funciones.GetComponent<puntuacion>();     // puntuacion.cs
    ol1ascript1 = Funciones.GetComponent<OL1AScript>();     // ol1ascript.cs


     // Log en fb puntuacion1.FacebookLogin();
          
        
        // Lee prefs y Pone check de opciones en on/off según Playerprefs
        PlayerPrefs.SetString("pantalla", "portada");   
        AplicaOpciones();
        
        puntuacion1.RellenaPuntos("portada");
//        int i = PlayerPrefs.GetInt("puntos");


        //bandera1.sprite = Resources.Load<Sprite>("fotos/esp");


    }
    // END START
    // *****






    // Update is called once per frame
    void Update()
    {

    }    
                /*
     * 
     * 
     * 
     * */







// Cierra los paneles al finalizar alguna función. se llama desde otra funcion: StartCoroutine(CierraPaneles());
public IEnumerator CierraPaneles()
{
    yield return new WaitForSeconds(1.0f);
    Canvasopciones.gameObject.SetActive(false);
    Canvasranking.gameObject.SetActive(false);

}









/* RELLENA EL SKIN DE LA PANTALLA SELECCIONADA
Sky, Green, Red, Orange

*/
public void RellenaSkin (string pantalla)
 {

string pantallaActual = PlayerPrefs.GetString("pantalla");
string skin = PlayerPrefs.GetString("skin"), skin2;

   if (skin == "Sky" || skin == "Green" || skin == "Red" || skin == "Orange")
    {
    skin2 = skin;
    } else skin2 = "Sky";

PlayerPrefs.SetString("skin", skin2);    // Establece PlayerPrefs de skin


if (pantallaActual=="opciones")
    {

// Primero asigna el circulo a todos
GameObject.Find ("iconoazul").GetComponent<Image> ().sprite = Resources.Load<Sprite>("Simple UI/Simple Vector Icons/128px/UI_Icon_BtnPsCircle");
GameObject.Find ("iconoverde").GetComponent<Image> ().sprite = Resources.Load<Sprite>("Simple UI/Simple Vector Icons/128px/UI_Icon_BtnPsCircle");
GameObject.Find ("iconorojo").GetComponent<Image> ().sprite = Resources.Load<Sprite>("Simple UI/Simple Vector Icons/128px/UI_Icon_BtnPsCircle");
GameObject.Find ("icononaranja").GetComponent<Image> ().sprite = Resources.Load<Sprite>("Simple UI/Simple Vector Icons/128px/UI_Icon_BtnPsCircle");

// Cambia el color de cada objeto
    GameObject.Find ("PanelOpciones").GetComponent<Image> ().sprite = Resources.Load<Sprite>("Simple UI/PNG/UI_Fill_"+skin2);

        Debug.Log("MSC="+PlayerPrefs.GetInt("musica"));

        Slidermusica_on.enabled = true;
           Slidermusica_on.sprite = Resources.Load<Sprite>("Simple UI/PNG/UI_Button_Standard_"+skin2);

        Sliderfx_on.enabled = true;
           Sliderfx_on.sprite = Resources.Load<Sprite>("Simple UI/PNG/UI_Button_Standard_"+skin2);

      //  if (PlayerPrefs.GetInt("musica") != 1) { Slidermusica_on.enabled = false; Slidermusica_off.enabled = true; }

    GameObject.Find ("fill_volumen").GetComponent<Image> ().sprite = Resources.Load<Sprite>("Simple UI/PNG/UI_Fill_"+skin2);

    GameObject.Find ("botonfaq").GetComponent<Image> ().sprite = Resources.Load<Sprite>("Simple UI/PNG/UI_Button_Standard_"+skin2);
    GameObject.Find ("botoncerrar").GetComponent<Image> ().sprite = Resources.Load<Sprite>("Simple UI/PNG/UI_Button_Standard_"+skin2);

// Pone check al botón de skin seleccionado
   switch (skin)
   {
    case "Sky":
    GameObject.Find ("iconoazul").GetComponent<Image> ().sprite = Resources.Load<Sprite>("Simple UI/PNG/UI_Icon_Confirm2");
    break;

    case "Green":
    GameObject.Find ("iconoverde").GetComponent<Image> ().sprite = Resources.Load<Sprite>("Simple UI/PNG/UI_Icon_Confirm2");
    break;

    case "Red":
    GameObject.Find ("iconorojo").GetComponent<Image> ().sprite = Resources.Load<Sprite>("Simple UI/Simple Vector Icons/128px/UI_Icon_Confirm");
    break;

    case "Orange":
    GameObject.Find ("icononaranja").GetComponent<Image> ().sprite = Resources.Load<Sprite>("Simple UI/Simple Vector Icons/128px/UI_Icon_Confirm");
    break;            
    }

        }
// end opciones



if (pantallaActual=="ranking")
    {

/* GameObject.Find ("iconoazul").GetComponent<Image> ().sprite = Resources.Load<Sprite>("Simple UI/Simple Vector Icons/128px/UI_Icon_BtnPsCircle");
*/

    GameObject.Find ("PanelRanking").GetComponent<Image> ().sprite = Resources.Load<Sprite>("Simple UI/PNG/UI_Fill_"+skin2);
}
// end ranking

    boton_start.image.sprite = Resources.Load<Sprite>("Simple UI/PNG/UI_Button_Standard_"+skin2);
   // boton_ranking.image.sprite = Resources.Load<Sprite>("Simple UI/PNG/UI_Button_Standard_"+skin2);
    boton_opciones.image.sprite = Resources.Load<Sprite>("Simple UI/PNG/UI_Button_Standard_"+skin2);




 }
// END RELLENASKIN
// ***************















    // Aplica las opciones existentes en PlayerPrefs
public void AplicaOpciones()
{
    RellenarIdioma("portada");

    string skin;
    skin = PlayerPrefs.GetString("skin", "Sky");
    RellenaSkin(skin); // APLICA EL SKIN EXISTENTE EN PLAYERPREFS

        Debug.Log("*****MUSICAMAIN="+PlayerPrefs.GetInt("musica"));

   
if (PlayerPrefs.HasKey("musica")==false) PlayerPrefs.SetInt("musica",1);


    // Musica
    if (PlayerPrefs.GetInt("musica") == 1)
    {
    
        Slidermusica_off.gameObject.SetActive(false);
        Slidermusica_on.gameObject.SetActive(true);

        musicam.clip = musicafondo;
        musicam.Play();


    }
    else
    {
        Slidermusica_off.gameObject.SetActive(true);
        Slidermusica_on.gameObject.SetActive(false);
        musicam.Stop();

    }

    /*
    if (PlayerPrefs.GetInt("fx") == 1)
    {
        Sliderfx_on.gameObject.SetActive(true);
        Sliderfx_off.gameObject.SetActive(false);
    }
    else
    {
        Sliderfx_on.gameObject.SetActive(false);
        Sliderfx_off.gameObject.SetActive(true);
    }


    // Guardar online
    if (PlayerPrefs.GetInt("online") == 1)
    {
    
        Slideronline_off.gameObject.SetActive(false);
        Slideronline_on.gameObject.SetActive(true);

    }
    else
    {
        Slideronline_off.gameObject.SetActive(true);
        Slideronline_on.gameObject.SetActive(false);

    }

*/



    // Volumen
    if (PlayerPrefs.HasKey("volumen")==false) { NumeroFloat=0.5f; PlayerPrefs.SetFloat("volumen",0.5f); }
        else
        NumeroFloat = PlayerPrefs.GetFloat("volumen");
    SliderVol.value = (NumeroFloat * 100);
    musicam.volume = NumeroFloat;

        

    // CERRAR LOS PANELES JUSTO AL ACABAR ESTA FUNCIÓN  
StartCoroutine(CierraPaneles());
   

}
// FIN AplicaOpciones












public void spinleft ()
    {
        transform.Rotate(0, 0, 60 * Time.deltaTime);
    }












    public void Salir ()
    {
        Application.Quit();
    }







 












    // Se ejecuta cuando se mueve el toggle de volumen
    public void toggleVol()
    {
        Debug.Log(SliderVol.value);
        NumeroFloat = (SliderVol.value / 100);
        musicam.volume = NumeroFloat;
        PlayerPrefs.SetFloat("volumen", NumeroFloat);
    }





    // Se ejecuta cuando se teclea el toggle de musica
    public void toggleMusica(bool musica)
    { 
	if (musica==false)
     {
         musicam.Stop();
         PlayerPrefs.SetInt("musica", 0);
     } else
     {
         musicam.Play();
         PlayerPrefs.SetInt("musica", 1);
     }
Debug.Log("MUSICA VALOR="+PlayerPrefs.GetInt("musica"));
            
    }











    // Se ejecuta cuando se teclea el toggle de guardar online
    public void toggleOnline(bool on)
    { 

	if (on==false)
     {
         PlayerPrefs.SetInt("online", 0);
     } else
     {
      //   puntuacion1.DimePuntosOnline();
         PlayerPrefs.SetInt("online", 1);
     }
            
    }













    // Se ejecuta cuando se teclea el toggle de Fx-efectos
    public void toggleFX(bool musicafx)
    {
        if (musicafx == false)
        {
 
          PlayerPrefs.SetInt("fx", 0);
        }
        else
        {
          PlayerPrefs.SetInt("fx", 1);
        }


    }

    // Cambia el idioma y rellena la pantalla de opciones
    public void CambiarIdiomaPortada(int uno)
    {
        PlayerPrefs.SetInt("idioma", uno);
         RellenarIdioma("portada");
    }



    // Cambia el idioma y rellena la pantalla de opciones
    public void CambiarIdiomaOpciones(int uno)
    {
        PlayerPrefs.SetInt("idioma", uno);
        RellenarIdioma("opciones");
    }










    // Cambia los textos UI al idioma indicado (0es,1en,2fr)
    // Cambia la pantalla indicada únicamente (ej: "portada", opciones, juego) al "idioma" de PlayerPrefs
    public void RellenarIdioma(string pantalla)
    {
        int b;


        b = PlayerPrefs.GetInt("idioma");

        if (b==null)
        {
            b = 0;
        }

        PlayerPrefs.SetInt("idioma", b);

 

        bandera1.color = Color.grey;
        bandera2.color = Color.grey;
        bandera3.color = Color.grey;



        if (pantalla == "portada" || pantalla=="portadaopciones")
                {
                    Txtbstart1.text = TStart[b];
                    Txtbopc.text = TOptions[b];
                    Txtbcr.text = TCredit[b];
                    IntroCredit.text = TCredittxt[b];
                   // Txtpoints.text = TPoints[b];
                }
                

                if (pantalla == "opciones" || pantalla == "portadaopciones")
                {


                texto1 = GameObject.Find("titulo_opciones").GetComponent<Text>();
                texto2 = GameObject.Find("TextoVolumen").GetComponent<Text>();

                    texto1.text = TOptions[b];
                    Textomusica.text = TMusic[b];
                    Textofectos.text = TFx[b];
                    texto2.text = TVolumen[b];
                }




                if (pantalla == "ranking")
                {
                

                                
                PuntosRanking.text =  puntuacion1.Puntos.ToString("C0");
                
            /*    Fbkuser.text = puntuacion1.Usuario.ToString();
                Fbkimagen.sprite = puntuacion1.Sprite1;

               // Debug.Log("RELLENA.R>> "+ puntuacion1.Usuario.ToString()); */
    
  
                }



                
        if (b == 0) { bandera1.color = new Color(130, 130, 130, 100); }
        else if (b == 1) { bandera2.color = new Color(130, 130, 130, 100); }
        else { bandera3.color = new Color(130, 130, 130, 100); }
        
    }












    public void CambiarSkin()
    {
        PlayerPrefs.SetInt("skin", Dropskin.value);
        Debug.Log(Dropskin.value);
      switch(Dropskin.value)
        {
          case 0:
                Colorskin = new Color32(60, 140, 60, 255);
                boton_start.image.color = Colorskin;
                Maincamera.backgroundColor = Colorskin;
              //  Txtbstart1.color = new Color32(255, 255, 255, 100);
                break;

          case 1:
                Colorskin = new Color32(51, 100, 140, 200);
                boton_start.image.color = Colorskin;
                Maincamera.backgroundColor = Colorskin;
            //    Txtbstart1.color = new Color32(255, 255, 255, 100);
                break;

          case 2:
                Colorskin = new Color32(198, 108, 108, 100);
                boton_start.image.color = Colorskin;
                Maincamera.backgroundColor = Colorskin;
             //   Txtbstart1.color = new Color(255, 255, 255, 100);
                break;

        }

    }



                                       }
