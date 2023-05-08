namespace MoogleEngine;
using System.Text.RegularExpressions;
public class Metodos
{
    /*Este metodo no recibe ningun parametro y devuelve un array string[] con las direcciones de cada documento*/
    public static string[] Direcciones()
    {
        string direccion = @"..\Content";
        string[] direcciones = Directory.GetFiles(direccion, "*.txt");
        return direcciones;
    }

    /*Este metodo toma como parámetro un array string[] que debe de contener las direcciones de cada documento y devuelve un array string[] con
    el título de cada documento*/
    public static string[] Titulos(string[] direc)
    {
        string[] titulos = new string[direc.Length];
        for (int i = 0; i < titulos.Length; i++)
        {
            titulos[i] = Path.GetFileNameWithoutExtension(direc[i]);
        }
        return titulos;
    }
    
    /*Este metodo toma como parámetro un array string[] que debe de contener las direcciones de cada documento y devuelve un array string[] con
    el contenido de cada documento (SIN NORMALIZAR)*/
    public static string[] Contenido(string[] direc)
    {
        string[] contenido = new string[direc.Length];
        for (int i = 0; i < contenido.Length; i++)
        {
            contenido[i] = File.ReadAllText(direc[i]);
        }
        return contenido;
    }   
    
    /*Este metodo recibe como parámetro un array string[] que debe de contener el contenido de cada documento y devuelve un array string[] con el
    contenido de cada documento pero esta vez en minúsculas (SIN NORMALIZAR)*/
    public static string[] ContenidoMinusculas(string[] contenido)
    {
        string[] cont = new string[contenido.Length];
        for (int i = 0; i < contenido.Length; i++)
        {
            cont[i] = contenido[i].ToLower();
        }
        return cont;
    }

    /*Este metodo recibe como parámetro un array string[] que debe de contener el contenido de cada documento y devuelve un array string[] que 
    contiene el mismo contenido pero ya normalizado*/
    public static string[] ContNorm(string[] cont)
    {
        string[] contNor = new string[cont.Length];
        for (int i = 0; i < cont.Length; i++)
        {
            string documentoNorm = NormString(cont[i]);
            contNor[i] = documentoNorm;
        }
        return contNor;
    }

    /*Este metodo recibe como parámetro un string que debe de ser un texto y devuelve un string con el texto ya normalizado*/
    public static string NormString(string texto)
    {
        string textoNorm = Regex.Replace(texto, @"[^\w\s]+", "").ToLower();
        return textoNorm;
    }

    /*Este metodo recibe un string que debe de ser un texto y devuelve un array string[] con una palabra del texto en cada posición del array*/
    public static string[] PalabrasSeparadas(string texto)
    {
        string[] texto1 = texto.Split();
        int cont = 0;
        int cont2 = 0;
        int cont3 = 0;
        for (int i = 0; i < texto1.Length; i++)
        {
            if (string.IsNullOrEmpty(texto1[i]) == false)
            {   
                cont++;
            }
        }
        string[] palabSep = new string[cont];
        for (int i = 0; i < palabSep.Length; i++)
        {
            for (int j = cont2 + cont3; j < texto1.Length; j++)
            {
                cont3 = 1;
                if (string.IsNullOrEmpty(texto1[j]) == false)
                {   
                    palabSep[i] = texto1[j];
                    cont2 = j;
                    break;
                }
            }
        }
        return palabSep;
    }   

    /*Este metodo recibe como parámetro un string que debe de ser la query normalizada y devuelve un Dictionary<string,double> con el respectivo
    TF de cada palabra de la query*/
    public static Dictionary<string,double> QueryTF(string NorQuery)
    {
        Dictionary<string,double> queryTF = TrabajoSinQuery.TF(NorQuery);
        return queryTF;
    }

    /*Este metodo recibe como parámetros un string que debe de ser la query normalizada y un Dictionary<string,double> con el IDF de los 
    documentos y devuelve un Dictionary<string,double> el TF-IDF de cada palabra de la query*/
    public static Dictionary<string,double> QueryTFIDF(string NorQuery, Dictionary<string,double> iDf)
    {
        Dictionary<string,double> queryTFIDF = new Dictionary<string, double>();
        Dictionary<string,double> queryTF = QueryTF(NorQuery);
        foreach (string palabra in queryTF.Keys)
        {
            if (iDf.ContainsKey(palabra) == false)
            {
                queryTFIDF.Add(palabra, 1);
            }
            else
            {
                queryTFIDF.Add(palabra, (queryTF[palabra]) * iDf[palabra]);
            }
        }
        return queryTFIDF;
    }

    /*Este metodo recibe como parametro un Dictionary<string,double> que debería de contener el TF-IDF de un documento y devuelve un double con la 
    suma de los cuadrados de los elementos del diccionario*/
    public static double SumCuadTermDoc(Dictionary<string,double> tfidf)
    {
        double sumCuad = 0;
        foreach (string palabra in tfidf.Keys)
        {
            if (tfidf[palabra] != 0)
            {
                sumCuad += Math.Pow(tfidf[palabra], 2);
            }
            else
            {
                continue;
            }
        }
        return sumCuad;
    }
    
    /*Este metodo recibe como parámetros un array Dictionary<string,double>[] que debería de contener los diccionarios de cada documento, 
    estos deberán contener los TF-IDF de cada palabra de dichos documentos, también recibe un Dictionary<string,double> con el TF-IDF de la query,
    este metodo devuelve un array double[] que contiene el peso de cada documento con respecto a la query (Cálculo de Similitud del Coseno)*/ 
    public static double[] Similitud(Dictionary<string,double>[] tfidfDoc , Dictionary<string,double> tfidfQuery)
    {
        double num = 0;
        double[] sumCos = new double[tfidfDoc.Length];
        for (int i = 0; i < tfidfDoc.Length; i++)
        {
            foreach (string palabra in tfidfQuery.Keys)
            {
                if (tfidfDoc[i].ContainsKey(palabra))
                {
                    num += tfidfQuery[palabra] * tfidfDoc[i][palabra];
                }
            }
            sumCos[i] = num / (Math.Sqrt(SumCuadTermDoc(tfidfDoc[i])) * Math.Sqrt(SumCuadTermDoc(tfidfQuery)));
            num = 0;
        }
        return sumCos;
    }

    /*Este metodo recibe como parámetos un array string[] con los documentos y un array double[] con el peso de cada documento con respecto a la
    query y devuelve una lista de tuplas List<Tuple<double,int>> con los documentos organizados en orden descendente*/
    public static List<Tuple<double, int>> OrdenarDocDescend(string[] totalDeDoc, double[] cos)
    {
        List<Tuple<double, int>> orden = new List<Tuple<double, int>>();
        for(int i = 0; i < totalDeDoc.Length; i++)
        {
            if(cos[i] > 0)
            {
                orden.Add(new Tuple<double, int>(cos[i], i));
            }
        }
        orden.Sort((x,y) => y.Item1.CompareTo(x.Item1));
        return orden;
    }
}