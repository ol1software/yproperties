using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System;

using UnityEngine.SceneManagement;
using UnityEngine.UI;







// SCRIPT PARA CONTROLAR LOS PANELES:
// -- PanelTorre: "QUIEN QUIERE SER MILLONARIO"
// -- PanelCrowd
// ************************************************************************

public class cstorre : MonoBehaviour {

    public Image imagen, imagen2;
    public int   contador = 100;
    public float j;
    public bool par, iluminacion = false;

    public GameObject Funciones;
    public GameObject Panelcstorre; // Para acceder al panel torre
    public GameObject Panelcssiguiente; // Para acceder al panel siguiente

    public Canvas Canvastorre;
    public Canvas Canvas1;

     public Button botok;   // Botón Ok del panel 

     // Acceso texto de "Panelsiguiente"
    public Text texto_ok;


    // acceso scripts
    public OL1AScript ol1ascript1;
    public XML xmlscript;
    public puntuacion puntuacion1;










    // Use this for initialization
    void Start () {
        PlayerPrefs.SetInt("torreFinalizada", 0); // No ha finalizado la torre de iluminarse

        xmlscript = Funciones.GetComponent<XML>();
        ol1ascript1 = Funciones.GetComponent<OL1AScript>();     // ol1ascript.cs
        puntuacion1 = Funciones.GetComponent<puntuacion>();


    botok = GameObject.Find("botokpanel").GetComponent<Button>();
    botok.enabled = false;

        //Panelcstorre = GameObject.Find("Paneltorre").GetComponent<GameObject>();
        xmlscript.Paneltorre.SetActive(true);

    


    }



    public void CierraPanelCrowd()
    {
        //Mueve el panel fuera de la pantalla
        Vector3 temp = new Vector3(925f, -2f, 0);
        xmlscript.PanelCrowd.transform.position+= temp;
    }


    public void CierraPanelTlf()
    {
        //Mueve el panel fuera de la pantalla
        Vector3 temp = new Vector3(925f, -2f, 0);
        xmlscript.Paneltlf.transform.position += temp;
    }












    /* (se ejecuta desde Panelsiguiente) 
 *  Avanza el nivel +1  y  preguntaactual +1
     ** AÑADE EL DINERO GANADO AL JUGADOR
    */
    public void AvanzaNivel()
    {

        int p = PlayerPrefs.GetInt("preguntaActual")+1;


        PlayerPrefs.SetInt("dinero", puntuacion1.dineroActual[p]);
        
// **** AÑADIR DINERO A PUNTOS GANADOS PLAYFAB

      //  Debug.Log("avanzando nivel>> "+p);

    


        PlayerPrefs.SetInt("nivelActual", p);
        PlayerPrefs.SetInt("preguntaActual", p);
        PlayerPrefs.SetInt("torreFinalizada", 1);

        xmlscript.NuevaPregunta();   // Continuamos

    }
    // END AvanzaNivel()








    // Ilumina todos los niveles hasta el nivel (integer) indicado 1-14
    // nivel = "nivelActual" indicado en PlayerPrefs
    // Parte I - Continúa en "IluminaNivel2"
    public void IluminaNivel ()
    {
        int nivel = 1, i;
        Text texto1;
            Canvas1.enabled = false;



        if (PlayerPrefs.HasKey("preguntaActual")) { nivel=PlayerPrefs.GetInt("preguntaActual");  }
        

        // CARGA/ILUMINA TODOS LOS BOTONES (NIVELES) HASTA EL NIVEL ALCANZADO CON IMAGEN ON
            for (i = 1; i <= nivel; i++)
            {
                //  Debug.Log(i);
                imagen = GameObject.Find("nf" + i).GetComponent<Image>();
                imagen.sprite = Resources.Load<Sprite>("Simple UI/PNG/UI_Fill_Green");

                texto1 = GameObject.Find("nft" + i).GetComponent<Text>();
                texto1.color = Color.red;
                


            }
        // FIN CARGA

        Debug.Log("IMAGEN f" + nivel);

        Image boton = GameObject.Find("nf" + nivel).GetComponent<Image>();  // Asigna imagen2 al objeto imagen del nivel alcanzado

        StartCoroutine(IluminaNivel2(boton));
    }






    /* (Parte II de "IluminaNivel")
    * Ilumina un botón "boton" con dos sprites diferentes durante unos segundos (suspense)
      y luego sigue la torre   "StartCoroutine(IluminaNivel(b1));"
    */
    int valorActual;
    public IEnumerator IluminaNivel2(Image boton)
    {
        valorActual = 5; //segundos q dura el suspense

        /*  Bucle que se ejecuta durante sec segundos
            Se espera 1 segundo entre cada ejecución del bucle
        Start Suspense
         */
        while (valorActual > 0)
        {
if (valorActual % 2 == 0) 
    boton.sprite = Resources.Load<Sprite>("Simple UI/PNG/UI_Frame_Dark_White");
   else 
    boton.sprite = Resources.Load<Sprite>("Simple UI/PNG/UI_Frame_Dark_Transparent");
            // Asigna un botón u otro dependiendo si es par-impar

            //Debug.Log("Countdown: " + valorActual);
            yield return new WaitForSeconds(1.0f);
            valorActual--;
        }
boton.sprite = Resources.Load<Sprite>("Simple UI/PNG/UI_Fill_Green");

              
              botok.enabled = true;

        // Hemos acabado la torre, por lo que avanzamos.

       
        //Mueve el paneltorre fuera de la pantalla 
        ol1ascript1.SacaPanel(xmlscript.Paneltorre);       

        // Ponemos dinero ganado en Panelsiguiente        
        int p = PlayerPrefs.GetInt("preguntaActual");

//Debug.Log(puntuacion1.dineroActual[p].ToString()+" €");
        string dinerog = puntuacion1.dineroActual[p].ToString()+" €";
        texto_ok.text = dinerog;

        // Muestra el Canvas de fondo

        xmlscript.CanvasFondo.enabled = true;

       // Canvas canvasf = GameObject.Find("CanvasFondo").GetComponent<Canvas>(); canvasf.enabled = true;

        // y Entra "Panelsiguiente"
        Animation aPanelC = GameObject.Find("Panelsiguiente").GetComponent<Animation>(); 
        aPanelC.Play();

        // texto del panel "Retire", por si se retira, rellenamos lo que ha ganado
        Text ts = GameObject.Find("eurretire").GetComponent<Text>();
        ts.text = dinerog;

         // el botón de Panelsiguiente ejecuta "AvanzaNivel"

    } 
    // END IluminaNivel2




        // Update is called once per frame
        void Update ()
      {
      }
}
