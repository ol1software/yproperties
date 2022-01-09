
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Xml;

using UnityEngine.SceneManagement;






// SCRIPT PARA OBTENER Y PONER PUNTOS EN "EASYFAB & FACEBOOK-PLAYFAB"
// **** REQUISITO: TENER INSTALADO EASYFAB (ASSETSTORE)
//
// Rellena los campos: UserText - PointsText || Usuarioid - PuntosTotal
public class puntuacion : MonoBehaviour
{

 //   private FacebookAndPlayFabManager _facebookAndPlayFabManager;

    public bool entrado_getleaderboard = false;

    // Máximo nº de resultados a mostrar
    public int maxResultsCount = 25;


    public int[] dineroActual = new int[] {0, 1, 100, 200, 300, 500, 1000, 2000, 4000, 8000, 16000, 32000, 64000, 125000, 250000, 500000, 1000000};


    // Objetos necesarios para recoger los datos de las puntuaciones
    public Transform leaderboardEntryParent;
    private int _entries = 0;
  //  public LeaderboardEntry entryPrefab;

    // Canvas de la escena "Portada" (Utilizado para tener acceso )
    public Canvas CanvasrankingFb;
    public Text PuntosPortada;



    // Variables donde se escriben las Puntuaciones y nombreusuario del Usuario Actual
    public int Puntos; //puntos del usuario
    public string Usuario; // nombre de usuario face del usuario
    public int Posicion;    // posición en el Leaderboard
    public Sprite Sprite1;  // sprite del usuario (facebook)

    // Variables del resto de jugadores
    public int[] posicionU = new int[200];    // posicion de 40 usuarios
    public int[] puntosU = new int[200];    // puntos de 40 usuarios
    public string[] nombresU = new string[200];  // nomusuario de 40 usuarios
    public Sprite[] Sprites = new Sprite[200];  // sprites de 40 usuarios
    public int numeroJugadores = 0;                 //  nº de jugadores registrados en el leaderboard

    // campo texto para los puntos USADO SOLO PARA TESTS
    public Text puntosprueba;

    public Text Usuarioid, UserText, PointsText, tituloPos;



    // Textos de: nombre de usuario, puntos e imagen (pantalla opciones-online)
    public Text rk_usuario, rk_puntos, rk_posicion;
    public Image rk_imagen;

    // Textos de: nombreusuario, puntos (pantalla opciones-local)
    public Text local_usuario, local_puntos;


    public Text t1;

    public int  Ach1M, Ach2M = 0;

    // acceso scripts
    public OL1AScript ol1ascript1;
    public cstorre torresc;




    void Start()
    {



    }


    public void Funcion1()
   {
      int p = DimePuntos();
    string s = p.ToString();
    Debug.Log("PUNTOS LOCAL: "+s);  
   }


    public void Funcion2()
    {
    int n = int.Parse(puntosprueba.text.ToString());

    PonPuntos(n);
    RellenaPuntos("portada");
    }








    // Devuelve el dinero actual ganado en la partida
     public int DimeDineroActual()
    {
       int p = PlayerPrefs.GetInt("preguntaActual");
        return(dineroActual[p]);
    }













// FUNCIÓN DE RETIRARSE Y GUARDAR DINERO/PUNTOS: ONLINE Y PLAYPREFS (anyadepuntos) añadir panelsiguiente para ocultarlo
public void Retire()
{
//torresc.Panelcssiguiente.SetActive(false);

    //  Obtiene el dinero ganado en la partida actual

    int d = DimeDineroActual();

        PlayerPrefs.SetInt("dinero", d);

        Animation aPanelC = GameObject.Find("PanelRetire").GetComponent<Animation>(); 
        aPanelC.Play();
 


}





















// Carga la puntuación local del usuario
public int DimePuntos()
{
    if (PlayerPrefs.HasKey("puntos"))
        Puntos = PlayerPrefs.GetInt("puntos");
        else
        {
        Puntos = 1;
        PlayerPrefs.SetInt("puntos", Puntos);
        }


return(Puntos);    

}










/* PONE "PUNTOS" AL JUGADOR ACTUAL (PLAYERPREFS "puntos") (--> mejor AnyadePuntos)
    tbien EN EL LEADERBOARD DE "PLAYFAB.COM" 
    >> Si puntos=-1 : se pone en el "PLAYFAB" los puntos de PlayerPrefs
*/ 
public void PonPuntos(int puntosb)
{
        //_facebookAndPlayFabManager = FacebookAndPlayFabManager.Instance;
    //    FacebookLogin();
    int i = PlayerPrefs.GetInt("puntos");
    if (puntosb==-1) puntosb = i;

  //_facebookAndPlayFabManager.UpdateStat("Scoreboard First Football Quiz", puntosb, null);
    PlayerPrefs.SetInt("puntos", puntosb);
    Debug.Log("PTS: "+puntosb);  

}





/* Variante de "PonPuntos"
 * Añade "PUNTOS" a los ya existentes AL JUGADOR ACTUAL (PLAYERPREFS "puntos")
    tbien EN EL LEADERBOARD DE "PLAYFAB.COM" 
*/ 
public void AnyadePuntos(int puntosb)
{

int p = PlayerPrefs.GetInt("puntos")+puntosb;

        //_facebookAndPlayFabManager = FacebookAndPlayFabManager.Instance;
    //    FacebookLogin();

  //_facebookAndPlayFabManager.UpdateStat("Scoreboard First Football Quiz", p, null);
    PlayerPrefs.SetInt("puntos", p);
    Debug.Log("PTS total: "+p);  

}






/* 
 * - Loguea en facebook/leaderboard y obtiene puntuaciones en:
   - Playerprefs: usuario, puntos, posicion 
   - Rellena los campos (getleaderboard):  rk_puntos, rk_usuario, rk_imagen
 
public void DimePuntosOnline()
{
        _facebookAndPlayFabManager = FacebookAndPlayFabManager.Instance;
        FacebookLogin();

}


*/








// Rellena los campos de puntos/usuario/foto fb de la pantalla dada
// Logear previamente en fb (rellena con > Puntos, Usuario, Sprite1)

public void RellenaPuntos(string pantalla)
{
string s = DimePuntos().ToString("N0")+" €"; //playerprefs

if (pantalla=="opciones")
    { 
        /*Online, se rellenan los campos en GetLeaderboard directamente (rkpuntos-sprite1-rkusuario)
        if (Play erPrefs.GetInt("online")==1)
                 DimePuntosOnline();
*/
        // Local, se rellena el campo desde playerprefs
        local_puntos.text = s;
    }

if (pantalla=="portada")
    {
        PuntosPortada.text =  s;             
    }


//Debug.Log("PTS: "+Puntos.ToString());
}








/* * MOSTRAR LISTA EN CANVASRANKING
* */

// Carga las puntuaciones de los distintos usuarios PLAYFAB > Playerprefs
// CARGA LOS puntos PLAYFAB > PlayerPrefs("puntos")
public void CargaLeaderboard()
{
     
        // indicamos que no hemos leido aun los puntos del usuario
        PlayerPrefs.SetInt("acabadouser", 0);
 GetLeaderboard("Scoreboard First Football Quiz", false, 0);

}






















public void Cierrafb()
{
//_facebookAndPlayFabManager.LogOutFacebook();
}








/* ********************************************************************************
 CONJUNTO DE RUTINAS "FACEBOOK LOGIN"
 1- Loguea al usuario en facebook y rellena las variables globales del usuario 
    (en GetLeaderboardCallback -> "USUARIO", foto "SPRITE1" , "POSICION" y "PUNTOS")

 2- Obtiene puntos online > Playerprefs: usuario, puntos, posicion

 ******************************************************************
*/
    public void FacebookLogin()
    {
/*
        if (string.IsNullOrEmpty(_facebookAndPlayFabManager.playFabTitleId))
        {
            Debug.LogError("PlayFab Title Id is null.");
            return;
        }


        _facebookAndPlayFabManager.LogOnFacebook(res =>
        {
            StartCoroutine(WaitForPlayFabLoginCoroutine());
            StartCoroutine(WaitForUserNameCoroutine());
            StartCoroutine(WaitForProfilePictureCoroutine());
        });
*/
    }
/*
    private IEnumerator WaitForPlayFabLoginCoroutine()
    {
        yield return new WaitUntil(() => _facebookAndPlayFabManager.IsLoggedOnPlayFab);

        GetLeaderboard(PlayFabStatConstants.Test, false, 0);
    }

    private IEnumerator WaitForUserNameCoroutine()
    {
        yield return new WaitUntil(() => !string.IsNullOrEmpty(_facebookAndPlayFabManager.FacebookUserName));
    //    Usuario = _facebookAndPlayFabManager.FacebookUserName;

    }

    private IEnumerator WaitForProfilePictureCoroutine()
    {
        yield return new WaitUntil(() => _facebookAndPlayFabManager.FacebookUserPictureSprite != null);

       // Sprite1 = _facebookAndPlayFabManager.FacebookUserPictureSprite;
    
    CargaLeaderboard();

    }
// END
*/




// ***********************************************************************************
// ***********************************************************************************





// ** CONTINUACIÓN DE FACEBOOKLOGIN
// 1- Rellena variables de usuario: Playerprefs: usuario, puntos, posicion
//  
// **

  public void GetLeaderboard(string statisticName, bool friendsOnly, int startPosition)
    {
     //   _scrollRect.vertical = false;
        //messagePanel.SetActive(true);
     //   _facebookAndPlayFabManager.GetLeaderboard(statisticName, friendsOnly, maxResultsCount, GetLeaderboardCallback, startPosition);
    }


/* ***************************************
 *
*    BUCLE Y ASINACIÓN DE VALORES A USUARIOS 

public void GetLeaderboardCallback(GetLeaderboardResult result)
{        
Debug.Log("********************ENTRANDO EN get");

// evita que entre mucha veces en getleaderboard if (entrado_getleaderboard==true) return;

bool encontradousuario = false;


    foreach (PlayerLeaderboardEntry playerEntry in result.Leaderboard)
    {
      int i = numeroJugadores;

       numeroJugadores++;
        Debug.Log("POSICION: "+i.ToString());
//if (i>0) {
       nombresU[i] = _facebookAndPlayFabManager.FacebookUserName;             //playerEntry.DisplayName;
       puntosU[i] = playerEntry.StatValue;
       posicionU[i] = playerEntry.Position;

        Debug.Log("POSICION: "+i.ToString()+nombresU[i].ToString());


        PlayerPrefs.SetString("usuario"+i, nombresU[i]);
        PlayerPrefs.SetInt("puntos"+i, puntosU[i]);
        PlayerPrefs.SetInt("posicion"+i, posicionU[i]);    
//           }           
    
if (playerEntry.DisplayName == _facebookAndPlayFabManager.FacebookUserId)
        {
Debug.Log(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>ENTRANDO EN usr");

         encontradousuario = true;
        // ES EL USUARIO ACTUAL, ASIGNA LOS VALORES GLOBALES y rellena variables
       Usuario = nombresU[i];
       Puntos = puntosU[i];
       Posicion = posicionU[i];
       Sprite1 = _facebookAndPlayFabManager.FacebookUserPictureSprite;
        PlayerPrefs.SetString("usuario", Usuario);
        PlayerPrefs.SetInt("puntos", Puntos);
        PlayerPrefs.SetInt("posicion", Posicion);

        // Rellena campo puntos desde playfab (pantalla opciones)
        rk_puntos.text =  Puntos.ToString();             
        rk_usuario.text = Usuario.ToString(); 
        rk_posicion.text = Posicion.ToString();
        rk_imagen.sprite = Sprite1; 
        




    UnityEngine.Debug.Log("<*> ENCONTRADO USUARIO: "+Puntos.ToString());
    
        // indicamos que ya hemos leido los puntos del usuario
        PlayerPrefs.SetInt("acabadouser", 1);
         //   entry.SetUserPictureSprite(_facebookAndPlayFabManager.FacebookUserPictureSprite);
        }
        else
        {
            _facebookAndPlayFabManager.GetFacebookUserName(playerEntry.DisplayName, res =>
            {

           // nombresU[numeroJugadores] = res.ResultDictionary["name"].ToString();
          //      entry.SetUserName(res.ResultDictionary["name"].ToString());
            });

            _facebookAndPlayFabManager.GetFacebookUserPicture(playerEntry.DisplayName, 100, 100, res =>
            {
            Sprites[i] = Sprite.Create(res.Texture, new Rect(0, 0, 100, 100), Vector2.zero);
            //    entry.SetUserPictureSprite(Sprite.Create(res.Texture, new Rect(0, 0, width, height), Vector2.zero));
            });

        }
    UnityEngine.Debug.Log("POS: "+posicionU[i]+" ##usuario "+i+" # "+nombresU[i]+"-> "+puntosU[i]+"*** "+Puntos);

        _entries++;
    }



}
// END
*/










}
