namespace MoogleEngine;


public static class Moogle
{
    public static SearchResult Query(string query)
    {
        //DEFINIENDO VARIABLES
        //Antes del trabajo con la query
        string[] direcciones = Metodos.Direcciones();//Array string[] que contiene las direcciones de cada documento
        string[] titulos = Metodos.Titulos(direcciones);//Array string[] que contiene los títulos de cada documento
        string[] contenidos = Metodos.Contenido(direcciones);//Array string[] que contiene los contenidos de cada documento (SIN NORMALIZAR)
        string[] Min = Metodos.ContenidoMinusculas(contenidos);//Array string[] que contiene los contenidos de cada documento pero en minúsculas (SIN NORMALIZAR)
        string[] contenNorm = Metodos.ContNorm(contenidos);//Array string[] que contiene los contenidos de cada documento pero normalizado
        Dictionary<string,double>[] totalTF = TrabajoSinQuery.TotalTF(contenNorm);//Array de diccionarios que contiene el TF de cada documento (El TF de cada palabra de cada documento)
        Dictionary<string,int> frecDePalCorpus = TrabajoSinQuery.frecPalCorpusPorDoc(contenNorm);//Diccionario que contiene la frecuencia de cada palabra en el corpus
        Dictionary<string,double> idf = TrabajoSinQuery.IDF(frecDePalCorpus, contenNorm);//Diccionario que contiene el IDF de cada palabra del corpus
        Dictionary<string,double>[] TfIdf = TrabajoSinQuery.TFIDF(totalTF, idf, contenNorm);//Array de diccionarios que contiene el TF-IDF de cada documento (El TF-IDF de cada palabra de cada documento)
        //********************************************************************************************************************************************************************************************************
        //Trabajando con la query
        string NormQuery = Metodos.NormString(query);//Normalizando la query
        double[] SimCos = Metodos.Similitud(TfIdf , Metodos.QueryTFIDF(NormQuery, idf));//Array con el peso de cada documento con respecto a la consulta
        List<Tuple<double, int>> DocOrdenados = Metodos.OrdenarDocDescend(contenNorm, SimCos);//Lista de tuplas con los pesos de cada documento ordenados de forma descendente
        //********************************************************************************************************************************************************************************************************
        SearchItem[] items = new SearchItem[DocOrdenados.Count];//Creando un array SearchItem con una amplitud del tamaño de la cantidad de documentos relevantes
         if(query == string.Empty)//Si no se introduce nada en la query, retorna una advertencia
        {
            items = new SearchItem[1];
            items[0] = new SearchItem ("Aún no ha insertado una consulta","",0.9f);
        }

        
        int i = 0;//Creando un contador para que recorra el array items
        foreach(var tup in DocOrdenados)//A cada elemento de items se le añade el título adecuado, una porción del texto que esté relacionado con la query y un respectivo float que significa el score del documento
        {
            items[i] = new SearchItem(titulos[tup.Item2], Snippet.Fragmento(Min[tup.Item2], Metodos.QueryTFIDF(NormQuery, idf)), Convert.ToSingle(SimCos[tup.Item2]));
            i++;
        }

        return new SearchResult(items, query);
    }
}
