using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System;

using UnityEngine.SceneManagement;
using UnityEngine.UI;







public class OL1AScript : MonoBehaviour {


    /*
*  SCRIPT COLECCION DE FUNCIONES REUTILIZABLES 
*  USO: 
*  INICIO) Script desde el que llamamos: "micodigo.cs" // GameObject global, donde se arrastran todos los scripts: "FUNCIONES"
  1--EN MICODIGO.CS--
      public GameObject funciones;      

  2--EN EL INSPECTOR DE MICODIGOCS--
      Le asignamos a "funciones" el objecto "Funciones" que contiene todos los scripts
      
  3 --EN MICODIGO.CS--
      public OL1AScript ol1ascript1;                        // en declaración
      ol1ascript1 = funciones.GetComponent<OL1AScript>();   // en Start
*  
*  uso) LLAMADA:  ol1ascript1.FuncionLlamada(1);
*                                                                                
************************************************************
      
      
     * */


    public Text textoo;
    public Animation aPanel;







// Devuelve un número entre 1 y 25, sin repetir
// Si reset=true, vuelve a empezar con los 25 números.
public int Dime25sinRepetir(bool reset)
{
int i = 0, r = -1, c = 0;
bool ok = false;

   if (reset==true)
    {
     for (i=1;i<25;i++)
        {
        PlayerPrefs.SetInt("n"+i.ToString(), 0);
        } 
    }


    // Devuelve el nº random 1-15

        while (ok==false) 
        {
         r= UnityEngine.Random.Range(1, 26);
        i = PlayerPrefs.GetInt("n"+r.ToString());
         if (i == 1) ok=false; 
                     else 
                     { PlayerPrefs.SetInt("n"+r.ToString(),1);    ok=true;   }
        }



Debug.Log("R="+r);


return(r);

} // END
// ******************************












// Saca un panel fuera de la pantalla
public void SacaPanel(GameObject panel)
{
        Vector3 temp = new Vector3(925f, -2f, 0);        
        panel.transform.position += temp;

        // Entra "Panelsiguiente"
        //GameObject panel1 = GameObject.Find("Panelsiguiente").GetComponent<Animation>(); 

}



// Establece el valor indicado para la var "skin"
public void PlayerPfskin(string valor)
{
PlayerPrefs.SetString("skin", valor);
}


    


// Establece el valor indicado para la var "pantalla"
public void PlayerPfpantalla(string valor)
{
PlayerPrefs.SetString("pantalla", valor);
}





    public void CambiaSprite(Image objeto, string nuevosprite)
   {      
      objeto.enabled = true;
      objeto.sprite = Resources.Load<Sprite>(nuevosprite);

   }













    // Calcula 4 tantos por ciento, devuelve uno de ellos, entre 70 y 85, el resto con lo que sobra (15 a 30)
    // DEVUELVE 4 float: t1t2t3,  el maximo lo devuelve en la funcion
    public float TantosPorciento(ref float t1, ref float t2, ref float t3)
    {
        // Rellena el correcto con numero entre 75 y 98
        float correcto = UnityEngine.Random.Range(0.70f, 0.90f);
        float resto = 1.0f - correcto;
       
        // Rellena el erroneo1 entre 0 y lo que queda despues de restar el correcto
        // con los 3 erroneos restantes
        float error1 = UnityEngine.Random.Range(-0.0f, resto);
        float resto2 = resto - error1; // le quitamos el porcentaje del error1

        float error2 = UnityEngine.Random.Range(-0.0f, resto2);
        float resto3 = resto2 - error2; // le quitamos el porcentaje del error2

        float error3 = UnityEngine.Random.Range(-0.0f, resto3);

        t1 = error1;
        t2 = error2;
        t3 = error3;


        /* Se calcula el sobrante, ya que el Random deja algun porcentaje sin asignar
        float sobrante = 1.0f - (t1 + t2 + t3);
        // Lo sumamos al 1
        t1 = t1 + sobrante;
        */

        return (correcto);
    }
    // END TantosPorciento
    // **












    // Play una Animación de un Objeto
    // 1- Crear Objeto("ObjetoAnimar") y seleccionarlo   2- Crear Animación (window-animation-animation) .anim   3-Asignar anim al Objeto
    // 
    public void AnimaPanel(string ObjetoAnimar)
    {
        aPanel = GameObject.Find(ObjetoAnimar).GetComponent<Animation>();
        aPanel.Play();

    }
    // END AnimaPanel
    // **












    // funcion 1 para el fadein de texto
    // USO
    // StartCoroutine(Fadein1(1f, GetComponent<Text>()));
    public IEnumerator Fadein1(float t, Text i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
        while (i.color.a < 2.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
            yield return null;
        }
    }



    // funcion 2 para el fadein de texto
    public IEnumerator Fadein2(float t, Text i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }
    }










    // Cambia la variable "Text" de un GameObject llamado "nombre"
    public void CambiarTexto(string nombre, string valor)
    {
        textoo = GameObject.Find(nombre).GetComponent<Text>();
        textoo.text = valor;
    }











    // ABRE URL EN EL NAVEGADOR
    public void Abrirurl(string u)
    {
        Application.OpenURL(u);
    }










    // Carga la "escena" indicada
    public void CargaEscena(string escena)
    {
            SceneManager.LoadScene(escena);
        //Debug.Log(escena);
    }











    // DEVUELVE UNA KEY TIPO STRING GUARDADA EN PLAYERPREFS 
    public string ObtenerKeyString(string nombre)
    {
        if (PlayerPrefs.HasKey(nombre))
        {
            return (PlayerPrefs.GetString(nombre));
        }
        else { return ("00"); }

    }




    // DEVUELVE UNA KEY TIPO INTEGER GUARDADA EN PLAYERPREFS 
    public int ObtenerKeyInteger(string nombre)
    {
        if (PlayerPrefs.HasKey(nombre))
        {
            return (PlayerPrefs.GetInt(nombre)); ;

        }
        else { return (0); }

    }









    // INSERTA UNA KEY CON UN VALOR EN PLAYERPREFS (TIPO: 1=STRING, 2=INTEGER)
    public void PonerKey(string nombre, string valor, int tipo)
    {
        switch (tipo)
        {
            case 1:
                PlayerPrefs.SetString(nombre, valor);
                break;
            case 2:

                PlayerPrefs.SetInt(nombre, Convert.ToInt32(valor));
                break;
        }

    }










    // LEE PREGUNTAS DE UN XML en el array "arrpreguntas" // FORMATO: id pregunta opcion1 opcion2 opcion3 opcion4 nivel respuesta
    /*  Lectura en array:  string[,] preguntas25facil = new string[120, 10];
          
     *     1) CREAR CSV CON LAS PREGUNTAS
           id	pregunta	            opcion1	opcion2	opcion3	opcion4	nivel	respuesta
            1	DIFICIL¿Cuando fichó?	2009	2006	2010	2012	3	    1

     *     2) CONVERTIR A CSV http://www.convertcsv.com/csv-to-xml.htm
          
  <row>
    1 = <id>1</id>
    2 = <pregunta>¿Cuando fichó este jugador por el Real Madrid?</pregunta>
    3 = <opcion1>2009</opcion1>
    4 = <opcion2>2006</opcion2>
    5 = <opcion3>2010</opcion3>
    6 = <opcion4>2012</opcion4>
    7 = <nivel>1</nivel>
    8 = <respuesta>1</respuesta>
  </row>
    9 = fotos/0n (n=numero de pregunta)

    *       3) COPIAR EN \Resources como .txt
        


     * */
    public void leerpreguntas(string[,] arrpreguntas, int niveldificultad) // niveldificultad (1=facil,2=normal,3=dificil)

    // quitar <row> del xml ** lee preguntas en el array: preguntas[] (0=todas)
    {
        int preg = 1;
        int campo = 1;
        string i="", p, fichero = "preguntasf", carpeta ="", ext="";
        int idm;


        idm = PlayerPrefs.GetInt("idioma"); // 0es 1en 2fr

        if (idm==0)
        ext = "";

        else if (idm==1) 
        ext = "en";

        else
        ext = "fr";

        Debug.Log("IDIOMA="+idm);

        string[] cadena = new string[200];

        switch (niveldificultad)
        {
            case 1:
                //  Debug.Log("FACIL");
                fichero = "preguntasf"+ext;     // preguntasfen - preguntasffr
                carpeta ="";
                break;
            case 2:
                //    Debug.Log("NORMAL");

                fichero = "preguntasn"+ext;
                carpeta ="FN/";
                break;

            case 3:
                //      Debug.Log("DIFICIL");
                fichero = "preguntasd"+ext;
                carpeta ="FD/";
                break;
        }

        TextAsset textXML = (TextAsset)Resources.Load(fichero, typeof(TextAsset));
        XmlDocument xml = new XmlDocument();
        xml.LoadXml(textXML.text);
        XmlNode root = xml.FirstChild;

        foreach (XmlNode node in root.ChildNodes)
        {
        
            //               id pregunta    opcion1 opcion2 opcion3 opcion4 nivel respuesta
            if (node.FirstChild.NodeType == XmlNodeType.Text)
            { // Debug.Log("NODO>> "+node.InnerText);
            if (campo==1) i = node.InnerText;

                if (campo == 8)
                {
                    // respuestas[preg] = Convert.ToInt32(node.InnerText);
                    arrpreguntas[preg, 8] = node.InnerText;

                    // FOTOS
                   
                    if (preg < 10) { p = "0" + i; } else p = i;

                    arrpreguntas[preg, 9] = "fotos/" + carpeta + p;

                    var gameobject = Resources.Load("fotos/" + p) as GameObject;
                    if (gameobject == null)
                    {
                        arrpreguntas[preg, 9] = "fotos/" + carpeta + p;
                    }
                    else
                    {
                        arrpreguntas[preg, 9] = "fotos/generica";

                    }
                    


                    //  Debug.Log("LEEPREGUNTA"+p+"=="+arrpreguntas[preg, 9]);
                    preg++;
                    campo = 1;
                }
                else
                 if (campo <= 7)
                {
                    //if (node.Name == "id")
                    arrpreguntas[preg, campo] = node.InnerText;
      //  Debug.Log(preg+"-"+campo+"="+arrpreguntas[preg, campo]);

                    campo++;
                }


                //    Debug.Log(preg+":"+campo+"> "+arrpreguntas[preg, campo]);



            }
        }
    }
    // END LeerPreguntas
    // **










    // Reproduce el audio, espera 3 sec, carga la "escena" (si se proporciona) al finalizar 
    // llamar asi: StartCoroutine(ol1ascript1.ReproduceAudio...)
    public IEnumerator ReproduceAudio(AudioSource audiosrc,  AudioClip aud, string escena)
    {
        audiosrc.clip = aud;
        audiosrc.Play();

        yield return new WaitForSeconds(3);
        
        if (escena.Length > 1) {
            SceneManager.LoadScene(escena);
                             }
        //Application.LoadLevel("scene");
    }


    // Delay sc segundos y luego asigna 1 al PlayPrefs "playprefc"
    // llamar asi: StartCoroutine(ol1ascript1.Delay...)
    public IEnumerator Delay(int sc, string playprefc)
    {
        yield return new WaitForSeconds(sc);
        PlayerPrefs.SetInt(playprefc, 1);
    }




}
