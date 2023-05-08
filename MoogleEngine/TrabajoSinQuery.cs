namespace MoogleEngine;

public class TrabajoSinQuery
{
    /*Este metodo recibe como parámetro un string que debería de ser un documento y devuelve un Dictionary<string,double> con el TF de cada 
    palabra del documento*/
    public static Dictionary<string, double> TF(string Doc)
    {
        Dictionary<string,double> tf = new Dictionary<string, double>();
        string[] palabras = Metodos.PalabrasSeparadas(Doc);
        int totalPalabras = palabras.Length;
        foreach (string palabra in palabras)
        {
            if (tf.ContainsKey(palabra))
            {
                tf[palabra]++;
            }
            else
            {
                tf.Add(palabra, 1);
            }
        }
        foreach (string palabra in tf.Keys.ToList())
        {
            tf[palabra] = tf[palabra] / totalPalabras;
        }
        return tf;
    }

    /*Este metodo recibe como parámetro un array string[] con todos los documentos (normalizados) y devuelve un array Dictionary<string,double> con
    los Diccionarios que contienen el TF de las palabras de cada documento*/
    public static Dictionary<string,double>[] TotalTF(string[] totalDeDoc)
    {
        Dictionary<string,double>[] totalTF = new Dictionary<string,double>[totalDeDoc.Length];
        for (int i = 0; i < totalDeDoc.Length; i++)
        {
            totalTF[i] = TF(totalDeDoc[i]);
        }
        return totalTF;
    }

    /*Este metodo recibe como parametro un array string[] que contiene los documentos (normalizados) y devuelve un Dictionary<string,int> con la frecuencia de   
    cada palabra en el corpus*/
    public static Dictionary<string,int> frecPalCorpusPorDoc(string[] totalDeDoc)
    {
        Dictionary<string,int> frec = new Dictionary<string, int>();
        foreach (string documento in totalDeDoc)
        {
            string[] palabras = Metodos.PalabrasSeparadas(documento);
            HashSet<string> palabrasUnicas = new HashSet<string>(palabras);
            foreach (string palabra in palabrasUnicas)
            {
                if (!frec.ContainsKey(palabra))
                {
                    frec[palabra] = 0;
                }
                frec[palabra]++;
            }
        }
        return frec;
    }

    /*Este metodo recibe como parametros un Dictionary<string,int> que debe de contener la frecuencia de cada palabra en el corpus, y también 
    recibe un array string[] con los documentos (normalizados), este metodo devuelve un Dictionary<string,double> con el IDF de todas las palabras del 
    corpus*/
    public static Dictionary<string,double> IDF(Dictionary<string,int> frecDoc, string[] totalDeDoc)
    {
        int totalDocumentos = totalDeDoc.Length;
        Dictionary<string,double> idf = new Dictionary<string, double>();
        foreach (KeyValuePair<string,int> fr in frecDoc)
        {
            string palabra = fr.Key;
            int frecuencia = fr.Value;
            double idfPalabra = Math.Log10((1 + (double)totalDocumentos) / (double)frecuencia);
            idf[palabra] = idfPalabra;
        }
        return idf;
    }

    /*Este metodo recibe como parámetros un array Dictionary<string,double>[] que contiene los diccionarios que contienen los TF de cada palabra
    en su respectivo documento, también recibe un Dictionary<string,double> con el IDF de cada palabra del corpus y un array string[] con los 
    documentos (normalizados) y devuelve un array Dictionary<string,double>[] con los TF-IDF de las palabras de cada documento*/
    public static Dictionary<string,double>[] TFIDF(Dictionary<string,double>[] totTF, Dictionary<string,double> iDf, string[] totalDeDoc)
    {
        Dictionary<string,double>[] tfidf = new Dictionary<string,double>[totalDeDoc.Length];
        for (int i = 0; i < totalDeDoc.Length; i++)
        {
            Dictionary<string,double> tempTFIDF = new Dictionary<string, double>();
            foreach (string palabra in totTF[i].Keys)
            {
                tempTFIDF.Add(palabra, totTF[i][palabra] * iDf[palabra]);
            }
            tfidf[i] = tempTFIDF;
        }
        return tfidf;
    }
}