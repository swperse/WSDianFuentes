using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacturacionElectronica.Controlador
{
    class Utilitarios
    {
        public String completarLinea(String Nit)
        {
            String respueta = Nit;

            while (respueta.Length < 10)
            {
                respueta = "0" + respueta;
            }
            return respueta;
        }

        public String EncodinHex(int NroFactura)
        {
            String respuesta = "";
            try
            {
                String hexString = NroFactura.ToString("X");
                respuesta = this.completarLinea(hexString.ToLower());
            } catch (Exception ex )
            {
                respuesta = "ERROR: "  + ex.Message ;
            }
            
            return respuesta;
        }
    }
}
