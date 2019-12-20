using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication21.Utility
{
    public class Util
    {
        public static string ReadAllBytes(string Path)
        {
            // byte[] bytes = System.IO.File.ReadAllBytes(response.Result.ToString());
            string data = string.Empty;
            using (FileStream fs = new FileStream(Path, FileMode.Open, FileAccess.Read))
            {
                byte[] buffer = new byte[fs.Length];
                fs.Read(buffer, 0, (int)fs.Length);
                data = Convert.ToBase64String(buffer);
            }
            return data;

        }

        public static string NormalizarCampo(string campo, int numeroMaximoCaracteres)
        {

            int longitudCampo = campo.Length;
            if (longitudCampo > numeroMaximoCaracteres)
            {
                throw new Exception("La longitud del campo es mayor que el numero maximo de caracteres indicados");
            }
            int diferencia = numeroMaximoCaracteres - longitudCampo;
            string caracteresAgregar = "";
            for (int i = 0; i < diferencia; i++)
            {
                caracteresAgregar += "0";
            }
            return caracteresAgregar + campo;
        }

        public static string MonthName(DateTime fecha)

        {

            fecha =DateTime.Now;

            string mes = "";

            switch (fecha.Month)

            {
                case 1:

                    mes ="Enero";

                    break;

                case 2:

                    mes ="Febrero";
                    break;

                case 3:

                    mes ="Marzo";

                    break;

                case 4:

                    mes ="Abril";

                    break;

                case 5:

                    mes ="Mayo";

                    break;

                case 6:

                    mes ="Junio";

                    break;

                case 7:

                    mes ="Julio";

                    break;


                case 8:

                    mes ="Agosto";

                    break;


                case 9:

                    mes ="Septiembre";

                    break;


                case 10:

                    mes ="Octubre";

                    break;

                case 11:

                    mes ="Noviembre";

                    break;

                case 12:

                    mes ="Diciembre";

                    break;

            };

            return mes;

        }


    }
}
