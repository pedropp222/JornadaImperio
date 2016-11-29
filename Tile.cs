using UnityEngine;
using System.Collections;

public class Tile
{
    public TileTipo tipo;
    public bool explorado;

	//METODO RECURSIVO PARA ALTERAR O TIPO DE TILE, PARA EXPANDIR E CRIAR AS TAIS ILHAS GRANDES
	
    public void AlterarTipo(CriadorMundo cm, int x, int y, int sz, bool force = false)
    {
		//SE O TILE FOR DO TIPO AGUA OU O BOOL FORCE FOR TRUE
        if (tipo == cm.tilesTipos[0] || force)
        {
			//SE O RANDOM DER 2 (1/3 CHANCE), ELE MANDA EXECUTAR ESTE MESMO METODO PARA OS TILES AO LADO
			//COM ISTO E POSSIVEL QUE COMEÇE E ACABE UMA CADEIA DE REAÇAO QUE FAÇA UM CONTINENTE GRANDE, UMA PEQUENA ILHA
			//OU APENAS UMA COISINHA COM 1 TILE
			
            if (Random.Range(0, 3) == 2)
            {
                cm.mundo[x, y].tipo = cm.tilesTipos[1];
                if (x != 0)
                {
                    x--;
                    cm.mundo[x, y].AlterarTipo(cm, x, y, sz);
                }
                if (!(x >= sz-3))
                {
                    x += 2;
                    cm.mundo[x, y].AlterarTipo(cm, x, y, sz);
                }
                if (y != 0)
                {
                    y--;
                    cm.mundo[x, y].AlterarTipo(cm, x, y, sz);
                }
                if (!(y >= sz-3))
                {
                    y += 2;
                    cm.mundo[x, y].AlterarTipo(cm, x, y, sz);
                }
            }
        }
    }
}
