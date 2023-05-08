namespace MoogleEngine;

public class Snippet
{
    /*Este metodo recibe como parámetro un string que debe de contener un texto y devuelve una lista List<string> con cada oracion del texto*/
    public static List<string> SepOraciones(string texto)
    {
        string[] oraciones = texto.Split('.');
        List<string> oraciones2 = new List<string>();
        foreach (string oracion in oraciones)
        {
            oraciones2.Add(oracion);
        }
        return oraciones2;
    }  

    /*Este metodo recibe como parámetros un string texto y un Dictionary<string,double> que debe de contener el TF-IDF de la query, este metodo
    devuelve un string el cual sería una oración del texto donde se encuentre la palabra con mayor TF-IDF de la query, si no se encuentra, devuelve un
    una oración del texto donde se encuentre la segunda palabra con mayor TF-IDF de la query y así sucesivamente...*/
    public static string Fragmento(string texto, Dictionary<string,double> TfIdfQuery)
    {
        List<string> oraciones = SepOraciones(texto);
        List<Tuple<double, string>> lista = new List<Tuple<double, string>>();    
        foreach(KeyValuePair<string, double> pair in TfIdfQuery)
        {
            if(TfIdfQuery[pair.Key] > 0)
            {
                lista.Add(new Tuple<double, string>(TfIdfQuery[pair.Key], pair.Key));
            }
        }
        lista.Sort((a,b) => b.Item1.CompareTo(a.Item1));
        foreach(var i in lista)
        {
            foreach(var s in oraciones)
            {
                string[] wordsSentences = Metodos.PalabrasSeparadas(Metodos.NormString(s));
                if(Array.Exists(wordsSentences, x => x == i.Item2))
                {
                    return s;
                }
            }
        }
        return " ";
    }
}