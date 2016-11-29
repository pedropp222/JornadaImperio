using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CriadorMundo : MonoBehaviour
{
	
	//TILESTIPOS[0] = AGUA | 1 = TERRA | 2 = AREIA
    [SerializeField]
    public TileTipo[] tilesTipos;

    public Tile[,] mundo;

    public GameObject spriteMapa;

    GameObject cnv;

    public int tamanho;

    void Start()
    {
        cnv = GameObject.Find("Canvas");
        Gerar();
        CriarPraias();
        DesenharMapa();
    }

    void Gerar()
    {
        //CRIAR UM NOVO MUNDO
        mundo = new Tile[tamanho, tamanho];

        //ENCHER COM TIPO ELEMENTO 0 (AGUA)
        for (int x = 0; x < tamanho; x++)
        {
            for (int y = 0; y < tamanho; y++)
            {
                mundo[x, y] = new Tile();
                mundo[x, y].tipo = tilesTipos[0];
            }
        }

        //ITERAR OUTRA VEZ PELOS TILES E TEM 1/100 DE PROBABILIDADE DE VIR A TORNAR UM TILE EM TIPO 1 (TERRA)

        for (int x = 0; x < tamanho; x++)
        {
            for (int y = 0; y < tamanho; y++)
            {
                if (Random.Range(0, 100) == 4)
                {
                    mundo[x, y].tipo = tilesTipos[1];
                }
            }
        }
        AumentarIlhas();
    }

    void AumentarIlhas()
    {
		//CLONAR O MUNDO PARA UM NOVO ARRAY
		
        Tile[,] mundoBack = (Tile[,])mundo.Clone();
		
		//LOOP PARA PERCORRER E ENCONTRAR APENAS AS ILHAS INICIAIS
		
        for (int x = 0; x < tamanho; x++)
        {
            for (int y = 0; y < tamanho; y++)
            {
                if (mundoBack[x,y].tipo == tilesTipos[1])
                {
					//INICIAR O METODO RECURSIVO NUMA DAS ILHAS INICIAIS, PARA COM SORTE CRIAR OS CONTINENTES GRANDES
					//ESTA AQUI O UNICO SITIO ONDE COLOCAMOS O BOOL FORCE COMO TRUE PORQUE O METODO RECURSIVO
					//SO VERIFICA SE O TILE E AGUA, MAS AQUI FORÇAMOS A VERIFICAÇAO APESAR DE SER TILE DE TERRA
					
                    mundo[x, y].AlterarTipo(this, x, y,tamanho,true);
                }
            }
        }
    }

    void CriarPraias()
    {
		//BASICAMENTE PERCORRE APENAS PELOS TILES DE TERRA E SE TIVER NO MINIMO 1 TILE DE AGUA EM CADA UM
		//DOS LADOS ELE MUDA ESSE TILE DE TERRA PARA AREIA
		
        int tm = tamanho;
        for (int x = 0; x < tamanho; x++)
        {
            for (int y = 0; y < tamanho; y++)
            {
                if (mundo[x, y].tipo == tilesTipos[1])
                {
                    int xb = x;
                    tm = tamanho;
                    if (xb != 0)
                    {
                        xb--;
                        if (mundo[xb,y].tipo == tilesTipos[0])
                        {
                            mundo[x, y].tipo = tilesTipos[2];
                            continue;
                        }
                    }
                    xb = x;
                    tm--;
                    if (xb != tm)
                    {
                        xb++;
                        if (mundo[xb, y].tipo == tilesTipos[0])
                        {
                            mundo[x, y].tipo = tilesTipos[2];
                            continue;
                        }
                    }

                    int yb = y;
                    tm = tamanho;
                    if (yb != 0)
                    {
                        yb--;
                        if (mundo[x, yb].tipo == tilesTipos[0])
                        {
                            mundo[x, y].tipo = tilesTipos[2];
                            continue;
                        }
                    }
                    yb = y;
                    tm--;
                    if (yb != tm)
                    {
                        yb++;
                        if (mundo[x, yb].tipo == tilesTipos[0])
                        {
                            mundo[x, y].tipo = tilesTipos[2];
                            continue;
                        }
                    }
                }
            }
        }
    }

    void DesenharMapa()
    {
		//CRIAR MILHARES DE SPRITES NO MUNDO PARA SE VER O QUE REALMENTE CRIOU
		
        for (int x = 0; x < tamanho; x++)
        {
            for (int y = 0; y < tamanho; y++)
            {
                GameObject obj = (GameObject)Instantiate(spriteMapa);
                obj.GetComponent<Image>().material = mundo[x, y].tipo.corMapa;
                obj.transform.SetParent(cnv.transform, false);
                obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(x*50, -y*50);
            }
        }
    }
}