using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System;

using UnityEngine.SceneManagement;
using UnityEngine.UI;





// SCRIPT PRINCIPAL USADO EN "JUEGO"
// *********************************

public class XML : MonoBehaviour {


    string[,] preguntas25facil = new string[120, 10];
    string[,] preguntas25normal = new string[120, 10];
    string[,] preguntas25dificil = new string[120, 10];


    string[,] preguntas15 = new string[20, 10];

    bool[] preguntaPublicada = new bool[20];


    int[] respuestas = new int[200];

    public Text cpregunta, texto1, texto2, texto3, texto4, cdinero, textonivel;

    public Text textnivel;

    public int juegoempezado = 0, i, j, k, r;

    public Image imagen, fillnivel;
    
    // Canvas
    public GameObject Canvas2;
    public Canvas CanvasFondo;

    public GameObject Paneltorre, PanelCrowd, Paneltlf;

    public Button b1, b2, b3, b4, b5050, crowd, call;

    public Image bmusica;  // botón-imagen de música on/off

    public Image CrowdFill1, CrowdFill2, CrowdFill3, CrowdFill4;
    public Text Crowdt1, Crowdt2, Crowdt3, Crowdt4;

    public Text Tlftext;

    public int preguntaActual, c5050, ccrowd, ctlf;
    public int RespuestaElegida;
    public int nivelActual;
    //    public Color ColorActual; 


    // REPRODUCTORES USADOS
    public AudioSource audio1;
    public AudioSource audio2;
    public AudioSource audioM;

    // MP3 USADOS
    public AudioClip success;
    public AudioClip wrong;
    public AudioClip suspense;
    public AudioClip musicafondo;
    public AudioClip comodin;

    // VALORES UTILIZADOS EN EL UPDATE
    // *
    public int t=100; // Contador del update
    public bool b1menos50 = false; // indica si ya se ha entrado al bucle de 1 a -50

    // BOOLEAN EMPLEADO PARA USAR EN EL UPDATE, EN CIERTOS MOMENTOS DEL JUEGO
    public bool par, iluminaBoton=false, audio2Activar=false;

    public GameObject funciones;


    // acceso scripts
    public OL1AScript ol1ascript1;
    public cstorre torresc;
    public puntuacion puntuacion1;





    private float numfloat, NumeroFloat;


   
    // Sprites de los estados de los botones al iluminar


    public Sprite Bactual;
    // Imágenes de los botones (básica, verde y roja)

    public Sprite Boriginal;
    public Sprite Bright;
    public Sprite Bwrong;
    public Sprite Bdisabled;

    // usado en Update para iluminar los botones tras la respuesta
    public Sprite Bbase;
    public Sprite Bbase2;

    public bool BotonIntermitentefinalizado = false; // para controlar cuando acaba esta función

    public string skin;











    // Use this for initialization
    void Start () {

    
        // VALORES INICIALES A ESTABLECER
    
    // Imágenes de los estados de los botones
     Boriginal = Resources.Load<Sprite>("Simple UI/PNG/UI_Button_Standard_Sky");
     Bright = Resources.Load<Sprite>("Simple UI/PNG/UI_Button_Standard_Green");
     Bwrong = Resources.Load<Sprite>("Simple UI/PNG/UI_Button_Standard_Red");
     Bdisabled = Resources.Load<Sprite>("Simple UI/Simple Vector Icons/128px/UI_Icon_BtnPsCrossRound");

     Bbase = Resources.Load<Sprite>("Simple UI/PNG/UI_Frame_Dark_White");
     Bbase2 = Resources.Load<Sprite>("Simple UI/PNG/UI_Button_Standard_Orange");


    Canvas2.SetActive(false);
    imagen = GameObject.Find("Imagen").GetComponent<Image>();

    // Acceso a los otros scripts
    ol1ascript1 = funciones.GetComponent<OL1AScript>();
    torresc = funciones.GetComponent<cstorre>();
    puntuacion1 = funciones.GetComponent<puntuacion>();

   
    // Aplica Opciones: música, idioma
    AplicaOpciones();

		
    // ***** LECTURAS DE LAS PREGUNTAS DEL XML (txt)
    // *****
    // id pregunta    opcion1 opcion2 opcion3 opcion4 nivel respuesta        
    ol1ascript1.leerpreguntas(preguntas25facil, 1);
    ol1ascript1.leerpreguntas(preguntas25normal, 2);
    ol1ascript1.leerpreguntas(preguntas25dificil, 3);


    // LECTURA DE PLAYERPREFS
    juegoempezado = ol1ascript1.ObtenerKeyInteger("juegoempezado");






// *************************************** QUITAR PREGUNTAS REPETIDAS (r) (ARRAY DE 25 BOOLEAN)
// >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>




        

//Randomiza 25 x 3 preguntas en 5/5/5  preguntas15 <= (preguntas25facil, preguntas25normal, preguntas25dificil)
for (i = 1; i < 16; i++)
{

  if (i==1 || i==5 || i==11) r = ol1ascript1.Dime25sinRepetir(true); else r = ol1ascript1.Dime25sinRepetir(false);


    for (j = 1; j < 10; j++) { // j = num. de campos
    if (i <= 5)
    {
        preguntas15[i, j] = preguntas25facil[r, j];
        respuestas[i] = Convert.ToInt32(preguntas25facil[r, 8]);
       //    Debug.Log("R:"+r+"-"+preguntas15[i, 2]+"-"+ respuestas[i]+"-"+ preguntas25facil[i, 8]);
    }
    else
    if (i>5 && i<=10)
    {
            
        preguntas15[i, j] = preguntas25normal[r, j];
        respuestas[i] = Convert.ToInt32(preguntas25normal[r, 8]);

    }
    else
    {
        preguntas15[i, j] = preguntas25dificil[r, j];
        respuestas[i] = Convert.ToInt32(preguntas25dificil[r, 8]);

    }


    }

}

        // ***** CONTINÚA EL JUEGO
        // *****
       
        if (juegoempezado == 1)
        {
            nivelActual = ol1ascript1.ObtenerKeyInteger("nivelActual");
            preguntaActual = PlayerPrefs.GetInt("preguntaActual");
            
            c5050= ol1ascript1.ObtenerKeyInteger("c5050");
            ccrowd = ol1ascript1.ObtenerKeyInteger("ccrowd");
            ctlf = ol1ascript1.ObtenerKeyInteger("ctlf");


        } else

        // ***** COMIENZO DEL JUEGO
        // *****

        {
           // Debug.Log("JUEGOEMPEZADO"+juegoempezado);
        preguntaActual = 1;
        nivelActual = 1;

        ol1ascript1.PonerKey("nivelActual", "1", 2);
        PlayerPrefs.SetInt("preguntaActual", 1);
        ol1ascript1.PonerKey("dineroActual", "1", 2);

        ol1ascript1.PonerKey("c5050", "1", 2);
        ol1ascript1.PonerKey("ccrowd", "1", 2);
        ol1ascript1.PonerKey("ctlf", "1", 2);

        ol1ascript1.PonerKey("juegoempezado", "1", 2);

        }

skin = PlayerPrefs.GetString("skin");
// ESTABLECE EL SKIN DEL JUEGO, LOS 4 BOTONES
CargaSkinJ();





        // ***** MUESTRA LA SIGUIENTE PREGUNTA Y RELLENA LA PANTALLA
        // *****

        NuevaPregunta();

        
        

    }
    /* 
     * END START
    *******************************
     */
























// ESTABLECE EL SKIN DEL JUEGO, LOS 4 BOTONES
// ****
public void CargaSkinJ()
{
    b1.image.sprite = Resources.Load<Sprite>("Simple UI/PNG/UI_Button_Standard_"+skin);
    b2.image.sprite = Resources.Load<Sprite>("Simple UI/PNG/UI_Button_Standard_"+skin);
    b3.image.sprite = Resources.Load<Sprite>("Simple UI/PNG/UI_Button_Standard_"+skin);
    b4.image.sprite = Resources.Load<Sprite>("Simple UI/PNG/UI_Button_Standard_"+skin);
}










    // Función que resetea el juego a la pregunta indicada en el input -textnivel-
    // SOLO ÚTIL PARA TESTEO DE PROGRAMACIÓN
   public void Resetea2()
   {
        int n = int.Parse(textnivel.text);
        Resetea(n);
   }
    // END RESETEA2
    // ****













    // Función que resetea el juego como si fuera un juego nuevo
    // SOLO ÚTIL PARA TESTEO DE PROGRAMACIÓN
    public void Resetea(int pregunta)
    {
        {
        //Debug.Log("JUEGOEMPEZADO" + juegoempezado);

        preguntaActual = pregunta;
        nivelActual = pregunta;

        PlayerPrefs.SetInt("nivelActual", preguntaActual);
        PlayerPrefs.SetInt("preguntaActual", preguntaActual);

        ol1ascript1.PonerKey("dineroActual", "1", 2);

        ol1ascript1.PonerKey("c5050", "1", 2);

        ol1ascript1.PonerKey("ccrowd", "1", 2);

        crowd.image.color = Color.clear;
        crowd.enabled = true;

        ol1ascript1.PonerKey("ctlf", "1", 2);
        call.enabled = true;

        ol1ascript1.PonerKey("juegoempezado", "1", 2);

        }

        NuevaPregunta();
    }
    // END Resetea
















    // Aplica el comodín Telefono deshabilitando el boton
    // *
    public void ComodinTlf()
    {

        // Comodin utilizado, ponerlo a 1
        PlayerPrefs.SetInt("ctlf", 1);

        // Deshabilita el botón crowd
        call.image.color = Color.red;
        call.enabled = false;

        // Reproduce audio relacionado con comodin
        StartCoroutine(ol1ascript1.ReproduceAudio(audio1, comodin, ""));


       // preguntaActual = PlayerPrefs.GetInt("preguntaActual");

        int r = respuestas[preguntaActual]; // respuesta correcta

       // Debug.Log("R-->" + r);
        Tlftext.text = preguntas15[preguntaActual, r+2];


        // Animacion
        Animation aPanelC = GameObject.Find("PanelTlf").GetComponent<Animation>(); // Entra el panel en escena
        aPanelC.Play();



      

    }
    // END ComodinTlf














    // Aplica el comodín crowd deshabilitando el boton
    // *
    public void comodinCrowd()
    {

        // Comodin utilizado, ponerlo a 1
        PlayerPrefs.SetInt("ccrowd", 1);

        // Deshabilita el botón crowd
        crowd.image.color = Color.red;
        crowd.enabled = false;

        // Reproduce audio relacionado con comodin
        StartCoroutine(ol1ascript1.ReproduceAudio(audio1, comodin, ""));

        // Deshabilitar incorrectos

        Animation aPanelC = GameObject.Find("PanelCrowd").GetComponent<Animation>(); // Entra el panel en escena
        aPanelC.Play();

        float correcto;
        float error1 = 0.0f;
        float error2 = 0.0f;
        float error3 = 0.0f;

        correcto = ol1ascript1.TantosPorciento(ref error1,  ref error2,  ref error3);

        int r = respuestas[preguntaActual]; // respuesta correcta
        switch (r)
        {
            case 1:
                CrowdFill1.fillAmount = correcto;
                CrowdFill2.fillAmount = error1;
                CrowdFill3.fillAmount = error2;
                CrowdFill4.fillAmount = error3;
                break;

            case 2:
                CrowdFill2.fillAmount = correcto;
                CrowdFill1.fillAmount = error1;
                CrowdFill3.fillAmount = error2;
                CrowdFill4.fillAmount = error3;
                break;

            case 3:
                CrowdFill3.fillAmount = correcto;
                CrowdFill2.fillAmount = error1;
                CrowdFill1.fillAmount = error2;
                CrowdFill4.fillAmount = error3;
                break;

            case 4:
                CrowdFill4.fillAmount = correcto;
                CrowdFill2.fillAmount = error1;
                CrowdFill3.fillAmount = error2;
                CrowdFill1.fillAmount = error3;
                break;
        }

        // Convierte los 4 porcentajes a integer
        int tporciento1 = Mathf.RoundToInt(CrowdFill1.fillAmount * 100);
        int tporciento2 = Mathf.RoundToInt(CrowdFill2.fillAmount * 100);
        int tporciento3 = Mathf.RoundToInt(CrowdFill3.fillAmount * 100);
        int tporciento4 = Mathf.RoundToInt(CrowdFill4.fillAmount * 100);    

        Crowdt1.text = tporciento1.ToString() + "%";
        Crowdt2.text = tporciento2.ToString() + "%";
        Crowdt3.text = tporciento3.ToString() + "%";
        Crowdt4.text = tporciento4.ToString() + "%";




    }
    // END ComodinCrowd













    // Aplica el comodín 50/50 deshabilitando dos botones incorrectos
    // *
    public void comodin5050()
{
        
        // Comodin utilizado, ponerlo a 1
        PlayerPrefs.SetInt("c5050", 1);

        // Deshabilita el botón 5050
        b5050.image.color = Color.red;
        b5050.enabled = false;

        // Reproduce audio relacionado con comodin
        StartCoroutine(ol1ascript1.ReproduceAudio(audio1, comodin, ""));

        // Deshabilitar incorrectos
        int r = respuestas[preguntaActual]; // respuesta correcta
        switch (r)
        {
                case 1:                
                    b2.image.sprite = Bdisabled;
                    b4.image.sprite = Bdisabled;
                    b2.enabled = false;
                    b4.enabled = false;
                    break;

                case 2:
                b1.image.sprite = Bdisabled;
                b4.image.sprite = Bdisabled;
                b1.enabled = false;
                b4.enabled = false;
                break;

                case 3:
                b1.image.sprite = Bdisabled;
                b4.image.sprite = Bdisabled;
                b1.enabled = false;
                b4.enabled = false;
                break;

                case 4:
                b2.image.sprite = Bdisabled;
                b3.image.sprite = Bdisabled;
                b2.enabled = false;
                b3.enabled = false;
                break;
        }


} 
// END Comodin5050











    // Comprueba la respuesta elegida, y continúa el juego o acaba.
    // Parte I - Utiliza CompruebaRespuesta2 -
    public void CompruebaRespuesta(int r)
    {
        int i;
        Button Bt;

        RespuestaElegida = r;

        // Deshabilita los botones para no ser pulsados mientras se comprueba la respuesta
        b1.enabled = false;
        b2.enabled = false;
        b3.enabled = false;
        b4.enabled = false;

        audioM.Stop();

        if (PlayerPrefs.GetInt("musicalocal") == 1)
        { 
        audio2.clip = suspense;
        audio2.loop = true;
        audio2.volume=PlayerPrefs.GetFloat("volumen");
        audio2.Play();        
        }


        if (r == 1) Bt = b1; else if (r==2) Bt = b2; else if (r==3) Bt = b3; else Bt = b4;

        StartCoroutine(CompruebaRespuesta2(Bt)); // Continuamos

    }
    // End CompruebaRespuesta









    /* (Parte II de "CompruebaRespuesta")
     * Ilumina un botón "boton" con dos sprites diferentes durante unos segundos (suspense)
       y luego sigue el juego      
     StartCoroutine(BotonIntermitente(10, b1));    
     */
    int valorActual;
    public IEnumerator CompruebaRespuesta2(Button boton)
    {
        valorActual = 5; //segundos q dura el suspense

        /*  Bucle que se ejecuta durante sec segundos
            Se espera 1 segundo entre cada ejecución del bucle

        Start Suspense
         */
       while (valorActual > 0)
       {
          if (valorActual % 2 == 0) Bactual = Bbase; else Bactual = Bbase2; // Asigna un botón u otro dependiendo si es par-impar
          
          yield return new WaitForSeconds(1.0f);
          valorActual--;
          boton.image.sprite = Bactual;
       }

        // Fin suspense


audio2.Stop(); // Stop música suspense

        // RESPUESTA = CORRECTA
        // *
        if (RespuestaElegida == respuestas[preguntaActual])
        { 
 int nivel=PlayerPrefs.GetInt("preguntaActual");
 string s = puntuacion1.dineroActual[nivel].ToString();
 //Debug.Log("**DINERO: "+s);


/*
 * ASIGNAR PUNTUACIONES SEGÚN NIVEL
 */
        
        if (PlayerPrefs.GetInt("musicalocal") == 1)
            StartCoroutine(ol1ascript1.ReproduceAudio(audio1, success, ""));

        boton.image.sprite = Bright;

if (PlayerPrefs.GetInt("musicalocal")==1) audioM.Play();

//Debug.Log(">>>>>>>>>PREGUNTA: " + preguntaActual);

                // SI HEMOS RESPONDIDO LA ÚLTIMA PREGUNTA = FIN
                // ********************************************
                if (preguntaActual==15)
                     {
                        Animation aPanel2 = GameObject.Find("Panelright").GetComponent<Animation>(); // Entra el panel en escena
                        aPanel2.Play();
                    }
                    else
                    {
                Animation aPanel = GameObject.Find("Paneltorre").GetComponent<Animation>(); // Entra el panel en escena
                aPanel.Play();            
                    // Cesa iluminacion de botones y pasa el control a la torre            
                    torresc.IluminaNivel();
                    }
            
        }
        else
        // RESPUESTA INCORRECTA 
        {

        if (PlayerPrefs.GetInt("musicalocal") == 1)  StartCoroutine(ol1ascript1.ReproduceAudio(audio1, wrong, ""));
            Bactual = Bwrong;

            // ASIGNAR PUNTUACION EN GOOGLEPLAY Y PLAYERPREFS -->> SALIR * ***********************************************

            Animation aPanelC = GameObject.Find("Panelwrong").GetComponent<Animation>(); // Entra el panel en escena
            aPanelC.Play();
            
        }


    }
    // END BOTONPULSADO











    public void jocempezado()
    {
        juegoempezado = 0;
    }

    






    // CARGA LOS 4 FADES DE BOTONES EN ORDEN 
    public IEnumerator Carga12Fades()
    {

        StartCoroutine(ol1ascript1.Fadein1(1f, texto1));
        yield return new WaitForSeconds(3);
        StartCoroutine(ol1ascript1.Fadein1(1f, texto2));
        Carga34Fades();


    }

    public IEnumerator Carga34Fades()
    {

        StartCoroutine(ol1ascript1.Fadein1(1f, texto3));
        yield return new WaitForSeconds(3);
        StartCoroutine(ol1ascript1.Fadein1(1f, texto4));


    }








    // Cambia on/off la Musica
    // 0/1= off/on  -1 = cambia de un estado a otro
 public void CambiaMusica (int musica)
  {
       float vol = PlayerPrefs.GetFloat("volumen");
Debug.Log("*********ESTADO DE MUSICA: "+musica.ToString()+"--"+vol);

       audioM.volume=vol;

// CAMBIA MÚSICA SOLO PARA ESTE JUEGO (local)
 if (musica==-1)
    {
     if (audioM.isPlaying) { 
    audioM.Stop(); 
    PlayerPrefs.SetInt("musicalocal",0);
    bmusica.sprite = Resources.Load<Sprite>("Simple UI/Simple Vector Icons/128px/UI_Icon_MusicOff");       
                           } else 
                            {  
    audioM.enabled=true; 
    audioM.Play(); 
    PlayerPrefs.SetInt("musicalocal",1);
     bmusica.sprite = Resources.Load<Sprite>("Simple UI/Simple Vector Icons/128px/UI_Icon_MusicOn");
                            }
    } 
// FIN LOCAL

    else

// CAMBIA LA MUSICA GENERAL DEL JUEGO
    if (musica==1)
      {

       audioM.enabled=true; 
       audioM.Play();
     bmusica.sprite = Resources.Load<Sprite>("Simple UI/Simple Vector Icons/128px/UI_Icon_MusicOn");      
      } else
        {
        audioM.Stop();
     bmusica.sprite = Resources.Load<Sprite>("Simple UI/Simple Vector Icons/128px/UI_Icon_MusicOff");
        }

    if (musica!=-1) PlayerPrefs.SetInt("musica", musica);  

  }








    // Aplica las opciones existentes en PlayerPrefs
    // opcion> 3 = todas, 1= musica, 2= fx
    public void AplicaOpciones()
    {
        audioM.clip = musicafondo;
    // Volumen
    NumeroFloat = PlayerPrefs.GetFloat("volumen");  
    audioM.volume = NumeroFloat;

    
        // Musica

        Debug.Log("MUSICAPREF="+PlayerPrefs.GetInt("musica"));

int i = PlayerPrefs.GetInt("musica");
    CambiaMusica(i);

    // Fx
    


  }
    // FIN AplicaOpciones









    // Rellena todos los campos con una nueva pregunta
    // *según los datos recogidos de PalerPrefs
    public void NuevaPregunta()
    {
      //  CambiaMusica(-1);
        b1.enabled = true;
        b2.enabled = true;
        b3.enabled = true;
        b4.enabled = true;

        CargaSkinJ();


        preguntaActual = PlayerPrefs.GetInt("preguntaActual");
        //int nivelActual = PlayerPrefs.GetInt("nivelActual");
        

      // Debug.Log("NUEVA PREGUNTA "+preguntaActual);
       cpregunta.text = preguntas15[preguntaActual, 2];
        texto1.text = preguntas15[preguntaActual, 3];
        texto2.text = preguntas15[preguntaActual, 4];
        texto3.text = preguntas15[preguntaActual, 5];
        texto4.text = preguntas15[preguntaActual, 6];
        cdinero.text = Convert.ToString(puntuacion1.dineroActual[preguntaActual-1])+" €";
        textonivel.text = Convert.ToString(preguntaActual) + " / 15";

        numfloat = (preguntaActual * 10);
        fillnivel.fillAmount = (numfloat / 140);

            //   Debug.Log("RESP:fotos/" + preguntas15[preguntaActual, 9]);


        imagen.sprite = Resources.Load<Sprite>(preguntas15[preguntaActual, 9]);

        // FADEIN PARA LA PREGUNTA Y LAS 4 RESPUESTAS 
      
        StartCoroutine(ol1ascript1.Fadein1(1f, texto1));
        StartCoroutine(ol1ascript1.Fadein1(1f, texto2));
        StartCoroutine(ol1ascript1.Fadein1(2f, texto3));
        StartCoroutine(ol1ascript1.Fadein1(2f, texto4)); 
    }









    // Pasa al siguiente nivel 
    public void PasaNivel(bool operacion)
    {

        if (operacion ==true)
        { // se pasa al siguiente nivel       
          //CanvasOk.SetActive(true);    
          // ELIMINAR COLOR DE LOS BOTONES

         preguntaActual++;
         PlayerPrefs.SetInt("preguntaActual", preguntaActual);

         //Botonactual = GameObject.Find("boriginal").GetComponent<Image>();

         


            NuevaPregunta();
       //   Debug.Log("pasada");

        }
        else
        {
         //  StartCoroutine(WaitForSound(wrong, false));
           Canvas2.SetActive(true);
        }

    }










    // Termina el update, Reseteando los valores utilizados en el Update
    // Si se pasa al siguiente nivel pasa=true
    // ****
    public void EndUpdate(bool pasa)
    {
        t = 100;
        b1menos50 = false;
        iluminaBoton = false;
        PasaNivel(pasa);

    }














    // Finaliza el juego
    // RIGHT = true > ganado, false > error
    public void GameOver(bool right)
    {

    //  Obtiene el dinero ganado en la partida actual

    int d = puntuacion1.DimeDineroActual();

      if (right == true)
        {
         // GUARDA PUNTUACIÓN
         // Guarda Score en LeaderBoard
       //  Debug.Log("puntos ganados: "+d);

        puntuacion1.AnyadePuntos(d);

        } 

        
          SceneManager.LoadScene("portada");
        
    }













    // UPDATE **************************************************************
    // Mediante "iluminaBoton" se ejecuta la iluminación intermitente del botón seleccionado
    // **************************************************************

    int con = 0;
    

    void Update()
 {
/*
        // ILUMINACION DE BOTONES (1 de los 4) AL RESPONDER
        if (iluminaBoton == true) 
        // inicialmente t=100
        {

            // Start música suspense
                if (t == 100)
                {
                audioM.Stop();

                audio2.clip = suspense;
                audio2.loop = true;
                audio2.Play();
                }
            // END  


            t--;
            j = t % 100;


            
                // SI T<-50 Termina y pasa el control a la Torre (luego a PasaNivel() y sale del bucle)
                if (t < -50)             
                {
                
                    // Si ya ha acabado la torre, salir del Update
                    //
                    if (PlayerPrefs.GetInt("torreFinalizada") == 1)
                    {
//                    Debug.Log("PREG: "+preguntaActual+" RESULTADO: " +RespuestaElegida+"> correcta="+respuestas[preguntaActual]);
                     EndUpdate(true); // salir de update                    
                    } 
                    //
                    // END PasaNivel()
                } // END Termina y pasa el control a la Torre

    
                
                // SI T ENTRE -50/+1 MOSTRAMOS LA SOLUCION: SI ES CORRECTA, ILUMINA LA TORRE (ANIMAPANEL)
                // Ilumina el botón con el color final (rojo wrong, verde right), stop "suspense" y reproduce audio (con torre incluido)
                else if (t < 1 && t > -50)
                if (b1menos50 == false)
                {
                    b1menos50 = true; // Impide que vuelva a entrar aquí

                    audio2.Stop(); // Stop música suspense

                    if (RespuestaElegida == respuestas[preguntaActual])
                        {

                            StartCoroutine(ol1ascript1.ReproduceAudio(audio1, success, ""));
                            Bactual = Bright;

                    Debug.Log("ANIMANDO PANEL");        
                            ol1ascript1.AnimaPanel("Paneltorre"); // Entra el panel en escena

                            // Cesa iluminacion de botones y pasa el control a la torre
                            torresc.iluminacion = true;
                            torresc.IluminaNivel();

                        }
                        else
                        {
                        StartCoroutine(ol1ascript1.ReproduceAudio(audio1, wrong, ""));
                        Bactual = Bwrong;
                        EndUpdate(false); // salir de update                    
                    }

                }


                // SI T>1 Y T<100 ILUMINA UNO DE LOS CUATRO BOTONES (EL RESPONDIDO)
                // INTERMITENCIA DE COLOR
                else

            if (t > 75 && t < 100) { par = true; }
                    else
            if (t > 50 && t < 75) { par = false; }
                    else
            if (t > 25 && t < 50) { par = true; }
                    else
            if (t > 1 && t < 25) { par = false; }
                    else { par = true; }

                    // iluminando el botón alternativamente
                    if (t > 1)
                    {
                        if (par == true)
                        Bactual = Bbase;
                            else
                        Bactual = Bbase2;
                        
                    }





                if (t > -50) // Si aún estamos con  la pregunta, ilumina el bbotón
                {
                // Ilumina el botón elegido
                switch (RespuestaElegida)
                    {
                        case 1:
                            b1.image.sprite = Bactual;
                            break;
                        case 2:
                            b2.image.sprite = Bactual;
                            break;
                        case 3:
                            b3.image.sprite = Bactual;
                            break;
                        case 4:
                            b4.image.sprite = Bactual;
                            break;
                    }
                }




        } // end if iluminaboton
        */
    } // END UPDATE



}
