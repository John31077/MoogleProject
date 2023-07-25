echo "****MARQUE SU OPCIÓN****"
echo "run: ejecutar el proyecto (Para cerrar pulsar Ctrl+c en la terminal)"
echo "report: compilar el pdf del informe"
echo "slides: compilar el pdf de la presentación"
echo "show_report: ejecuta el pdf del informe (En caso de no estar compilado lo compila)"
echo "show_slides: ejecuta el pdf del informe (En caso de no estar compilado lo compila)"
echo "clean: elimina los archivos residuales de la compilación de los pdf y el proyecto"
read opcion

if [ $opcion = "run" ];
then
    cd ../
    dotnet watch run --project MoogleServer
#*********************************************************************************************************************
elif [ $opcion = "report" ];
then
    cd ../informe
    pdflatex informe.tex
    makeindex informe.tex
    pdflatex informe.tex
#*********************************************************************************************************************
elif [ $opcion = "slides" ];
then
    cd ../presentacion
    pdflatex presentacion.tex
    makeindex presentacion.tex
    pdflatex presentacion.tex
#*********************************************************************************************************************
elif [ $opcion = "show_report" ];
then
    echo "Desea usar un comando en específico para abrir el PDF (pulse *1* para si o *0* para no)"
    read respuesta
    if [ $respuesta = 1 ];
    then
        echo "Escriba su comando"
        read comando
    elif [ $respuesta = 0 ];
    then
        comando="start"
    fi
    cd ../informe
    if [ -f ../informe/informe.pdf ];
    then
        $comando informe.pdf
    else
        pdflatex informe.tex
        makeindex informe.tex
        pdflatex informe.tex
        $comando informe.pdf
    fi
#************************************************************************************************************************
elif [ $opcion = "show_slides" ];
then
    echo "Desea usar un comando en específico para abrir el PDF (pulse *1* para si o *0* para no)"
    read respuesta
    if [ $respuesta = 1 ];
    then
        echo "Escriba su comando"
        read comando
    elif [ $respuesta = 0 ];
    then
        comando="start"
    fi
    cd ../presentacion
    if [ -f ../presentacion/presentacion.pdf ];
    then
        $comando presentacion.pdf
    else
    pdflatex presentacion.tex
    makeindex presentacion.tex
    pdflatex presentacion.tex
    $comando presentacion.pdf
    fi
#*************************************************************************************************************************
elif [ $opcion = "clean" ];
then
    cd ../informe
    for file in $(ls);
    do
      if [[ $file != "informe.pdf" && $file != "informe.tex" ]];
      then
          rm -f "$file"
      fi
    done
    cd ../presentacion
    for file in $(ls);
    do
      if [[ $file != "presentacion.pdf" && $file != "presentacion.tex" && $file != "formula.jpg" && $file != "grafica.png" ]];
      then
          rm -f "$file"
      fi
    done
    cd ../MoogleServer
    rm -r bin obj
    cd ../MoogleEngine
    rm -r bin obj
fi
#falta   publicar.